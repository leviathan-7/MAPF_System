using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MAPF_System
{
    public partial class Form2 : Form
    {
        private Board Board;
        public Form2(Board Board,int kol_iterat = 0)
        {
            this.Board = Board;
            InitializeComponent();
            pictureBox1.Paint += Draw2DArray;
            if(kol_iterat != 0)
                label_kol_iterat.Text = "Количество шагов = " + kol_iterat;
        }

        private void Form2_Load(object sender, EventArgs e) { }

        private void Draw2DArray(object sender, PaintEventArgs e){ Board.Draw(this.CreateGraphics()); }

        private void button_Start_Click(object sender, EventArgs e)
        {
            Board TimeBoard = Board.CopyWithoutBlocks();
            int i = 0;
            while (!TimeBoard.IsEnd() ) //&& (i<1))
            {
                TimeBoard.MakeStep(Board);
                i++;
            }
            Form2 F = new Form2(TimeBoard, i);
            F.Show();
        }

        private void button_Save_Click(object sender, EventArgs e)
        {
            if(textBox_Name.Text.Length == 0)
            {
                label_Error.Text = "Вы не ввели имя файла!";
                return;
            }
            Board.Save(textBox_Name.Text);
            label_Error.Text = "Сохранено!";
        }


    }
}
