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
    public partial class FormAlgorithm : Form
    {
        private Board Board;
        private bool was_game;

        public FormAlgorithm(Board Board, int kol_iterat = 0, bool error = false, string str_kol_iter_a_star = "", bool block_elem = false)
        {
            this.Board = Board;
            InitializeComponent();
            textBox_kol_iter_a_star.Text = str_kol_iter_a_star;
            label5.Text = Board.Name();
            // Позиция данной формы
            StartPosition = FormStartPosition.Manual;
            Location = new Point(100, 100);
            // Отрисовка поля
            pictureBox1.Paint += delegate { Board.Draw(CreateGraphics()); };
            if (kol_iterat != 0)
                label_kol_iterat.Text = "Количество шагов = " + kol_iterat;
            if (error)
                label_Error.Text = "Ошибка! Алгоритм зациклен";
            if (block_elem)
            {
                button_Start.Dispose();
                Controls.Remove(button_Start);
                button_Step.Dispose();
                Controls.Remove(button_Step);
                was_game = true;
                label4.Text = "";
            }
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox_kol_iter_a_star.Text, out int kol_iter_a_star) || (kol_iter_a_star < 7) || (kol_iter_a_star > 15))
            {
                SystemSounds.Beep.Play();
                label_Error.Text = "Глубина не верна!";
                return;
            }
            // Максимальное колличество итераций
            int N = 5000;
            Board TimeBoard = Board.CopyWithoutBlocks();
            int i = 0;
            while (!TimeBoard.IsEnd() && (i++) < (N-1))
                TimeBoard.MakeStep(Board, kol_iter_a_star);
            (new FormAlgorithm(TimeBoard, i, i == N, "" + kol_iter_a_star, true)).Show();
        }

        private void button_Step_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(textBox_kol_iter_a_star.Text, out int kol_iter_a_star) || (kol_iter_a_star < 7) || (kol_iter_a_star > 15))
            {
                SystemSounds.Beep.Play();
                label_Error.Text = "Глубина не верна!";
                return;
            }
            Board TimeBoard = Board.CopyWithoutBlocks();
            int i = 1;
            FormAlgorithm F = new FormAlgorithm(TimeBoard, i, false, "" + kol_iter_a_star, true);
            F.Show();
            while (!TimeBoard.IsEnd()) 
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
            if (textBox_Name.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                label_Error.Text = "Вы не ввели имя файла!";
                return;
            }
            label5.Text = Board.Save(textBox_Name.Text);
            label_Error.Text = "Сохранено!";
        }

    }
}
