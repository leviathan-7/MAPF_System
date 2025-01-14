using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MAPF_System
{
    public class Unit
    {
        protected int X_Board;
        protected int Y_Board;
        protected int[,] Arr;
        protected Unit last_Unit;
        public bool flag;
        public int id { get; protected set; }
        public int x { get; protected set; }
        public int y { get; protected set; }
        public int x_Purpose { get; protected set; }
        public int y_Purpose { get; protected set; }
        public String str
        {
            get { return x + " " + y + " " + x_Purpose + " " + y_Purpose; }
        }
        public bool isRealEnd
        {
            get { return (x == x_Purpose) && (y == y_Purpose); }
        }
        public bool isEnd
        {
            get { return isRealEnd && !flag; }
        }
        public int realManheton
        {
            get { return RealManheton(x, y); }
        }
        public Unit copy
        {
            get 
            {
                switch (this)
                {
                    case UnitCentr _:
                        return ((UnitCentr)this).copy;
                    case UnitDec _:
                        return ((UnitDec)this).copy;
                    default:
                        return null;
                }
            }
        }

        public Unit(int x, int y, int X, int Y, int id, bool flag, int x_Purpose, int y_Purpose)
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
        public Unit(String str, int X, int Y, int i)
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
        protected int RealManheton(int x, int y) { return Math.Abs(x_Purpose - x) + Math.Abs(y_Purpose - y); }
        public void PlusArr() { Arr[x, y]++; }
        public void ClearArr() { Arr = new int[X_Board, Y_Board]; }
        public bool Move(Tuple<int, int> C, bool b)
        {
            if (b)
                (x, y) = (C.Item1, C.Item2);
            else
                (x_Purpose, y_Purpose) = (C.Item1, C.Item2);
            return true;
        }
        public void NewArr(int X, int Y)
        {
            (X_Board, Y_Board) = (X, Y);
            Arr = new int[X, Y];
        }
        
    }
}
