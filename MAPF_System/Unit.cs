using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAPF_System
{
    public class Unit
    {
        private int id;
        private int x;
        private int y;
        private int last__x;
        private int last__y;
        private int x_Purpose;
        private int y_Purpose;
        private bool was_step;
        private bool flag;
        public Unit(int x, int y, int x_Purpose, int y_Purpose, int id, int last__x, int last__y, bool was_step = false, bool flag = false) {
            this.id = id;
            this.was_step = was_step;
            this.flag = flag;
            // Задание местоположения юнита
            this.x = x;
            this.y = y;
            // Задание местоположения цели юнити
            this.x_Purpose = x_Purpose;
            this.y_Purpose = y_Purpose;
            this.last__x = last__x;
            this.last__y = last__y;
        }
        public Unit(string str, int i)
        {
            flag = false;
            was_step = false;
            string[] arr = str.Split(' ');
            // Задание параметров юнита на основе строки из файла
            x = int.Parse(arr[0]);
            y = int.Parse(arr[1]);
            x_Purpose = int.Parse(arr[2]);
            y_Purpose = int.Parse(arr[3]);
            id = i;
            last__x = -1;
            last__y = -1;
        }
        public void NotWasStep() { was_step = false; }
        public int X() { return x; }
        public int Y() { return y; }
        public int Id() { return id; }
        public int X_Purpose() { return x_Purpose; }
        public int Y_Purpose() { return y_Purpose; }
        public Unit Copy() { return new Unit(x, y, x_Purpose, y_Purpose, id, last__x, last__y, was_step, flag); }
        public string ToStr() { return x + " " + y + " " + x_Purpose + " " + y_Purpose; }
        public bool IsEnd(){ return IsRealEnd() && !flag; }
        public bool IsRealEnd() { return (x == x_Purpose) && (y == y_Purpose); }
        public void MakeStep(Board Board, IEnumerable<Unit> AnotherUnits)
        {
            // Обнуление флага, когда юнит прошел через свою цель
            if (flag && (x == x_Purpose) && (y == y_Purpose))
                flag = false;
            // Проверяем, что юнит еще не работал на данной итерации
            if (was_step)
                return;
            // Глубина, на которую юнит просматривает свои ходы
            int kol_iter_a_star = 7;
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
            int min_i = MIN_I(hh, ff, Board, UsUnits, new List<int> { 0,0,0,0}, new List<int> { 0, 0, 0, 0 }, -1, -1, kol_iter_a_star);
            was_step = (min_i != -10);
            if (was_step)
            {
                // Сохраняем прошлое местоположение юнита 
                last__x = x;
                last__y = y;
                if (min_i == 0)
                    y = y - 1;
                if (min_i == 1)
                    y = y + 1;
                if (min_i == 2)
                    x = x - 1;
                if (min_i == 3)
                    x = x + 1;
                if (min_i != 4)
                {
                    // Помечаем клетку как посещенную
                    Board.MakeVisit(x, y, id);
                    return;
                }
            }
            else
            {
                last__x = -1;
                last__y = -1;
            }
        }
        private bool MakeStep(Board Board, IEnumerable<Unit> AnotherUnits, int xx, int yy, int kol_iter_a_star, bool signal, Unit AU = null)
        {
            // Проверяем, что юнит еще не работал на данной итерации
            if (was_step)
                return false;
            // Проверяем, надо ли ставить флаг того, что 2 юнита оказались в тупике и им надо на места друг-друга
            int t = 0;
            if (!Board.IsEmpthy(x, y - 1))
                t++;
            if (!Board.IsEmpthy(x, y + 1))
                t++;
            if (!Board.IsEmpthy(x - 1, y))
                t++;
            if (!Board.IsEmpthy(x + 1, y))
                t++;
            if ((h(x, y) == 1) && (t == 3 || t == 2))
                flag = signal;
            if (flag)
                AU.flag = true;
            // Список значений эвристической функции для каждой клетки
            List<float> ff = new List<float> { -1, -1, -1, -1, -1 };
            // Список значений расстояний для каждой клетки
            List<float> hh = new List<float> { -1, -1, -1, -1, -1 };
            // Список юнитов для каждой клетки
            List<Unit> UsUnits = new List<Unit> { null, null, null, null, null };
            // Заполняем значения ff и UsUnits
            IfBoardIsEmpthy(hh, ff, Board, UsUnits, AnotherUnits, kol_iter_a_star, true);
            // Находим подходящую нам клетку
            int min_i = MIN_I(hh, ff, Board, UsUnits, new List<int> { x, x, x - 1, x + 1 }, new List<int> { y - 1, y + 1, y, y }, xx, yy, kol_iter_a_star);
            was_step = (min_i != -10);
            if (was_step)
            {
                // Сохраняем прошлое местоположение юнита 
                last__x = x;
                last__y = y;
                if (min_i == 0)
                    y = y - 1;
                if (min_i == 1)
                    y = y + 1;
                if (min_i == 2)
                    x = x - 1;
                if (min_i == 3)
                    x = x + 1;
                if (min_i != 4)
                {
                    // Помечаем клетку как посещенную
                    Board.MakeVisit(x, y, id);
                    return true;
                }
            }
            else
            {
                last__x = -1;
                last__y = -1;
            }

            return was_step;
        }
        private int MIN_I(List<float> hh, List<float> ff, Board Board, List<Unit> UsUnits, List<int> a, List<int> b, int xx, int yy, int kol_iter_a_star)
        {
            ff[4] = int.MaxValue;
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
                bb = UsUnits[min_i].MakeStep(Board, from u in Board.Units where u != UsUnits[min_i] select u, x, y, kol_iter_a_star, min == 0, this);
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
                    bb = UsUnits[min_i].MakeStep(Board, from u in Board.Units where u != UsUnits[min_i] select u, x, y, kol_iter_a_star, min == 0, this);
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
                    bb = UsUnits[min_i].MakeStep(Board, from u in Board.Units where u != UsUnits[min_i] select u, x, y, kol_iter_a_star, min == 0, this);
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
                    bb = UsUnits[min_i].MakeStep(Board, from u in Board.Units where u != UsUnits[min_i] select u, x, y, kol_iter_a_star, min == 0, this);
            }
            // Возвращаем флаг -10, если юнит никуда сдвинуться не сможет
            if (!bb)
                return -10;
            // Возвращаем подходящую нам клетку
            return min_i;
        }
        private void IfBoardIsEmpthy(List<float> hh, List<float> ff, Board Board, List<Unit> UsUnits, IEnumerable<Unit> AnotherUnits, int kol_iter_a_star, bool b = false)
        {
            if (Board.IsEmpthy(x, y - 1) && (!((last__x == x) && (last__y == y - 1)) || b))
                GetUnitAndF(0, hh, ff, UsUnits, x, y - 1, x, y, Board, kol_iter_a_star, AnotherUnits);
            if (Board.IsEmpthy(x, y + 1) && (!((last__x == x) && (last__y == y + 1)) || b))
                GetUnitAndF(1, hh, ff, UsUnits, x, y + 1, x, y, Board, kol_iter_a_star, AnotherUnits);
            if (Board.IsEmpthy(x - 1, y) && (!((last__x == x - 1) && (last__y == y)) || b))
                GetUnitAndF(2, hh, ff, UsUnits, x - 1, y, x, y, Board, kol_iter_a_star, AnotherUnits);
            if (Board.IsEmpthy(x + 1, y) && (!((last__x == x + 1) && (last__y == y)) || b))
                GetUnitAndF(3, hh, ff, UsUnits, x + 1, y, x, y, Board, kol_iter_a_star, AnotherUnits);
        }
        private void GetUnitAndF(int i, List<float> hh, List<float> ff, List<Unit> UsUnits, int x0, int y0, int x, int y, Board Board, int kol_iter_a_star, IEnumerable<Unit> AnotherUnits)
        {
            ff[i] = f(x0, y0, Board, kol_iter_a_star, x, y);
            hh[i] = h(x0, y0);
            foreach (var au in AnotherUnits)
                if ((au.x == x0) && (au.y == y0))
                {
                    UsUnits[i] = au;
                    return;
                }
        }
        private float f(int x, int y, Board Board, int kol_iter_a_star, int last_x, int last_y)
        {
            // Стоимость нулевая, если юнит достиг цели
            if ((x == x_Purpose) && (y == y_Purpose))
                return 0;
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
                ff[4] = int.MaxValue;
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
