using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAPF_System
{
    public class TunellCentr : Tunell
    {
        public TunellCentr(Board board, List<Tunell> LT, int x, int y) 
            : base(board, LT, x, y) { }

        public bool Contains(bool isReal, int id)
        {
            if (!isReal && (from unit in board.units select unit.id).Except(tunell_units.Select(item => (int)item))
                .Any(Unit_id => board.InTunell(board.units.Find(u => u.id == Unit_id), this)))
                    return false;

            List<int> lst = new List<int>();
            foreach (var Unit_id in tunell_units)
            {
                int i = (int)Unit_id;
                lst.Add(i);
                if (!tunells.Any(tunell => board.InTunell(board.units.Find(u => u.id == i), tunell)))
                    return lst.Contains(id);
            }
            return lst.Contains(id);
        }
    }
}
