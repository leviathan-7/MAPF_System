using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAPF_System
{
    public class Tunell
    {
        private List<Unit> units;
        private List<Unit> tunell_units;

        public Tunell(List<Unit> units)
        {
            this.units = units;
            tunell_units = new List<Unit>();
        }
        public void Add(int x, int y)
        {
            foreach (var Unit in units)
                if ((Unit.X_Purpose() == x) && (Unit.Y_Purpose() == y))
                {
                    tunell_units.Add(Unit);
                    break;
                }
        }
        public void MakeFlags()
        {
            bool b = true;
            foreach (var Unit in tunell_units)
            {
                b = b && Unit.IsEnd();
                if (Unit.IsEnd() && !b)
                    Unit.MakeFlag();
            }
        }
        public int Id()
        {
            if (tunell_units.Count <= 2)
                return -1;
            foreach (var Unit in tunell_units)
                if (!Unit.IsEnd())
                    return Unit.Id();
            return -1;
        }
    }
}
