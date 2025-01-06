using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAPF_System
{
    public class Tunell<U, T> where U : Unit 
    {
        protected Board<U, T> board;
        protected List<Tunell<U, T>> tunells;
        protected List<T> tunell_units;

        public Tunell(Board<U, T> board, List<Tunell<U, T>> LT, int x, int y)
        {
            this.board = board;
            tunells = new List<Tunell<U, T>>();
            tunell_units = new List<T>();

            foreach (var tunell in LT)
            {
                tunells.Add(tunell);
                foreach (var u in tunell.tunell_units)
                    tunell_units.Add(u);
                foreach (var tt in tunell.tunells)
                    tunells.Add(tt);
            }

            foreach (var Unit in board.units)
                if ((Unit.x_Purpose == x) && (Unit.y_Purpose == y))
                {
                    if (this is TunellDec)
                        (this as TunellDec).tunell_units.Add(Unit);
                    if (this is TunellCentr)
                        (this as TunellCentr).tunell_units.Add(Unit.id);
                    break;
                }
        }
    }
}
