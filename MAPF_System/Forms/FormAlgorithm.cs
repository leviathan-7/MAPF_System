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
    public partial class FormAlgorithm<U, T> : Form where U : Unit
    {
        private Board<U, T> Board;
        private bool was_game;
        private bool move;
        private bool isCentr;
        private Tuple<int, int> C;
        private Tuple<int, int> CC;

        public FormAlgorithm(Board<U, T> Board, bool isCentr, int kol_iterat = 0, bool error = false, string str_kol_iter_a_star = "", bool block_elem = false, bool viewtunnel = true)
        {
            if (Board.units is null)
            {
                Close();
                return;
            }
            this.Board = Board;
            InitializeComponent();
            this.isCentr = isCentr;
            if (isCentr) 
            {
                label2.Visible = false;
                textBox_kol_iter_a_star.Visible = false;
                Text = "Запуск MAPF - Централизованный";
            }
            else
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
                button_Start.Dispose();
                Controls.Remove(button_Start);
                button_Step.Dispose();
                Controls.Remove(button_Step);
                ButtonPlusUnit.Dispose();
                Controls.Remove(ButtonPlusUnit);
                ButtonMinusUnit.Dispose();
                Controls.Remove(ButtonMinusUnit);
                ButtonPlusRow.Dispose();
                Controls.Remove(ButtonPlusRow);
                ButtonPlusColumn.Dispose();
                Controls.Remove(ButtonPlusColumn);
                ButtonDelBlock.Dispose();
                Controls.Remove(ButtonDelBlock);
                ButtonDelUnits.Dispose();
                Controls.Remove(ButtonDelUnits);
                label4.Text = "";
                label7.Text = "";
                label8.Text = "";
                label10.Text = "";
                label11.Text = "";
                label12.Text = "";
                label13.Text = "";
                label14.Text = "";
                label15.Text = "";
                label16.Text = "";
                label17.Text = "";
                label21.Text = "";
                label22.Text = "";
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
            Board<U, T> TimeBoard = Board.CopyWithoutBlocks(isCentr);
            int i = 0;
            while (!TimeBoard.isEnd && (i++) < (N - 1))
                TimeBoard.MakeStep(Board, kol_iter_a_star, isCentr);
            FormAlgorithm<U, T> F = new FormAlgorithm<U, T>(TimeBoard, isCentr, i, i == N, "" + kol_iter_a_star, true);
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
            Board<U, T> TimeBoard = Board.CopyWithoutBlocks(isCentr);
            int i = 1;
            FormAlgorithm<U, T> F = new FormAlgorithm<U, T>(TimeBoard, isCentr, 0, false, "" + kol_iter_a_star, true);
            F.Icon = Icon;
            F.Show();
            while (!TimeBoard.isEnd) 
            {
                TimeBoard.MakeStep(Board, kol_iter_a_star, isCentr);
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
            int height = 18;
            if (Math.Max(Board.X, Board.Y) < 30)
                height = 24;
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
            if (move)
                if(Board.Move(C, C1))
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
                var Font = new Font("Arial", 7, FontStyle.Bold);
                var Font1 = new Font("Arial", 7, FontStyle.Bold | FontStyle.Underline);
                if (!(CC is null) && (CC.Item1 < Board.X) && (CC.Item2 < Board.Y))
                {
                    g.DrawString("" + CC.Item2, Font1, Brushes.White, new Point(88, 124 + height * CC.Item2));
                    g.DrawString("" + CC.Item1, Font1, Brushes.White, new Point(104 + height * CC.Item1, 108));
                    g.DrawString("" + CC.Item2, Font, Brushes.Coral, new Point(88, 124 + height * CC.Item2));
                    g.DrawString("" + CC.Item1, Font, Brushes.Coral, new Point(104 + height * CC.Item1, 108));
                }
                if ((C.Item1 < Board.X) && (C.Item2 < Board.Y) && (e.Location.Y > 115) && (e.Location.X > 95))
                {
                    if (!was_game)
                        Cursor = Cursors.Hand;
                    g.DrawString("" + C.Item2, Font1, Brushes.Blue, new Point(88, 124 + height * C.Item2));
                    g.DrawString("" + C.Item1, Font1, Brushes.Blue, new Point(104 + height * C.Item1, 108));
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
            Board.PlusUnit(isCentr);
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
