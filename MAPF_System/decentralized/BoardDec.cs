using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Security;
using System.Windows.Forms;
using System.Media;

namespace MAPF_System
{
    public class BoardDec : Board<UnitDec>, BoardInterface
    {
        public BoardDec(int X, int Y, int Blocks, int N_Units)
        {
            rnd = new Random();
            SampleConstructor(X, Y, new Cell[X, Y], new List<UnitDec>(), "", new List<TunellInterface>());
            int x = rnd.Next(X);
            int y = rnd.Next(Y);
            // Генерация пустых узлов
            GenerationEmthy(X, Y, Blocks, rnd, x, y);
            // Генерация юнитов
            int id = 0;
            while (N_Units > 0)
            {
                x = rnd.Next(X);
                y = rnd.Next(Y);
                int x_Purpose = rnd.Next(X);
                int y_Purpose = rnd.Next(Y);
                bool b = (Arr[x, y] != null) && (Arr[x_Purpose, y_Purpose] != null) && !((x == x_Purpose) && (y == y_Purpose));
                foreach (var Unit in units)
                    b = b && !((Unit.X() == x) && (Unit.Y() == y)) && !((Unit.X_Purpose() == x_Purpose) && (Unit.Y_Purpose() == y_Purpose))
                        && !((Unit.X() == x_Purpose) && (Unit.Y() == y_Purpose)) && !((Unit.X_Purpose() == x) && (Unit.Y_Purpose() == y));
                if (b)
                {
                    units.Add(new UnitDec(x, y, x_Purpose, y_Purpose, id, -1, -1, X, Y));
                    N_Units--;
                    id++;
                }
            }
            // Генерация препятствий
            GenerationBlocks(X, Y);
        }
        public BoardDec(int X, int Y, Cell[,] Arr, List<UnitDec> units, string name, List<TunellInterface> tunells)
        {
            SampleConstructor(X, Y, Arr, units, name, tunells);
        }
        public BoardDec()
        {
            // Открытие файла в формате board
            OpenFileDialog openFileDialog1 = new OpenFileDialog() { Filter = "(*.board)|*.board", };
            openFileDialog1.ShowDialog();
            // Проверка на то, что board файл был выбран
            if (openFileDialog1.FileName == "")
                return;
            Constructor(openFileDialog1.FileName);
        }
        public BoardDec(string path) { Constructor(path); }
        public BoardInterface CopyWithoutBlocks()
        {
            Cell[,] CopyArr = new Cell[X, Y];
            List<UnitDec> CopyUnits = new List<UnitDec>();
            // Скопировать юнитов
            foreach (var Unit in units)
                CopyUnits.Add(Unit.Copy());
            // Скопировать доску без блоков
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    CopyArr[i, j] = Arr[i, j].CopyWithoutBlock();
            return new BoardDec(X, Y, CopyArr, CopyUnits, name, tunells);
        }
        public void Draw(Graphics t, bool b = true, Tuple<int, int> C = null, Tuple<int, int> C1 = null, bool viewtunnel = true)
        {
            int height = 18;
            if (Math.Max(X, Y) < 30)
                height = 24;
            int YY = 115;
            int XX = 95;
            using (Graphics g = t)
            {
                if (b)
                    g.Clear(SystemColors.Control); // Очистка области рисования
                using (Pen pen = new Pen(Color.Blue, 1))
                {
                    var Size = new Size(height, height);
                    var Font = new Font("Arial", 9, FontStyle.Bold);
                    var Font1 = new Font("Arial", 7, FontStyle.Bold);
                    for (int i = 0; i < X; i++)
                    {
                        if (b)
                            g.DrawString("" + i, Font1, Brushes.Coral, new Point(XX + 9 + height * i, YY - 7));
                        for (int j = 0; j < Y; j++)
                        {
                            Rectangle rect = new Rectangle(new Point(XX + 5 + height * i, YY + 5 + height * j), Size);
                            if (b)
                            {
                                g.DrawString("" + j, Font1, Brushes.Coral, new Point(XX - 7, YY + 9 + height * j));
                                g.DrawRectangle(pen, rect);
                            }
                            // Отрисовка блоков
                            if (Arr[i, j].IsBlock())
                                g.FillRectangle(Brushes.Black, rect);
                            // Отрисовка пройденного пути
                            if (Arr[i, j].WasVisit())
                            {
                                int W = 100 + 100 * (Arr[i, j].IdVisit() + 1) / units.Count;
                                g.FillRectangle(new SolidBrush(Color.FromArgb(W, W, 255)), rect);
                            }
                            // Отрисовка плохих узлов
                            if (Arr[i, j].IsBad())
                                g.DrawString("X", Font1, Brushes.Red, new Point(XX + 9 + height * i, YY + 9 + height * j));
                            // Отрисовка туннеля
                            if (viewtunnel && Arr[i, j].IsTunell())
                                g.DrawRectangle(new Pen(Color.Red, 1), new Rectangle(new Point(XX + 6 + height * i, YY + 6 + height * j), new Size(height - 2, height - 2)));
                        }
                    }
                    Size = new Size(height - 5, height - 5);
                    foreach (var Unit in units)
                    {
                        // Отрисовка цели
                        if (!Unit.IsRealEnd())
                        {
                            g.FillRectangle(Brushes.LawnGreen, new Rectangle(new Point(XX + 8 + height * Unit.X_Purpose(), YY + 8 + height * Unit.Y_Purpose()), Size));
                            g.DrawString("" + Unit.Id(), Font, Brushes.Black, new Point(XX + 8 + height * Unit.X_Purpose(), YY + 8 + height * Unit.Y_Purpose()));
                        }
                    }
                    foreach (var Unit in units)
                    {
                        // Отрисовка юнитов
                        g.FillRectangle(Brushes.Red, new Rectangle(new Point(XX + 8 + height * Unit.X(), YY + 8 + height * Unit.Y()), Size));
                        g.DrawString("" + Unit.Id(), Font, Brushes.Black, new Point(XX + 8 + height * Unit.X(), YY + 8 + height * Unit.Y()));
                    }
                    if (!(C is null))
                    {
                        Size = new Size(height, height);
                        g.FillRectangle(Brushes.White, new Rectangle(new Point(XX + 5 + height * C.Item1, YY + 5 + height * C.Item2), Size));
                        g.DrawRectangle(pen, new Rectangle(new Point(XX + 5 + height * C.Item1, YY + 5 + height * C.Item2), Size));
                        if (!(C1 is null))
                        {
                            g.FillRectangle(Brushes.White, new Rectangle(new Point(XX + 5 + height * C1.Item1, YY + 5 + height * C1.Item2), Size));
                            g.DrawRectangle(pen, new Rectangle(new Point(XX + 5 + height * C1.Item1, YY + 5 + height * C1.Item2), Size));
                        }
                    }
                }
            }
        }
        public void MakeStep(BoardInterface Board, int kol_iter_a_star)
        {
            BoardDec BoardDec = (BoardDec)Board;
            // Обнуление значений was_step
            foreach (var Unit in units)
                Unit.NotWasStep();
            // Добавить блоки в пределах видимости юнитов
            foreach (var Unit in units)
            {
                MakeBlock(BoardDec, Unit.X(), Unit.Y() - 1);
                MakeBlock(BoardDec, Unit.X(), Unit.Y() + 1);
                MakeBlock(BoardDec, Unit.X() - 1, Unit.Y());
                MakeBlock(BoardDec, Unit.X() + 1, Unit.Y());
            }
            // Добавить плохие узлы
            while (true)
            {
                int k = 0;
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                        if (!(IsBad(i, j) || Arr[i, j].IsBlock()))
                        {
                            // Проверка на отсутсвие целей
                            bool b = true;
                            foreach (var Unit in units)
                                b = b && !((Unit.X_Purpose() == i) && (Unit.Y_Purpose() == j)) && !((Unit.X() == i) && (Unit.Y() == j));
                            if (b)
                            {
                                int kk = 0;
                                kk += BadAndNoEmpthy(i - 1, j);
                                kk += BadAndNoEmpthy(i + 1, j);
                                kk += BadAndNoEmpthy(i, j - 1);
                                kk += BadAndNoEmpthy(i, j + 1);

                                if (kk == 3)
                                {
                                    Arr[i, j].MakeBad();
                                    k++;
                                }
                            }
                        }

                if (k == 0)
                    break;
            }
            // Добавить узлы -- части туннелей
            while (true)
            {
                int k = 0;
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                        if (!(IsTunell(i, j) || Arr[i, j].IsBlock()))
                        {
                            int kk = 0;
                            kk += TunellAndNoEmpthy(i - 1, j);
                            kk += TunellAndNoEmpthy(i + 1, j);
                            kk += TunellAndNoEmpthy(i, j - 1);
                            kk += TunellAndNoEmpthy(i, j + 1);

                            if (kk == 3)
                            {
                                List<TunellDec> LT = new List<TunellDec>();
                                LT_ADD(LT, i - 1, j);
                                LT_ADD(LT, i + 1, j);
                                LT_ADD(LT, i, j - 1);
                                LT_ADD(LT, i, j + 1);

                                var T = new TunellDec(this);
                                T.Add(LT);
                                Arr[i, j].MakeTunell(T);
                                tunells.Add(T);
                                T.Add(i, j);
                            }
                        }

                if (k == 0)
                    break;
            }
            // Поставить флаги юнитам, которые в простом туннеле перегораживают проезд
            foreach (var t in tunells)
                ((TunellDec)t).MakeFlags(this);
            // Сделать шаг теми юнитами, которые еще не достигли своей цели, при этом давая приоритет тем юнитам, которые дальше от цели
            List<UnitDec> Was_bool_step_units = new List<UnitDec>();
            List<UnitDec> Was_near_end_units = new List<UnitDec>();
            List<UnitDec> NOT_Was_near_end_units = new List<UnitDec>();
            List<UnitDec> Tunell_NOT_Was_near_end_units = new List<UnitDec>();

            foreach (var Unit in units)
                if (Unit.Was_near_end())
                {
                    if (Unit.Was_bool_step())
                        Was_bool_step_units.Add(Unit);
                    else
                        Was_near_end_units.Add(Unit);
                }
                else
                {
                    if (IsTunell(Unit.X(), Unit.Y()))
                        Tunell_NOT_Was_near_end_units.Add(Unit);
                    else
                        NOT_Was_near_end_units.Add(Unit);
                }


            foreach (var Unit in NOT_Was_near_end_units.OrderBy(u => -u.F()))
                if (!Unit.IsEnd())
                    Unit.MakeStep(this, from u in units where u != Unit select u, kol_iter_a_star);
            foreach (var Unit in Was_near_end_units.OrderBy(u => -u.F()))
                if (!Unit.IsEnd())
                    Unit.MakeStep(this, from u in units where u != Unit select u, kol_iter_a_star);
            foreach (var Unit in Tunell_NOT_Was_near_end_units.OrderBy(u => -u.F()))
                if (!Unit.IsEnd())
                    Unit.MakeStep(this, from u in units where u != Unit select u, kol_iter_a_star);
            foreach (var Unit in Was_bool_step_units.OrderBy(u => -u.F()))
                if (!Unit.IsEnd())
                    Unit.MakeStep(this, from u in units where u != Unit select u, kol_iter_a_star);

        }
        
