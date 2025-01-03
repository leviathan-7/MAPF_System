using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAPF_System
{
    public class UnitCentr : Unit
    {
        public UnitCentr(int [,] Arr, int x, int y, int x_Purpose, int y_Purpose, int id, int last__x, int last__y, int X, int Y, bool was_step = false, bool flag = false) 
            : base(x, y, X, Y, id, flag, x_Purpose, y_Purpose)
        {
            this.Arr = Arr;
        }
        public UnitCentr(string str, int i, int X, int Y) : base(str, X, Y, i) { }
        public UnitCentr Copy(bool b = false) 
        {
            if (b)
                Arr = new int[X_Board, Y_Board];
            return new UnitCentr(Arr, x, y, x_Purpose, y_Purpose, id, -1, -1, X_Board, Y_Board, false, flag); 
        }
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
                        if (last_Unit is null || !(U.x == last_Unit.x && U.y == last_Unit.y) || !(Board.units.Find(unit => unit.x == U.x && unit.y == U.y) == null))
                            lstUnits.Add(U);
                    }
                    else
                        lstUnits.Add(U);
                    
                }

            if (NoOneCell(x, y, was_step, units))
                lstUnits.Add(Copy());

            return lstUnits;
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
                    if ((!claster.Contains(unit)) &&
                        ((((u.x + 1 == unit.x) || (u.x - 1 == unit.x)) && ((u.y == unit.y) || (u.y - 1 == unit.y) || (u.y + 1 == unit.y))) ||
                        (((u.x + 2 == unit.x) || (u.x - 2 == unit.x)) && (u.y == unit.y)) ||
                        ((u.x == unit.x) && ((u.y - 1 == unit.y) || (u.y + 1 == unit.y) || (u.y - 2 == unit.y) || (u.y + 2 == unit.y)))))
                    {
                        claster.Add(unit);
                        stack.Push(unit);
                    }
            }

            return claster;
        }
        public int Manheton(BoardCentr board)
        {
            int s = FindMin(x, y, board, true);

            TunellCentr T = board.Tunell(x, y);
            int a = Arr[x, y];
            if (!(T is null) && !T.Contains(false, id))
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

    }
}
