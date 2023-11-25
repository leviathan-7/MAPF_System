﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Security;
using System.Windows.Forms;

namespace MAPF_System
{
    public class Board
    {
        public List<Unit> Units;
        public Cell[,] Arr;

        private int X;
        private int Y;
        private Random rnd;
        public Board(int X, int Y, int Blocks, int N_Units)
        {
            this.X = X;
            this.Y = Y;
            Arr = new Cell[X, Y];
            this.rnd = new Random();
            GenerationDefaults(Blocks);
            GenerationUnits(N_Units);
            GenerationBlocks();
        }
        public Board(int X, int Y, Cell[,] Arr, List<Unit> Units)
        {
            this.X = X;
            this.Y = Y;
            this.Arr = Arr;
            this.Units = Units;
        }
        public Board()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog(){ Filter = "(*.board)|*.board", };
            openFileDialog1.ShowDialog();
            string[] readText = File.ReadAllLines(openFileDialog1.FileName);
            string[] arr = readText[0].Split(' ');
            // Размеры поля
            X = int.Parse(arr[0]);
            Y = int.Parse(arr[1]);
            // Количество юнитов
            int N_Units = int.Parse(arr[2]);
            // Создать доску по данным файла
            Arr = new Cell[X, Y];
            int t = 1;
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                {
                    Arr[i, j] = new Cell(readText[t]);
                    t++;
                }
            // Создать юнитов по данным файла
            Units = new List<Unit>();
            for (int i = 0; i < N_Units; i++)
            {
                Units.Add(new Unit(readText[t],i));
                t++;
            }
        }
        public Board CopyWithoutBlocks()
        {
            Cell[,] CopyArr = new Cell[X, Y];
            List<Unit> CopyUnits = new List<Unit>();
            // Скопировать юнитов
            foreach (var item in Units)
                CopyUnits.Add(item.Copy());
            // Скопировать доску без блоков
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    CopyArr[i, j] = Arr[i, j].CopyWithoutBlock();

            return new Board(X, Y, CopyArr,CopyUnits);
        }
        private void GenerationDefaults(int Blocks)
        {
            int N = X * Y - Blocks - 1;

            int x = rnd.Next(X);
            int y = rnd.Next(Y);

            int x_sum = x;
            int y_sum = y;
            int kol = 1;

            Arr[x, y] = new Cell(false);
            while (N > 0)
            {
                int x1 = rnd.Next(X);
                int y1 = rnd.Next(Y);
                int x2 = rnd.Next(X);
                int y2 = rnd.Next(Y);
                int x3 = rnd.Next(X);
                int y3 = rnd.Next(Y);
                if (Math.Abs(x1 - x_sum / kol) < Math.Abs(x2 - x_sum / kol))
                    x = x1;
                else
                    x = x2;
                if (Math.Abs(y1 - y_sum / kol) < Math.Abs(y2 - y_sum / kol))
                    y = y1;
                else
                    y = y2;


                if (Math.Abs(x - x_sum / kol) < Math.Abs(x3 - x_sum / kol))
                    x = x3;
                if (Math.Abs(y1 - y_sum / kol) < Math.Abs(y3 - y_sum / kol))
                    y = y3;


                x_sum += x;
                y_sum += y;
                kol++;

                bool a = (x == 0) || (Arr[x - 1, y] is null);
                bool b = (x == X - 1) || (Arr[x + 1, y] is null);
                bool c = (y == 0) || (Arr[x, y - 1] is null);
                bool d = (y == Y - 1) || (Arr[x, y + 1] is null);
                if (Arr[x, y] is null && !(a && b && c && d))
                {
                    Arr[x, y] = new Cell(false);
                    N--;
                }
            }
        }
        private void GenerationUnits(int N_Units)
        {
            Units = new List<Unit>();
            int id = 0;
            while (N_Units > 0)
            {
                int x = rnd.Next(X);
                int y = rnd.Next(Y);

                int x_Purpose = rnd.Next(X);
                int y_Purpose = rnd.Next(Y);

                bool b = (Arr[x, y] != null) && (Arr[x_Purpose, y_Purpose] != null) && !((x == x_Purpose) && (y == y_Purpose));

                foreach (var item in Units)
                {
                    b = b && !((item.X() == x) && (item.Y() == y)) && !((item.X_Purpose() == x_Purpose) && (item.Y_Purpose() == y_Purpose))
                        && !((item.X() == x_Purpose) && (item.Y() == y_Purpose)) && !((item.X_Purpose() == x) && (item.Y_Purpose() == y));
                }

                if (b)
                {
                    //Arr[x, y].MakeVisit(id);
                    Unit U = new Unit(x, y, x_Purpose, y_Purpose, id, -1, -1);
                    Units.Add(U);
                    N_Units--;
                    id++;
                }
            }
        }
        private void GenerationBlocks()
        {
            for (int i = 0; i < X; i++)
                for (int j = 0; j < Y; j++)
                    if (Arr[i, j] is null)
                        Arr[i, j] = new Cell(true);
        }
        public int Get_X() { return X; }
        public int Get_Y() { return Y; }
        public void Draw(Graphics t)
        {
            int height = 17;
            int YY = 55;
            using (Graphics g = t)
            {
                g.Clear(SystemColors.Control); // Clear the draw area
                using (Pen pen = new Pen(Color.Blue, 1))
                {
                    for (int i = 0; i < X; i++)
                        for (int j = 0; j < Y; j++)
                        {
                            Rectangle rect = new Rectangle(new Point(5 + height * i, YY + 5 + height * j), new Size(height, height));
                            g.DrawRectangle(pen, rect);
                            // Отрисовка блоков
                            if (Arr[i, j].IsBlock())
                                g.FillRectangle(System.Drawing.Brushes.Black, rect);
                            // Отрисовка пройденного пути
                            if (Arr[i, j].WasVisit())
                            {
                                int W = 100 + 100 * (Arr[i, j].IdVisit()+1) / Units.Count;
                                Color C = Color.FromArgb(W, W, 255);
                                SolidBrush Brush = new SolidBrush(C);
                                g.FillRectangle(Brush, rect);
                            }
                        }
                    foreach (var item in Units)
                    {
                        //MessageBox.Show("" + item.X() + " " + item.Y());
                        // Отрисовка юнитов
                        Rectangle rect = new Rectangle(new Point(8 + height * item.X(), YY + 8 + height * item.Y()), new Size(height - 6, height - 6));
                        g.FillRectangle(System.Drawing.Brushes.Red, rect);
                        // Отрисовка цели
                        rect = new Rectangle(new Point(10 + height * item.X_Purpose(), YY + 10 + height * item.Y_Purpose()), new Size(height - 10, height - 10));
                        g.FillRectangle(System.Drawing.Brushes.Green, rect);
                    }
                }
            }
        }
        public void Save(string name)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(name + ".board");
                StreamWriter sw = fileInfo.CreateText();
                sw.WriteLine(X + " " + Y + " " + Units.Count);
                // Записать в файл доску с блоками и пройденным путем
                for (int i = 0; i < X; i++)
                    for (int j = 0; j < Y; j++)
                        sw.WriteLine(Arr[i, j].ToStr());
                // Записать в файл юнитов
                foreach (var item in Units)
                    sw.WriteLine(item.ToStr());
                sw.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public bool IsEnd()
        {
            bool b = true;
            // Проверяем, что все юниты дошли до своих целей
            foreach (var Unit in Units)
                b = b && Unit.IsEnd();
            return b;
        }
        public void MakeStep(Board Board)
        {
            // Добавить блоки в пределах видимости юнитов
            GetNewBlocks(Board);
            // Сделать шаг теми юнитами, которые еще не достигли своей цели
            foreach (var Unit in Units)
            {
                //MessageBox.Show("xx=" + Unit.X() + " yy=" + Unit.Y());
                if (!Unit.IsEnd())
                    Unit.MakeStep(this, from u in Units where u != Unit select u);
            }
                
        }
        private void GetNewBlocks(Board Board)
        {
            foreach (var Unit in Units)
            {
                int x = Unit.X();
                int y = Unit.Y();
                int x0 = Unit.X() - 1;
                int x1 = Unit.X() + 1;
                int y0 = Unit.Y() - 1;
                int y1 = Unit.Y() + 1;
                if (Board.IsBlock(x, y0))
                    Arr[x, y0].MakeBlock();
                if (Board.IsBlock(x, y1))
                    Arr[x, y1].MakeBlock();
                if (Board.IsBlock(x0, y))
                    Arr[x0, y].MakeBlock();
                if (Board.IsBlock(x1, y))
                    Arr[x1, y].MakeBlock();
            }
        }
        private bool IsBlock(int x, int y)
        {
            // Проверка на выход за пределы поля
            if ((x < 0) || (y < 0) || (x >= X) || (y >= Y))
                return false;
            return Arr[x, y].IsBlock();
        }
        public bool IsEmpthy(int x, int y)
        {
            // Проверка на выход за пределы поля
            if ((x < 0) || (y < 0) || (x >= X) || (y >= Y))
                return false;
            return !Arr[x, y].IsBlock();
        }
        public void MakeVisit(int x, int y, int id) { Arr[x, y].MakeVisit(id); }
    }

}