        public bool IsEmpthyAndNoTunel(int x, int y)
        {
            // Проверка на выход за пределы поля
            if ((x < 0) || (y < 0) || (x >= X) || (y >= Y))
                return false;
            if (Arr[x, y].IsTunell())
                return false;
            return !Arr[x, y].IsBlock();
        }
        public bool IsBadCell(int x, int y) { return Arr[x, y].IsBad(); }
        public void MakeVisit(int x, int y, int id) { Arr[x, y].MakeVisit(id); }
        public int TunellId(int x, int y) { return ((TunellDec)Arr[x, y].Tunell()).Id(); }
        public bool InTunell(Unit unit, TunellInterface tunell) { return Arr[unit.X(), unit.Y()].Tunell() == tunell; }
        public void PlusUnit()
        {
            int Blocks = 0;
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    if (Arr[i, j].IsBlock())
                        Blocks++;
            if ((Blocks + 2 * units.Count + 2) >= (X * Y))
            {
                SystemSounds.Beep.Play();
                return;
            }
            rnd = new Random();
            while (true)
            {
                int x = rnd.Next(X);
                int y = rnd.Next(Y);
                int x_Purpose = rnd.Next(X);
                int y_Purpose = rnd.Next(Y);
                bool b = !Arr[x, y].IsBlock() && !Arr[x_Purpose, y_Purpose].IsBlock() && (Arr[x, y] != null) && (Arr[x_Purpose, y_Purpose] != null) && !((x == x_Purpose) && (y == y_Purpose));
                foreach (var Unit in units)
                    b = b && !((Unit.X() == x) && (Unit.Y() == y)) && !((Unit.X_Purpose() == x_Purpose) && (Unit.Y_Purpose() == y_Purpose))
                        && !((Unit.X() == x_Purpose) && (Unit.Y() == y_Purpose)) && !((Unit.X_Purpose() == x) && (Unit.Y_Purpose() == y));
                if (b)
                {
                    units.Add(new UnitDec(x, y, x_Purpose, y_Purpose, units.Count, -1, -1, X, Y));
                    return;
                }
            }
        }
        
