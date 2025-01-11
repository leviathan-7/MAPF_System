using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAPF_System
{
    public class TunellDec : Tunell
    {
        public int id
        {
            get { return (tunell_units.FirstOrDefault(Unit => !tunells.Any(tunell => board.InTunell(Unit as Unit, tunell))) as Unit)?.id ?? -1; }
        }

        public TunellDec(Board board, List<Tunell> LT, int x, int y) 
            : base(board, LT, x, y) { }

        public void MakeFlags(BoardDec Board)
        {
            bool b = true;
            tunell_units.ForEach(item =>
            {
                Unit Unit = item as Unit;
                b = b && tunells.Any(tunell => Board.InTunell(Unit, tunell));
                if (Unit.isRealEnd)
                    Unit.flag = !b;
            });
        }

    }
}
