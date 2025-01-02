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
        protected List<Tunell<T>> tunells;
        protected List<T> tunell_units;

        protected void Add(Tunell<T> tunell) 
        {
            tunells.Add(tunell);
            foreach (var u in tunell.tunell_units)
                tunell_units.Add(u);
            foreach (var tt in tunell.tunells)
                tunells.Add(tt);
        }

        protected void Constructor(BoardInterface board) 
        {
            this.board = board;
            tunells = new List<Tunell<T>>();
            tunell_units = new List<T>();
        }
    }
}
