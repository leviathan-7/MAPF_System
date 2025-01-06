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
        private bool AreNotTunells;

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

                                    var T = new TunellCentr(this, LT, i, j);
                                    Arr[i, j].tunell = T;
                                    tunells.Add(T);
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
                IEnumerable<UnitCentr> res = NewUnitsStack(TT, min_sum, Claster, true);
                if (!(res is null))
                    new_units.AddRange(res);
                else
                    new_units.AddRange(NewUnitsStack(TT, min_sum, Claster, false));
            }
            units = new_units;

        }
        
        public TunellCentr Tunell(int x, int y) 
        { 
            return (TunellCentr)Arr[x, y].tunell; 
        }
        public bool InTunell(int unit_id, Tunell<UnitCentr, int> tunell) 
        {
            return InTunell(units.Find(u => u.id == unit_id), tunell);
        }

        private IEnumerable<UnitCentr> NewUnitsStack(Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>> TT, int min_sum, IEnumerable<UnitCentr> claster, bool b)
        {
            int sum = int.MaxValue;
            Stack<Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>>> stack = new Stack<Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>>>();
            stack.Push(TT);
            IEnumerable<UnitCentr> res = null;
            while (stack.Count() != 0)
            {
                var T = stack.Pop();
                if (T.Item2.Count() == 0)
                {
                    var s = T.Item1.Sum(unit => unit.Manheton(this));
                    if (s == min_sum)
                        return T.Item1;

                    if ((s < sum) && isntEqw(claster, T.Item1))
                    {
                        sum = s;
                        res = T.Item1;
                    }
                }
                else
                    foreach (var unit in T.Item2.First().MakeStep(this, T.Item1, claster, b))
                        stack.Push(new Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>>(T.Item1.Concat(new List<UnitCentr> { unit }), T.Item2.Skip(1)));
            }
            return res;
        }
        private bool isntEqw(IEnumerable<UnitCentr> units1, IEnumerable<UnitCentr> units2)
        {
            var sort1 = units1.OrderBy(unit => unit.id).ToList();
            var sort2 = units2.OrderBy(unit => unit.id).ToList();
            for (int i = 0; i < sort1.Count(); i++)
                if ((sort1[i].x != sort2[i].x) || (sort1[i].y != sort2[i].y))
                {
                    if (sort1[i].isRealEnd && !sort2[i].isRealEnd)
                    {
                        TunellCentr T = Tunell(sort1[i].x, sort1[i].y);
                        if (!(T is null) && !T.Contains(true, sort1[i].id))
                            return true;
                    }
                    else
                        return true;
                }
            return false;
        }

    }
}
