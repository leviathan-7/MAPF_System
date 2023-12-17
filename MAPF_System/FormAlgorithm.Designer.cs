
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
            this.button_Start = new System.Windows.Forms.Button();
            this.button_Save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_Name = new System.Windows.Forms.TextBox();
            this.label_Error = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_kol_iterat = new System.Windows.Forms.Label();
            this.button_step = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_kol_iter_a_star = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_Start
            // 
            this.button_Start.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_Start.Location = new System.Drawing.Point(30, 37);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(143, 45);
            this.button_Start.TabIndex = 1;
            this.button_Start.Text = "▶  Запустить";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // button_Save
            // 
            this.button_Save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_Save.Location = new System.Drawing.Point(772, 37);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(143, 45);
            this.button_Save.TabIndex = 2;
            this.button_Save.Text = "💾  Сохранить";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label1.Location = new System.Drawing.Point(607, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 18);
            this.label1.TabIndex = 10;
            this.label1.Text = "Введите имя: ";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBox_Name.Location = new System.Drawing.Point(610, 58);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(132, 24);
            this.textBox_Name.TabIndex = 9;
            // 
            // label_Error
            // 
            this.label_Error.AutoSize = true;
            this.label_Error.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label_Error.ForeColor = System.Drawing.Color.Red;
            this.label_Error.Location = new System.Drawing.Point(941, 60);
            this.label_Error.Name = "label_Error";
            this.label_Error.Size = new System.Drawing.Size(0, 18);
            this.label_Error.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(349, 83);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(10, 10);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // label_kol_iterat
            // 
            this.label_kol_iterat.AutoSize = true;
            this.label_kol_iterat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label_kol_iterat.Location = new System.Drawing.Point(941, 36);
            this.label_kol_iterat.Name = "label_kol_iterat";
            this.label_kol_iterat.Size = new System.Drawing.Size(12, 18);
            this.label_kol_iterat.TabIndex = 14;
            this.label_kol_iterat.Text = " ";
            // 
            // button_step
            // 
            this.button_step.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_step.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_step.Location = new System.Drawing.Point(438, 37);
            this.button_step.Name = "button_step";
            this.button_step.Size = new System.Drawing.Size(143, 45);
            this.button_step.TabIndex = 15;
            this.button_step.Text = "▶▶  Пошагово";
            this.button_step.UseVisualStyleBackColor = true;
            this.button_step.Click += new System.EventHandler(this.button_Step_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label2.Location = new System.Drawing.Point(192, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 18);
            this.label2.TabIndex = 17;
            this.label2.Text = "Глубина просмотра (от 7 до 15): ";
            // 
            // textBox_kol_iter_a_star
            // 
            this.textBox_kol_iter_a_star.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBox_kol_iter_a_star.Location = new System.Drawing.Point(193, 58);
            this.textBox_kol_iter_a_star.Name = "textBox_kol_iter_a_star";
            this.textBox_kol_iter_a_star.Size = new System.Drawing.Size(132, 24);
            this.textBox_kol_iter_a_star.TabIndex = 16;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.button_step);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.button_Start);
            this.groupBox1.Controls.Add(this.textBox_kol_iter_a_star);
            this.groupBox1.Controls.Add(this.button_Save);
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
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label4.Location = new System.Drawing.Point(709, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(269, 18);
            this.label4.TabIndex = 20;
            this.label4.Text = "F5 - Запуск; F10 - Пошаговый запуск;";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label3.Location = new System.Drawing.Point(975, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(294, 18);
            this.label3.TabIndex = 19;
            this.label3.Text = "Cntrl+S - Сохранить; Esc - Закрыть окно;";
            // 
            // FormAlgorithm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 954);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FormAlgorithm";
            this.Text = "Запуск MAPF";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormAlgorithm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Button button_Save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_Name;
        private System.Windows.Forms.Label label_Error;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label_kol_iterat;
        private System.Windows.Forms.Button button_step;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_kol_iter_a_star;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}