        private void Constructor(string path)
        {
            try
            {
                name = path.Split('\\').Last();
                string[] readText = File.ReadAllLines(path);
                string[] arr = readText[0].Split(' ');
                // Размеры поля
                X = int.Parse(arr[0]);
                Y = int.Parse(arr[1]);
                // Количество юнитов
                int N_Units = int.Parse(arr[2]);
                // Была ли игра
                WasGame = (arr[3] == "True");
                // Создать доску по данным файла
                Arr = new Cell[X, Y];
                int t = 1;
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                    {
                        Arr[i, j] = new Cell(readText[t]);
                        t++;
                    }
                // Создать юнитов по данным файла
                units = new List<UnitDec>();
                for (int i = 0; i < N_Units; i++)
                {
                    units.Add(new UnitDec(readText[t], i, X, Y));
                    t++;
                }
                tunells = new List<TunellInterface>();
            }
            catch (Exception e)
            {
                MessageBox.Show("Файл повреждён.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsBad(int x, int y)
        {
            // Проверка на выход за пределы поля
            if ((x < 0) || (y < 0) || (x >= X) || (y >= Y))
                return false;
            return Arr[x, y].IsBad();
        }
        private void LT_ADD(List<TunellDec> LT, int i, int j)
        {
            if (IsTunell(i, j) && !IsBad(i, j))
                LT.Add((TunellDec)Arr[i, j].Tunell());
        }
        private int BadAndNoEmpthy(int i, int j)
        {
            if (!IsEmpthy(i, j) || IsBad(i, j))
                return 1;
            return 0;
        }
        public bool IsBlock(int x, int y)
        {
            return IsBlockBoard(x, y);
        }
    }
}
