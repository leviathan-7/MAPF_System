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
        public Unit(int x, int y, int x_Purpose, int y_Purpose, int id, int last__x, int last__y, bool was_step = false) {
            this.id = id;
            this.was_step = was_step;
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
        public Unit Copy() { return new Unit(x, y, x_Purpose, y_Purpose, id, last__x, last__y, was_step); }
        public string ToStr() { return x + " " + y + " " + x_Purpose + " " + y_Purpose; }
        public bool IsEnd(){ return (x == x_Purpose) && (y == y_Purpose); }
        public void MakeStep(Board Board, IEnumerable<Unit> AnotherUnits)
        {
            // Проверяем, что юнит еще не работал на данной итерации
            if (was_step)
                return;
            int x0 = x - 1;
            int x1 = x + 1;
            int y0 = y - 1;
            int y1 = y + 1;
            // Глубина, на которую юнит просматривает свои ходы
            int kol_iter_a_star = 7;
            // Список значений эвристической функции для каждой клетки
            List<float> ff = new List<float> { -1, -1, -1, -1, -1 };
            // Список статических юнитов, которые придется двигать
            List<Unit> StatUnits = new List<Unit> { null, null, null, null, null };

            List<Unit> UsUnits = new List<Unit> { null, null, null, null, null };

            if (Board.IsEmpthy(x,y0) && !((last__x == x) && (last__y == y0)))
            {
                ff[0] = f(x, y0, Board, kol_iter_a_star, x, y);
                StatUnits[0] = GetStatUnit(x, y0, AnotherUnits);
                UsUnits[0] = GetUnit(x, y0, AnotherUnits);
            }
            if (Board.IsEmpthy(x, y1) && !((last__x == x) && (last__y == y1)))
            {
                ff[1] = f(x, y1, Board, kol_iter_a_star, x, y);
                StatUnits[1] = GetStatUnit(x, y1, AnotherUnits);
                UsUnits[1] = GetUnit(x, y1, AnotherUnits);
            }
            if (Board.IsEmpthy(x0, y) && !((last__x == x0) && (last__y == y)))
            {
                ff[2] = f(x0, y, Board, kol_iter_a_star, x, y);
                StatUnits[2] = GetStatUnit(x0, y, AnotherUnits);
                UsUnits[2] = GetUnit(x0, y, AnotherUnits);
            }
            if (Board.IsEmpthy(x1, y) && !((last__x == x1) && (last__y == y)))
            {
                ff[3] = f(x1, y, Board, kol_iter_a_star, x, y);
                StatUnits[3] = GetStatUnit(x1, y, AnotherUnits);
                UsUnits[3] = GetUnit(x1, y, AnotherUnits);
            }
            ff[4] = int.MaxValue;
            // Находим клетку с минимальным значением эвристической функции
            float min = ff[4];
            int min_i = 4;
            for (int i = 0; i < 4; i++)
                if (((min > ff[i])||((min == ff[i]) && (StatUnits[i] is null))) && (ff[i] != -1) && (UsUnits[i] is null))
                {
                    if ((StatUnits[i] is null) || (!(StatUnits[i] is null) && !StatUnits[i].was_step))
                    {
                        min = ff[i];
                        min_i = i;
                    }
                }
            // Помечаем старую клетку как посещенную
            Board.MakeVisit(x, y, id);
            // Сохраняем прошлое местоположение юнита 
            last__x = x;
            last__y = y;
            // Сдвигаем статический юнит, если надо
            bool bb = min_i != 4;
            // Помечаем, что юнит отработал на данной итерации
            was_step = true;
            if (!(StatUnits[min_i] is null))
                bb = StatUnits[min_i].MakeStep(Board, from u in Board.Units where u != StatUnits[min_i] select u,-1,-1);
            was_step = bb;
            if (bb)
            {
                if (min_i == 0)
                    y = y0;
                if (min_i == 1)
                    y = y1;
                if (min_i == 2)
                    x = x0;
                if (min_i == 3)
                    x = x1;
                if (min_i != 4)
                {
                    // Помечаем новую клетку как посещенную
                    Board.MakeVisit(x, y, id);
                    return;
                }
            }
            else
            {
                min_i = 4;
                for (int i = 0; i < 4; i++)
                    if (!(UsUnits[i] is null) && !UsUnits[i].was_step)
                        min_i = i;
                if (min_i == 4)
                    return;
                was_step = true;
                bb = UsUnits[min_i].MakeStep(Board, from u in Board.Units where u != UsUnits[min_i] select u, x, y);
            }
            was_step = bb;
            if (bb)
            {
                if (min_i == 0)
                    y = y0;
                if (min_i == 1)
                    y = y1;
                if (min_i == 2)
                    x = x0;
                if (min_i == 3)
                    x = x1;
            }
        }
        private bool MakeStep(Board Board, IEnumerable<Unit> AnotherUnits, int xx, int yy)
        {
            // Проверяем, что юнит еще не работал на данной итерации
            if (was_step)
                return false;
            int x0 = x - 1;
            int x1 = x + 1;
            int y0 = y - 1;
            int y1 = y + 1;
            // Список значений эвристической функции для каждой клетки
            List<float> ff = new List<float> { -1, -1, -1, -1, -1 };
            // Список статических юнитов, которые придется двигать
            List<Unit> StatUnits = new List<Unit> { null, null, null, null, null };

            List<Unit> UsUnits = new List<Unit> { null, null, null, null, null };
            List<int> a = new List<int> { x, x, x0, x1 };
            List<int> b = new List<int> { y0, y1, y, y };
            if (Board.IsEmpthy(x, y0))
            {
                ff[0] = 1;
                StatUnits[0] = GetStatUnit(x, y0, AnotherUnits);
                UsUnits[0] = GetUnit(x, y0, AnotherUnits);
            }
            if (Board.IsEmpthy(x, y1))
            {
                ff[1] = 1;
                StatUnits[1] = GetStatUnit(x, y1, AnotherUnits);
                UsUnits[1] = GetUnit(x, y1, AnotherUnits);
            }
            if (Board.IsEmpthy(x0, y))
            {
                ff[2] = 1;
                StatUnits[2] = GetStatUnit(x0, y, AnotherUnits);
                UsUnits[2] = GetUnit(x0, y, AnotherUnits);
            }
            if (Board.IsEmpthy(x1, y))
            {
                ff[3] = 1;
                StatUnits[3] = GetStatUnit(x1, y, AnotherUnits);
                UsUnits[3] = GetUnit(x1, y, AnotherUnits);
            }
            ff[4] = int.MaxValue;
            // Находим клетку с минимальным значением эвристической функции
            float min = ff[4];
            int min_i = 4;
            for (int i = 0; i < 4; i++)
                if (((min > ff[i]) || (StatUnits[i] is null)) && (ff[i] != -1) && !((xx == a[i]) && (yy == b[i])) && (UsUnits[i] is null))
                {
                    if ((StatUnits[i] is null) || (!(StatUnits[i] is null) && !StatUnits[i].was_step))
                    {
                        min = ff[i];
                        min_i = i;
                    }
                }
            was_step = true;
            bool bb = min_i != 4;
            if (!(StatUnits[min_i] is null))
                bb = StatUnits[min_i].MakeStep(Board, from u in Board.Units where u != StatUnits[min_i] select u, x, y);
            was_step = bb;
            if (bb)
            {
                if (min_i == 0)
                    y = y0;
                if (min_i == 1)
                    y = y1;
                if (min_i == 2)
                    x = x0;
                if (min_i == 3)
                    x = x1;
                if (min_i != 4)
                {
                    // Помечаем новую клетку как посещенную
                    Board.MakeVisit(x, y, id);
                    return true;
                }
            }
            else
            {
                min_i = 4;
                for (int i = 0; i < 4; i++)
                    if (!(UsUnits[i] is null) && !UsUnits[i].was_step)
                        min_i = i;
                if (min_i == 4)
                    return false;
                was_step = true;
                bb = UsUnits[min_i].MakeStep(Board, from u in Board.Units where u != UsUnits[min_i] select u, x, y);
            }
            was_step = bb;
            if (bb)
            {
                if (min_i == 0)
                    y = y0;
                if (min_i == 1)
                    y = y1;
                if (min_i == 2)
                    x = x0;
                if (min_i == 3)
                    x = x1;
            }
            return bb;
        }
        private Unit GetStatUnit(int x, int y, IEnumerable<Unit> AnotherUnits)
        {
            foreach (var au in AnotherUnits)
                if ((au.x == x) && (au.y == y) && (au.x == au.x_Purpose) && (au.y == au.y_Purpose))
                    return au;
            return null;
        }
        private Unit GetUnit(int x, int y, IEnumerable<Unit> AnotherUnits)
        {
            foreach (var au in AnotherUnits)
                if ((au.x == x) && (au.y == y) && !((au.x == au.x_Purpose) && (au.y == au.y_Purpose)))
                    return au;
            return null;
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
                int x0 = x - 1;
                int x1 = x + 1;
                int y0 = y - 1;
                int y1 = y + 1;
                // Список значений эвристической функции для каждой клетки
                List<float> ff = new List<float> { -1, -1, -1, -1, -1 };
                if (Board.IsEmpthy(x, y0) && !((last_x == x) &&(last_y == y0)))
                    ff[0] = f(x, y0, Board, kol_iter_a_star, x, y);
                if (Board.IsEmpthy(x, y1) && !((last_x == x) && (last_y == y1)))
                    ff[1] = f(x, y1, Board, kol_iter_a_star, x, y);
                if (Board.IsEmpthy(x0, y) && !((last_x == x0) && (last_y == y)))
                    ff[2] = f(x0, y, Board, kol_iter_a_star, x, y);
                if (Board.IsEmpthy(x1, y) && !((last_x == x1) && (last_y == y)))
                    ff[3] = f(x1, y, Board, kol_iter_a_star, x, y);
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
        private float h(int x, int y)
        {
            // Оценка, основанная на манхэттенском расстоянии
            return Math.Abs(x_Purpose - x) + Math.Abs(y_Purpose - y);
        }
        
    }
}
