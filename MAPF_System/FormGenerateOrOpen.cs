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
    public partial class FormGenerateOrOpen : Form
    {
        public FormGenerateOrOpen() { InitializeComponent(); }
        private void button_Generation_Click(object sender, EventArgs e)
        {
            // Считывание введенных данных
            int X = 0, Y = 0, Blocks = 0, Units = 0;
            bool isNumeric = int.TryParse(textBox_X.Text, out X) && int.TryParse(textBox_Y.Text, out Y)
                && int.TryParse(textBox_Blocks.Text, out Blocks) && int.TryParse(textBox_Units.Text, out Units);

            // Проверка введенных данных на правильность
            if (!isNumeric)
            {
                SystemSounds.Beep.Play();
                label_Error.Text = "Вы ввели не число!";
                return;
            }
            if ((X > 50) || (Y > 50))
            {
                SystemSounds.Beep.Play();
                label_Error.Text = "Размер поля превышает пределы!";
                return;
            }
            if ((X < 2) || (Y < 2))
            {
                SystemSounds.Beep.Play();
                label_Error.Text = "Поле слишком маленькое!";
                return;
            }
            if (Blocks < 0)
            {
                SystemSounds.Beep.Play();
                label_Error.Text = "Не должно быть отрицательных чисел!";
                return;
            }
            if (Units < 1)
            {
                SystemSounds.Beep.Play();
                label_Error.Text = "Должен быть хоть один юнит!";
                return;
            }
            if ((Blocks + 2 * Units) >= (X * Y))
            {
                SystemSounds.Beep.Play();
                label_Error.Text = "Количество препятствий и юнитов слишком большое!";
                return;
            }
            // Если все данные введены правильно
            label_Error.Text = "Ok!";
            (new FormAlgorithm(new Board(X, Y, Blocks, Units), out bool b)).Show();
        }
        private void button_Load_Click(object sender, EventArgs e) 
        {
            FormAlgorithm F = new FormAlgorithm(new Board(), out bool b);
            if (b)
                F.Show();
        }
    }
}
