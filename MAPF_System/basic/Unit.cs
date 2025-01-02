using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MAPF_System
{
    public class Unit
    {
        protected int X_Board;
        protected int Y_Board;
        protected int id;
        protected int x;
        protected int y;
        protected int x_Purpose;
        protected int y_Purpose;
        protected int[,] Arr;
        protected Unit last_Unit;
        public bool flag;

        public int X() { return x; }
        public int Y() { return y; }
        public int X_Purpose() { return x_Purpose; }
        public int Y_Purpose() { return y_Purpose; }
        public int Id() { return id; }
        public string ToStr() { return x + " " + y + " " + x_Purpose + " " + y_Purpose; }
        public bool IsRealEnd() { return (x == x_Purpose) && (y == y_Purpose); }
        public bool IsEnd() { return IsRealEnd() && !flag; }
        protected int RealManheton(int x, int y) { return Math.Abs(x_Purpose - x) + Math.Abs(y_Purpose - y); }
        public int RealManheton() { return RealManheton(x, y); }
        public void PlusArr() { Arr[x, y]++; }
        public void ClearArr() { Arr = new int[X_Board, Y_Board]; }
        public bool Move(Tuple<int, int> C, bool b)
        {
            if (b)
            {
                x = C.Item1;
                y = C.Item2;
            }
            else
            {
                x_Purpose = C.Item1;
                y_Purpose = C.Item2;
            }
            return true;
        }
        public void NewArr(int X, int Y)
        {
            X_Board = X;
            Y_Board = Y;
            Arr = new int[X, Y];
        }
        protected void Constructor(int x, int y, int X, int Y, int id, bool flag, int x_Purpose, int y_Purpose)
        {
            this.id = id;
            this.flag = flag;
            X_Board = X;
            Y_Board = Y;
            // Задание местоположения юнита
            this.x = x;
            this.y = y;
            // Задание местоположения цели юнити
            this.x_Purpose = x_Purpose;
            this.y_Purpose = y_Purpose;
        }
        protected void ConstructorStr(String str, int X, int Y, int i)
        {
            flag = false;
            string[] arr = str.Split(' ');
            X_Board = X;
            Y_Board = Y;
            // Задание параметров юнита на основе строки из файла
            x = int.Parse(arr[0]);
            y = int.Parse(arr[1]);
            x_Purpose = int.Parse(arr[2]);
            y_Purpose = int.Parse(arr[3]);
            id = i;
            // Массив с количеством посещений узлов
            Arr = new int[X, Y];
        }
    }
}
