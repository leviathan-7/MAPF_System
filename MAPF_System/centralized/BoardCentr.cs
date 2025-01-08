using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Security;
using System.Windows.Forms;
using System.Media;
using System.Collections;

namespace MAPF_System
{
    public class BoardCentr : Board<UnitCentr, int>
    {
        public BoardCentr(int X, int Y, int Blocks, int N_Units) : base(X, Y, Blocks, N_Units) { }
        public BoardCentr(int X, int Y, Cell<UnitCentr, int>[,] Arr, List<UnitCentr> units, string name, List<Tunell<UnitCentr, int>> tunells)
            : base(X, Y, Arr, units, name, tunells) { }
        public BoardCentr(string path = null) : base(path) { }
        
        public void MakeStep(BoardCentr Board)
        {
            // Добавить блоки в пределах видимости юнитов
            MakeBlocks(Board);
            // Добавить узлы -- части туннелей
            if (!AreNotTunells)
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

                                if (!AreNotTunells && kk == 3)
                                {
                                    List<Tunell<UnitCentr, int>> LT = new List<Tunell<UnitCentr, int>>();
                                    for (int w = 0; w < 4; w++)
                                    {
                                        int newI = i + xx[w], newJ = j + yy[w];
                                        if (IsTunell(newI, newJ))
                                            LT.Add(Arr[newI, newJ].tunell);
                                    }

                                    tunells.Add(Arr[i, j].tunell = new TunellCentr(this, LT, i, j));
                                }

                                if (kk == 4 && Arr[i,j].wasvisited)
                                {
                                    AreNotTunells = true;
                                    for (int ii = 0; ii < X; ii++)
                                        for (int jj = 0; jj < Y; jj++)
                                            Arr[ii, jj].tunell = null;
                                }
                            }

                    if (k == 0)
                        break;
                }

            foreach (var Unit in units)
            {
                Arr[Unit.x, Unit.y].MakeVisit(Unit.id);
                Unit.PlusArr();
                if (Unit.isRealEnd)
                    Unit.ClearArr();
            }

            var new_units = new List<UnitCentr>();

            // Нахождение кластеров
            HashSet<UnitCentr> clasterizations = new HashSet<UnitCentr>();
            List<HashSet<UnitCentr>> clasters = new List<HashSet<UnitCentr>>();
            foreach (var unit in units)
                if (!clasterizations.Contains(unit))
                {
                    HashSet<UnitCentr> claster = unit.FindClaster(units);
                    clasters.Add(claster);
                    clasterizations.UnionWith(claster);
                }

            foreach (var Claster in clasters)
            {
                Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>> TT = new Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>>(new List<UnitCentr>(), Claster);
                int min_sum = Claster.Sum(unit => unit.realManheton) - Claster.Count(unit => !unit.isRealEnd);
                IEnumerable<UnitCentr> res = null;
                bool b = true;
                while (res is null)
                {
                    int sum = int.MaxValue;
                    Stack<Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>>> stack = new Stack<Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>>>();
                    stack.Push(TT);
                    while (stack.Count() != 0)
                    {
                        var T = stack.Pop();
                        if (T.Item2.Count() == 0)
                        {
                            var s = T.Item1.Sum(unit => unit.Manheton(this));
                            if (s == min_sum)
                            {
                                res = T.Item1;
                                break;
                            }

                            // Проверка на неравнество кластеров
                            bool isntEqw = false;
                            var sort1 = Claster.OrderBy(unit => unit.id).ToList();
                            var sort2 = T.Item1.OrderBy(unit => unit.id).ToList();
                            for (int i = 0; i < sort1.Count(); i++)
                                if ((sort1[i].x != sort2[i].x) || (sort1[i].y != sort2[i].y))
                                {
                                    if (sort1[i].isRealEnd && !sort2[i].isRealEnd)
                                    {
                                        TunellCentr TC = (TunellCentr)Arr[sort1[i].x, sort1[i].y].tunell;
                                        if (!(TC is null) && !TC.Contains(true, sort1[i].id))
                                        {
                                            isntEqw = true;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        isntEqw = true;
                                        break;
                                    }
                                }

                            if ((s < sum) && isntEqw)
                            {
                                sum = s;
                                res = T.Item1;
                            }
                        }
                        else
                            foreach (var unit in T.Item2.First().MakeStep(this, T.Item1, Claster, b))
                                stack.Push(new Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>>(T.Item1.Concat(new List<UnitCentr> { unit }), T.Item2.Skip(1)));
                    }

                    b = false;
                }
                new_units.AddRange(res);
            }
            units = new_units;

        }

    }
}
