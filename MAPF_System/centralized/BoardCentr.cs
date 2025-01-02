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
using System.Collections;

namespace MAPF_System
{
    public class BoardCentr : Board<UnitCentr>, BoardInterface
    {
        private bool AreNotTunells;

        public BoardCentr(int X, int Y, int Blocks, int N_Units)
        {
            rnd = new Random();
            SampleConstructor(X, Y, new Cell[X, Y], new List<UnitCentr>(), "", new List<TunellInterface>());
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
                    units.Add(new UnitCentr(new int[X, Y], x, y, x_Purpose, y_Purpose, id, -1, -1, X, Y));
                    N_Units--;
                    id++;
                }
            }
            // Генерация препятствий
            GenerationBlocks(X, Y);
        }
        public BoardCentr(int X, int Y, Cell[,] Arr, List<UnitCentr> units, string name, List<TunellInterface> tunells)
        {
            SampleConstructor(X, Y, Arr, units, name, tunells);
        }
        public BoardCentr()
        {
            // Открытие файла в формате board
            OpenFileDialog openFileDialog1 = new OpenFileDialog() { Filter = "(*.board)|*.board", };
            openFileDialog1.ShowDialog();
            // Проверка на то, что board файл был выбран
            if (openFileDialog1.FileName == "")
                return;
            Constructor(openFileDialog1.FileName);
        }
        public BoardCentr(string path) { Constructor(path); }
        public BoardInterface CopyWithoutBlocks()
        {
            Cell[,] CopyArr = new Cell[X, Y];
            List<UnitCentr> CopyUnits = new List<UnitCentr>();
            // Скопировать юнитов
            foreach (var Unit in units)
                CopyUnits.Add(Unit.Copy(true));
            // Скопировать доску без блоков
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    CopyArr[i, j] = Arr[i, j].CopyWithoutBlock();
            return new BoardCentr(X, Y, CopyArr, CopyUnits, name, tunells);
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
            BoardCentr BoardCentr = (BoardCentr)Board;
            // Добавить блоки в пределах видимости юнитов
            foreach (var Unit in units)
            {
                MakeBlock(BoardCentr, Unit.X(), Unit.Y() - 1);
                MakeBlock(BoardCentr, Unit.X(), Unit.Y() + 1);
                MakeBlock(BoardCentr, Unit.X() - 1, Unit.Y());
                MakeBlock(BoardCentr, Unit.X() + 1, Unit.Y());
            }

            // Добавить узлы -- части туннелей
            if (!AreNotTunells)
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

                                if (!AreNotTunells && kk == 3)
                                {
                                    List<TunellCentr> LT = new List<TunellCentr>();
                                    LT_ADD(LT, i - 1, j);
                                    LT_ADD(LT, i + 1, j);
                                    LT_ADD(LT, i, j - 1);
                                    LT_ADD(LT, i, j + 1);

                                    var T = new TunellCentr(this);
                                    T.Add(LT);
                                    Arr[i, j].MakeTunell(T);
                                    tunells.Add(T);
                                    T.Add(i, j);
                                }

                                if (kk == 4 && Arr[i,j].WasVisit())
                                {
                                    AreNotTunells = true;
                                    for (int ii = 0; ii < X; ii++)
                                        for (int jj = 0; jj < Y; jj++)
                                            Arr[ii, jj].ClearTunell();
                                }
                            }

                    if (k == 0)
                        break;
                }

            foreach (var Unit in units)
            {
                Arr[Unit.X(), Unit.Y()].MakeVisit(Unit.Id());
                Unit.PlusArr();
                if (Unit.IsRealEnd())
                    Unit.ClearArr();
            }

            var new_units = new List<UnitCentr>();
            foreach (var Claster in Clasterization(units))
                new_units.AddRange(NewUnits(Claster));
            units = new_units;

        }

        private List<HashSet<UnitCentr>> Clasterization(List<UnitCentr> units)
        {
            HashSet<UnitCentr> clasterizations = new HashSet<UnitCentr>();
            List<HashSet<UnitCentr>> clasters = new List<HashSet<UnitCentr>>();
            foreach (var unit in units)
                if (!clasterizations.Contains(unit))
                {
                    HashSet<UnitCentr> claster = unit.FindClaster(units);
                    clasters.Add(claster);
                    clasterizations.UnionWith(claster);
                }
            return clasters;
        }

        private IEnumerable<UnitCentr> NewUnits(IEnumerable<UnitCentr> claster)
        {
            Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>> TT = new Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>>(new List<UnitCentr>(), claster);
            int min_sum = claster.Sum(unit => unit.RealManheton()) - claster.Count(unit => !unit.IsRealEnd());
            IEnumerable<UnitCentr> res = NewUnitsStack(TT, min_sum, claster, true);
            if (!(res is null))
                return res;
            return NewUnitsStack(TT, min_sum, claster, false);
        }

        private IEnumerable<UnitCentr> NewUnitsStack(Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>> TT, int min_sum, IEnumerable<UnitCentr> claster, bool b)
        {
            int sum = int.MaxValue;
            Stack<Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>>> stack = new Stack<Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>>>();
            stack.Push(TT);
            IEnumerable<UnitCentr> res = null;
            while (stack.Count() != 0)
            {
                var T = stack.Pop();
                if (T.Item2.Count() == 0)
                {
                    var s = T.Item1.Sum(unit => unit.Manheton(this));
                    if (s == min_sum)
                        return T.Item1;
                    if ((s < sum) && isntEqw(claster, T.Item1))
                    {
                        sum = s;
                        res = T.Item1;
                    }
                }
                else
                    foreach (var unit in T.Item2.First().MakeStep(this, T.Item1, claster, b))
                        stack.Push(new Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>>(T.Item1.Concat(new List<UnitCentr> { unit }), T.Item2.Skip(1)));
            }
            return res;
        }

        private bool isntEqw(IEnumerable<UnitCentr> units1, IEnumerable<UnitCentr> units2)
        {
            var sort1 = units1.OrderBy(unit => unit.Id()).ToList();
            var sort2 = units2.OrderBy(unit => unit.Id()).ToList();
            for (int i = 0; i < sort1.Count(); i++)
                if ((sort1[i].X() != sort2[i].X()) || (sort1[i].Y() != sort2[i].Y()))
                {
                    if (sort1[i].IsRealEnd() && !sort2[i].IsRealEnd())
                    {
                        TunellCentr T = Tunell(sort1[i].X(), sort1[i].Y());
                        if (!(T is null) && !T.RealIds().Contains(sort1[i].Id()))
                            return true;
                    }
                    else
                        return true;
                }


            return false;
        }

        public TunellCentr Tunell(int x, int y) 
        { 
            return (TunellCentr)Arr[x, y].Tunell(); 
        }

        public bool InTunell(int unit_id, TunellInterface tunell) 
        {
            UnitCentr unit = units.Find(u => u.Id() == unit_id);
            return Arr[unit.X(), unit.Y()].Tunell() == tunell; 
        }

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
                    units.Add(new UnitCentr(new int[X, Y], x, y, x_Purpose, y_Purpose, units.Count, -1, -1, X, Y));
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
                units = new List<UnitCentr>();
                for (int i = 0; i < N_Units; i++)
                {
                    units.Add(new UnitCentr(readText[t], i, X, Y));
                    t++;
                }
                tunells = new List<TunellInterface>();
            }
            catch (Exception e)
            {
                MessageBox.Show("Файл повреждён.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LT_ADD(List<TunellCentr> LT, int i, int j)
        {
            if (IsTunell(i, j))
                LT.Add((TunellCentr)Arr[i, j].Tunell());
        }
        public bool IsBlock(int x, int y)
        {
            return IsBlockBoard(x, y);
        }
    }
}
