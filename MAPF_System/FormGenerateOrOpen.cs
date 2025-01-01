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
using System.IO;

namespace MAPF_System
{
    public partial class FormGenerateOrOpen : Form
    {
        public FormGenerateOrOpen(string[] args) 
        { 
            // Запущенный по двойному щелчку по файлу .Board запускается как централизованный
            InitializeComponent();
            if (args.Length != 0)
                (new FormAlgorithm(null, new BoardCentr(args[0]), true, 0, true, "7", false, false)).ShowDialog();
        }
        
        private void button_Generation_Click_Dec(object sender, EventArgs e)
        {
            generation(false);
        }

        private void button_Generation_Click_Centr(object sender, EventArgs e)
        {
            generation(true);
        }

        private void generation(bool isCentr)
        {
            label_Error.Text = "";
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
            if ((X > 45) || (Y > 45))
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
            if (isCentr)
                (new FormAlgorithm(null, new BoardCentr(X, Y, Blocks, Units), true, 0, false, "7")).Show();
            else
                (new FormAlgorithm(new BoardDec(X, Y, Blocks, Units), null, false, 0, false, "7")).Show();
        }

        private void button_Load_Click_Dec(object sender, EventArgs e) 
        {
            label_Error.Text = "";
            label12.Text = "⏳";
            (new FormAlgorithm(new BoardDec(), null, false, 0, false, "7", false, false)).Show();
            label12.Text = "";
        }

        private void button_Load_Click_Centr(object sender, EventArgs e)
        {
            label_Error.Text = "";
            label12.Text = "⏳";
            (new FormAlgorithm(null, new BoardCentr(), true, 0, false, "7", false, false)).Show();
            label12.Text = "";
        }

        private void button_BigStart_Click_Dec(object sender, EventArgs e)
        {
            label_Error.Text = "";
            label11.Text = "⏳";
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    DataTable table = new DataTable("resultsDec");
                    table.Columns.Add("Имя файла", typeof(string));
                    table.Columns.Add("Колличество шагов", typeof(string));
                    table.Columns.Add("Площадь", typeof(string));
                    table.Columns.Add("Колличество агентов", typeof(string));
                    table.Columns.Add("Плотность", typeof(string));
                    int a = 0, b = 0;
                    foreach (var f in (from f in Directory.GetFiles(fbd.SelectedPath) where Path.GetExtension(f).ToLower() == ".board" select f))
                    {
                        BoardDec Board = new BoardDec(f);
                        // Плотность
                        double density = 1.0 * Board.Units().Count / (Board.GET_X() * Board.GET_Y());
                        int kol_iter_a_star = 7;
                        // Максимальное колличество итераций
                        int N = 5000;
                        BoardDec TimeBoard = (BoardDec)Board.CopyWithoutBlocks();
                        int i = 0;
                        while (!TimeBoard.IsEnd() && (i++) < (N - 1))
                            TimeBoard.MakeStep(Board, kol_iter_a_star);
                        if (i == N)
                        {
                            table.Rows.Add(f.Split('\\').Last(), "Ошибка");
                            a++;
                        }
                        else
                        {
                            table.Rows.Add(f.Split('\\').Last(), "" + i , "" + (Board.GET_X() * Board.GET_Y()) , "" + Board.Units().Count , "" + density);
                            b++;
                        }
                    }
                    table.WriteXml(fbd.SelectedPath + "\\resultsDec.xml");
                    label11.Text = "";
                    MessageBox.Show("Пройденно " + b + " из " + (a + b) + "\nРезультаты сохранены в файл resultsDec.xml", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }
                label11.Text = "";
            }
        }

