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
        public new UnitCentr copy
        {
            get { return new UnitCentr(new int[X_Board, Y_Board], x, y, x_Purpose, y_Purpose, id, -1, -1, X_Board, Y_Board, false, flag); }
        }

        public UnitCentr(int [,] Arr, int x, int y, int x_Purpose, int y_Purpose, int id, int last__x, int last__y, int X, int Y, bool was_step = false, bool flag = false) 
            : base(x, y, X, Y, id, flag, x_Purpose, y_Purpose)
        {
            this.Arr = Arr;
        }
        public UnitCentr(string str, int i, int X, int Y) : base(str, X, Y, i) { }
        public List<Unit> MakeStep(BoardCentr Board, IEnumerable<Unit> was_step, IEnumerable<Unit> units, bool b)
        {
            List <Unit> lstUnits = new List<Unit>();
            int[] dx = { 0, 0, -1, 1, 0 }, dy = { -1, 1, 0, 0, 0 };

            for (int i = 0; i <= 4; i++)
            {
                int _x = x + dx[i], _y = y + dy[i];

                if (Board.IsEmpthy(_x, _y) 
                    && !was_step.Any(unit => unit.x == _x && unit.y == _y)
                    && !units.Any(unit => unit.x == _x && unit.y == _y && was_step.Any(u => u.id == unit.id && u.x == x && u.y == y)))
                {
                    UnitCentr U = new UnitCentr(Arr, x, y, x_Purpose, y_Purpose, id, -1, -1, X_Board, Y_Board, false, flag);
                    if (i == 4)
                        lstUnits.Add(U);
                    else
                    {
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
        public int Manheton(BoardCentr board)
        {
            // Находим минимальное значение для вычесления расстояния
            int min = -1;
            bool isStart = true;
            Stack<Tuple<int, int>> stack = new Stack<Tuple<int, int>>();
            stack.Push(new Tuple<int, int>(x, y));
            List<int> list = new List<int>();
            int[] xx = { -1, 1, 0, 0 }, yy = { 0, 0, -1, 1 };
            while (stack.Count > 0)
            {
                var t = stack.Pop();
                int s = RealManheton(t.Item1, t.Item2);
                if (s <= 1)
                {
                    min = s + (isStart ? 0 : 1);
                    break;
                }
                for (int w = 0; w < 4; w++)
                {
                    int newI = t.Item1 + xx[w], newJ = t.Item2 + yy[w];
                    if (board.IsEmpthy(newI, newJ))
                    {
                        if (isStart)
                            stack.Push(new Tuple<int, int>(newI, newJ));
                        else
                            list.Add(RealManheton(newI, newJ) + 1);
                    }
                }
                isStart = false;
            }
            if (min == -1)
                min = 1 + list.Min();

            //

            TunellCentr T = board.Tunell(x, y) as TunellCentr;
            int a = Arr[x, y];
            if (!(T is null) && !T.Contains(false, id))
                return 1000 + min + 2 * a;

            if (!(T is null) && !(last_Unit is null) && board.Tunell(last_Unit.x, last_Unit.y) is null)
                a = 0;

            return min != 0 ? min + 2 * a : 0;
        }
        
    }
}
