﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAPF_System
{
    public class UnitCentr
    {
        private int X_Board;
        private int Y_Board;
        private int id;
        private int x;
        private int y;
        private int x_Purpose;
        private int y_Purpose;
        private int[,] Arr;

        private UnitCentr last_Unit;

        public bool flag;


        public UnitCentr(int [,] Arr, int x, int y, int x_Purpose, int y_Purpose, int id, int last__x, int last__y, int X, int Y, bool was_step = false, bool flag = false) {
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
            // Массив с количеством посещений узлов
            this.Arr = Arr;
        }
        public UnitCentr(string str, int i, int X, int Y)
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
        public UnitCentr Copy(bool b = false) 
        {
            if (b)
                Arr = new int[X_Board, Y_Board];
            return new UnitCentr(Arr, x, y, x_Purpose, y_Purpose, id, -1, -1, X_Board, Y_Board, false, flag); 
        }
        public int X() { return x; }
        public int Y() { return y; }
        public int X_Purpose() { return x_Purpose; }
        public int Y_Purpose() { return y_Purpose; }
        public int Id() { return id; }
        public string ToStr() { return x + " " + y + " " + x_Purpose + " " + y_Purpose; }
        public bool IsRealEnd() { return (x == x_Purpose) && (y == y_Purpose); }
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
        
        
        //

        public List<UnitCentr> MakeStep(BoardCentr Board, IEnumerable<UnitCentr> was_step, IEnumerable<UnitCentr> units, bool b)
        {
            List <UnitCentr> lstUnits = new List<UnitCentr>();
            int[] dx = { 0, 0, -1, 1 };
            int[] dy = { -1, 1, 0, 0 };
            for (int i = 0; i < 4; i++)
                if (Board.IsEmpthy(x + dx[i], y + dy[i]) && NoOneCell(x + dx[i], y + dy[i], was_step, units))
                {
                    UnitCentr U = Copy();
                    U.x = x + dx[i];
                    U.y = y + dy[i];
                    U.last_Unit = this;
                    if (b)
                    {
                        if (last_Unit is null || !(U.x == last_Unit.x && U.y == last_Unit.y) || !(Board.Units().Find(unit => unit.x == U.x && unit.y == U.y) == null))
                            lstUnits.Add(U);
                    }
                    else
                        lstUnits.Add(U);
                    
                }

            if (NoOneCell(x, y, was_step, units))
                lstUnits.Add(Copy());

            return lstUnits;
        }

        private bool NoOneCell(int _x, int _y, IEnumerable<UnitCentr> was_step, IEnumerable<UnitCentr> units)
        {
            foreach (var unit in was_step)
                if ((unit.x == _x) && (unit.y == _y))
                    return false;

            foreach (var unit in units)
                if ((unit.x == _x) && (unit.y == _y))
                    foreach (var u in was_step)
                        if((u.id == unit.id) && (u.x == x) && (u.y == y))
                            return false;

            return true;
        }

        public void PlusArr()
        {
            Arr[x, y]++;
        }

        public int Manheton(BoardCentr board)
        {
            int s = FindMin(x, y, board, true);

            TunellCentr T = board.Tunell(x, y);
            int a = Arr[x, y];
            if (!(T is null) && !T.Ids().Contains(id))
                return 1000 + s + 2 * a;

            if (!(T is null) && !(last_Unit is null) && board.Tunell(last_Unit.x, last_Unit.y) is null)
                a = 0;

            return s != 0 ? s + 2 * a : 0;
        }

        private int FindMin(int x, int y, BoardCentr board, bool iter)
        {
            int s = RealManheton(x, y);
            if (s <= 1)
                return s;
            List<int> list = new List<int>();
            if (iter)
            {
                if (board.IsEmpthy(x + 1, y))
                    list.Add(FindMin(x + 1, y, board, !iter));
                if (board.IsEmpthy(x - 1, y))
                    list.Add(FindMin(x - 1, y, board, !iter));
                if (board.IsEmpthy(x, y + 1))
                    list.Add(FindMin(x, y + 1, board, !iter));
                if (board.IsEmpthy(x, y - 1))
                    list.Add(FindMin(x, y - 1, board, !iter));
            }
            else 
            {
                if (board.IsEmpthy(x + 1, y))
                    list.Add(RealManheton(x + 1, y));
                if (board.IsEmpthy(x - 1, y))
                    list.Add(RealManheton(x - 1, y));
                if (board.IsEmpthy(x, y + 1))
                    list.Add(RealManheton(x, y + 1));
                if (board.IsEmpthy(x, y - 1))
                    list.Add(RealManheton(x, y - 1));
            }
            return 1 + list.Min();
        }

        public int RealManheton()
        {
            return RealManheton(x, y);
        }

        private int RealManheton(int x, int y)
        {
            return Math.Abs(x_Purpose - x) + Math.Abs(y_Purpose - y);
        }

        public HashSet<UnitCentr> FindClaster(List<UnitCentr> units)
        {
            HashSet<UnitCentr> claster = new HashSet<UnitCentr>() { this };
            Stack<UnitCentr> stack = new Stack<UnitCentr>();
            stack.Push(this);
            while (stack.Count() != 0)
            {
                UnitCentr u = stack.Pop();
                foreach (var unit in units)
                    if((!claster.Contains(unit)) && 
                        ((((u.x+1 == unit.x) || (u.x -1 == unit.x)) && ((u.y == unit.y) || (u.y - 1 == unit.y) || (u.y + 1 == unit.y))) ||
                        (((u.x + 2 == unit.x) || (u.x - 2 == unit.x)) && (u.y == unit.y)) ||
                        ((u.x == unit.x) && ((u.y - 1 == unit.y) || (u.y + 1 == unit.y) || (u.y - 2 == unit.y) || (u.y + 2 == unit.y)))))
                    {
                        claster.Add(unit);
                        stack.Push(unit);
                    }
            }

            return claster;
        }

        public void ClearArr()
        {
            Arr = new int[X_Board, Y_Board];
        }
    }
}
