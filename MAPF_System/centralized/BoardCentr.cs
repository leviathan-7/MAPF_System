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
    public class BoardCentr : Board<UnitCentr, int>, BoardInterface
    {
        private bool AreNotTunells;

        public BoardCentr(int X, int Y, int Blocks, int N_Units)
        {
            rnd = new Random();
            SampleConstructor(X, Y, new Cell<int>[X, Y], new List<UnitCentr>(), "", new List<Tunell<int>>());
            int x = rnd.Next(X);
            int y = rnd.Next(Y);
            // Генерация пустых узлов
            GenerationEmthy(X, Y, Blocks, rnd, x, y);
            // Генерация юнитов
            int id = 0;
            while (N_Units > 0)
            {
                x = rnd.Next(X);
                y = rnd.Next(Y);
                int x_Purpose = rnd.Next(X);
                int y_Purpose = rnd.Next(Y);
                bool b = (Arr[x, y] != null) && (Arr[x_Purpose, y_Purpose] != null) && !((x == x_Purpose) && (y == y_Purpose));
                foreach (var Unit in units)
                    b = b && !((Unit.x == x) && (Unit.y == y)) && !((Unit.x_Purpose == x_Purpose) && (Unit.y_Purpose == y_Purpose))
                        && !((Unit.x == x_Purpose) && (Unit.y == y_Purpose)) && !((Unit.x_Purpose == x) && (Unit.y_Purpose == y));
                if (b)
                {
                    units.Add(new UnitCentr(new int[X, Y], x, y, x_Purpose, y_Purpose, id, -1, -1, X, Y));
                    N_Units--;
                    id++;
                }
            }
            // Генерация препятствий
            GenerationBlocks(X, Y);
        }
        public BoardCentr(int X, int Y, Cell<int>[,] Arr, List<UnitCentr> units, string name, List<Tunell<int>> tunells)
        {
            SampleConstructor(X, Y, Arr, units, name, tunells);
        }
        public BoardCentr()
        {
            // Открытие файла в формате board
            OpenFileDialog openFileDialog1 = new OpenFileDialog() { Filter = "(*.board)|*.board", };
            openFileDialog1.ShowDialog();
            // Проверка на то, что board файл был выбран
            if (openFileDialog1.FileName == "")
                return;
            Constructor(openFileDialog1.FileName);
        }
        public BoardCentr(string path) { Constructor(path); }
        public BoardInterface CopyWithoutBlocks()
        {
            return new BoardCentr(X, Y, copyArrWithoutBlocks, units.Select(unit => unit.Copy(true)).ToList(), name, tunells);
        }
        public void MakeStep(BoardInterface Board, int kol_iter_a_star)
        {
            // Добавить блоки в пределах видимости юнитов
            foreach (var Unit in units)
                MakeBlocks(Board, Unit);
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
                                kk += TunellAndNoEmpthy(i - 1, j);
                                kk += TunellAndNoEmpthy(i + 1, j);
                                kk += TunellAndNoEmpthy(i, j - 1);
                                kk += TunellAndNoEmpthy(i, j + 1);

                                if (!AreNotTunells && kk == 3)
                                {
                                    List<Tunell<int>> LT = new List<Tunell<int>>();
                                    LT_ADD(LT, i - 1, j);
                                    LT_ADD(LT, i + 1, j);
                                    LT_ADD(LT, i, j - 1);
                                    LT_ADD(LT, i, j + 1);

                                    var T = new TunellCentr(this);
                                    T.Add(LT);
                                    Arr[i, j].tunell = T;
                                    tunells.Add(T);
                                    T.Add(i, j);
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
            foreach (var Claster in Clasterization(units))
                new_units.AddRange(NewUnits(Claster));
            units = new_units;

        }
        public TunellCentr Tunell(int x, int y) 
        { 
            return (TunellCentr)Arr[x, y].tunell; 
        }
        public bool InTunell(int unit_id, Tunell<int> tunell) 
        {
            return InTunell(units.Find(u => u.id == unit_id), tunell);
        }
        public void PlusUnit()
        {
            if ((countBlocks + 2 * units.Count + 2) >= (X * Y))
            {
                SystemSounds.Beep.Play();
                return;
            }
            rnd = new Random();
            while (true)
            {
                int x = rnd.Next(X);
                int y = rnd.Next(Y);
                int x_Purpose = rnd.Next(X);
                int y_Purpose = rnd.Next(Y);
                bool b = !Arr[x, y].isBlock && !Arr[x_Purpose, y_Purpose].isBlock && (Arr[x, y] != null) && (Arr[x_Purpose, y_Purpose] != null) && !((x == x_Purpose) && (y == y_Purpose));
                foreach (var Unit in units)
                    b = b && !((Unit.x == x) && (Unit.y == y)) && !((Unit.x_Purpose == x_Purpose) && (Unit.y_Purpose == y_Purpose))
                        && !((Unit.x == x_Purpose) && (Unit.y == y_Purpose)) && !((Unit.x_Purpose == x) && (Unit.y_Purpose == y));
                if (b)
                {
                    units.Add(new UnitCentr(new int[X, Y], x, y, x_Purpose, y_Purpose, units.Count, -1, -1, X, Y));
                    return;
                }
            }
        }

        private void Constructor(string path)
        {
            try
            {
                Tuple<int, string[]> tuple = ConstructParams(path);
                int t = 1;
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                    {
                        Arr[i, j] = new Cell<int>(tuple.Item2[t]);
                        t++;
                    }
                // Создать юнитов по данным файла
                units = new List<UnitCentr>();
                for (int i = 0; i < tuple.Item1; i++)
                {
                    units.Add(new UnitCentr(tuple.Item2[t], i, X, Y));
                    t++;
                }
                tunells = new List<Tunell<int>>();
            }
            catch (Exception)
            {
                MessageBox.Show("Файл повреждён.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LT_ADD(List<Tunell<int>> LT, int i, int j)
        {
            if (IsTunell(i, j))
                LT.Add((TunellCentr)Arr[i, j].tunell);
        }
        private List<HashSet<UnitCentr>> Clasterization(List<UnitCentr> units)
        {
            HashSet<UnitCentr> clasterizations = new HashSet<UnitCentr>();
            List<HashSet<UnitCentr>> clasters = new List<HashSet<UnitCentr>>();
            foreach (var unit in units)
                if (!clasterizations.Contains(unit))
                {
                    HashSet<UnitCentr> claster = unit.FindClaster(units);
                    clasters.Add(claster);
                    clasterizations.UnionWith(claster);
                }
            return clasters;
        }
        private IEnumerable<UnitCentr> NewUnits(IEnumerable<UnitCentr> claster)
        {
            Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>> TT = new Tuple<IEnumerable<UnitCentr>, IEnumerable<UnitCentr>>(new List<UnitCentr>(), claster);
            int min_sum = claster.Sum(unit => unit.realManheton) - claster.Count(unit => !unit.isRealEnd);
            IEnumerable<UnitCentr> res = NewUnitsStack(TT, min_sum, claster, true);
            if (!(res is null))
                return res;
            return NewUnitsStack(TT, min_sum, claster, false);
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
