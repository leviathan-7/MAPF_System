using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MAPF_System
{
    public class UnitDec : Unit
    {
        private int last__x;
        private int last__y;
        private bool was_step;
        private int spec;
        public bool was_near_end { get; private set; }
        public bool was_bool_step { get; private set; }
        public float F { get; private set; }
        public new UnitDec copy
        {
            get { return new UnitDec(x, y, x_Purpose, y_Purpose, id, last__x, last__y, X_Board, Y_Board, was_step, flag); }
        }

        public UnitDec(int x, int y, int x_Purpose, int y_Purpose, int id, int last__x, int last__y, int X, int Y, bool was_step = false, bool flag = false)
            : base(x, y, X, Y, id, flag, x_Purpose, y_Purpose) 
        {
            this.was_step = was_step;
            MakeLast(last__x, last__y);
            Arr = new int[X, Y];
        }
        public UnitDec(string str, int i, int X, int Y) : base(str, X, Y, i)
        {
            MakeLast(-1, -1);
        }
        public void NotWasStep() { was_step = false; }
        public void MakeStep(BoardDec Board, IEnumerable<Unit> AnotherUnits, int kol_iter_a_star)
        {
            bool lasttrue = isEnd;
            // Обнуление флага, когда юнит прошел через свою цель
            if (flag && (x == x_Purpose) && (y == y_Purpose))
                flag = false;

            if (!StartStep())
                return;

            // Алгоритм для решения проблемы перпендикулярного хождения юнитов
            if (!(last_Unit is null) && was_near_end && !flag && ((UnitDec)last_Unit).isEnd)
                MakeLast(-1, -1);
            last_Unit = null;

            MakeMinStep(Board, AnotherUnits, kol_iter_a_star, new List<int> { 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0 }, -1, -1, false, lasttrue, null);
        }

        private bool MakeBoolStep(BoardDec Board, IEnumerable<Unit> AnotherUnits, int xx, int yy, int kol_iter_a_star, bool signal, UnitDec AU)
        {
            bool lasttrue = isEnd;
            if (!StartStep())
                return false;

            // Проверяем, надо ли ставить флаг того, что 2 юнита оказались в тупике и им надо на места друг-друга
            int[] _x = { -1, 1, 0, 0 }, _y = { 0, 0, -1, 1 };
            int t = Enumerable.Range(0, 4).Sum(w => Board.IsEmpthyAndNoTunel(x + _x[w], y + _y[w]) ? 0 : 1);

            if ((RealManheton(x, y) == 1) && (t >= 2))
                flag = signal;
            if (flag)
            {
                AU.flag = true;
                was_near_end = true;
            }
            else // Случай, когда юнит не даёт проехать в тунеле другому и ему надо выехать из тунеля, но при этом случай не соответсвует случаю, когда двум юнитам надо на место друг-друга
            {
                if (t >= 3)
                    flag = signal;
                if (flag)
                    was_near_end = true;
            }
            
            return MakeMinStep(Board, AnotherUnits, kol_iter_a_star, new List<int> { x, x, x - 1, x + 1 }, new List<int> { y - 1, y + 1, y, y }, xx, yy, true, lasttrue, AU);
        }
        private bool MakeMinStep(BoardDec Board, IEnumerable<Unit> AnotherUnits, int kol_iter_a_star, List<int> a, List<int> b, int xx, int yy, bool is_bool_step, bool lasttrue, Unit AU)
        {
            // 1) Находим подходящую нам клетку

            // Помечаем клетку как посещенную
            Board.MakeVisit(x, y, id);

            // Список значений эвристической функции для каждой клетки
            List<float> ff = new List<float> { -1, -1, -1, -1, -1 };
            // Список значений расстояний для каждой клетки
            List<float> rr = new List<float> { -1, -1, -1, -1, -1 };
            // Список юнитов для каждой клетки
            List<Unit> UsUnits = new List<Unit> { null, null, null, null, null };

            // Заполняем значения ff и UsUnits
            int[] _xx = { 0, 0, -1, 1 }, _yy = { -1, 1, 0, 0 };
            Parallel.For(0, 4, (i) =>
            {
                int x0 = x + _xx[i], y0 = y + _yy[i];
                if (Board.IsEmpthy(x0, y0) && (!((last__x == x0) && (last__y == y0)) || is_bool_step))
                {
                    float[,,,] ArrG = new float[X_Board, Y_Board, X_Board, Y_Board];
                    int MaxG = int.MaxValue;
                    bool GreatFlag = false;
                    ff[i] = f(x0, y0, Board, kol_iter_a_star, x, y, is_bool_step, 1, ref ArrG, ref MaxG, ref GreatFlag);
                    // Добавляем коэффицент на стоимость вершины в виде количества её посещений данным юнитом
                    if (!was_near_end && (ff[i] != 0))
                        ff[i] += Arr[x0, y0];
                    rr[i] = (float)Math.Sqrt(Math.Pow(x_Purpose - x0, 2) + Math.Pow(y_Purpose - y0, 2));
                    // Алгоритм для решения проблемы параллельного хождения
                    if (was_near_end)
                        foreach (var au in AnotherUnits)
                            if ((au.x_Purpose == x0) && (au.y_Purpose == y0))
                            {
                                ff[i] += 0.5f;
                                if (!au.isEnd)
                                    ff[i] += 0.5f;
                            }
                    foreach (var au in AnotherUnits)
                        if ((au.x == x0) && (au.y == y0))
                        {
                            UsUnits[i] = au;
                            return;
                        }
                }
            });

            // Находим подходящую нам клетку
            ff[4] = int.MaxValue;
            float min = ff[4];
            float minr = ff[4];
            int min_i = 4;
            for (int i = 0; i < 4; i++)
            {
                if (((min > ff[i]) || ((min == ff[i]) && (minr > rr[i])) || ((minr == rr[i]) && (min == ff[i]) && (UsUnits[i] is null))) && (ff[i] != -1) && !((xx == a[i]) && (yy == b[i])))
                {
                    if ((UsUnits[i] is null) || (!(UsUnits[i] is null) && !(UsUnits[i] as UnitDec).was_step))
                    {
                        min = ff[i];
                        minr = rr[i];
                        min_i = i;
                    }
                }
            }

            bool bb = min_i != 4;
            was_step = true;
            if (!(UsUnits[min_i] is null))
                bb = (UsUnits[min_i] as UnitDec).MakeBoolStep(Board, from u in Board.units where u != UsUnits[min_i] select u, x, y, kol_iter_a_star, min == 0, this);
            int min_i_1 = min_i;
            if (!bb)
            {
                min = ff[4];
                minr = ff[4];
                min_i = 4;
                for (int i = 0; i < 4; i++)
                    if (((min > ff[i]) || ((min == ff[i]) && (minr > rr[i])) || ((minr == rr[i]) && (min == ff[i]) && (UsUnits[i] is null))) && (ff[i] != -1) && (min_i_1 != i) && !((xx == a[i]) && (yy == b[i])))
                    {
                        if ((UsUnits[i] is null) || (!(UsUnits[i] is null) && !(UsUnits[i] as UnitDec).was_step))
                        {
                            min = ff[i];
                            minr = rr[i];
                            min_i = i;
                        }
                    }
                bb = min_i != 4;
                if (!(UsUnits[min_i] is null))
                    bb = (UsUnits[min_i] as UnitDec).MakeBoolStep(Board, from u in Board.units where u != UsUnits[min_i] select u, x, y, kol_iter_a_star, min == 0, this);
            }
            int min_i_2 = min_i;
            if (!bb)
            {
                min = ff[4];
                minr = ff[4];
                min_i = 4;
                for (int i = 0; i < 4; i++)
                    if (((min > ff[i]) || ((min == ff[i]) && (minr > rr[i])) || ((minr == rr[i]) && (min == ff[i]) && (UsUnits[i] is null))) && (ff[i] != -1) && (min_i_1 != i) && (min_i_2 != i) && !((xx == a[i]) && (yy == b[i])))
                    {
                        if ((UsUnits[i] is null) || (!(UsUnits[i] is null) && !(UsUnits[i] as UnitDec).was_step))
                        {
                            min = ff[i];
                            minr = rr[i];
                            min_i = i;
                        }
                    }
                bb = min_i != 4;
                if (!(UsUnits[min_i] is null))
                    bb = (UsUnits[min_i] as UnitDec).MakeBoolStep(Board, from u in Board.units where u != UsUnits[min_i] select u, x, y, kol_iter_a_star, min == 0, this);
            }
            int min_i_3 = min_i;
            if (!bb)
            {
                min = ff[4];
                minr = ff[4];
                min_i = 4;
                for (int i = 0; i < 4; i++)
                    if (((min > ff[i]) || ((min == ff[i]) && (minr > rr[i])) || ((minr == rr[i]) && (min == ff[i]) && (UsUnits[i] is null))) && (ff[i] != -1) && (min_i_1 != i) && (min_i_2 != i) && (min_i_3 != i) && !((xx == a[i]) && (yy == b[i])))
                    {
                        if ((UsUnits[i] is null) || (!(UsUnits[i] is null) && !(UsUnits[i] as UnitDec).was_step))
                        {
                            min = ff[i];
                            minr = rr[i];
                            min_i = i;
                        }
                    }
                bb = min_i != 4;
                if (!(UsUnits[min_i] is null))
                    bb = (UsUnits[min_i] as UnitDec).MakeBoolStep(Board, from u in Board.units where u != UsUnits[min_i] select u, x, y, kol_iter_a_star, min == 0, this);
            }

            // Возвращаем флаг -10, если юнит никуда сдвинуться не сможет
            if (!bb)
            {
                MakeLast(-1, -1);
                return was_step = false;
            }
            else
            {
                // 2) если юнит сдвинулся
                F = min;
                was_bool_step = is_bool_step;
                if (lasttrue)
                    MakeLast(-1, -1);
                else
                    MakeLast(x, y);

                switch (min_i)
                {
                    case 0: y--; break;
                    case 1: y++; break;
                    case 2: x--; break;
                    case 3: x++; break;
                    default: break;
                }

                if (min_i != 4)
                {
                    if (is_bool_step)
                        last_Unit = AU;

                    // Алгоритм для решения проблемы перпендикулярного хождения юнитов
                    if (!(last_Unit is null) && was_near_end && !flag && ((UnitDec)last_Unit).isEnd)
                    {
                        MakeLast(-1, -1);
                        last_Unit = null;
                    }

                    if (!is_bool_step && isRealEnd)
                    {
                        was_near_end = true;
                        spec = X_Board * Y_Board;
                    }
                    // Запоминаем пройденный путь
                    if (!was_near_end)
                        Arr[x, y] += 4;
                }
                return true;
            }
        }
        private float f(int x, int y, BoardDec Board, int kol_iter_a_star, int last_x, int last_y, bool is_bool_step, int g, ref float[,,,] ArrG, ref int MaxG, ref bool GreatFlag)
        {
            if ((g > MaxG) || GreatFlag)
                return int.MaxValue / 2;
            // Стоимость нулевая, если юнит достиг цели
            if ((x == x_Purpose) && (y == y_Purpose))
            {
                MaxG = g;
                return 0;
            }
            if ((ArrG[x, y, last_x, last_y] != 0) && (g > ArrG[x, y, last_x, last_y]))
                return int.MaxValue / 2;
            ArrG[x, y, last_x, last_y] = g;

            // Случай, когда узел плохой
            if (Board.IsBadCell(x, y))
            {
                // В случае "выталкивания", плохой случай наоборот считается хорошим
                if (is_bool_step && !Board.IsTunell(last_x, last_y))
                {
                    GreatFlag = true;
                    return 1;
                }
                return int.MaxValue / 2;
            }
            // Случай, когда простой туннель
            if (Board.IsTunell(x, y))
            {
                int ID = (Board.Tunell(x, y) as TunellDec).id;
                if (ID == id)
                {
                    if (g == Math.Abs(x - this.x) + Math.Abs(y - this.y))
                        GreatFlag = true;
                    return 1;
                }
                if (!(ID == -1))
                    return int.MaxValue / 2;
            }
            // Если глубина не достигнута, тогда рассматриваем клетки, в которые можем попасть
            if (kol_iter_a_star != 0)
            {
                kol_iter_a_star--;
                // Список значений эвристической функции для каждой клетки
                List<float> ff = new List<float> { -1, -1, -1, -1, -1 };
                int[] xx = { 0, 0, -1, 1 }, yy = { -1, 1, 0, 0 };
                for (int w = 0; w < 4; w++)
                {
                    int newI = x + xx[w], newJ = y + yy[w];
                    if (Board.IsEmpthy(newI, newJ) && !((last_x == newI) && (last_y == newJ)))
                        ff[w] = f(newI, newJ, Board, kol_iter_a_star, x, y, false, g + 1, ref ArrG, ref MaxG, ref GreatFlag);
                    if (ff[w] == 0)
                        return 1;
                }
                ff[4] = int.MaxValue / 2;
                // Находим клетку с минимальным значением эвристической функции
                float min = ff[4];
                for (int i = 0; i < 4; i++)
                    if ((min >= ff[i]) && (ff[i] != -1))
                        min = ff[i];
                return 1 + min;
            }
            if (RealManheton(x, y) + g == Math.Abs(x_Purpose - this.x) + Math.Abs(y_Purpose - this.y))
                GreatFlag = true;
            // Считаем эвристическую оценку, если максимальная глубина достигнута
            return RealManheton(x, y);
        }
        private bool StartStep()
        {
            // Проверяем, что юнит еще не работал на данной итерации
            if (was_step)
                return false;
            if (was_near_end = (spec > 0))
                spec--;
            return true;
        }
        private void MakeLast(int x, int y)
        {
            last__x = x;
            last__y = y;
        }
    
    }
}
