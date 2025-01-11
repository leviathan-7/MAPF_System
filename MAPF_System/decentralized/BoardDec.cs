﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Security;
using System.Windows.Forms;
using System.Media;

namespace MAPF_System
{
    public class BoardDec : Board<Unit>
    {
        public BoardDec(int X, int Y, int Blocks, int N_Units) : base(X, Y, Blocks, N_Units) { }
        public BoardDec(int X, int Y, Cell<Unit>[,] Arr, List<Unit> units, string name, List<Tunell<Unit>> tunells)
            : base(X, Y, Arr, units, name, tunells) { }
        public BoardDec(string path = null) : base(path) { }

        public void MakeStep(BoardDec Board, int kol_iter_a_star)
        {
            // Обнуление значений was_step
            foreach (var Unit in units)
                (Unit as UnitDec).NotWasStep();
            // Добавить плохие узлы
            while (true)
            {
                int k = 0;
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                        if (!(Arr[i, j].isBad || Arr[i, j].isBlock))
                        {
                            // Проверка на отсутсвие целей
                            bool b = true;
                            foreach (var Unit in units)
                                b = b && !((Unit.x_Purpose == i) && (Unit.y_Purpose == j)) && !((Unit.x == i) && (Unit.y == j));
                            if (b)
                            {
                                int kk = 0;
                                int[] xx = { -1, 1, 0, 0 }, yy = { 0, 0, -1, 1 };
                                for (int w = 0; w < 4; w++)
                                {
                                    int newI = i + xx[w], newJ = j + yy[w];
                                    if (!IsEmpthy(newI, newJ) || Arr[newI, newJ].isBad)
                                        kk ++;
                                }

                                if (kk == 3)
                                {
                                    Arr[i, j].MakeBad();
                                    k++;
                                }
                            }
                        }

                if (k == 0)
                    break;
            }
            // Добавить узлы -- части туннелей
            while (true)
            {
                int k = 0;
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                        if (!(IsTunell(i, j) || Arr[i, j].isBlock))
                        {
                            int kk = 0;
                            int[] xx = { -1, 1, 0, 0 }, yy = { 0, 0, -1, 1 };
                            for (int w = 0; w < 4; w++)
                            {
                                int newI = i + xx[w], newJ = j + yy[w];
                                if (!IsEmpthy(newI, newJ) || IsTunell(newI, newJ))
                                    kk++;
                            }

                            if (kk == 3)
                            {
                                List<Tunell<Unit>> LT = new List<Tunell<Unit>>();
                                for (int w = 0; w < 4; w++)
                                {
                                    int newI = i + xx[w], newJ = j + yy[w];
                                    if (IsTunell(newI, newJ) && !Arr[newI, newJ].isBad)
                                        LT.Add(Arr[newI, newJ].tunell);
                                }

                                tunells.Add(Arr[i, j].tunell = new TunellDec(this, LT, i, j));
                            }
                        }

                if (k == 0)
                    break;
            }
            // Поставить флаги юнитам, которые в простом туннеле перегораживают проезд
            foreach (var t in tunells)
                ((TunellDec)t).MakeFlags(this);
            // Сделать шаг теми юнитами, которые еще не достигли своей цели, при этом давая приоритет тем юнитам, которые дальше от цели
            List<Unit> Was_bool_step_units = new List<Unit>(), Was_near_end_units = new List<Unit>(),
            NOT_Was_near_end_units = new List<Unit>(), Tunell_NOT_Was_near_end_units = new List<Unit>();

            foreach (var Unit in units)
                if ((Unit as UnitDec).was_near_end)
                {
                    if ((Unit as UnitDec).was_bool_step)
                        Was_bool_step_units.Add(Unit);
                    else
                        Was_near_end_units.Add(Unit);
                }
                else
                {
                    if (IsTunell(Unit.x, Unit.y))
                        Tunell_NOT_Was_near_end_units.Add(Unit);
                    else
                        NOT_Was_near_end_units.Add(Unit);
                }

            new List<List<Unit>>() { NOT_Was_near_end_units, Was_near_end_units, Tunell_NOT_Was_near_end_units, Was_bool_step_units }.ForEach(list =>
            {
                foreach (var Unit in list.OrderBy(u => -(u as UnitDec).F))
                    if (!Unit.isEnd)
                        (Unit as UnitDec).MakeStep(this, from u in units where u != Unit select u, kol_iter_a_star);
            });
        }
       
        public bool IsEmpthyAndNoTunel(int x, int y)
        {
            return !((x < 0) || (y < 0) || (x >= X) || (y >= Y) || Arr[x, y].isTunell || Arr[x, y].isBlock);
        }
        public bool IsBadCell(int x, int y) { return Arr[x, y].isBad; }
        public void MakeVisit(int x, int y, int id) { Arr[x, y].MakeVisit(id); }

    }
}
