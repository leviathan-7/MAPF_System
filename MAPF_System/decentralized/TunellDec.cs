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
        private BoardDec board;
        private List<UnitDec> tunell_units;
        private List<TunellDec> tunells;

        public TunellDec(BoardDec board)
        {
            this.board = board;
            tunell_units = new List<UnitDec>();
            tunells = new List<TunellDec>();
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
        public void Add(List<TunellDec> LT)
        {
            foreach (var t in LT)
            {
                tunells.Add(t);
                foreach (var u in t.tunell_units)
                    tunell_units.Add(u);
                foreach (var tt in t.tunells)
                    tunells.Add(tt);
            }
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
                    t = t || Board.InTunell(Unit, tunell);
                b = b && t;
                if (Unit.IsRealEnd() && !b)
                    Unit.flag = true;
            }
        }
        public int Id()
        {
            foreach (var Unit in tunell_units)
            {
                bool b = board.InTunell(Unit, this);
                foreach (var tunell in tunells)
                    b = b || board.InTunell(Unit, tunell);
                if (!b)
                    return Unit.Id();
            }
            return -1;
        }
    }
}
