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
    public class BoardDec : Board<UnitDec>, BoardInterface
    {
        public BoardDec(int X, int Y, int Blocks, int N_Units)
        {
            rnd = new Random();
            SampleConstructor(X, Y, new Cell[X, Y], new List<UnitDec>(), "", new List<TunellInterface>());
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
                    units.Add(new UnitDec(x, y, x_Purpose, y_Purpose, id, -1, -1, X, Y));
                    N_Units--;
                    id++;
                }
            }
            // Генерация препятствий
            GenerationBlocks(X, Y);
        }
        public BoardDec(int X, int Y, Cell[,] Arr, List<UnitDec> units, string name, List<TunellInterface> tunells)
        {
            SampleConstructor(X, Y, Arr, units, name, tunells);
        }
        public BoardDec()
        {
            // Открытие файла в формате board
            OpenFileDialog openFileDialog1 = new OpenFileDialog() { Filter = "(*.board)|*.board", };
            openFileDialog1.ShowDialog();
            // Проверка на то, что board файл был выбран
            if (openFileDialog1.FileName == "")
                return;
            Constructor(openFileDialog1.FileName);
        }
        public BoardDec(string path) { Constructor(path); }
        public BoardInterface CopyWithoutBlocks()
        {
            return new BoardDec(X, Y, copyArrWithoutBlocks, units.Select(unit => unit.copy).ToList(), name, tunells);
        }
        public void MakeStep(BoardInterface Board, int kol_iter_a_star)
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
                                List<TunellInterface> LT = new List<TunellInterface>();
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
                    units.Add(new UnitDec(x, y, x_Purpose, y_Purpose, units.Count, -1, -1, X, Y));
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
                        Arr[i, j] = new Cell(tuple.Item2[t]);
                        t++;
                    }
                // Создать юнитов по данным файла
                units = new List<UnitDec>();
                for (int i = 0; i < tuple.Item1; i++)
                {
                    units.Add(new UnitDec(tuple.Item2[t], i, X, Y));
                    t++;
                }
                tunells = new List<TunellInterface>();
            }
            catch (Exception)
            {
                MessageBox.Show("Файл повреждён.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private bool IsBad(int x, int y)
        {
            // Проверка на выход за пределы поля
            if ((x < 0) || (y < 0) || (x >= X) || (y >= Y))
                return false;
            return Arr[x, y].isBad;
        }
        private void LT_ADD(List<TunellInterface> LT, int i, int j)
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
