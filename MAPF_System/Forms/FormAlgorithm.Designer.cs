﻿
namespace MAPF_System
{
    partial class FormAlgorithm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_Error = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_kol_iterat = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.textBox_kol_iter_a_star = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label20 = new System.Windows.Forms.Label();
            this.button_Start = new CustomControls.Style.RJButton();
            this.button_Step = new CustomControls.Style.RJButton();
            this.button_Save = new CustomControls.Style.RJButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ButtonDelUnits = new CustomControls.Style.RJButton();
            this.label21 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.ButtonDelBlock = new CustomControls.Style.RJButton();
            this.label16 = new System.Windows.Forms.Label();
            this.ButtonPlusColumn = new CustomControls.Style.RJButton();
            this.label17 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.ButtonPlusRow = new CustomControls.Style.RJButton();
            this.ButtonMinusUnit = new CustomControls.Style.RJButton();
            this.label12 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.ButtonPlusUnit = new CustomControls.Style.RJButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_Error
            // 
            this.label_Error.AutoSize = true;
            this.label_Error.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label_Error.ForeColor = System.Drawing.Color.Red;
            this.label_Error.Location = new System.Drawing.Point(1255, 79);
            this.label_Error.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Error.Name = "label_Error";
            this.label_Error.Size = new System.Drawing.Size(0, 24);
            this.label_Error.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label1.Location = new System.Drawing.Point(809, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 24);
            this.label1.TabIndex = 10;
            this.label1.Text = "Введите имя: ";
            // 
            // label_kol_iterat
            // 
            this.label_kol_iterat.AutoSize = true;
            this.label_kol_iterat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label_kol_iterat.Location = new System.Drawing.Point(1255, 49);
            this.label_kol_iterat.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_kol_iterat.Name = "label_kol_iterat";
            this.label_kol_iterat.Size = new System.Drawing.Size(15, 24);
            this.label_kol_iterat.TabIndex = 14;
            this.label_kol_iterat.Text = " ";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBox_Name.Location = new System.Drawing.Point(813, 76);
            this.textBox_Name.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(175, 29);
            this.textBox_Name.TabIndex = 9;
            // 
            // textBox_kol_iter_a_star
            // 
            this.textBox_kol_iter_a_star.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBox_kol_iter_a_star.Location = new System.Drawing.Point(257, 76);
            this.textBox_kol_iter_a_star.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_kol_iter_a_star.Name = "textBox_kol_iter_a_star";
            this.textBox_kol_iter_a_star.Size = new System.Drawing.Size(175, 29);
            this.textBox_kol_iter_a_star.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label2.Location = new System.Drawing.Point(256, 34);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(302, 24);
            this.label2.TabIndex = 17;
            this.label2.Text = "Глубина просмотра (от 7 до 20): ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(43, 364);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 24);
            this.label5.TabIndex = 19;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label19);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(-4, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(2660, 36);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(436, 10);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(1060, 24);
            this.label4.TabIndex = 27;
            this.label4.Text = "F5 - Запуск; F10 - Пошаговый запуск; Двойной щелчок - добавить/убрать блок; Нажат" +
    "иями переносятся юниты/цели;";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label19.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label19.Location = new System.Drawing.Point(1113, 10);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(415, 24);
            this.label19.TabIndex = 27;
            this.label19.Text = "Квадраты - туннели; Крестики - плохие узлы;";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(19, 9);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 24);
            this.label6.TabIndex = 19;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(1540, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(377, 24);
            this.label3.TabIndex = 19;
            this.label3.Text = "Cntrl+S - Сохранить; Esc - Закрыть окно; ";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.button_Start);
            this.groupBox1.Controls.Add(this.button_Step);
            this.groupBox1.Controls.Add(this.button_Save);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_kol_iter_a_star);
            this.groupBox1.Controls.Add(this.textBox_Name);
            this.groupBox1.Controls.Add(this.label_kol_iterat);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label_Error);
            this.groupBox1.Location = new System.Drawing.Point(-5, -6);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(2660, 130);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label20.ForeColor = System.Drawing.Color.Red;
            this.label20.Location = new System.Drawing.Point(1549, 80);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(0, 24);
            this.label20.TabIndex = 28;
            // 
            // button_Start
            // 
            this.button_Start.BackColor = System.Drawing.Color.SkyBlue;
            this.button_Start.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.button_Start.BorderColor = System.Drawing.Color.LightSlateGray;
            this.button_Start.BorderRadius = 10;
            this.button_Start.BorderSize = 1;
            this.button_Start.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Start.FlatAppearance.BorderSize = 0;
            this.button_Start.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_Start.ForeColor = System.Drawing.Color.Black;
            this.button_Start.Location = new System.Drawing.Point(35, 50);
            this.button_Start.Margin = new System.Windows.Forms.Padding(4);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(193, 55);
            this.button_Start.TabIndex = 19;
            this.button_Start.Text = "▶  Запустить";
            this.button_Start.TextColor = System.Drawing.Color.Black;
            this.button_Start.UseVisualStyleBackColor = false;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // button_Step
            // 
            this.button_Step.BackColor = System.Drawing.Color.SkyBlue;
            this.button_Step.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.button_Step.BorderColor = System.Drawing.Color.LightSlateGray;
            this.button_Step.BorderRadius = 10;
            this.button_Step.BorderSize = 1;
            this.button_Step.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Step.FlatAppearance.BorderSize = 0;
            this.button_Step.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Step.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_Step.ForeColor = System.Drawing.Color.Black;
            this.button_Step.Location = new System.Drawing.Point(581, 50);
            this.button_Step.Margin = new System.Windows.Forms.Padding(4);
            this.button_Step.Name = "button_Step";
            this.button_Step.Size = new System.Drawing.Size(193, 55);
            this.button_Step.TabIndex = 20;
            this.button_Step.Text = "▶▶  Пошагово";
            this.button_Step.TextColor = System.Drawing.Color.Black;
            this.button_Step.UseVisualStyleBackColor = false;
            this.button_Step.Click += new System.EventHandler(this.button_Step_Click);
            // 
            // button_Save
            // 
            this.button_Save.BackColor = System.Drawing.Color.SkyBlue;
            this.button_Save.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.button_Save.BorderColor = System.Drawing.Color.LightSlateGray;
            this.button_Save.BorderRadius = 10;
            this.button_Save.BorderSize = 1;
            this.button_Save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Save.FlatAppearance.BorderSize = 0;
            this.button_Save.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_Save.ForeColor = System.Drawing.Color.Black;
            this.button_Save.Location = new System.Drawing.Point(1031, 50);
            this.button_Save.Margin = new System.Windows.Forms.Padding(4);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(193, 55);
            this.button_Save.TabIndex = 21;
            this.button_Save.Text = "💾  Сохранить";
            this.button_Save.TextColor = System.Drawing.Color.Black;
            this.button_Save.UseVisualStyleBackColor = false;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.groupBox4.Controls.Add(this.ButtonDelUnits);
            this.groupBox4.Controls.Add(this.label21);
            this.groupBox4.Controls.Add(this.label18);
            this.groupBox4.Controls.Add(this.label22);
            this.groupBox4.Controls.Add(this.ButtonDelBlock);
            this.groupBox4.Controls.Add(this.label16);
            this.groupBox4.Controls.Add(this.ButtonPlusColumn);
            this.groupBox4.Controls.Add(this.label17);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label15);
            this.groupBox4.Controls.Add(this.ButtonPlusRow);
            this.groupBox4.Controls.Add(this.ButtonMinusUnit);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.ButtonPlusUnit);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Location = new System.Drawing.Point(-21, 114);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(136, 1138);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            // 
            // ButtonDelUnits
            // 
            this.ButtonDelUnits.BackColor = System.Drawing.Color.Orchid;
            this.ButtonDelUnits.BackgroundColor = System.Drawing.Color.Orchid;
            this.ButtonDelUnits.BorderColor = System.Drawing.Color.LightSlateGray;
            this.ButtonDelUnits.BorderRadius = 10;
            this.ButtonDelUnits.BorderSize = 1;
            this.ButtonDelUnits.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonDelUnits.FlatAppearance.BorderSize = 0;
            this.ButtonDelUnits.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDelUnits.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.ButtonDelUnits.ForeColor = System.Drawing.Color.Black;
            this.ButtonDelUnits.Location = new System.Drawing.Point(29, 805);
            this.ButtonDelUnits.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonDelUnits.Name = "ButtonDelUnits";
            this.ButtonDelUnits.Size = new System.Drawing.Size(69, 55);
            this.ButtonDelUnits.TabIndex = 32;
            this.ButtonDelUnits.Text = "❌";
            this.ButtonDelUnits.TextColor = System.Drawing.Color.Black;
            this.ButtonDelUnits.UseVisualStyleBackColor = false;
            this.ButtonDelUnits.Click += new System.EventHandler(this.ButtonDelUnits_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label21.Location = new System.Drawing.Point(32, 782);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(79, 24);
            this.label21.TabIndex = 31;
            this.label21.Text = "юниты: ";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label18.Location = new System.Drawing.Point(-15, 4);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(140, 24);
            this.label18.TabIndex = 27;
            this.label18.Text = "                          ";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label22.Location = new System.Drawing.Point(32, 759);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(86, 24);
            this.label22.TabIndex = 30;
            this.label22.Text = "Удалить";
            // 
            // ButtonDelBlock
            // 
            this.ButtonDelBlock.BackColor = System.Drawing.Color.Orchid;
            this.ButtonDelBlock.BackgroundColor = System.Drawing.Color.Orchid;
            this.ButtonDelBlock.BorderColor = System.Drawing.Color.LightSlateGray;
            this.ButtonDelBlock.BorderRadius = 10;
            this.ButtonDelBlock.BorderSize = 1;
            this.ButtonDelBlock.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonDelBlock.FlatAppearance.BorderSize = 0;
            this.ButtonDelBlock.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonDelBlock.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.ButtonDelBlock.ForeColor = System.Drawing.Color.Black;
            this.ButtonDelBlock.Location = new System.Drawing.Point(29, 662);
            this.ButtonDelBlock.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonDelBlock.Name = "ButtonDelBlock";
            this.ButtonDelBlock.Size = new System.Drawing.Size(69, 55);
            this.ButtonDelBlock.TabIndex = 35;
            this.ButtonDelBlock.Text = "❌";
            this.ButtonDelBlock.TextColor = System.Drawing.Color.Black;
            this.ButtonDelBlock.UseVisualStyleBackColor = false;
            this.ButtonDelBlock.Click += new System.EventHandler(this.ButtonDelBlock_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label16.Location = new System.Drawing.Point(32, 639);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(72, 24);
            this.label16.TabIndex = 34;
            this.label16.Text = "блоки: ";
            // 
            // ButtonPlusColumn
            // 
            this.ButtonPlusColumn.BackColor = System.Drawing.Color.Orchid;
            this.ButtonPlusColumn.BackgroundColor = System.Drawing.Color.Orchid;
            this.ButtonPlusColumn.BorderColor = System.Drawing.Color.LightSlateGray;
            this.ButtonPlusColumn.BorderRadius = 10;
            this.ButtonPlusColumn.BorderSize = 1;
            this.ButtonPlusColumn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonPlusColumn.FlatAppearance.BorderSize = 0;
            this.ButtonPlusColumn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonPlusColumn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.ButtonPlusColumn.ForeColor = System.Drawing.Color.Black;
            this.ButtonPlusColumn.Location = new System.Drawing.Point(29, 510);
            this.ButtonPlusColumn.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonPlusColumn.Name = "ButtonPlusColumn";
            this.ButtonPlusColumn.Size = new System.Drawing.Size(69, 55);
            this.ButtonPlusColumn.TabIndex = 32;
            this.ButtonPlusColumn.Text = "➕";
            this.ButtonPlusColumn.TextColor = System.Drawing.Color.Black;
            this.ButtonPlusColumn.UseVisualStyleBackColor = false;
            this.ButtonPlusColumn.Click += new System.EventHandler(this.ButtonPlusColumn_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label17.Location = new System.Drawing.Point(32, 617);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(86, 24);
            this.label17.TabIndex = 33;
            this.label17.Text = "Удалить";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label14.Location = new System.Drawing.Point(32, 486);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 24);
            this.label14.TabIndex = 31;
            this.label14.Text = "столбец: ";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label15.Location = new System.Drawing.Point(32, 464);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(99, 24);
            this.label15.TabIndex = 30;
            this.label15.Text = "Добавить";
            // 
            // ButtonPlusRow
            // 
            this.ButtonPlusRow.BackColor = System.Drawing.Color.Orchid;
            this.ButtonPlusRow.BackgroundColor = System.Drawing.Color.Orchid;
            this.ButtonPlusRow.BorderColor = System.Drawing.Color.LightSlateGray;
            this.ButtonPlusRow.BorderRadius = 10;
            this.ButtonPlusRow.BorderSize = 1;
            this.ButtonPlusRow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonPlusRow.FlatAppearance.BorderSize = 0;
            this.ButtonPlusRow.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonPlusRow.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.ButtonPlusRow.ForeColor = System.Drawing.Color.Black;
            this.ButtonPlusRow.Location = new System.Drawing.Point(29, 359);
            this.ButtonPlusRow.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonPlusRow.Name = "ButtonPlusRow";
            this.ButtonPlusRow.Size = new System.Drawing.Size(69, 55);
            this.ButtonPlusRow.TabIndex = 29;
            this.ButtonPlusRow.Text = "➕";
            this.ButtonPlusRow.TextColor = System.Drawing.Color.Black;
            this.ButtonPlusRow.UseVisualStyleBackColor = false;
            this.ButtonPlusRow.Click += new System.EventHandler(this.ButtonPlusRow_Click);
            // 
            // ButtonMinusUnit
            // 
            this.ButtonMinusUnit.BackColor = System.Drawing.Color.Orchid;
            this.ButtonMinusUnit.BackgroundColor = System.Drawing.Color.Orchid;
            this.ButtonMinusUnit.BorderColor = System.Drawing.Color.LightSlateGray;
            this.ButtonMinusUnit.BorderRadius = 10;
            this.ButtonMinusUnit.BorderSize = 1;
            this.ButtonMinusUnit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonMinusUnit.FlatAppearance.BorderSize = 0;
            this.ButtonMinusUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonMinusUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.ButtonMinusUnit.ForeColor = System.Drawing.Color.Black;
            this.ButtonMinusUnit.Location = new System.Drawing.Point(29, 213);
            this.ButtonMinusUnit.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonMinusUnit.Name = "ButtonMinusUnit";
            this.ButtonMinusUnit.Size = new System.Drawing.Size(69, 55);
            this.ButtonMinusUnit.TabIndex = 29;
            this.ButtonMinusUnit.Text = "➖";
            this.ButtonMinusUnit.TextColor = System.Drawing.Color.Black;
            this.ButtonMinusUnit.UseVisualStyleBackColor = false;
            this.ButtonMinusUnit.Click += new System.EventHandler(this.ButtonMinusUnit_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label12.Location = new System.Drawing.Point(32, 336);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 24);
            this.label12.TabIndex = 28;
            this.label12.Text = "строку: ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label7.Location = new System.Drawing.Point(32, 190);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(66, 24);
            this.label7.TabIndex = 28;
            this.label7.Text = "юнит: ";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label13.Location = new System.Drawing.Point(32, 314);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(99, 24);
            this.label13.TabIndex = 27;
            this.label13.Text = "Добавить";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(43, 364);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 24);
            this.label9.TabIndex = 19;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label8.Location = new System.Drawing.Point(32, 167);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(86, 24);
            this.label8.TabIndex = 27;
            this.label8.Text = "Удалить";
            // 
            // ButtonPlusUnit
            // 
            this.ButtonPlusUnit.BackColor = System.Drawing.Color.Orchid;
            this.ButtonPlusUnit.BackgroundColor = System.Drawing.Color.Orchid;
            this.ButtonPlusUnit.BorderColor = System.Drawing.Color.LightSlateGray;
            this.ButtonPlusUnit.BorderRadius = 10;
            this.ButtonPlusUnit.BorderSize = 1;
            this.ButtonPlusUnit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ButtonPlusUnit.FlatAppearance.BorderSize = 0;
            this.ButtonPlusUnit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonPlusUnit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.ButtonPlusUnit.ForeColor = System.Drawing.Color.Black;
            this.ButtonPlusUnit.Location = new System.Drawing.Point(29, 71);
            this.ButtonPlusUnit.Margin = new System.Windows.Forms.Padding(4);
            this.ButtonPlusUnit.Name = "ButtonPlusUnit";
            this.ButtonPlusUnit.Size = new System.Drawing.Size(69, 55);
            this.ButtonPlusUnit.TabIndex = 22;
            this.ButtonPlusUnit.Text = "➕";
            this.ButtonPlusUnit.TextColor = System.Drawing.Color.Black;
            this.ButtonPlusUnit.UseVisualStyleBackColor = false;
            this.ButtonPlusUnit.Click += new System.EventHandler(this.ButtonPlusUnit_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label10.Location = new System.Drawing.Point(32, 48);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(66, 24);
            this.label10.TabIndex = 20;
            this.label10.Text = "юнит: ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label11.Location = new System.Drawing.Point(32, 26);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(99, 24);
            this.label11.TabIndex = 19;
            this.label11.Text = "Добавить";
            // 
            // FormAlgorithm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1919, 1055);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormAlgorithm";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormAlgorithm_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormAlgorithm_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FormAlgorithm_MouseDoubleClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormAlgorithm_MouseMove);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_Error;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label_kol_iterat;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.TextBox textBox_kol_iter_a_star;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private CustomControls.Style.RJButton button_Save;
        private CustomControls.Style.RJButton button_Step;
        private CustomControls.Style.RJButton button_Start;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label9;
        private CustomControls.Style.RJButton ButtonPlusUnit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private CustomControls.Style.RJButton ButtonMinusUnit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private CustomControls.Style.RJButton ButtonPlusRow;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private CustomControls.Style.RJButton ButtonPlusColumn;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private CustomControls.Style.RJButton ButtonDelBlock;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private CustomControls.Style.RJButton ButtonDelUnits;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
    }
}