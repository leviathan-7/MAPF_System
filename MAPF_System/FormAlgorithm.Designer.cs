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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAlgorithm));
            this.label_Error = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label_kol_iterat = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.textBox_kol_iter_a_star = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.button_Start = new CustomControls.RJControls.RJButton();
            this.button_Step = new CustomControls.RJControls.RJButton();
            this.button_Save = new CustomControls.RJControls.RJButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.ButtonPlusUnit = new CustomControls.RJControls.RJButton();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.ButtonMinusUnit = new CustomControls.RJControls.RJButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.label_Error.Location = new System.Drawing.Point(941, 64);
            this.label_Error.Name = "label_Error";
            this.label_Error.Size = new System.Drawing.Size(0, 18);
            this.label_Error.TabIndex = 12;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label1.Location = new System.Drawing.Point(607, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 18);
            this.label1.TabIndex = 10;
            this.label1.Text = "Введите имя: ";
            // 
            // label_kol_iterat
            // 
            this.label_kol_iterat.AutoSize = true;
            this.label_kol_iterat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label_kol_iterat.Location = new System.Drawing.Point(941, 40);
            this.label_kol_iterat.Name = "label_kol_iterat";
            this.label_kol_iterat.Size = new System.Drawing.Size(12, 18);
            this.label_kol_iterat.TabIndex = 14;
            this.label_kol_iterat.Text = " ";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBox_Name.Location = new System.Drawing.Point(610, 62);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(132, 24);
            this.textBox_Name.TabIndex = 9;
            // 
            // textBox_kol_iter_a_star
            // 
            this.textBox_kol_iter_a_star.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBox_kol_iter_a_star.Location = new System.Drawing.Point(193, 62);
            this.textBox_kol_iter_a_star.Name = "textBox_kol_iter_a_star";
            this.textBox_kol_iter_a_star.Size = new System.Drawing.Size(132, 24);
            this.textBox_kol_iter_a_star.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label2.Location = new System.Drawing.Point(192, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 18);
            this.label2.TabIndex = 17;
            this.label2.Text = "Глубина просмотра (от 7 до 15): ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(349, 87);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(10, 10);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(32, 296);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(0, 18);
            this.label5.TabIndex = 19;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(255)))));
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(-3, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1995, 29);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(14, 7);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(0, 18);
            this.label6.TabIndex = 19;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(327, 8);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(831, 18);
            this.label4.TabIndex = 20;
            this.label4.Text = "F5 - Запуск; F10 - Пошаговый запуск; Двойной щелчок - добавить/убрать блок; Нажат" +
    "иями переносятся юниты/цели;";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(1155, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(298, 18);
            this.label3.TabIndex = 19;
            this.label3.Text = "Cntrl+S - Сохранить; Esc - Закрыть окно; ";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.button_Start);
            this.groupBox1.Controls.Add(this.button_Step);
            this.groupBox1.Controls.Add(this.button_Save);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBox_kol_iter_a_star);
            this.groupBox1.Controls.Add(this.textBox_Name);
            this.groupBox1.Controls.Add(this.label_kol_iterat);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label_Error);
            this.groupBox1.Location = new System.Drawing.Point(-4, -5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1995, 106);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
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
            this.button_Start.Location = new System.Drawing.Point(26, 41);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(145, 45);
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
            this.button_Step.Location = new System.Drawing.Point(436, 41);
            this.button_Step.Name = "button_Step";
            this.button_Step.Size = new System.Drawing.Size(145, 45);
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
            this.button_Save.Location = new System.Drawing.Point(773, 41);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(145, 45);
            this.button_Save.TabIndex = 21;
            this.button_Save.Text = "💾  Сохранить";
            this.button_Save.TextColor = System.Drawing.Color.Black;
            this.button_Save.UseVisualStyleBackColor = false;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.groupBox4.Controls.Add(this.ButtonMinusUnit);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.label9);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.ButtonPlusUnit);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Location = new System.Drawing.Point(-16, 93);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(102, 925);
            this.groupBox4.TabIndex = 26;
            this.groupBox4.TabStop = false;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(32, 296);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(0, 18);
            this.label9.TabIndex = 19;
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
            this.ButtonPlusUnit.Location = new System.Drawing.Point(22, 58);
            this.ButtonPlusUnit.Name = "ButtonPlusUnit";
            this.ButtonPlusUnit.Size = new System.Drawing.Size(52, 45);
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
            this.label10.Location = new System.Drawing.Point(24, 39);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(50, 18);
            this.label10.TabIndex = 20;
            this.label10.Text = "юнит: ";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label11.Location = new System.Drawing.Point(24, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 18);
            this.label11.TabIndex = 19;
            this.label11.Text = "Добавить";
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
            this.ButtonMinusUnit.Location = new System.Drawing.Point(22, 185);
            this.ButtonMinusUnit.Name = "ButtonMinusUnit";
            this.ButtonMinusUnit.Size = new System.Drawing.Size(52, 45);
            this.ButtonMinusUnit.TabIndex = 29;
            this.ButtonMinusUnit.Text = "➖";
            this.ButtonMinusUnit.TextColor = System.Drawing.Color.Black;
            this.ButtonMinusUnit.UseVisualStyleBackColor = false;
            this.ButtonMinusUnit.Click += new System.EventHandler(this.ButtonMinusUnit_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label7.Location = new System.Drawing.Point(24, 166);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 18);
            this.label7.TabIndex = 28;
            this.label7.Text = "юнит: ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label8.Location = new System.Drawing.Point(24, 148);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(67, 18);
            this.label8.TabIndex = 27;
            this.label8.Text = "Удалить";
            // 
            // FormAlgorithm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1439, 954);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormAlgorithm";
            this.Text = "Запуск алгоритма MAPF";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormAlgorithm_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormAlgorithm_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FormAlgorithm_MouseDoubleClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FormAlgorithm_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
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
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private CustomControls.RJControls.RJButton button_Save;
        private CustomControls.RJControls.RJButton button_Step;
        private CustomControls.RJControls.RJButton button_Start;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label9;
        private CustomControls.RJControls.RJButton ButtonPlusUnit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private CustomControls.RJControls.RJButton ButtonMinusUnit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}