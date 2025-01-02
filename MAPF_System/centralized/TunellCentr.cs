using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAPF_System
{
    public class TunellCentr : Tunell<int>
    {
        public TunellCentr(BoardInterface board)
        {
            Constructor(board);
        }
        public void Add(int x, int y)
        {
            foreach (var Unit in ((BoardCentr)board).Units())
                if ((Unit.X_Purpose() == x) && (Unit.Y_Purpose() == y))
                {
                    tunell_units.Add(Unit.Id());
                    break;
                }
        }
        public void Add(List<TunellCentr> LT)
        {
            foreach (var tunell in LT)
                Add(tunell);
        }

        public List<int> Ids()
        {
            foreach (var Unit_id in (from unit in ((BoardCentr)board).Units() select unit.Id()).Except(tunell_units))
                if (((BoardCentr)board).InTunell(Unit_id, this))
                    return new List<int>();
            return RealIds();
        }

        public List<int> RealIds()
        {
            List<int> lst = new List<int>();
            foreach (var Unit_id in tunell_units)
            {
                lst.Add(Unit_id);
                bool b = ((BoardCentr)board).InTunell(Unit_id, this);
                foreach (var tunell in tunells)
                    b = b || ((BoardCentr)board).InTunell(Unit_id, tunell);
                if (!b)
                    return lst;
            }
            return lst;
        }
    }
}
