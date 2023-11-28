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
        public FormAlgorithm(Board Board, out bool b, int kol_iterat = 0)
        {
            b = false;
            if (Board.Units is null)
                return;
            b = true;
            this.Board = Board;
            InitializeComponent();
            // Отрисовка поля
            pictureBox1.Paint += delegate { Board.Draw(this.CreateGraphics()); };
            if(kol_iterat != 0)
                label_kol_iterat.Text = "Количество шагов = " + kol_iterat;
        }

        private void button_Start_Click(object sender, EventArgs e)
        {
            Board TimeBoard = Board.CopyWithoutBlocks();
            int i = 0;
            while (!TimeBoard.IsEnd())
            {
                TimeBoard.MakeStep(Board);
                i++;
            }
            (new FormAlgorithm(TimeBoard, out bool b, i)).Show();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            if(textBox_Name.Text.Length == 0)
            {
                SystemSounds.Beep.Play();
                label_Error.Text = "Вы не ввели имя файла!";
                return;
            }
            Board.Save(textBox_Name.Text);
            label_Error.Text = "Сохранено!";
        }

        private void button_step_Click(object sender, EventArgs e)
        {
            Board TimeBoard = Board.CopyWithoutBlocks();
            int i = 0;
            while (!TimeBoard.IsEnd()) 
            {
                TimeBoard.MakeStep(Board);
                i++;
                FormAlgorithm F = new FormAlgorithm(TimeBoard, out bool b, i);
                F.Show();
                MessageBox.Show("Далее?");
                if(!TimeBoard.IsEnd())
                    F.Close();
            }
            
        }
    }
}
