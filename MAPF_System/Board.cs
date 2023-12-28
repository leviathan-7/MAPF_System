using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Security;
using System.Windows.Forms;

namespace MAPF_System
{
    public class Board
    {
        public List<Unit> Units;

        private Cell[,] Arr;
        private int X;
        private int Y;
        private Random rnd;
        private int KolBad;
        private string name;

        public Board(int X, int Y, int Blocks, int N_Units)
        {
            name = "";
            this.X = X;
            this.Y = Y;
            Arr = new Cell[X, Y];
            rnd = new Random();
            KolBad = 0;
            GenerationDefaults(Blocks);
            GenerationUnits(N_Units);
            GenerationBlocks();
        }
        public Board(int X, int Y, Cell[,] Arr, List<Unit> Units, int KolBad, string name)
        {
            this.name = name;
            this.X = X;
            this.Y = Y;
            this.Arr = Arr;
            this.Units = Units;
            this.KolBad = KolBad;
        }
        public Board()
        {
            // Открытие файла в формате board
            OpenFileDialog openFileDialog1 = new OpenFileDialog(){ Filter = "(*.board)|*.board", };
            openFileDialog1.ShowDialog();
            // Проверка на то, что board файл был выбран
            if (openFileDialog1.FileName == "")
                return;
            Constructor(openFileDialog1.FileName);
        }
        public Board(string path){ Constructor(path); }
        public Board CopyWithoutBlocks()
        {
            Cell[,] CopyArr = new Cell[X, Y];
            List<Unit> CopyUnits = new List<Unit>();
            // Скопировать юнитов
            foreach (var Unit in Units)
                CopyUnits.Add(Unit.Copy());
            // Скопировать доску без блоков
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    CopyArr[i, j] = Arr[i, j].CopyWithoutBlock();

            return new Board(X, Y, CopyArr, CopyUnits, KolBad, name);
        }
        public void Draw(Graphics t)
        {
            int height = 17;
            int YY = 110;
            int XX = 10;
            using (Graphics g = t)
            {
                g.Clear(SystemColors.Control); // Clear the draw area
                using (Pen pen = new Pen(Color.Blue, 1))
                {
                    for (int i = 0; i < X; i++)
                        for (int j = 0; j < Y; j++)
                        {
                            Rectangle rect = new Rectangle(new Point(XX + 5 + height * i, YY + 5 + height * j), new Size(height, height));
                            g.DrawRectangle(pen, rect);
                            // Отрисовка блоков
                            if (Arr[i, j].IsBlock())
                                g.FillRectangle(Brushes.Black, rect);
                            // Отрисовка пройденного пути
                            if (Arr[i, j].WasVisit())
                            {
                                int W = 100 + 100 * (Arr[i, j].IdVisit() + 1) / Units.Count;
                                g.FillRectangle(new SolidBrush(Color.FromArgb(W, W, 255)), rect);
                            }
                            // Отрисовка плохих узлов
                            if (Arr[i, j].IsBad())
                                g.DrawString("X", new Font("Arial", 7, FontStyle.Bold), Brushes.Red, new Point(XX + 9 + height * i, YY + 9 + height * j));
                        }

                    var Font = new Font("Arial", 9, FontStyle.Bold);
                    foreach (var Unit in Units)
                    {
                        // Отрисовка цели
                        g.FillRectangle(Brushes.LawnGreen, new Rectangle(new Point(XX + 8 + height * Unit.X_Purpose(), YY + 8 + height * Unit.Y_Purpose()), new Size(height - 5, height - 5)));
                        g.DrawString("" + Unit.Id(), Font, Brushes.Black, new Point(XX + 8 + height * Unit.X_Purpose(), YY + 8 + height * Unit.Y_Purpose()));
                    }
                    foreach (var Unit in Units)
                    {
                        // Отрисовка юнитов
                        g.FillRectangle(Brushes.Red, new Rectangle(new Point(XX + 8 + height * Unit.X(), YY + 8 + height * Unit.Y()), new Size(height - 5, height - 5)));
                        g.DrawString("" + Unit.Id(), Font, Brushes.Black, new Point(XX + 8 + height * Unit.X(), YY + 8 + height * Unit.Y()));
                    }
                }
            }
        }
        public string Save(string name_)
        {
            try
            {
                StreamWriter sw = (new FileInfo(name_ + ".board")).CreateText();
                sw.WriteLine(X + " " + Y + " " + Units.Count + " " + KolBad);
                // Записать в файл доску с блоками и пройденным путем
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                        sw.WriteLine(Arr[i, j].ToStr());
                // Записать в файл юнитов
                foreach (var item in Units)
                    sw.WriteLine(item.ToStr());
                sw.Close();
                name = name_ + ".board";
            }
            catch (Exception e) { }
            return name;
        }
        public void MakeStep(Board Board, int kol_iter_a_star)
        {
            // Обнуление значений was_step
            foreach (var Unit in Units)
                Unit.NotWasStep();
            // Добавить блоки в пределах видимости юнитов
            foreach (var Unit in Units)
            {
                if (Board.IsBlock(Unit.X(), Unit.Y() - 1))
                    Arr[Unit.X(), Unit.Y() - 1].MakeBlock();
                if (Board.IsBlock(Unit.X(), Unit.Y() + 1))
                    Arr[Unit.X(), Unit.Y() + 1].MakeBlock();
                if (Board.IsBlock(Unit.X() - 1, Unit.Y()))
                    Arr[Unit.X() - 1, Unit.Y()].MakeBlock();
                if (Board.IsBlock(Unit.X() + 1, Unit.Y()))
                    Arr[Unit.X() + 1, Unit.Y()].MakeBlock();
            }
            // Добавить плохие узлы
            while (true)
            {
                int k = 0;
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                    {
                        if (!(IsBad(i, j) || Arr[i, j].IsBlock()))
                        {
                            // Проверка на отсутсвие целей
                            bool b = true;
                            foreach (var Unit in Units)
                                b = b && !((Unit.X_Purpose() == i) && (Unit.Y_Purpose() == j)) && !((Unit.X() == i) && (Unit.Y() == j));
                            if (b)
                            {
                                int kk = 0;
                                if (!IsEmpthy(i - 1, j) || IsBad(i - 1, j))
                                    kk++;
                                if (!IsEmpthy(i + 1, j) || IsBad(i + 1, j))
                                    kk++;
                                if (!IsEmpthy(i, j - 1) || IsBad(i, j - 1))
                                    kk++;
                                if (!IsEmpthy(i, j + 1) || IsBad(i, j + 1))
                                    kk++;
                                if (kk == 3)
                                {
                                    Arr[i, j].MakeBad();
                                    k++;
                                }
                            }
                        }
                    }

                if (k == 0)
                    break;
                KolBad += k;
            }
            // Добавить узлы -- части туннелей
            while (true)
            {
                int k = 0;
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                    {
                        if (!(IsTunell(i, j) || Arr[i, j].IsTunell()))
                        {
                            int kk = 0;
                            if (!IsEmpthy(i - 1, j) || IsTunell(i - 1, j))
                                kk++;
                            if (!IsEmpthy(i + 1, j) || IsTunell(i + 1, j))
                                kk++;
                            if (!IsEmpthy(i, j - 1) || IsTunell(i, j - 1))
                                kk++;
                            if (!IsEmpthy(i, j + 1) || IsTunell(i, j + 1))
                                kk++;
                            if (kk == 3)
                            {
                                Arr[i, j].MakeTunell();
                                k++;
                            }
                        }
                    }

                if (k == 0)
                    break;
            }
            // Сделать шаг теми юнитами, которые еще не достигли своей цели, при этом давая приоритет тем юнитам, которые дальше от цели
            List<Unit> Was_near_end_units = new List<Unit>();
            List<Unit> NOT_Was_near_end_units = new List<Unit>();
            foreach (var Unit in Units)
                if (Unit.Was_near_end())
                    Was_near_end_units.Add(Unit);
                else
                    NOT_Was_near_end_units.Add(Unit);

            foreach (var Unit in NOT_Was_near_end_units.OrderBy(u => -u.F()))
                if (!Unit.IsEnd())
                    Unit.MakeStep(this, from u in Units where u != Unit select u, kol_iter_a_star);
            foreach (var Unit in Was_near_end_units.OrderBy(u => -u.F()))
                if (!Unit.IsEnd())
                    Unit.MakeStep(this, from u in Units where u != Unit select u, kol_iter_a_star);
        }
        public bool IsEnd()
        {
            bool b = true;
            // Проверяем, что все юниты дошли до своих целей
            foreach (var Unit in Units)
                b = b && Unit.IsRealEnd();
            return b;
        }
        public bool IsEmpthy(int x, int y)
        {
            // Проверка на выход за пределы поля
            if ((x < 0) || (y < 0) || (x >= X) || (y >= Y))
                return false;
            return !Arr[x, y].IsBlock();
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
        public string Name() { return name; }
        
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
                // Количество плохих узлов
                KolBad = int.Parse(arr[3]);
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
                Units = new List<Unit>();
                for (int i = 0; i < N_Units; i++)
                {
                    Units.Add(new Unit(readText[t], i, X, Y));
                    t++;
                }
            }
            catch (Exception e) 
            {
                MessageBox.Show("Файл повреждён.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void GenerationDefaults(int Blocks)
        {
            int N = X * Y - Blocks - 1;

            int x = rnd.Next(X);
            int y = rnd.Next(Y);

            int x_sum = x;
            int y_sum = y;
            int kol = 1;

            Arr[x, y] = new Cell(false);
            while (N > 0)
            {
                int x1 = rnd.Next(X);
                int y1 = rnd.Next(Y);
                int x2 = rnd.Next(X);
                int y2 = rnd.Next(Y);
                int x3 = rnd.Next(X);
                int y3 = rnd.Next(Y);
                if (Math.Abs(x1 - x_sum / kol) > Math.Abs(x2 - x_sum / kol))
                    x = x1;
                else
                    x = x2;
                if (Math.Abs(y1 - y_sum / kol) > Math.Abs(y2 - y_sum / kol))
                    y = y1;
                else
                    y = y2;

                if (Math.Abs(x - x_sum / kol) < Math.Abs(x3 - x_sum / kol))
                    x = x3;
                if (Math.Abs(y - y_sum / kol) < Math.Abs(y3 - y_sum / kol))
                    y = y3;

                x_sum += x;
                y_sum += y;
                kol++;

                bool a = (x == 0) || (Arr[x - 1, y] is null);
                bool b = (x == X - 1) || (Arr[x + 1, y] is null);
                bool c = (y == 0) || (Arr[x, y - 1] is null);
                bool d = (y == Y - 1) || (Arr[x, y + 1] is null);
                if (Arr[x, y] is null && !(a && b && c && d))
                {
                    Arr[x, y] = new Cell(false);
                    N--;
                }
            }
        }
        private void GenerationUnits(int N_Units)
        {
            Units = new List<Unit>();
            int id = 0;
            while (N_Units > 0)
            {
                int x = rnd.Next(X);
                int y = rnd.Next(Y);

                int x_Purpose = rnd.Next(X);
                int y_Purpose = rnd.Next(Y);

                bool b = (Arr[x, y] != null) && (Arr[x_Purpose, y_Purpose] != null) && !((x == x_Purpose) && (y == y_Purpose));

                foreach (var Unit in Units)
                    b = b && !((Unit.X() == x) && (Unit.Y() == y)) && !((Unit.X_Purpose() == x_Purpose) && (Unit.Y_Purpose() == y_Purpose))
                        && !((Unit.X() == x_Purpose) && (Unit.Y() == y_Purpose)) && !((Unit.X_Purpose() == x) && (Unit.Y_Purpose() == y));

                if (b)
                {
                    Units.Add(new Unit(x, y, x_Purpose, y_Purpose, id, -1, -1, X, Y));
                    N_Units--;
                    id++;
                }
            }
        }
        private void GenerationBlocks()
        {
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    if (Arr[i, j] is null)
                        Arr[i, j] = new Cell(true);
        }
        private bool IsBad(int x, int y)
        {
            // Проверка на выход за пределы поля
            if ((x < 0) || (y < 0) || (x >= X) || (y >= Y))
                return false;
            return Arr[x, y].IsBad();
        }
        private bool IsTunell(int x, int y)
        {
            // Проверка на выход за пределы поля
            if ((x < 0) || (y < 0) || (x >= X) || (y >= Y))
                return false;
            return Arr[x, y].IsTunell();
        }
        private bool IsBlock(int x, int y)
        {
            // Проверка на выход за пределы поля
            if ((x < 0) || (y < 0) || (x >= X) || (y >= Y))
                return false;
            return Arr[x, y].IsBlock();
        }
    
    }
}
