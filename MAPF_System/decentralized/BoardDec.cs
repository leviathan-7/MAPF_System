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
    public class BoardDec : Board<UnitDec, Unit>
    {
        public BoardDec(int X, int Y, int Blocks, int N_Units)
        {
            Generation(X, Y, Blocks, N_Units, false);
        }
        public BoardDec(int X, int Y, Cell<UnitDec, Unit>[,] Arr, List<UnitDec> units, string name, List<Tunell<UnitDec, Unit>> tunells)
        {
            SampleConstructor(X, Y, Arr, units, name, tunells);
        }
        public BoardDec(){ Constructor(false); }
        public BoardDec(string path) { Constructor(path, false); }
        
        public void MakeStep(BoardDec Board, int kol_iter_a_star)
        {
            // Обнуление значений was_step
            foreach (var Unit in units)
                Unit.NotWasStep();
            // Добавить блоки в пределах видимости юнитов
            foreach (var Unit in units)
                MakeBlocks(Board, Unit);
            // Добавить плохие узлы
            while (true)
            {
                int k = 0;
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                        if (!(IsBad(i, j) || Arr[i, j].isBlock))
                        {
                            // Проверка на отсутсвие целей
                            bool b = true;
                            foreach (var Unit in units)
                                b = b && !((Unit.x_Purpose == i) && (Unit.y_Purpose == j)) && !((Unit.x == i) && (Unit.y == j));
                            if (b)
                            {
                                int kk = 0;
                                kk += BadAndNoEmpthy(i - 1, j);
                                kk += BadAndNoEmpthy(i + 1, j);
                                kk += BadAndNoEmpthy(i, j - 1);
                                kk += BadAndNoEmpthy(i, j + 1);

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
                            kk += TunellAndNoEmpthy(i - 1, j);
                            kk += TunellAndNoEmpthy(i + 1, j);
                            kk += TunellAndNoEmpthy(i, j - 1);
                            kk += TunellAndNoEmpthy(i, j + 1);

                            if (kk == 3)
                            {
                                List<Tunell<UnitDec, Unit>> LT = new List<Tunell<UnitDec, Unit>>();
                                LT_ADD(LT, i - 1, j);
                                LT_ADD(LT, i + 1, j);
                                LT_ADD(LT, i, j - 1);
                                LT_ADD(LT, i, j + 1);

                                var T = new TunellDec(this);
                                T.Add(LT);
                                Arr[i, j].tunell = T;
                                tunells.Add(T);
                                T.Add(i, j);
                            }
                        }

                if (k == 0)
                    break;
            }
            // Поставить флаги юнитам, которые в простом туннеле перегораживают проезд
            foreach (var t in tunells)
                ((TunellDec)t).MakeFlags(this);
            // Сделать шаг теми юнитами, которые еще не достигли своей цели, при этом давая приоритет тем юнитам, которые дальше от цели
            List<UnitDec> Was_bool_step_units = new List<UnitDec>();
            List<UnitDec> Was_near_end_units = new List<UnitDec>();
            List<UnitDec> NOT_Was_near_end_units = new List<UnitDec>();
            List<UnitDec> Tunell_NOT_Was_near_end_units = new List<UnitDec>();

            foreach (var Unit in units)
                if (Unit.was_near_end)
                {
                    if (Unit.was_bool_step)
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


            foreach (var Unit in NOT_Was_near_end_units.OrderBy(u => -u.F))
                if (!Unit.isEnd)
                    Unit.MakeStep(this, from u in units where u != Unit select u, kol_iter_a_star);
            foreach (var Unit in Was_near_end_units.OrderBy(u => -u.F))
                if (!Unit.isEnd)
                    Unit.MakeStep(this, from u in units where u != Unit select u, kol_iter_a_star);
            foreach (var Unit in Tunell_NOT_Was_near_end_units.OrderBy(u => -u.F))
                if (!Unit.isEnd)
                    Unit.MakeStep(this, from u in units where u != Unit select u, kol_iter_a_star);
            foreach (var Unit in Was_bool_step_units.OrderBy(u => -u.F))
                if (!Unit.isEnd)
                    Unit.MakeStep(this, from u in units where u != Unit select u, kol_iter_a_star);

        }
       
        public bool IsEmpthyAndNoTunel(int x, int y)
        {
            // Проверка на выход за пределы поля
            if ((x < 0) || (y < 0) || (x >= X) || (y >= Y))
                return false;
            if (Arr[x, y].isTunell)
                return false;
            return !Arr[x, y].isBlock;
        }
        public bool IsBadCell(int x, int y) { return Arr[x, y].isBad; }
        public void MakeVisit(int x, int y, int id) { Arr[x, y].MakeVisit(id); }
        public int TunellId(int x, int y) { return ((TunellDec)Arr[x, y].tunell).id; }

        private bool IsBad(int x, int y)
        {
            // Проверка на выход за пределы поля
            if ((x < 0) || (y < 0) || (x >= X) || (y >= Y))
                return false;
            return Arr[x, y].isBad;
        }
        private void LT_ADD(List<Tunell<UnitDec, Unit>> LT, int i, int j)
        {
            if (IsTunell(i, j) && !IsBad(i, j))
                LT.Add((TunellDec)Arr[i, j].tunell);
        }
        private int BadAndNoEmpthy(int i, int j)
        {
            if (!IsEmpthy(i, j) || IsBad(i, j))
                return 1;
            return 0;
        }

    }
}
