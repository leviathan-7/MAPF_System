using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAPF_System
{
    public class TunellDec : Tunell<Unit>
    {
        public TunellDec(BoardInterface board)
        {
            Constructor(board);
        }
        public void Add(int x, int y)
        {
            foreach (var Unit in ((BoardDec)board).Units())
                if ((Unit.X_Purpose() == x) && (Unit.Y_Purpose() == y))
                {
                    tunell_units.Add(Unit);
                    break;
                }
        }
        public void Add(List<TunellDec> LT)
        {
            foreach (var tunell in LT)
                Add(tunell);
        }
        public void MakeFlags(BoardDec Board)
        {
            bool b = true;
            foreach (var Unit in tunell_units)
            {
                if (Unit.IsRealEnd())
                    Unit.flag = false;
                bool t = Board.InTunell(Unit, this);
                foreach (var tunell in tunells)
                    t = t || Board.InTunell(Unit, (TunellDec)tunell);
                b = b && t;
                if (Unit.IsRealEnd() && !b)
                    Unit.flag = true;
            }
        }
        public int Id()
        {
            foreach (var Unit in tunell_units)
            {
                bool b = ((BoardDec)board).InTunell(Unit, this);
                foreach (var tunell in tunells)
                    b = b || ((BoardDec)board).InTunell(Unit, tunell);
                if (!b)
                    return Unit.Id();
            }
            return -1;
        }
    }
}
