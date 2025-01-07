using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAPF_System
{
    public class TunellDec : Tunell<UnitDec ,Unit>
    {
        public int id
        {
            get 
            {
                foreach (var Unit in tunell_units)
                {
                    bool b = false;
                    foreach (var tunell in tunells)
                        b = b || board.InTunell(Unit, tunell);
                    if (!b)
                        return Unit.id;
                }
                return -1;
            }
        }

        public TunellDec(Board<UnitDec, Unit> board, List<Tunell<UnitDec, Unit>> LT, int x, int y) 
            : base(board, LT, x, y) { }

        public void MakeFlags(BoardDec Board)
        {
            bool b = true;
            foreach (var Unit in tunell_units)
            {
                if (Unit.isRealEnd)
                    Unit.flag = false;
                bool t = false;
                foreach (var tunell in tunells)
                    t = t || Board.InTunell(Unit, tunell);
                b = b && t;
                if (Unit.isRealEnd && !b)
                    Unit.flag = true;
            }
        }

    }
}
