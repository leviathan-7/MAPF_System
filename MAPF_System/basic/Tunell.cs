using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAPF_System
{
    public class Tunell<T>: TunellInterface
    {
        protected BoardInterface board;
        protected List<TunellInterface> tunells;
        protected List<T> tunell_units;

        public void Add(List<TunellInterface> LT)
        {
            foreach (var tunell in LT)
            {
                tunells.Add(tunell);
                foreach (var u in ((Tunell<T>)tunell).tunell_units)
                    tunell_units.Add(u);
                foreach (var tt in ((Tunell<T>)tunell).tunells)
                    tunells.Add(tt);
            }
        }

        public Tunell(BoardInterface board)
        {
            this.board = board;
            tunells = new List<TunellInterface>();
            tunell_units = new List<T>();
        }
    }
}
