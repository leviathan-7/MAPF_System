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
    public class BoardCentr : Board
    {
        public BoardCentr(int X, int Y, int Blocks, int N_Units) : base(X, Y, Blocks, N_Units) { }
        public BoardCentr(int X, int Y, Cell[,] Arr, List<Unit> units, string name, List<Tunell> tunells)
            : base(X, Y, Arr, units, name, tunells) { }
        public BoardCentr(string path = null) : base(path) { }
        
        public void MakeStep()
        {
            // Добавить узлы -- части туннелей
            if (!AreNotTunells)
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                        if (!(IsTunell(i, j) || Arr[i, j].isBlock))
                        {
                            int[] xx = { -1, 1, 0, 0 }, yy = { 0, 0, -1, 1 };
                            IEnumerable<int> range = Enumerable.Range(0, 4);
                            int kk = range.Sum(w => (!IsEmpthy(i + xx[w], j + yy[w]) || IsTunell(i + xx[w], j + yy[w])) ? 1 : 0);

                            if (!AreNotTunells && kk == 3)
                                tunells.Add(Arr[i, j].tunell = new TunellCentr(this, range.Where(w => IsTunell(i + xx[w], j + yy[w])).Select(w => Arr[i + xx[w], j + yy[w]].tunell).ToList(), i, j));
                            else if (kk == 4 && Arr[i, j].wasvisited)
                            {
                                AreNotTunells = true;
                                for (int ii = 0; ii < X; ii++)
                                    for (int jj = 0; jj < Y; jj++)
                                        Arr[ii, jj].tunell = null;
                            }
                        }

            units.ForEach(Unit => 
            {
                Arr[Unit.x, Unit.y].MakeVisit(Unit.id);
                Unit.PlusArr();
                if (Unit.isRealEnd)
                    Unit.ClearArr();
            });

            // Нахождение кластеров
            HashSet<Unit> clasterizations = new HashSet<Unit>();
            List<HashSet<Unit>> clasters = new List<HashSet<Unit>>();

            foreach (var unit in units.Where(u => !clasterizations.Contains(u)))
            {
                // Находим кластер
                HashSet<Unit> claster = new HashSet<Unit>() { unit };
                Stack<Unit> stack = new Stack<Unit>();
                stack.Push(unit);
                while (stack.Count() != 0)
                {
                    Unit u = stack.Pop();
                    units.ForEach(item =>
                    {
                        if ((!claster.Contains(item)) &&
                            ((((u.x + 1 == item.x) || (u.x - 1 == item.x)) && ((u.y == item.y) || (u.y - 1 == item.y) || (u.y + 1 == item.y))) ||
                            (((u.x + 2 == item.x) || (u.x - 2 == item.x)) && (u.y == item.y)) ||
                            ((u.x == item.x) && ((u.y - 1 == item.y) || (u.y + 1 == item.y) || (u.y - 2 == item.y) || (u.y + 2 == item.y)))))
                        {
                            claster.Add(item);
                            stack.Push(item);
                        }
                    });
                }

                // Добавляем найденный кластер
                clasters.Add(claster);
                clasterizations.UnionWith(claster);
            }

            units = clasters.SelectMany(Claster => 
            {
                Tuple<IEnumerable<Unit>, IEnumerable<Unit>> TT = new Tuple<IEnumerable<Unit>, IEnumerable<Unit>>(new List<Unit>(), Claster);
                int min_sum = Claster.Sum(unit => unit.realManheton) - Claster.Count(unit => !unit.isRealEnd);
                IEnumerable<Unit> res = null;
                bool b = true;
                while (res is null)
                {
                    int sum = int.MaxValue;
                    Stack<Tuple<IEnumerable<Unit>, IEnumerable<Unit>>> stack = new Stack<Tuple<IEnumerable<Unit>, IEnumerable<Unit>>>();
                    stack.Push(TT);
                    while (stack.Count() != 0)
                    {
                        var T = stack.Pop();
                        if (T.Item2.Count() == 0)
                        {
                            var s = T.Item1.Sum(unit => (unit as UnitCentr).Manheton(this));
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
                            foreach (var unit in (T.Item2.First() as UnitCentr).MakeStep(this, T.Item1, Claster, b))
                                stack.Push(new Tuple<IEnumerable<Unit>, IEnumerable<Unit>>(T.Item1.Concat(new List<Unit> { unit }), T.Item2.Skip(1)));
                    }
                    b = false;
                }
                return res;
            }).ToList();

        }

    }
}
