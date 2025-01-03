using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAPF_System
{
    public class Tunell<T>
    {
        protected BoardInterface board;
        protected List<Tunell<T>> tunells;
        protected List<T> tunell_units;

        public void Add(List<Tunell<T>> LT)
        {
            foreach (var tunell in LT)
            {
                tunells.Add(tunell);
                foreach (var u in tunell.tunell_units)
                    tunell_units.Add(u);
                foreach (var tt in tunell.tunells)
                    tunells.Add(tt);
            }
        }

        public Tunell(BoardInterface board)
        {
            this.board = board;
            tunells = new List<Tunell<T>>();
            tunell_units = new List<T>();
        }
    }
}
