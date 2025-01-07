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
            get { return tunell_units.FirstOrDefault(Unit => !tunells.Any(tunell => board.InTunell(Unit, tunell)))?.id ?? -1; }
        }

        public TunellDec(Board<UnitDec, Unit> board, List<Tunell<UnitDec, Unit>> LT, int x, int y) 
            : base(board, LT, x, y) { }

        public void MakeFlags(BoardDec Board)
        {
            bool b = true;
            tunell_units.ForEach(Unit =>
            {
                if (Unit.isRealEnd)
                    Unit.flag = false;
                b = b && tunells.Any(tunell => Board.InTunell(Unit, tunell));
                if (Unit.isRealEnd && !b)
                    Unit.flag = true;
            });
        }

    }
}
