using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace MAPF_System
{
    public class Board<T> where T : Unit
    {
        protected Cell[,] Arr;
        protected int X;
        protected int Y;
        protected Random rnd;
        protected string name;
        protected bool WasGame;
        protected List<TunellInterface> tunells;
        protected List<T> units;

        public string Name() { return name; }
        public int GET_X() { return X; }
        public int GET_Y() { return Y; }
        public bool GetWasGame() { return WasGame; }
        public List<T> Units() { return units; }

        public void DelBlokcs()
        {
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    Arr[i, j].DelBlokcs();
        }
        protected void MakeBlock(BoardInterface Board, int x, int y)
        {
            if (Board.IsBlock(x, y))
                Arr[x, y].MakeBlock();
        }
        public bool IsTunell(int x, int y)
        {
            // Проверка на выход за пределы поля
            if ((x < 0) || (y < 0) || (x >= X) || (y >= Y))
                return false;
            return Arr[x, y].IsTunell();
        }
        public bool IsEmpthy(int x, int y)
        {
            // Проверка на выход за пределы поля
            if ((x < 0) || (y < 0) || (x >= X) || (y >= Y))
                return false;
            return !Arr[x, y].IsBlock();
        }
        protected int TunellAndNoEmpthy(int i, int j)
        {
            if (!IsEmpthy(i, j) || IsTunell(i, j))
                return 1;
            return 0;
        }
        protected bool IsBlockBoard(int x, int y)
        {
            // Проверка на выход за пределы поля
            if ((x < 0) || (y < 0) || (x >= X) || (y >= Y))
                return false;
            return Arr[x, y].IsBlock();
        }
        protected void SampleConstructor(int X, int Y, Cell[,] Arr, List<T> units, string name, List<TunellInterface> tunells)
        {
            this.name = name;
            this.X = X;
            this.Y = Y;
            this.Arr = Arr;
            this.units = units;
            this.tunells = tunells;
        }
        public bool Move(Tuple<int, int> C0, Tuple<int, int> C1)
        {
            if (!IsEmpthy(C1.Item1, C1.Item2))
                return false;
            foreach (var unit in units)
                if (((unit.X() == C1.Item1) && (unit.Y() == C1.Item2)) || ((unit.X_Purpose() == C1.Item1) && (unit.Y_Purpose() == C1.Item2)))
                    return false;
            foreach (var unit in units)
            {
                if ((unit.X() == C0.Item1) && (unit.Y() == C0.Item2))
                    return unit.Move(C1, true);
                if ((unit.X_Purpose() == C0.Item1) && (unit.Y_Purpose() == C0.Item2))
                    return unit.Move(C1, false);
            }
            return false;
        }
        public int ReversBlock(Tuple<int, int> c)
        {
            // Проверка на выход за пределы поля
            if ((c.Item1 < 0) || (c.Item2 < 0) || (c.Item1 >= X) || (c.Item2 >= Y))
                return 0;
            foreach (var unit in units)
                if (((unit.X() == c.Item1) && (unit.Y() == c.Item2)) || ((unit.X_Purpose() == c.Item1) && (unit.Y_Purpose() == c.Item2)))
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
            return new Tuple<Tuple<int, int>, Tuple<int, int>>(new Tuple<int, int>(L.X(), L.Y()), new Tuple<int, int>(L.X_Purpose(), L.Y_Purpose()));
        }
        public bool DelUnits()
        {
            if (units.Count() <= 1)
            {
                SystemSounds.Beep.Play();
                return false;
            }
            units = new List<T> { units[0] };
            return true;
        }
        public void PlusColumn()
        {
            X++;
            var arr = new Cell[X, Y];
            for (int i = 0; i < X - 1; i++)
                for (int j = 0; j < Y; j++)
                    arr[i, j] = Arr[i, j];
            for (int j = 0; j < Y; j++)
                arr[X - 1, j] = new Cell(false);
            Arr = arr;
            foreach (var unit in units)
                unit.NewArr(X, Y);
        }
        public void PlusRow()
        {
            Y++;
            var arr = new Cell[X, Y];
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y - 1; j++)
                    arr[i, j] = Arr[i, j];
            for (int i = 0; i < X; i++)
                arr[i, Y - 1] = new Cell(false);
            Arr = arr;
            foreach (var unit in units)
                unit.NewArr(X, Y);
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
                        sw.WriteLine(Arr[i, j].ToStr());
                // Записать в файл юнитов
                foreach (var item in units)
                    sw.WriteLine(item.ToStr());
                sw.Close();
                name = name_ + ".board";
            }
            catch (Exception e) { }
            return name;
        }
        public bool IsEnd()
        {
            bool b = true;
            // Проверяем, что все юниты дошли до своих целей
            foreach (var Unit in units)
                b = b && Unit.IsRealEnd();
            return b;
        }
        protected void GenerationEmthy(int X, int Y, int Blocks, Random rnd, int x, int y)
        {
            int N = X * Y - Blocks - 1;
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
        protected void GenerationBlocks(int X, int Y)
        {
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    if (Arr[i, j] is null)
                        Arr[i, j] = new Cell(true);
        }

    }
}
