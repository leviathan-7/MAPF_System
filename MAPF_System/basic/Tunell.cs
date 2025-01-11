﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAPF_System
{
    public class Tunell<T>
    {
        protected Board<T> board;
        protected List<Tunell<T>> tunells;
        protected List<T> tunell_units;

        public Tunell(Board<T> board, List<Tunell<T>> LT, int x, int y)
        {
            this.board = board;
            tunells = new List<Tunell<T>>() { this };
            tunell_units = new List<T>();

            LT.ForEach(tunell =>
            {
                tunell_units.AddRange(tunell.tunell_units);
                tunells.AddRange(tunell.tunells);
            });

            foreach (var Unit in board.units)
                if ((Unit.x_Purpose == x) && (Unit.y_Purpose == y))
                {
                    if (this is TunellDec)
                        (this as TunellDec).tunell_units.Add(Unit);
                    if (this is TunellCentr)
                        (this as TunellCentr).tunell_units.Add(Unit.id);
                    break;
                }
        }
    }
}
