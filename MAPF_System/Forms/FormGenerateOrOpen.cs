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
            {
                FormAlgorithm<UnitCentr, int> F = new FormAlgorithm<UnitCentr, int>(new BoardCentr(args[0]), 0, false, "7", false, false);
                F.Icon = Icon;
                F.ShowDialog();
            }
        }
        
        private void button_Generation_Click_Dec(object sender, EventArgs e){ generation(false); }

        private void button_Generation_Click_Centr(object sender, EventArgs e){ generation(true); }

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
            {
                FormAlgorithm<UnitCentr, int> F = new FormAlgorithm<UnitCentr, int>(new BoardCentr(X, Y, Blocks, Units), 0, false, "7");
                F.Icon = Icon;
                F.Show();
            }
            else
            {
                FormAlgorithm<UnitDec, Unit> F = new FormAlgorithm<UnitDec, Unit>(new BoardDec(X, Y, Blocks, Units), 0, false, "7");
                F.Icon = Icon;
                F.Show();
            }
        }

        private void button_Load_Click_Dec(object sender, EventArgs e) { load(false); }

        private void button_Load_Click_Centr(object sender, EventArgs e) { load(true); }

        private void load(bool isCentr)
        {
            label_Error.Text = "";
            label12.Text = "⏳";
            if (isCentr)
            {
                FormAlgorithm<UnitCentr, int> F = new FormAlgorithm<UnitCentr, int>(new BoardCentr(), 0, false, "7", false, false);
                if (!F.IsDisposed)
                {
                    F.Icon = Icon;
                    F.Show();
                }
            }
            else
            {
                FormAlgorithm<UnitDec, Unit> F = new FormAlgorithm<UnitDec, Unit>(new BoardDec(), 0, false, "7", false, false);
                if (!F.IsDisposed)
                {
                    F.Icon = Icon;
                    F.Show();
                }
            }
            label12.Text = "";
        }

        private void button_BigStart_Click_Dec(object sender, EventArgs e)
        {
            makeLoadText();
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    DataTable table = makeTable("resultsDec");
                    int a = 0, b = 0;
                    foreach (var f in (from f in Directory.GetFiles(fbd.SelectedPath) where Path.GetExtension(f).ToLower() == ".board" select f))
                    {
                        BoardDec Board = new BoardDec(f);
                        // Плотность
                        double density = 1.0 * Board.units.Count / (Board.X * Board.Y);
                        int kol_iter_a_star = 7;
                        // Максимальное колличество итераций
                        int N = 5000;
                        Board<UnitDec, Unit> TimeBoard = Board.CopyWithoutBlocks();
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
                            table.Rows.Add(f.Split('\\').Last(), "" + i , "" + (Board.X * Board.Y) , "" + Board.units.Count , "" + density);
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
            makeLoadText();
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    DataTable table = makeTable("resultsCentr");
                    int a = 0, b = 0;
                    foreach (var f in (from f in Directory.GetFiles(fbd.SelectedPath) where Path.GetExtension(f).ToLower() == ".board" select f))
                    {
                        BoardCentr Board = new BoardCentr(f);
                        // Плотность
                        double density = 1.0 * Board.units.Count / (Board.X * Board.Y);
                        // Максимальное колличество итераций
                        int N = 5000;
                        Board<UnitCentr, int> TimeBoard = Board.CopyWithoutBlocks();
                        int i = 0;
                        while (!TimeBoard.isEnd && (i++) < (N - 1))
                            TimeBoard.MakeStep(Board, 0);
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
                    table.WriteXml(fbd.SelectedPath + "\\resultsCentr.xml");
                    label11.Text = "";
                    MessageBox.Show("Пройденно " + b + " из " + (a + b) + "\nРезультаты сохранены в файл resultsCentr.xml", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }
                label11.Text = "";
            }
        }

        private void button_BigStart_Click_Unite(object sender, EventArgs e)
        {
            makeLoadText();
            using (FolderBrowserDialog fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    DataTable table = makeTable("resultsUnite");
                    int a = 0, b = 0;
                    foreach (var f in (from f in Directory.GetFiles(fbd.SelectedPath) where Path.GetExtension(f).ToLower() == ".board" select f))
                    {
                        BoardCentr Board = new BoardCentr(f);
                        // Плотность
                        double density = 1.0 * Board.units.Count / (Board.X * Board.Y);
                        int kol_iter_a_star = 7;
                        // Максимальное колличество итераций
                        int N = 5000;
                        int i = 0;

                        if (density >= 0.01)
                        {
                            Board<UnitCentr, int> TimeBoard = Board.CopyWithoutBlocks();
                            while (!TimeBoard.isEnd && (i++) < (N - 1))
                                TimeBoard.MakeStep(Board, kol_iter_a_star);
                        }
                        else
                        {
                            BoardDec BoardDec = new BoardDec(f);
                            Board<UnitDec, Unit> TimeBoard = BoardDec.CopyWithoutBlocks();
                            while (!TimeBoard.isEnd && (i++) < (N - 1))
                                TimeBoard.MakeStep(BoardDec, kol_iter_a_star);
                        }
                        

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
                    table.WriteXml(fbd.SelectedPath + "\\resultsUnite.xml");
                    label11.Text = "";
                    MessageBox.Show("Пройденно " + b + " из " + (a + b) + "\nРезультаты сохранены в файл resultsUnite.xml", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                }
                label11.Text = "";
            }
        }

        private DataTable makeTable(String str)
        {
            DataTable table = new DataTable(str);
            table.Columns.Add("Имя файла", typeof(string));
            table.Columns.Add("Колличество шагов", typeof(string));
            table.Columns.Add("Площадь", typeof(string));
            table.Columns.Add("Колличество агентов", typeof(string));
            table.Columns.Add("Плотность", typeof(string));
            return table;
        }

        private void makeLoadText()
        {
            label_Error.Text = "";
            label11.Text = "⏳";
        }

        private void FormGenerateOrOpen_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            FormAbout F = new FormAbout();
            F.Icon = Icon;
            F.Show();
            e.Cancel = true;
        }

        private void FormGenerateOrOpen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                FormAbout F = new FormAbout();
                F.Icon = Icon;
                F.Show();
            }
        }

    }
}
