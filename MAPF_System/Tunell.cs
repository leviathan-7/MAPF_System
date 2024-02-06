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
        private Board board;
        private List<Unit> tunell_units;

        public Tunell(Board board)
        {
            this.board = board;
            tunell_units = new List<Unit>();
        }
        public void Add(int x, int y)
        {
            foreach (var Unit in board.Units())
                if ((Unit.X_Purpose() == x) && (Unit.Y_Purpose() == y))
                {
                    tunell_units.Add(Unit);
                    break;
                }
        }
        public void MakeFlags(Board Board)
        {
            bool b = true;
            foreach (var Unit in tunell_units)
            {
                if (Unit.IsRealEnd())
                    Unit.flag = false;
                b = b && Unit.IsRealEnd();
                if (Unit.IsRealEnd() && !b)
                    Unit.flag = true;
            }
        }
        public int Id()
        {
            foreach (var Unit in tunell_units)
                if (!board.InTunell(Unit, this))
                    return Unit.Id();
            return -1;
        }
    }
}
