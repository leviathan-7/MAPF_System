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

namespace MAPF_System
{
    public class BoardDec : Board
    {
        public BoardDec(int X, int Y, int Blocks, int N_Units) : base(X, Y, Blocks, N_Units) { }
        public BoardDec(int X, int Y, Cell[,] Arr, List<Unit> units, string name, List<Tunell> tunells)
            : base(X, Y, Arr, units, name, tunells) { }
        public BoardDec(string path = null) : base(path) { }

        public void MakeStep(int kol_iter_a_star)
        {
            // Обнуление значений was_step
            units.ForEach(Unit => (Unit as UnitDec).NotWasStep());
            // Добавить плохие узлы
            int[] xx = { -1, 1, 0, 0 }, yy = { 0, 0, -1, 1 };
            IEnumerable<int> range = Enumerable.Range(0, 4);
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    if ((!(Arr[i, j].isBad || Arr[i, j].isBlock)) 
                        && units.All(Unit => !((Unit.x_Purpose == i) && (Unit.y_Purpose == j)) && !((Unit.x == i) && (Unit.y == j)))
                        && (range.Sum(w => (!IsEmpthy(i + xx[w], j + yy[w]) || Arr[i + xx[w], j + yy[w]].isBad) ? 1 : 0) == 3))
                    {
                        Arr[i, j].MakeBad();
                    }
            // Добавить узлы -- части туннелей
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    if ((!(IsTunell(i, j) || Arr[i, j].isBlock)) && (range.Sum(w => (!IsEmpthy(i + xx[w], j + yy[w]) || IsTunell(i + xx[w], j + yy[w])) ? 1 : 0) == 3))
                        tunells.Add(Arr[i, j].tunell = new TunellDec(this, range.Where(w => IsTunell(i + xx[w], j + yy[w]) && !Arr[i + xx[w], j + yy[w]].isBad).Select(w => Arr[i + xx[w], j + yy[w]].tunell).ToList(), i, j));
            // Поставить флаги юнитам, которые в простом туннеле перегораживают проезд
            tunells.ForEach(t => ((TunellDec)t).MakeFlags(this));
            // Сделать шаг теми юнитами, которые еще не достигли своей цели, при этом давая приоритет тем юнитам, которые дальше от цели
            List<Unit> Was_bool_step_units = new List<Unit>(), Was_near_end_units = new List<Unit>(),
            NOT_Was_near_end_units = new List<Unit>(), Tunell_NOT_Was_near_end_units = new List<Unit>();

            units.ForEach(Unit => 
            {
                if ((Unit as UnitDec).was_near_end)
                    ((Unit as UnitDec).was_bool_step ? Was_bool_step_units : Was_near_end_units).Add(Unit);
                else
                    (IsTunell(Unit.x, Unit.y) ? Tunell_NOT_Was_near_end_units : NOT_Was_near_end_units).Add(Unit);
            });

            new List<List<Unit>>() { NOT_Was_near_end_units, Was_near_end_units, Tunell_NOT_Was_near_end_units, Was_bool_step_units }.ForEach(list =>
            {
                foreach (var Unit in list.OrderBy(u => -(u as UnitDec).F).Where(u => !u.isEnd))
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
