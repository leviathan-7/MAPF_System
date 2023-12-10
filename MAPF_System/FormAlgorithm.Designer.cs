
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
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_Start
            // 
            this.button_Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_Start.Location = new System.Drawing.Point(12, 12);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(143, 45);
            this.button_Start.TabIndex = 1;
            this.button_Start.Text = "Запустить";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // button_Save
            // 
            this.button_Save.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_Save.Location = new System.Drawing.Point(797, 12);
            this.button_Save.Name = "button_Save";
            this.button_Save.Size = new System.Drawing.Size(143, 45);
            this.button_Save.TabIndex = 2;
            this.button_Save.Text = "Сохранить";
            this.button_Save.UseVisualStyleBackColor = true;
            this.button_Save.Click += new System.EventHandler(this.button_Save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label1.Location = new System.Drawing.Point(632, -2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(105, 18);
            this.label1.TabIndex = 10;
            this.label1.Text = "Введите имя: ";
            // 
            // textBox_Name
            // 
            this.textBox_Name.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBox_Name.Location = new System.Drawing.Point(635, 33);
            this.textBox_Name.Name = "textBox_Name";
            this.textBox_Name.Size = new System.Drawing.Size(132, 24);
            this.textBox_Name.TabIndex = 9;
            // 
            // label_Error
            // 
            this.label_Error.AutoSize = true;
            this.label_Error.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label_Error.ForeColor = System.Drawing.Color.Red;
            this.label_Error.Location = new System.Drawing.Point(971, 33);
            this.label_Error.Name = "label_Error";
            this.label_Error.Size = new System.Drawing.Size(0, 18);
            this.label_Error.TabIndex = 12;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(323, 47);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(10, 10);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // label_kol_iterat
            // 
            this.label_kol_iterat.AutoSize = true;
            this.label_kol_iterat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label_kol_iterat.Location = new System.Drawing.Point(971, 9);
            this.label_kol_iterat.Name = "label_kol_iterat";
            this.label_kol_iterat.Size = new System.Drawing.Size(12, 18);
            this.label_kol_iterat.TabIndex = 14;
            this.label_kol_iterat.Text = " ";
            // 
            // button_step
            // 
            this.button_step.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_step.Location = new System.Drawing.Point(433, 12);
            this.button_step.Name = "button_step";
            this.button_step.Size = new System.Drawing.Size(143, 45);
            this.button_step.TabIndex = 15;
            this.button_step.Text = "Пошагово";
            this.button_step.UseVisualStyleBackColor = true;
            this.button_step.Click += new System.EventHandler(this.button_step_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label2.Location = new System.Drawing.Point(158, -1);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(238, 18);
            this.label2.TabIndex = 17;
            this.label2.Text = "Глубина просмотра (от 7 до 15): ";
            // 
            // textBox_kol_iter_a_star
            // 
            this.textBox_kol_iter_a_star.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBox_kol_iter_a_star.Location = new System.Drawing.Point(175, 33);
            this.textBox_kol_iter_a_star.Name = "textBox_kol_iter_a_star";
            this.textBox_kol_iter_a_star.Size = new System.Drawing.Size(132, 24);
            this.textBox_kol_iter_a_star.TabIndex = 16;
            // 
            // FormAlgorithm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1261, 954);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_kol_iter_a_star);
            this.Controls.Add(this.button_step);
            this.Controls.Add(this.label_kol_iterat);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label_Error);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_Name);
            this.Controls.Add(this.button_Save);
            this.Controls.Add(this.button_Start);
            this.Name = "FormAlgorithm";
            this.Text = "Запуск MAPF";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}