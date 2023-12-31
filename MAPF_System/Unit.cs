﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAPF_System
{
    public class Unit
    {
        private int X_Board;
        private int Y_Board;
        private int id;
        private int x;
        private int y;
        private int last__x;
        private int last__y;
        private int x_Purpose;
        private int y_Purpose;
        private bool was_step;
        private bool was_near_end;
        private bool was_bool_step;
        private int[,] Arr;
        private Unit last_AU;
        private float F_;
        private bool gotolast;

        public bool flag;

        public Unit(int x, int y, int x_Purpose, int y_Purpose, int id, int last__x, int last__y, int X, int Y, bool was_step = false, bool flag = false) {
            this.id = id;
            this.was_step = was_step;
            this.flag = flag;
            X_Board = X;
            Y_Board = Y;
            // Задание местоположения юнита
            this.x = x;
            this.y = y;
            // Задание местоположения цели юнити
            this.x_Purpose = x_Purpose;
            this.y_Purpose = y_Purpose;
            this.last__x = last__x;
            this.last__y = last__y;
            // Массив с количеством посещений узлов
            Arr = new int[X, Y];
        }
        public Unit(string str, int i, int X, int Y)
        {
            flag = false;
            was_step = false;
            string[] arr = str.Split(' ');
            X_Board = X;
            Y_Board = Y;
            // Задание параметров юнита на основе строки из файла
            x = int.Parse(arr[0]);
            y = int.Parse(arr[1]);
            x_Purpose = int.Parse(arr[2]);
            y_Purpose = int.Parse(arr[3]);
            id = i;
            last__x = -1;
            last__y = -1;
            // Массив с количеством посещений узлов
            Arr = new int[X, Y];
        }
        public Unit Copy() { return new Unit(x, y, x_Purpose, y_Purpose, id, last__x, last__y, X_Board, Y_Board, was_step, flag); }
        public void NotWasStep() { was_step = false; }
        public void GoToLast() { gotolast = true; }
        public int X() { return x; }
        public int Y() { return y; }
        public int Last_X() { return last__x; }
        public int Last_Y() { return last__y; }
        public int X_Purpose() { return x_Purpose; }
        public int Y_Purpose() { return y_Purpose; }
        public int Id() { return id; }
        public float F() { return F_; }
        public bool Was_near_end() { return was_near_end; }
        public bool Was_bool_step() { return was_bool_step; }
        public string ToStr() { return x + " " + y + " " + x_Purpose + " " + y_Purpose; }
        public bool IsEnd(){ return IsRealEnd() && !flag; }
        public bool IsRealEnd() { return (x == x_Purpose) && (y == y_Purpose); }
        public void MakeStep(Board Board, IEnumerable<Unit> AnotherUnits, int kol_iter_a_star)
        {
            bool flagflag = flag;
            // Обнуление флага, когда юнит прошел через свою цель
            if (flag && (x == x_Purpose) && (y == y_Purpose))
                flag = false;
            // Проверяем, что юнит еще не работал на данной итерации
            if (was_step)
                return;
            // Алгоритм для решения проблемы перпендикулярного хождения юнитов
            if (!gotolast && !(last_AU is null) && was_near_end && !flag && last_AU.IsEnd())
            {
                last__x = -1;
                last__y = -1;
            }
            last_AU = null;
            // Список значений эвристической функции для каждой клетки
            List<float> ff = new List<float> { -1, -1, -1, -1, -1 };
            // Список значений расстояний для каждой клетки
            List<float> hh = new List<float> { -1, -1, -1, -1, -1 };
            // Список юнитов для каждой клетки
            List<Unit> UsUnits = new List<Unit> { null, null, null, null, null };
            // Заполняем значения ff и UsUnits
            IfBoardIsEmpthy(hh, ff, Board, UsUnits, AnotherUnits, kol_iter_a_star);
            // Помечаем старую клетку как посещенную
            Board.MakeVisit(x, y, id);
            // Находим подходящую нам клетку
            var T = MIN_I(hh, ff, Board, UsUnits, new List<int> { 0, 0, 0, 0 }, new List<int> { 0, 0, 0, 0 }, -1, -1, kol_iter_a_star);
            F_ = T.Item2;
            was_step = (T.Item1 != -10);
            if (was_step)
            {
                was_bool_step = false;
                // Сохраняем прошлое местоположение юнита 
                last__x = x;
                last__y = y;
                if (T.Item1 == 0)
                    y = y - 1;
                if (T.Item1 == 1)
                    y = y + 1;
                if (T.Item1 == 2)
                    x = x - 1;
                if (T.Item1 == 3)
                    x = x + 1;
                if (T.Item1 != 4)
                {
                    // Алгоритм для решения проблемы перпендикулярного хождения юнитов
                    if (!gotolast && !(last_AU is null) && was_near_end && !flag && last_AU.IsEnd())
                    {
                        last__x = -1;
                        last__y = -1;
                        last_AU = null;
                    }
                    if (IsRealEnd())
                        was_near_end = true;
                    // Помечаем клетку как посещенную
                    Board.MakeVisit(x, y, id);
                    Arr[x, y]+=4;
                    return;
                }
            }
            else
            {
                if (!gotolast)
                {
                    last__x = -1;
                    last__y = -1;
                }
                // Добавление дополнительного флага, в случае, когда юнит с флагом не вышел из туннеля
                int t = 0;
                if (Board.IsEmpthy(x - 1, y))
                    t++;
                if (Board.IsEmpthy(x + 1, y))
                    t++;
                if (Board.IsEmpthy(x, y - 1))
                    t++;
                if (Board.IsEmpthy(x, y + 1))
                    t++;
                if (t != 1)
                    flag = flagflag;
            }
        }

        private bool MakeStep(Board Board, IEnumerable<Unit> AnotherUnits, int xx, int yy, int kol_iter_a_star, bool signal, Unit AU)
        {
            // Проверяем, что юнит еще не работал на данной итерации
            if (was_step)
                return false;
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
            if ((h(x, y) == 1) && (t >= 2))
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
            List<float> hh = new List<float> { -1, -1, -1, -1, -1 };
            // Список юнитов для каждой клетки
            List<Unit> UsUnits = new List<Unit> { null, null, null, null, null };
            // Заполняем значения ff и UsUnits
            IfBoardIsEmpthy(hh, ff, Board, UsUnits, AnotherUnits, kol_iter_a_star, true);
            // Находим подходящую нам клетку
            var T = MIN_I(hh, ff, Board, UsUnits, new List<int> { x, x, x - 1, x + 1 }, new List<int> { y - 1, y + 1, y, y }, xx, yy, kol_iter_a_star);
            F_ = T.Item2;
            was_step = (T.Item1 != -10);
            if (was_step)
            {
                was_bool_step = true;
                // Сохраняем прошлое местоположение юнита 
                last__x = x;
                last__y = y;
                if (T.Item1 == 0)
                    y = y - 1;
                if (T.Item1 == 1)
                    y = y + 1;
                if (T.Item1 == 2)
                    x = x - 1;
                if (T.Item1 == 3)
                    x = x + 1;
                if (T.Item1 != 4)
                {
                    // Алгоритм для решения проблемы перпендикулярного хождения юнитов
                    var q = AU;
                    if (!gotolast && !(last_AU is null) && was_near_end && !flag && last_AU.IsEnd())
                    {
                        last__x = -1;
                        last__y = -1;
                        q = null;
                    }
                    last_AU = q;
                    // Помечаем клетку как посещенную
                    Board.MakeVisit(x, y, id);
                    Arr[x, y] += 4;
                    return true;
                }
                
            }
            else
            {
                if (!gotolast)
                {
                    last__x = -1;
                    last__y = -1;
                }
            }

            return was_step;
        }
        private Tuple<int, float> MIN_I(List<float> hh, List<float> ff, Board Board, List<Unit> UsUnits, List<int> a, List<int> b, int xx, int yy, int kol_iter_a_star)
        {
            ff[4] = int.MaxValue - 100;
            if (gotolast)
                ff[4] = 1;
            float min = ff[4];
            float minh = ff[4];
            int min_i = 4;
            for (int i = 0; i < 4; i++)
                if (((min > ff[i]) || ((min == ff[i]) && (minh > hh[i])) || ((minh == hh[i]) && (min == ff[i]) && (UsUnits[i] is null))) && (ff[i] != -1) && !((xx == a[i]) && (yy == b[i])))
                {
                    if ((UsUnits[i] is null) || (!(UsUnits[i] is null) && !UsUnits[i].was_step))
                    {
                        min = ff[i];
                        minh = hh[i];
                        min_i = i;
                    }
                }
            bool bb = min_i != 4;
            was_step = true;
            if (!(UsUnits[min_i] is null))
                bb = UsUnits[min_i].MakeStep(Board, from u in Board.Units() where u != UsUnits[min_i] select u, x, y, kol_iter_a_star, min == 0, this);
            int min_i_1 = min_i;
            if (!bb)
            {
                min = ff[4];
                minh = ff[4];
                min_i = 4;
                for (int i = 0; i < 4; i++)
                    if (((min > ff[i]) || ((min == ff[i]) && (minh > hh[i])) || ((minh == hh[i]) && (min == ff[i]) && (UsUnits[i] is null))) && (ff[i] != -1) && (min_i_1 != i) && !((xx == a[i]) && (yy == b[i])))
                    {
                        if ((UsUnits[i] is null) || (!(UsUnits[i] is null) && !UsUnits[i].was_step))
                        {
                            min = ff[i];
                            minh = hh[i];
                            min_i = i;
                        }
                    }
                bb = min_i != 4;
                if (!(UsUnits[min_i] is null))
                    bb = UsUnits[min_i].MakeStep(Board, from u in Board.Units() where u != UsUnits[min_i] select u, x, y, kol_iter_a_star, min == 0, this);
            }
            int min_i_2 = min_i;
            if (!bb)
            {
                min = ff[4];
                minh = ff[4];
                min_i = 4;
                for (int i = 0; i < 4; i++)
                    if (((min > ff[i]) || ((min == ff[i]) && (minh > hh[i])) || ((minh == hh[i]) && (min == ff[i]) && (UsUnits[i] is null))) && (ff[i] != -1) && (min_i_1 != i) && (min_i_2 != i) && !((xx == a[i]) && (yy == b[i])))
                    {
                        if ((UsUnits[i] is null) || (!(UsUnits[i] is null) && !UsUnits[i].was_step))
                        {
                            min = ff[i];
                            minh = hh[i];
                            min_i = i;
                        }
                    }
                bb = min_i != 4;
                if (!(UsUnits[min_i] is null))
                    bb = UsUnits[min_i].MakeStep(Board, from u in Board.Units() where u != UsUnits[min_i] select u, x, y, kol_iter_a_star, min == 0, this);
            }
            int min_i_3 = min_i;
            if (!bb)
            {
                min = ff[4];
                minh = ff[4];
                min_i = 4;
                for (int i = 0; i < 4; i++)
                    if (((min > ff[i]) || ((min == ff[i]) && (minh > hh[i])) || ((minh == hh[i]) && (min == ff[i]) && (UsUnits[i] is null))) && (ff[i] != -1) && (min_i_1 != i) && (min_i_2 != i) && (min_i_3 != i) && !((xx == a[i]) && (yy == b[i])))
                    {
                        if ((UsUnits[i] is null) || (!(UsUnits[i] is null) && !UsUnits[i].was_step))
                        {
                            min = ff[i];
                            minh = hh[i];
                            min_i = i;
                        }
                    }
                bb = min_i != 4;
                if (!(UsUnits[min_i] is null))
                    bb = UsUnits[min_i].MakeStep(Board, from u in Board.Units() where u != UsUnits[min_i] select u, x, y, kol_iter_a_star, min == 0, this);
            }

            // Возвращаем флаг -10, если юнит никуда сдвинуться не сможет
            if (!bb)
                return new Tuple<int, float>(-10, F_); //-10;
            
            gotolast = false;
            // Возвращаем подходящую нам клетку
            return new Tuple<int, float>(min_i, min);
        }
        private void IfBoardIsEmpthy(List<float> hh, List<float> ff, Board Board, List<Unit> UsUnits, IEnumerable<Unit> AnotherUnits, int kol_iter_a_star, bool is_bool_step = false)
        {
            if (Board.IsEmpthy(x, y - 1) && (!((last__x == x) && (last__y == y - 1)) || is_bool_step || gotolast))
                GetUnitAndF(0, hh, ff, UsUnits, x, y - 1, x, y, Board, kol_iter_a_star, AnotherUnits, is_bool_step);
            if (Board.IsEmpthy(x, y + 1) && (!((last__x == x) && (last__y == y + 1)) || is_bool_step || gotolast))
                GetUnitAndF(1, hh, ff, UsUnits, x, y + 1, x, y, Board, kol_iter_a_star, AnotherUnits, is_bool_step);
            if (Board.IsEmpthy(x - 1, y) && (!((last__x == x - 1) && (last__y == y)) || is_bool_step || gotolast))
                GetUnitAndF(2, hh, ff, UsUnits, x - 1, y, x, y, Board, kol_iter_a_star, AnotherUnits, is_bool_step);
            if (Board.IsEmpthy(x + 1, y) && (!((last__x == x + 1) && (last__y == y)) || is_bool_step || gotolast))
                GetUnitAndF(3, hh, ff, UsUnits, x + 1, y, x, y, Board, kol_iter_a_star, AnotherUnits, is_bool_step);
        }
        private void GetUnitAndF(int i, List<float> hh, List<float> ff, List<Unit> UsUnits, int x0, int y0, int x, int y, Board Board, int kol_iter_a_star, IEnumerable<Unit> AnotherUnits, bool is_bool_step)
        {
            if (gotolast)
            {
                if ((last__x == x0) && (last__y == y0))
                    ff[i] = 0;
                else
                    ff[i] = -1;
            }
            else
            {
                ff[i] = f(x0, y0, Board, kol_iter_a_star, x, y, is_bool_step);
                // Добавляем коэффицент на стоимость вершины в виде количества её посещений данным юнитом
                if (!was_near_end && (ff[i] != 0))
                    ff[i] += Arr[x0, y0];
                hh[i] = h(x0, y0);
                // Алгоритм для решения проблемы параллельного хождения
                if (was_near_end)
                    foreach (var au in AnotherUnits)
                        if ((au.x_Purpose == x0) && (au.y_Purpose == y0))
                        {
                            ff[i] += 0.5f;
                            if (!au.IsEnd())
                                ff[i] += 0.5f;
                        }
            }
            foreach (var au in AnotherUnits)
                if ((au.x == x0) && (au.y == y0))
                {
                    UsUnits[i] = au;
                    return;
                }
        }
        private float f(int x, int y, Board Board, int kol_iter_a_star, int last_x, int last_y, bool is_bool_step = false)
        {
            // Стоимость нулевая, если юнит достиг цели
            if ((x == x_Purpose) && (y == y_Purpose))
                return 0;
            // Случай, когда узел плохой
            if (Board.IsBadCell(x, y))
            {
                // В случае "выталкивания", плохой случай наоборот считается хорошим
                if (is_bool_step && !Board.IsTunell(last_x, last_y))
                    return 1;
                return int.MaxValue - 100;
            }
            // Случай, когда простой туннель
            if (Board.IsTunell(x, y) && Board.TunellIsNotNull(x, y))
            {
                if (Board.TunellId(x, y) == id)
                    return 1;
                if (!(Board.TunellId(x, y) == -1))
                    return int.MaxValue - 100;
            }
            // Если глубина не достигнута, тогда рассматриваем клетки, в которвые можем попасть
            if (kol_iter_a_star != 0)
            {
                kol_iter_a_star--;
                // Список значений эвристической функции для каждой клетки
                List<float> ff = new List<float> { -1, -1, -1, -1, -1 };
                if (Board.IsEmpthy(x, y - 1) && !((last_x == x) && (last_y == y - 1)))
                    ff[0] = f(x, y - 1, Board, kol_iter_a_star, x, y);
                if (Board.IsEmpthy(x, y + 1) && !((last_x == x) && (last_y == y + 1)))
                    ff[1] = f(x, y + 1, Board, kol_iter_a_star, x, y);
                if (Board.IsEmpthy(x - 1, y) && !((last_x == x - 1) && (last_y == y)))
                    ff[2] = f(x - 1, y, Board, kol_iter_a_star, x, y);
                if (Board.IsEmpthy(x + 1, y) && !((last_x == x + 1) && (last_y == y)))
                    ff[3] = f(x + 1, y, Board, kol_iter_a_star, x, y);
                ff[4] = int.MaxValue - 100;
                // Находим клетку с минимальным значением эвристической функции
                float min = ff[4];
                for (int i = 0; i < 4; i++)
                    if ((min >= ff[i]) && (ff[i] != -1))
                        min = ff[i];
                return 1 + min;
            }
            // Считаем эвристическую оценку, если максимальная глубина достигнута
            return h(x, y);
        }
        private float h(int x, int y){ return (float)Math.Sqrt( Math.Pow(x_Purpose - x, 2) + Math.Pow(y_Purpose - y, 2)); }
    
    }
}
