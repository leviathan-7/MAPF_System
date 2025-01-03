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
        public TunellCentr(BoardInterface board) : base(board) { }

        public void Add(int x, int y)
        {
            foreach (var Unit in ((BoardCentr)board).units)
                if ((Unit.x_Purpose == x) && (Unit.y_Purpose == y))
                {
                    tunell_units.Add(Unit.id);
                    break;
                }
        }

        public bool Contains(bool isReal, int id)
        {
            if (!isReal)
                foreach (var Unit_id in (from unit in ((BoardCentr)board).units select unit.id).Except(tunell_units))
                    if (((BoardCentr)board).InTunell(Unit_id, this))
                        return false;

            List<int> lst = new List<int>();
            foreach (var Unit_id in tunell_units)
            {
                lst.Add(Unit_id);
                bool b = ((BoardCentr)board).InTunell(Unit_id, this);
                foreach (var tunell in tunells)
                    b = b || ((BoardCentr)board).InTunell(Unit_id, tunell);
                if (!b)
                    return lst.Contains(id);
            }
            return lst.Contains(id);
        }
    }
}
