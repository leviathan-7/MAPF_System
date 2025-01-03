﻿using System;
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
        public UnitDec copy
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
        public void MakeStep(BoardDec Board, IEnumerable<UnitDec> AnotherUnits, int kol_iter_a_star)
        {
            bool lasttrue = isEnd;
            // Обнуление флага, когда юнит прошел через свою цель
            if (flag && (x == x_Purpose) && (y == y_Purpose))
                flag = false;
            // Проверяем, что юнит еще не работал на данной итерации
            if (was_step)
                return;
            StartSpec();
            // Алгоритм для решения проблемы перпендикулярного хождения юнитов
            if (!(last_Unit is null) && was_near_end && !flag && ((UnitDec)last_Unit).isEnd)
                MakeLast(-1, -1);
            last_Unit = null;
            // Список значений эвристической функции для каждой клетки
            List<float> ff = new List<float> { -1, -1, -1, -1, -1 };
            // Список значений расстояний для каждой клетки
            List<float> rr = new List<float> { -1, -1, -1, -1, -1 };
            // Список юнитов для каждой клетки
            List<UnitDec> UsUnits = new List<UnitDec> { null, null, null, null, null };
            // Заполняем значения ff и UsUnits
            IfBoardIsEmpthy(rr, ff, Board, UsUnits, AnotherUnits, kol_iter_a_star);
            // Помечаем старую клетку как посещенную
            Board.MakeVisit(x, y, id);
            // Находим подходящую нам клетку
            var T = MIN_I(rr, ff, Board, UsUnits, new List<int> { 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0 }, -1, -1, kol_iter_a_star);
            F = T.Item2;
            was_step = (T.Item1 != -10);
            if (was_step)
            {
                was_bool_step = false;
                InWasStep(T, lasttrue);
                if (T.Item1 != 4)
                {
                    // Алгоритм для решения проблемы перпендикулярного хождения юнитов
                    if (!(last_Unit is null) && was_near_end && !flag && ((UnitDec)last_Unit).isEnd)
                    {
                        last__x = -1;
                        last__y = -1;
                        last_Unit = null;
                    }
                    if (isRealEnd)
                    {
                        was_near_end = true;
                        spec = X_Board * Y_Board;
                    }
                    // Помечаем клетку как посещенную
                    Board.MakeVisit(x, y, id);
                    if (!was_near_end)
                        Arr[x, y] += 4;
                    return;
                }
            }
            else
                MakeLast(-1, -1);
        }

        private bool MakeStep(BoardDec Board, IEnumerable<UnitDec> AnotherUnits, int xx, int yy, int kol_iter_a_star, bool signal, UnitDec AU)
        {
            bool lasttrue = isEnd;
            // Проверяем, что юнит еще не работал на данной итерации
            if (was_step)
                return false;
            StartSpec();
            // Проверяем, надо ли ставить флаг того, что 2 юнита оказались в тупике и им надо на места друг-друга
            int t = 0;
            if (!Board.IsEmpthyAndNoTunel(x, y - 1))
                t++;
            if (!Board.IsEmpthyAndNoTunel(x, y + 1))
                t++;
            if (!Board.IsEmpthyAndNoTunel(x - 1, y))
                t++;
            if (!Board.IsEmpthyAndNoTunel(x + 1, y))
                t++;
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
            // Список значений эвристической функции для каждой клетки
            List<float> ff = new List<float> { -1, -1, -1, -1, -1 };
            // Список значений расстояний для каждой клетки
            List<float> rr = new List<float> { -1, -1, -1, -1, -1 };
            // Список юнитов для каждой клетки
            List<UnitDec> UsUnits = new List<UnitDec> { null, null, null, null, null };
            // Заполняем значения ff и UsUnits
            IfBoardIsEmpthy(rr, ff, Board, UsUnits, AnotherUnits, kol_iter_a_star, true);
            // Находим подходящую нам клетку
            var T = MIN_I(rr, ff, Board, UsUnits, new List<int> { x, x, x - 1, x + 1 }, new List<int> { y - 1, y + 1, y, y }, xx, yy, kol_iter_a_star);
            F = T.Item2;
            was_step = (T.Item1 != -10);
            if (was_step)
            {
                was_bool_step = true;
                InWasStep(T, lasttrue);
                if (T.Item1 != 4)
                {
                    // Алгоритм для решения проблемы перпендикулярного хождения юнитов
                    var q = AU;
                    if (!(last_Unit is null) && was_near_end && !flag && ((UnitDec)last_Unit).isEnd)
                    {
                        MakeLast(-1, -1);
                        q = null;
                    }
                    last_Unit = q;
                    // Помечаем клетку как посещенную
                    Board.MakeVisit(x, y, id);
                    if (!was_near_end)
                        Arr[x, y] += 4;
                    return true;
                }
            }
            else
                MakeLast(-1, -1);

            return was_step;
        }
        private void StartSpec()
        {
            if (was_near_end)
                was_near_end = false;
            if (spec > 0)
            {
                spec--;
                was_near_end = true;
            }
        }
        private Tuple<int, float> MIN_I(List<float> rr, List<float> ff, BoardDec Board, List<UnitDec> UsUnits, List<int> a, List<int> b, int xx, int yy, int kol_iter_a_star)
        {
            ff[4] = int.MaxValue;
            float min = ff[4];
            float minr = ff[4];
            int min_i = 4;
            for (int i = 0; i < 4; i++)
            {
                if (((min > ff[i]) || ((min == ff[i]) && (minr > rr[i])) || ((minr == rr[i]) && (min == ff[i]) && (UsUnits[i] is null))) && (ff[i] != -1) && !((xx == a[i]) && (yy == b[i])))
                {
                    if ((UsUnits[i] is null) || (!(UsUnits[i] is null) && !UsUnits[i].was_step))
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
                bb = UsUnits[min_i].MakeStep(Board, from u in Board.units where u != UsUnits[min_i] select u, x, y, kol_iter_a_star, min == 0, this);
            int min_i_1 = min_i;
            if (!bb)
            {
                min = ff[4];
                minr = ff[4];
                min_i = 4;
                for (int i = 0; i < 4; i++)
                    if (((min > ff[i]) || ((min == ff[i]) && (minr > rr[i])) || ((minr == rr[i]) && (min == ff[i]) && (UsUnits[i] is null))) && (ff[i] != -1) && (min_i_1 != i) && !((xx == a[i]) && (yy == b[i])))
                    {
                        if ((UsUnits[i] is null) || (!(UsUnits[i] is null) && !UsUnits[i].was_step))
                        {
                            min = ff[i];
                            minr = rr[i];
                            min_i = i;
                        }
                    }
                bb = min_i != 4;
                if (!(UsUnits[min_i] is null))
                    bb = UsUnits[min_i].MakeStep(Board, from u in Board.units where u != UsUnits[min_i] select u, x, y, kol_iter_a_star, min == 0, this);
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
                        if ((UsUnits[i] is null) || (!(UsUnits[i] is null) && !UsUnits[i].was_step))
                        {
                            min = ff[i];
                            minr = rr[i];
                            min_i = i;
                        }
                    }
                bb = min_i != 4;
                if (!(UsUnits[min_i] is null))
                    bb = UsUnits[min_i].MakeStep(Board, from u in Board.units where u != UsUnits[min_i] select u, x, y, kol_iter_a_star, min == 0, this);
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
                        if ((UsUnits[i] is null) || (!(UsUnits[i] is null) && !UsUnits[i].was_step))
                        {
                            min = ff[i];
                            minr = rr[i];
                            min_i = i;
                        }
                    }
                bb = min_i != 4;
                if (!(UsUnits[min_i] is null))
                    bb = UsUnits[min_i].MakeStep(Board, from u in Board.units where u != UsUnits[min_i] select u, x, y, kol_iter_a_star, min == 0, this);
            }

            // Возвращаем флаг -10, если юнит никуда сдвинуться не сможет
            if (!bb)
                return new Tuple<int, float>(-10, F);
            
            // Возвращаем подходящую нам клетку
            return new Tuple<int, float>(min_i, min);
        }
        private void IfBoardIsEmpthy(List<float> rr, List<float> ff, BoardDec Board, List<UnitDec> UsUnits, IEnumerable<UnitDec> AnotherUnits, int kol_iter_a_star, bool is_bool_step = false)
        {
            Parallel.For(0, 4, (i) => 
            {
                if ((i == 0) && Board.IsEmpthy(x, y - 1) && (!((last__x == x) && (last__y == y - 1)) || is_bool_step))
                    GetUnitAndF(0, rr, ff, UsUnits, x, y - 1, x, y, Board, kol_iter_a_star, AnotherUnits, is_bool_step);
                if ((i == 1) && Board.IsEmpthy(x, y + 1) && (!((last__x == x) && (last__y == y + 1)) || is_bool_step))
                    GetUnitAndF(1, rr, ff, UsUnits, x, y + 1, x, y, Board, kol_iter_a_star, AnotherUnits, is_bool_step);
                if ((i == 2) && Board.IsEmpthy(x - 1, y) && (!((last__x == x - 1) && (last__y == y)) || is_bool_step))
                    GetUnitAndF(2, rr, ff, UsUnits, x - 1, y, x, y, Board, kol_iter_a_star, AnotherUnits, is_bool_step);
                if ((i == 3) && Board.IsEmpthy(x + 1, y) && (!((last__x == x + 1) && (last__y == y)) || is_bool_step))
                    GetUnitAndF(3, rr, ff, UsUnits, x + 1, y, x, y, Board, kol_iter_a_star, AnotherUnits, is_bool_step);
            });
            
        }
        private void GetUnitAndF(int i, List<float> rr, List<float> ff, List<UnitDec> UsUnits, int x0, int y0, int x, int y, BoardDec Board, int kol_iter_a_star, IEnumerable<UnitDec> AnotherUnits, bool is_bool_step)
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
        private void InWasStep(Tuple<int, float> T, bool lasttrue)
        {
            MakeLast(x, y);
            if (T.Item1 == 0)
                y = y - 1;
            if (T.Item1 == 1)
                y = y + 1;
            if (T.Item1 == 2)
                x = x - 1;
            if (T.Item1 == 3)
                x = x + 1;
            if (lasttrue)
                MakeLast(-1, -1);
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
                if (Board.TunellId(x, y) == id)
                {
                    if (g == Math.Abs(x - this.x) + Math.Abs(y - this.y))
                        GreatFlag = true;
                    return 1;
                }
                if (!(Board.TunellId(x, y) == -1))
                    return int.MaxValue / 2;
            }
            // Если глубина не достигнута, тогда рассматриваем клетки, в которые можем попасть
            if (kol_iter_a_star != 0)
            {
                kol_iter_a_star--;
                // Список значений эвристической функции для каждой клетки
                List<float> ff = new List<float> { -1, -1, -1, -1, -1 };

                if (Board.IsEmpthy(x, y - 1) && !((last_x == x) && (last_y == y - 1)))
                    ff[0] = f(x, y - 1, Board, kol_iter_a_star, x, y, false, g + 1, ref ArrG, ref MaxG, ref GreatFlag);
                if (ff[0] == 0)
                    return 1;
                if (Board.IsEmpthy(x, y + 1) && !((last_x == x) && (last_y == y + 1)))
                    ff[1] = f(x, y + 1, Board, kol_iter_a_star, x, y, false, g + 1, ref ArrG, ref MaxG, ref GreatFlag);
                if (ff[1] == 0)
                    return 1;
                if (Board.IsEmpthy(x - 1, y) && !((last_x == x - 1) && (last_y == y)))
                    ff[2] = f(x - 1, y, Board, kol_iter_a_star, x, y, false, g + 1, ref ArrG, ref MaxG, ref GreatFlag);
                if (ff[2] == 0)
                    return 1;
                if (Board.IsEmpthy(x + 1, y) && !((last_x == x + 1) && (last_y == y)))
                    ff[3] = f(x + 1, y, Board, kol_iter_a_star, x, y, false, g + 1, ref ArrG, ref MaxG, ref GreatFlag);
                if (ff[3] == 0)
                    return 1;
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
        private void MakeLast(int x, int y)
        {
            last__x = x;
            last__y = y;
        }
    }
}
