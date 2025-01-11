using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MAPF_System
{
    public class Board<T>
    {
        protected Cell<T>[,] Arr;
        protected List<Tunell<T>> tunells;
        protected bool AreNotTunells;
        private Random rnd;
        public List<Unit> units { get; protected set; }
        public string name { get; protected set; }
        public bool WasGame { get; protected set; }
        public int X { get; protected set; }
        public int Y { get; protected set; }
        public bool isEnd
        {
            get
            {
                bool b = true;
                // Проверяем, что все юниты дошли до своих целей
                foreach (var Unit in units)
                    b = b && Unit.isRealEnd;
                return b;
            }
        }
        protected int countBlocks
        {
            get
            {
                int Blocks = 0;
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                        if (Arr[i, j].isBlock)
                            Blocks++;
                return Blocks;
            }
        }

        public Board(int X, int Y, int Blocks, int N_Units) 
            : this(X, Y, new Cell<T>[X, Y], new List<Unit>(), "", new List<Tunell<T>>())
        {
            rnd = new Random();
            int x = rnd.Next(X);
            int y = rnd.Next(Y);
            // Генерация пустых узлов
            int N = X * Y - Blocks - 1;
            int x_sum = x;
            int y_sum = y;
            int kol = 1;
            Arr[x, y] = new Cell<T>(false);
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
                    Arr[x, y] = new Cell<T>(false);
                    N--;
                }
            }
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
                    b = b && !((Unit.x == x) && (Unit.y == y)) && !((Unit.x_Purpose == x_Purpose) && (Unit.y_Purpose == y_Purpose))
                        && !((Unit.x == x_Purpose) && (Unit.y == y_Purpose)) && !((Unit.x_Purpose == x) && (Unit.y_Purpose == y));
                if (b)
                {
                    if (this is BoardCentr)
                        units.Add(new UnitCentr(new int[X, Y], x, y, x_Purpose, y_Purpose, id, -1, -1, X, Y));
                    if (this is BoardDec)
                        units.Add(new UnitDec(x, y, x_Purpose, y_Purpose, id, -1, -1, X, Y));
                    N_Units--;
                    id++;
                }
            }
            // Генерация препятствий
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    if (Arr[i, j] is null)
                        Arr[i, j] = new Cell<T>(true);
        }
        public Board(string path = null)
        {
            if (path is null)
            {
                // Открытие файла в формате board
                OpenFileDialog openFileDialog1 = new OpenFileDialog() { Filter = "(*.board)|*.board", };
                openFileDialog1.ShowDialog();
                // Проверка на то, что board файл был выбран
                if (openFileDialog1.FileName == "")
                    return;
                path = openFileDialog1.FileName;
            }

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
                Arr = new Cell<T>[X, Y];
                int t = 1;
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                    {
                        Arr[i, j] = new Cell<T>(readText[t]);
                        t++;
                    }
                // Создать юнитов по данным файла
                units = new List<Unit>();
                for (int i = 0; i < N_Units; i++)
                {
                    if (this is BoardCentr)
                        units.Add(new UnitCentr(readText[t], i, X, Y));
                    if (this is BoardDec)
                        units.Add(new UnitDec(readText[t], i, X, Y));
                    t++;
                }
                tunells = new List<Tunell<T>>();
            }
            catch (Exception)
            {
                MessageBox.Show("Файл повреждён.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Board(int X, int Y, Cell<T>[,] Arr, List<Unit> units, string name, List<Tunell<T>> tunells)
        {
            this.name = name;
            this.X = X;
            this.Y = Y;
            this.Arr = Arr;
            this.units = units;
            this.tunells = tunells;
        }

        public void DelBlokcs()
        {
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    Arr[i, j].isBlock = false;
        }
        public bool IsTunell(int x, int y)
        {
            // Проверка на выход за пределы поля
            if ((x < 0) || (y < 0) || (x >= X) || (y >= Y))
                return false;
            return Arr[x, y].isTunell;
        }
        public bool IsEmpthy(int x, int y)
        {
            // Проверка на выход за пределы поля
            if ((x < 0) || (y < 0) || (x >= X) || (y >= Y))
                return false;
            return !Arr[x, y].isBlock;
        }
        public bool Move(Tuple<int, int> C0, Tuple<int, int> C1)
        {
            if (!IsEmpthy(C1.Item1, C1.Item2))
                return false;
            foreach (var unit in units)
                if (((unit.x == C1.Item1) && (unit.y == C1.Item2)) || ((unit.x_Purpose == C1.Item1) && (unit.y_Purpose == C1.Item2)))
                    return false;
            foreach (var unit in units)
            {
                if ((unit.x == C0.Item1) && (unit.y == C0.Item2))
                    return unit.Move(C1, true);
                if ((unit.x_Purpose == C0.Item1) && (unit.y_Purpose == C0.Item2))
                    return unit.Move(C1, false);
            }
            return false;
        }
        public int ReversBlock(Tuple<int, int> c)
        {
            if ((c.Item1 < 0) || (c.Item2 < 0) || (c.Item1 >= X) || (c.Item2 >= Y) 
                || units.Any(unit => (unit.x == c.Item1 && unit.y == c.Item2) || (unit.x_Purpose == c.Item1 && unit.y_Purpose == c.Item2)))
                return 0;

            return Arr[c.Item1, c.Item2].ReversBlock();
        }
        public Tuple<Tuple<int, int>, Tuple<int, int>> MinusUnit()
        {
            if (units.Count() <= 1)
            {
                SystemSounds.Beep.Play();
                return null;
            }
            var L = units.Last();
            units.Remove(L);
            return new Tuple<Tuple<int, int>, Tuple<int, int>>(new Tuple<int, int>(L.x, L.y), new Tuple<int, int>(L.x_Purpose, L.y_Purpose));
        }
        public bool DelUnits()
        {
            if (units.Count() <= 1)
            {
                SystemSounds.Beep.Play();
                return false;
            }
            units = new List<Unit> { units[0] };
            return true;
        }
        public void PlusColumn()
        {
            X++;
            var arr = new Cell<T>[X, Y];
            for (int i = 0; i < X - 1; i++)
                for (int j = 0; j < Y; j++)
                    arr[i, j] = Arr[i, j];
            for (int j = 0; j < Y; j++)
                arr[X - 1, j] = new Cell<T>(false);
            Arr = arr;
            foreach (var unit in units)
                unit.NewArr(X, Y);
        }
        public void PlusRow()
        {
            Y++;
            var arr = new Cell<T>[X, Y];
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y - 1; j++)
                    arr[i, j] = Arr[i, j];
            for (int i = 0; i < X; i++)
                arr[i, Y - 1] = new Cell<T>(false);
            Arr = arr;
            foreach (var unit in units)
                unit.NewArr(X, Y);
        }
        public void PlusUnit()
        {
            if ((countBlocks + 2 * units.Count + 2) >= (X * Y))
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
                bool b = !Arr[x, y].isBlock && !Arr[x_Purpose, y_Purpose].isBlock && (Arr[x, y] != null) && (Arr[x_Purpose, y_Purpose] != null) && !((x == x_Purpose) && (y == y_Purpose));
                foreach (var Unit in units)
                    b = b && !((Unit.x == x) && (Unit.y == y)) && !((Unit.x_Purpose == x_Purpose) && (Unit.y_Purpose == y_Purpose))
                        && !((Unit.x == x_Purpose) && (Unit.y == y_Purpose)) && !((Unit.x_Purpose == x) && (Unit.y_Purpose == y));
                if (b)
                {
                    if (this is BoardCentr)
                        units.Add(new UnitCentr(new int[X, Y], x, y, x_Purpose, y_Purpose, units.Count, -1, -1, X, Y));
                    if (this is BoardDec)
                        units.Add(new UnitDec(x, y, x_Purpose, y_Purpose, units.Count, -1, -1, X, Y));
                    return;
                }
            }
        }
        public Board<T> CopyWithoutBlocks()
        {
            Cell<T>[,] CopyArr = new Cell<T>[X, Y];
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    CopyArr[i, j] = Arr[i, j].copyWithoutBlock;

            List<Unit> copy = units.Select(unit => unit.copy).ToList();
            if (this is BoardCentr)
                return new BoardCentr(X, Y, CopyArr as Cell<int>[,], copy, name, tunells as List<Tunell<int>>) as Board<T>;
            return new BoardDec(X, Y, CopyArr as Cell<Unit>[,], copy, name, tunells as List<Tunell<Unit>>) as Board<T>;
        }
        public void MakeStep(Board<T> Board, int kol_iter_a_star)
        {
            // Добавить блоки в пределах видимости юнитов
            int[] xx = { -1, 1, 0, 0 }, yy = { 0, 0, -1, 1 };
            foreach (var Unit in units)
                for (int w = 0; w < 4; w++)
                {
                    int newI = Unit.x + xx[w], newJ = Unit.y + yy[w];
                    // Проверка на выход за пределы поля
                    if ((newI < 0) || (newJ < 0) || (newI >= X) || (newJ >= Y))
                        continue;
                    if (Board.Arr[newI, newJ].isBlock)
                        Arr[newI, newJ].isBlock = true;
                }

            if (this is BoardCentr)
                (this as BoardCentr).MakeStep(Board as BoardCentr);
            if (this is BoardDec)
                (this as BoardDec).MakeStep(Board as BoardDec, kol_iter_a_star);
        }
        public string Save(string name_, bool b = false)
        {
            try
            {
                StreamWriter sw = (new FileInfo(name_ + ".board")).CreateText();
                sw.WriteLine(X + " " + Y + " " + units.Count + " " + b);
                // Записать в файл доску с блоками и пройденным путем
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                        sw.WriteLine(Arr[i, j].str);
                // Записать в файл юнитов
                foreach (var item in units)
                    sw.WriteLine(item.str);
                sw.Close();
                name = name_ + ".board";
            }
            catch (Exception) { }
            return name;
        }
        public bool InTunell(Unit unit, Tunell<T> tunell) { return Arr[unit.x, unit.y].tunell == tunell; }
        public Tunell<T> Tunell(int x, int y) { return Arr[x, y].tunell; }
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
                            if (Arr[i, j].isBlock)
                                g.FillRectangle(Brushes.Black, rect);
                            // Отрисовка пройденного пути
                            if (Arr[i, j].wasvisited)
                            {
                                int W = 100 + 100 * (Arr[i, j].idVisit + 1) / units.Count;
                                g.FillRectangle(new SolidBrush(Color.FromArgb(W, W, 255)), rect);
                            }
                            // Отрисовка плохих узлов
                            if (Arr[i, j].isBad)
                                g.DrawString("X", Font1, Brushes.Red, new Point(XX + 9 + height * i, YY + 9 + height * j));
                            // Отрисовка туннеля
                            if (viewtunnel && Arr[i, j].isTunell)
                                g.DrawRectangle(new Pen(Color.Red, 1), new Rectangle(new Point(XX + 6 + height * i, YY + 6 + height * j), new Size(height - 2, height - 2)));
                        }
                    }
                    Size = new Size(height - 5, height - 5);
                    foreach (var Unit in units)
                    {
                        // Отрисовка цели
                        if (!Unit.isRealEnd)
                        {
                            g.FillRectangle(Brushes.LawnGreen, new Rectangle(new Point(XX + 8 + height * Unit.x_Purpose, YY + 8 + height * Unit.y_Purpose), Size));
                            g.DrawString("" + Unit.id, Font, Brushes.Black, new Point(XX + 8 + height * Unit.x_Purpose, YY + 8 + height * Unit.y_Purpose));
                        }
                    }
                    foreach (var Unit in units)
                    {
                        // Отрисовка юнитов
                        g.FillRectangle(Brushes.Red, new Rectangle(new Point(XX + 8 + height * Unit.x, YY + 8 + height * Unit.y), Size));
                        g.DrawString("" + Unit.id, Font, Brushes.Black, new Point(XX + 8 + height * Unit.x, YY + 8 + height * Unit.y));
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

    }
}
