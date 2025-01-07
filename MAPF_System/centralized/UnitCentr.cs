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
            return new UnitCentr(b ? new int[X_Board, Y_Board] : Arr, x, y, x_Purpose, y_Purpose, id, -1, -1, X_Board, Y_Board, false, flag); 
        }
        public List<UnitCentr> MakeStep(BoardCentr Board, IEnumerable<UnitCentr> was_step, IEnumerable<UnitCentr> units, bool b)
        {
            List <UnitCentr> lstUnits = new List<UnitCentr>();
            int[] dx = { 0, 0, -1, 1, 0 }, dy = { -1, 1, 0, 0, 0 };

            for (int i = 0; i <= 4; i++)
            {
                int _x = x + dx[i], _y = y + dy[i];

                if (Board.IsEmpthy(_x, _y) 
                    && !was_step.Any(unit => unit.x == _x && unit.y == _y)
                    && !units.Any(unit => unit.x == _x && unit.y == _y && was_step.Any(u => u.id == unit.id && u.x == x && u.y == y)))
                {
                    if (i == 4)
                        lstUnits.Add(Copy());
                    else
                    {
                        UnitCentr U = Copy();
                        U.x = _x;
                        U.y = _y;
                        U.last_Unit = this;
                        if (!b || b && (last_Unit is null || !(U.x == last_Unit.x && U.y == last_Unit.y) || !(Board.units.Find(unit => unit.x == U.x && unit.y == U.y) == null)))
                            lstUnits.Add(U);
                    }
                }
            }
                
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
                units.ForEach(unit => 
                {
                    if ((!claster.Contains(unit)) &&
                        ((((u.x + 1 == unit.x) || (u.x - 1 == unit.x)) && ((u.y == unit.y) || (u.y - 1 == unit.y) || (u.y + 1 == unit.y))) ||
                        (((u.x + 2 == unit.x) || (u.x - 2 == unit.x)) && (u.y == unit.y)) ||
                        ((u.x == unit.x) && ((u.y - 1 == unit.y) || (u.y + 1 == unit.y) || (u.y - 2 == unit.y) || (u.y + 2 == unit.y)))))
                    {
                        claster.Add(unit);
                        stack.Push(unit);
                    }
                });
            }

            return claster;
        }
        public int Manheton(BoardCentr board)
        {
            int s = FindMin(x, y, board, true);

            TunellCentr T = board.Tunell(x, y) as TunellCentr;
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
            int[] xx = { -1, 1, 0, 0 }, yy = { 0, 0, -1, 1 };
            for (int w = 0; w < 4; w++)
            {
                int newI = x + xx[w], newJ = y + yy[w];
                if (board.IsEmpthy(newI, newJ))
                    list.Add(iter ? FindMin(newI, newJ, board, !iter) : RealManheton(newI, newJ));
            }
            return 1 + list.Min();
        }

    }
}
