﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAPF_System
{
    public class Tunell
    {
        protected Board board;
        protected List<Tunell> tunells;
        protected List<object> tunell_units;

        public Tunell(Board board, List<Tunell> LT, int x, int y)
        {
            this.board = board;
            tunells = new List<Tunell>() { this };
            tunells.AddRange(LT.SelectMany(tunell => tunell.tunells));
            tunell_units = LT.SelectMany(tunell => tunell.tunell_units).ToList();

            var foundUnit = board.units.FirstOrDefault(Unit => (Unit.x_Purpose == x) && (Unit.y_Purpose == y));
            if (foundUnit != null)
                switch (this)
                {
                    case TunellDec _:
                        tunell_units.Add(foundUnit);
                        break;
                    case TunellCentr _:
                        tunell_units.Add(foundUnit.id);
                        break;
                    default:
                        break;
                }
        }
    }
}
