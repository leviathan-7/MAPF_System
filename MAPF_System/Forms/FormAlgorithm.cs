﻿using CustomControls.Style;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAPF_System
{
    public partial class FormAlgorithm<T> : Form
    {
        private Board<T> Board;
        private bool was_game;
        private bool move;
        private Tuple<int, int> C;
        private Tuple<int, int> CC;

        public FormAlgorithm(Board<T> Board, int kol_iterat = 0, bool error = false, string str_kol_iter_a_star = "", bool block_elem = false, bool viewtunnel = true)
        {
            if (Board.units is null)
            {
                Close();
                return;
            }
            this.Board = Board;
            InitializeComponent();
            if (Board is BoardCentr) 
            {
                label2.Visible = false;
                textBox_kol_iter_a_star.Visible = false;
                Text = "Запуск MAPF - Централизованный";
            }
            if (Board is BoardDec)
                Text = "Запуск MAPF - Децентрализованный";

            was_game = Board.WasGame;
            textBox_kol_iter_a_star.Text = str_kol_iter_a_star;
            label6.Text = Board.name;
            // Позиция данной формы
            StartPosition = FormStartPosition.Manual;
            Location = new Point(100, 100);
            // Отрисовка поля
            Paint += delegate { Board.Draw(CreateGraphics(), true, null, null, viewtunnel); };
            if (kol_iterat != 0)
                label_kol_iterat.Text = "Количество шагов = " + kol_iterat;
            if (error)
                label_Error.Text = "Ошибка! Алгоритм зациклен";
            if (block_elem || was_game)
            {
                was_game = true;

                new List<RJButton>() { button_Start, button_Step, ButtonPlusUnit, ButtonMinusUnit, ButtonPlusRow,
                    ButtonPlusColumn, ButtonDelBlock, ButtonDelUnits}.ForEach(b => 
                    {
                        b.Dispose();
                        Controls.Remove(b);
                    });

                new List<Label>() { label4, label7, label8, label10, label11, label12, label13, label14, label15,
                    label16, label17, label21, label22}.ForEach(label => label.Text = "");
            }
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox_kol_iter_a_star.Text, out int kol_iter_a_star) || (kol_iter_a_star < 7) || (kol_iter_a_star > 20))
            {
                SystemSounds.Beep.Play();
                label_Error.Text = "Глубина не верна!";
                return;
            }
            // Максимальное колличество итераций
            int N = 5000;
            Board<T> TimeBoard = Board.CopyWithoutBlocks();
            int i = 0;
            while (!TimeBoard.isEnd && (i++) < (N - 1))
                TimeBoard.MakeStep(Board, kol_iter_a_star);
            FormAlgorithm<T> F = new FormAlgorithm<T>(TimeBoard, i, i == N, "" + kol_iter_a_star, true);
            F.Icon = Icon;
            F.Show();
        }

        private void button_Step_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox_kol_iter_a_star.Text, out int kol_iter_a_star) || (kol_iter_a_star < 7) || (kol_iter_a_star > 20))
            {
                SystemSounds.Beep.Play();
                label_Error.Text = "Глубина не верна!";
                return;
            }
            Board<T> TimeBoard = Board.CopyWithoutBlocks();
            int i = 1;
            FormAlgorithm<T> F = new FormAlgorithm<T>(TimeBoard, 0, false, "" + kol_iter_a_star, true);
            F.Icon = Icon;
            F.Show();
            while (!TimeBoard.isEnd) 
            {
                TimeBoard.MakeStep(Board, kol_iter_a_star);
                TimeBoard.Draw(F.CreateGraphics(), false);
                F.label_kol_iterat.Text = "Количество шагов = " + i++;
                if (MessageBox.Show("Далее?", "▶▶", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                {
                    F.Close();
                    return;
                }
            }
        }

        private void FormAlgorithm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
            if (e.Control && e.KeyCode == Keys.S)
                button_Save_Click(sender, null);
            if (!was_game && e.KeyCode == Keys.F5)
                button_Start_Click(sender, null);
            if (!was_game && e.KeyCode == Keys.F10)
                button_Step_Click(sender, null);
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            move = false;
            if (textBox_Name.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                label_Error.Text = "Вы не ввели имя файла!";
                return;
            }
            label6.Text = Board.Save(textBox_Name.Text, was_game);
            label_Error.Text = "Сохранено!";
        }

        private Tuple<int, int> CELL(MouseEventArgs e)
        {
            int height = Math.Max(Board.X, Board.Y) < 30 ? 24 : 18;
            return new Tuple<int, int>((e.Location.X - 100) / height, (e.Location.Y - 120) / height);
        }
        
        private void FormAlgorithm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (was_game)
                return;
            move = false;
            var C = CELL(e);
            int r = Board.ReversBlock(C);
            if (r == 1)
                Board.Draw(CreateGraphics(), false);
            if (r == 2)
                Board.Draw(CreateGraphics(), false, C);
        }

        private void FormAlgorithm_MouseClick(object sender, MouseEventArgs e)
        {
            if (was_game)
                return;
            var C1 = CELL(e);
            if (move && Board.Move(C, C1))
                Board.Draw(CreateGraphics(), false, C);
            C = C1;
            move = !move;
        }

        private void FormAlgorithm_MouseMove(object sender, MouseEventArgs e)
        {
            using (Graphics g = CreateGraphics())
            {
                var C = CELL(e);
                int height = 18;
                if (Math.Max(Board.X, Board.Y) < 30)
                    height = 24;
                int YY = 115;
                int XX = 95;
                var Size = new Size(height, height);
                var Font = new Font("Arial", 7, FontStyle.Bold);
                var Font1 = new Font("Arial", 7, FontStyle.Bold | FontStyle.Underline);
                if (!(CC is null) && (CC.Item1 < Board.X) && (CC.Item2 < Board.Y))
                {
                    g.DrawString("" + CC.Item2, Font1, Brushes.White, new Point(88, 124 + height * CC.Item2));
                    g.DrawString("" + CC.Item1, Font1, Brushes.White, new Point(104 + height * CC.Item1, 108));
                    g.DrawString("" + CC.Item2, Font, Brushes.Coral, new Point(88, 124 + height * CC.Item2));
                    g.DrawString("" + CC.Item1, Font, Brushes.Coral, new Point(104 + height * CC.Item1, 108));
                    if (!was_game)
                    {
                        Color clr = Color.Black;
                        if (Board.IsEmpthy(CC.Item1, CC.Item2))
                            clr = Color.Blue;
                        g.DrawRectangle(new Pen(clr, 1), new Rectangle(new Point(XX + 5 + height * CC.Item1, YY + 5 + height * CC.Item2), Size));
                    }
                }
                if ((C.Item1 < Board.X) && (C.Item2 < Board.Y) && (e.Location.Y > 115) && (e.Location.X > 95))
                {
                    g.DrawString("" + C.Item2, Font1, Brushes.Blue, new Point(88, 124 + height * C.Item2));
                    g.DrawString("" + C.Item1, Font1, Brushes.Blue, new Point(104 + height * C.Item1, 108));
                    if (!was_game)
                    {
                        g.DrawRectangle(new Pen(Color.DarkRed, 1), new Rectangle(new Point(XX + 5 + height * C.Item1, YY + 5 + height * C.Item2), Size));
                        Cursor = Cursors.Hand;
                    }
                }
                else
                {
                    if (!was_game)
                        Cursor = Cursors.Default;
                }
                CC = C;
            }
        }

        private void ButtonPlusUnit_Click(object sender, EventArgs e)
        {
            move = false;
            Board.PlusUnit();
            Board.Draw(CreateGraphics(), false);
        }

        private void ButtonMinusUnit_Click(object sender, EventArgs e)
        {
            move = false;
            var U = Board.MinusUnit();
            if (!(U is null))
                Board.Draw(CreateGraphics(), false, U.Item1, U.Item2);
        }

        private void ButtonPlusRow_Click(object sender, EventArgs e)
        {
            move = false;
            Board.PlusRow();
            Board.Draw(CreateGraphics());
        }

        private void ButtonPlusColumn_Click(object sender, EventArgs e)
        {
            move = false;
            Board.PlusColumn();
            Board.Draw(CreateGraphics());
        }

        private void ButtonDelBlock_Click(object sender, EventArgs e)
        {
            move = false;
            Board.DelBlokcs();
            Board.Draw(CreateGraphics());
        }

        private void ButtonDelUnits_Click(object sender, EventArgs e)
        {
            move = false;
            if(Board.DelUnits())
                Board.Draw(CreateGraphics());
        }

    }
}