        private void button_BigStart_Click_Centr(object sender, EventArgs e)
        {
            label_Error.Text = "";
            label11.Text = "⏳";
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    DataTable table = new DataTable("resultsCentr");
                    table.Columns.Add("Имя файла", typeof(string));
                    table.Columns.Add("Колличество шагов", typeof(string));
                    table.Columns.Add("Площадь", typeof(string));
                    table.Columns.Add("Колличество агентов", typeof(string));
                    table.Columns.Add("Плотность", typeof(string));
                    int a = 0, b = 0;
                    foreach (var f in (from f in Directory.GetFiles(fbd.SelectedPath) where Path.GetExtension(f).ToLower() == ".board" select f))
                    {
                        BoardCentr Board = new BoardCentr(f);
                        // Плотность
                        double density = 1.0 * Board.Units().Count / (Board.GET_X() * Board.GET_Y());
                        int kol_iter_a_star = 7;
                        // Максимальное колличество итераций
                        int N = 5000;
                        BoardCentr TimeBoard = (BoardCentr)Board.CopyWithoutBlocks();
                        int i = 0;
                        while (!TimeBoard.IsEnd() && (i++) < (N - 1))
                            TimeBoard.MakeStep(Board, kol_iter_a_star);
                        if (i == N)
                        {
                            table.Rows.Add(f.Split('\\').Last(), "Ошибка");
                            a++;
                        }
                        else
                        {
                            table.Rows.Add(f.Split('\\').Last(), "" + i, "" + (Board.GET_X() * Board.GET_Y()), "" + Board.Units().Count, "" + density);
                            b++;
                        }
                    }
                    table.WriteXml(fbd.SelectedPath + "\\resultsCentr.xml");
                    label11.Text = "";
                    MessageBox.Show("Пройденно " + b + " из " + (a + b) + "\nРезультаты сохранены в файл resultsCentr.xml", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }
                label11.Text = "";
            }
        }

        private void button_BigStart_Click_Unite(object sender, EventArgs e)
        {
            label_Error.Text = "";
            label11.Text = "⏳";
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    DataTable table = new DataTable("resultsUnite");
                    table.Columns.Add("Имя файла", typeof(string));
                    table.Columns.Add("Колличество шагов", typeof(string));
                    table.Columns.Add("Площадь", typeof(string));
                    table.Columns.Add("Колличество агентов", typeof(string));
                    table.Columns.Add("Плотность", typeof(string));
                    int a = 0, b = 0;
                    foreach (var f in (from f in Directory.GetFiles(fbd.SelectedPath) where Path.GetExtension(f).ToLower() == ".board" select f))
                    {
                        BoardCentr Board = new BoardCentr(f);
                        // Плотность
                        double density = 1.0 * Board.Units().Count / (Board.GET_X() * Board.GET_Y());
                        int kol_iter_a_star = 7;
                        // Максимальное колличество итераций
                        int N = 5000;
                        int i = 0;

                        if (density >= 0.01)
                        {
                            BoardCentr TimeBoard = (BoardCentr)Board.CopyWithoutBlocks();
                            while (!TimeBoard.IsEnd() && (i++) < (N - 1))
                                TimeBoard.MakeStep(Board, kol_iter_a_star);
                        }
                        else
                        {
                            BoardDec BoardDec = new BoardDec(f);
                            BoardDec TimeBoard = (BoardDec)BoardDec.CopyWithoutBlocks();
                            while (!TimeBoard.IsEnd() && (i++) < (N - 1))
                                TimeBoard.MakeStep(BoardDec, kol_iter_a_star);
                        }
                        

                        if (i == N)
                        {
                            table.Rows.Add(f.Split('\\').Last(), "Ошибка");
                            a++;
                        }
                        else
                        {
                            table.Rows.Add(f.Split('\\').Last(), "" + i, "" + (Board.GET_X() * Board.GET_Y()), "" + Board.Units().Count, "" + density);
                            b++;
                        }
                    }
                    table.WriteXml(fbd.SelectedPath + "\\resultsUnite.xml");
                    label11.Text = "";
                    MessageBox.Show("Пройденно " + b + " из " + (a + b) + "\nРезультаты сохранены в файл resultsUnite.xml", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }
                label11.Text = "";
            }
        }

        private void FormGenerateOrOpen_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            (new FormAbout()).Show();
            e.Cancel = true;
        }

        private void FormGenerateOrOpen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                (new FormAbout()).Show();
        }

    }
}
