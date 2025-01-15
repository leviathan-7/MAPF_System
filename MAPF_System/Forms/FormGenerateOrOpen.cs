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
                GetIconAndShow(new FormAlgorithm(new BoardCentr(args[0]), 0, false, "7", false, false), Icon);
        }

        public static void MakeError(Label label, String str)
        {
            SystemSounds.Beep.Play();
            label.Text = str;
        }

        public static void GetIconAndShow(Form F, Icon Icon)
        {
            F.Icon = Icon;
            F.Show();
        }

        private void button_Generation_Click_Dec(object sender, EventArgs e){ generation(false); }

        private void button_Generation_Click_Centr(object sender, EventArgs e){ generation(true); }

        private void generation(bool isCentr)
        {
            label_Error.Text = "";
            // Считывание введенных данных
            int X = 0, Y = 0, Blocks = 0, Units = 0;
            // Проверка введенных данных на правильность
            if (!(int.TryParse(textBox_X.Text, out X) && int.TryParse(textBox_Y.Text, out Y) && int.TryParse(textBox_Blocks.Text, out Blocks) && int.TryParse(textBox_Units.Text, out Units)))
                MakeError(label_Error, "Вы ввели не число!");
            else if ((X > 45) || (Y > 45))
                MakeError(label_Error, "Размер поля превышает пределы!");
            else if ((X < 2) || (Y < 2))
                MakeError(label_Error, "Поле слишком маленькое!");
            else if (Blocks < 0)
                MakeError(label_Error, "Не должно быть отрицательных чисел!");
            else if (Units < 1)
                MakeError(label_Error, "Должен быть хоть один юнит!");
            else if ((Blocks + 2 * Units) >= (X * Y))
                MakeError(label_Error, "Количество препятствий и юнитов слишком большое!");
            else
                GetIconAndShow(new FormAlgorithm(isCentr ? (Board)new BoardCentr(X, Y, Blocks, Units) : new BoardDec(X, Y, Blocks, Units), 0, false, "7"), Icon);
        }
        
        private void button_Load_Click_Dec(object sender, EventArgs e) { load(false); }

        private void button_Load_Click_Centr(object sender, EventArgs e) { load(true); }

        private void load(bool isCentr)
        {
            label_Error.Text = "";
            label12.Text = "⏳";
            FormAlgorithm F = new FormAlgorithm(isCentr ? (Board)new BoardCentr() : new BoardDec(), 0, false, "7", false, false);
            if (!F.IsDisposed)
                GetIconAndShow(F, Icon);
            label12.Text = "";
        }

        private void button_BigStart_Click_Dec(object sender, EventArgs e) { BigStart(false); }

        private void button_BigStart_Click_Centr(object sender, EventArgs e) { BigStart(true); }

        private void button_BigStart_Click_Unite(object sender, EventArgs e) { BigStart(true, true); }

        private void BigStart(bool isCentr, bool isUniteWithDec = false)
        {
            String name = isUniteWithDec ? "resultsUnite" : isCentr ? "resultsCentr" : "resultsDec";
            label_Error.Text = "";
            label11.Text = "⏳";
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    DataTable table = new DataTable(name);
                    new List<String>() { "Имя файла", "Колличество шагов", "Площадь", "Колличество агентов", "Плотность" }.ForEach(s => table.Columns.Add(s));
                    int a = 0, b = 0;
                    foreach (var f in (from f in Directory.GetFiles(fbd.SelectedPath) where Path.GetExtension(f).ToLower() == ".board" select f))
                    {
                        Board Board = isCentr ? (Board)new BoardCentr(f) : new BoardDec(f);
                        // Плотность
                        double density = 1.0 * Board.units.Count / (Board.X * Board.Y);
                        if (isUniteWithDec && density < 0.01)
                            Board = new BoardDec(f);
                        int kol_iter_a_star = 7;
                        // Максимальное колличество итераций
                        int N = 5000;
                        Board TimeBoard = Board.CopyWithoutBlocks();
                        int i = 0;
                        while (!TimeBoard.isEnd && (i++) < (N - 1))
                            TimeBoard.MakeStep(Board, kol_iter_a_star);
                        if (i == N)
                        {
                            table.Rows.Add(f.Split('\\').Last(), "Ошибка");
                            a++;
                        }
                        else
                        {
                            table.Rows.Add(f.Split('\\').Last(), "" + i, "" + (Board.X * Board.Y), "" + Board.units.Count, "" + density);
                            b++;
                        }
                    }
                    table.WriteXml(fbd.SelectedPath + "\\" + name + ".xml");
                    label11.Text = "";
                    MessageBox.Show("Пройденно " + b + " из " + (a + b) + "\nРезультаты сохранены в файл " + name + ".xml", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }
                label11.Text = "";
            }
        }

        private void FormGenerateOrOpen_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            GetIconAndShow(new FormAbout(), Icon);
            e.Cancel = true;
        }

        private void FormGenerateOrOpen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
                GetIconAndShow(new FormAbout(), Icon);
        }

    }
}
