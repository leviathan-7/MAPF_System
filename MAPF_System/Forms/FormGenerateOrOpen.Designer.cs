
namespace MAPF_System
{
    partial class FormGenerateOrOpen
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormGenerateOrOpen));
            this.textBox_X = new System.Windows.Forms.TextBox();
            this.textBox_Y = new System.Windows.Forms.TextBox();
            this.textBox_Blocks = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Units = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.button_Generation_Centr = new CustomControls.Style.RJButton();
            this.button_Generation_Dec = new CustomControls.Style.RJButton();
            this.label_Error = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.button_BigStart_Dec = new CustomControls.Style.RJButton();
            this.button_Load_Dec = new CustomControls.Style.RJButton();
            this.button_Load_Centr = new CustomControls.Style.RJButton();
            this.button_BigStart_Centr = new CustomControls.Style.RJButton();
            this.button_BigStart_Unite = new CustomControls.Style.RJButton();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox_X
            // 
            this.textBox_X.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBox_X.Location = new System.Drawing.Point(21, 68);
            this.textBox_X.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_X.Name = "textBox_X";
            this.textBox_X.Size = new System.Drawing.Size(175, 29);
            this.textBox_X.TabIndex = 1;
            // 
            // textBox_Y
            // 
            this.textBox_Y.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBox_Y.Location = new System.Drawing.Point(21, 162);
            this.textBox_Y.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Y.Name = "textBox_Y";
            this.textBox_Y.Size = new System.Drawing.Size(175, 29);
            this.textBox_Y.TabIndex = 2;
            // 
            // textBox_Blocks
            // 
            this.textBox_Blocks.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBox_Blocks.Location = new System.Drawing.Point(21, 258);
            this.textBox_Blocks.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Blocks.Name = "textBox_Blocks";
            this.textBox_Blocks.Size = new System.Drawing.Size(175, 29);
            this.textBox_Blocks.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label2.Location = new System.Drawing.Point(17, 130);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(203, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Размер поля Y <= 45 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label3.Location = new System.Drawing.Point(17, 228);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(244, 24);
            this.label3.TabIndex = 6;
            this.label3.Text = "Количество препятствий:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label1.Location = new System.Drawing.Point(17, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(205, 24);
            this.label1.TabIndex = 8;
            this.label1.Text = "Размер поля X <= 45 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label4.Location = new System.Drawing.Point(19, 318);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(195, 24);
            this.label4.TabIndex = 10;
            this.label4.Text = "Количество юнитов:";
            // 
            // textBox_Units
            // 
            this.textBox_Units.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBox_Units.Location = new System.Drawing.Point(23, 348);
            this.textBox_Units.Margin = new System.Windows.Forms.Padding(4);
            this.textBox_Units.Name = "textBox_Units";
            this.textBox_Units.Size = new System.Drawing.Size(175, 29);
            this.textBox_Units.TabIndex = 9;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.groupBox1.Location = new System.Drawing.Point(-25, -14);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1465, 17);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.groupBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox_Blocks);
            this.groupBox2.Controls.Add(this.textBox_X);
            this.groupBox2.Controls.Add(this.textBox_Y);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.textBox_Units);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(35, 31);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(276, 427);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label10.Location = new System.Drawing.Point(244, 9);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(45, 24);
            this.label10.TabIndex = 17;
            this.label10.Text = "       ";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label9.Location = new System.Drawing.Point(244, 14);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 24);
            this.label9.TabIndex = 17;
            this.label9.Text = "       ";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label8.Location = new System.Drawing.Point(237, 36);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(45, 24);
            this.label8.TabIndex = 17;
            this.label8.Text = "       ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label7.Location = new System.Drawing.Point(244, 58);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(45, 24);
            this.label7.TabIndex = 17;
            this.label7.Text = "       ";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label6.Location = new System.Drawing.Point(244, 80);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(45, 24);
            this.label6.TabIndex = 17;
            this.label6.Text = "       ";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label5.Location = new System.Drawing.Point(244, 102);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 24);
            this.label5.TabIndex = 16;
            this.label5.Text = "       ";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.groupBox3.Controls.Add(this.button_Generation_Centr);
            this.groupBox3.Controls.Add(this.button_Generation_Dec);
            this.groupBox3.Location = new System.Drawing.Point(185, 31);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(759, 127);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            // 
            // button_Generation_Centr
            // 
            this.button_Generation_Centr.BackColor = System.Drawing.Color.Orchid;
            this.button_Generation_Centr.BackgroundColor = System.Drawing.Color.Orchid;
            this.button_Generation_Centr.BorderColor = System.Drawing.Color.LightSlateGray;
            this.button_Generation_Centr.BorderRadius = 10;
            this.button_Generation_Centr.BorderSize = 1;
            this.button_Generation_Centr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Generation_Centr.FlatAppearance.BorderSize = 0;
            this.button_Generation_Centr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Generation_Centr.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_Generation_Centr.ForeColor = System.Drawing.Color.Black;
            this.button_Generation_Centr.Location = new System.Drawing.Point(461, 25);
            this.button_Generation_Centr.Margin = new System.Windows.Forms.Padding(4);
            this.button_Generation_Centr.Name = "button_Generation_Centr";
            this.button_Generation_Centr.Size = new System.Drawing.Size(256, 94);
            this.button_Generation_Centr.TabIndex = 20;
            this.button_Generation_Centr.Text = "⚙️ Сгенерировать поле с юнитами (централизованный)";
            this.button_Generation_Centr.TextColor = System.Drawing.Color.Black;
            this.button_Generation_Centr.UseVisualStyleBackColor = false;
            this.button_Generation_Centr.Click += new System.EventHandler(this.button_Generation_Click_Centr);
            // 
            // button_Generation_Dec
            // 
            this.button_Generation_Dec.BackColor = System.Drawing.Color.Orchid;
            this.button_Generation_Dec.BackgroundColor = System.Drawing.Color.Orchid;
            this.button_Generation_Dec.BorderColor = System.Drawing.Color.LightSlateGray;
            this.button_Generation_Dec.BorderRadius = 10;
            this.button_Generation_Dec.BorderSize = 1;
            this.button_Generation_Dec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Generation_Dec.FlatAppearance.BorderSize = 0;
            this.button_Generation_Dec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Generation_Dec.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_Generation_Dec.ForeColor = System.Drawing.Color.Black;
            this.button_Generation_Dec.Location = new System.Drawing.Point(171, 25);
            this.button_Generation_Dec.Margin = new System.Windows.Forms.Padding(4);
            this.button_Generation_Dec.Name = "button_Generation_Dec";
            this.button_Generation_Dec.Size = new System.Drawing.Size(256, 94);
            this.button_Generation_Dec.TabIndex = 17;
            this.button_Generation_Dec.Text = "⚙️ Сгенерировать поле с юнитами (децентрализованный)";
            this.button_Generation_Dec.TextColor = System.Drawing.Color.Black;
            this.button_Generation_Dec.UseVisualStyleBackColor = false;
            this.button_Generation_Dec.Click += new System.EventHandler(this.button_Generation_Click_Dec);
            // 
            // label_Error
            // 
            this.label_Error.AutoSize = true;
            this.label_Error.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label_Error.ForeColor = System.Drawing.Color.Red;
            this.label_Error.Location = new System.Drawing.Point(357, 418);
            this.label_Error.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label_Error.Name = "label_Error";
            this.label_Error.Size = new System.Drawing.Size(0, 24);
            this.label_Error.TabIndex = 11;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 44.25F);
            this.label11.Location = new System.Drawing.Point(1262, 316);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(0, 85);
            this.label11.TabIndex = 18;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 44.25F);
            this.label12.Location = new System.Drawing.Point(974, 188);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 85);
            this.label12.TabIndex = 19;
            // 
            // button_BigStart_Dec
            // 
            this.button_BigStart_Dec.BackColor = System.Drawing.Color.SkyBlue;
            this.button_BigStart_Dec.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.button_BigStart_Dec.BorderColor = System.Drawing.Color.LightSlateGray;
            this.button_BigStart_Dec.BorderRadius = 10;
            this.button_BigStart_Dec.BorderSize = 1;
            this.button_BigStart_Dec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_BigStart_Dec.FlatAppearance.BorderSize = 0;
            this.button_BigStart_Dec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_BigStart_Dec.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_BigStart_Dec.ForeColor = System.Drawing.Color.Black;
            this.button_BigStart_Dec.Location = new System.Drawing.Point(356, 316);
            this.button_BigStart_Dec.Margin = new System.Windows.Forms.Padding(4);
            this.button_BigStart_Dec.Name = "button_BigStart_Dec";
            this.button_BigStart_Dec.Size = new System.Drawing.Size(256, 98);
            this.button_BigStart_Dec.TabIndex = 17;
            this.button_BigStart_Dec.Text = "▶▶▶ Запустить все поля в папке (децентрализованный)";
            this.button_BigStart_Dec.TextColor = System.Drawing.Color.Black;
            this.button_BigStart_Dec.UseVisualStyleBackColor = false;
            this.button_BigStart_Dec.Click += new System.EventHandler(this.button_BigStart_Click_Dec);
            // 
            // button_Load_Dec
            // 
            this.button_Load_Dec.BackColor = System.Drawing.Color.SkyBlue;
            this.button_Load_Dec.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.button_Load_Dec.BorderColor = System.Drawing.Color.LightSlateGray;
            this.button_Load_Dec.BorderRadius = 10;
            this.button_Load_Dec.BorderSize = 1;
            this.button_Load_Dec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Load_Dec.FlatAppearance.BorderSize = 0;
            this.button_Load_Dec.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Load_Dec.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_Load_Dec.ForeColor = System.Drawing.Color.Black;
            this.button_Load_Dec.Location = new System.Drawing.Point(356, 193);
            this.button_Load_Dec.Margin = new System.Windows.Forms.Padding(4);
            this.button_Load_Dec.Name = "button_Load_Dec";
            this.button_Load_Dec.Size = new System.Drawing.Size(256, 78);
            this.button_Load_Dec.TabIndex = 16;
            this.button_Load_Dec.Text = "🕹️ Загрузить поле (децентрализованный)";
            this.button_Load_Dec.TextColor = System.Drawing.Color.Black;
            this.button_Load_Dec.UseVisualStyleBackColor = false;
            this.button_Load_Dec.Click += new System.EventHandler(this.button_Load_Click_Dec);
            // 
            // button_Load_Centr
            // 
            this.button_Load_Centr.BackColor = System.Drawing.Color.SkyBlue;
            this.button_Load_Centr.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.button_Load_Centr.BorderColor = System.Drawing.Color.LightSlateGray;
            this.button_Load_Centr.BorderRadius = 10;
            this.button_Load_Centr.BorderSize = 1;
            this.button_Load_Centr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Load_Centr.FlatAppearance.BorderSize = 0;
            this.button_Load_Centr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_Load_Centr.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_Load_Centr.ForeColor = System.Drawing.Color.Black;
            this.button_Load_Centr.Location = new System.Drawing.Point(646, 195);
            this.button_Load_Centr.Margin = new System.Windows.Forms.Padding(4);
            this.button_Load_Centr.Name = "button_Load_Centr";
            this.button_Load_Centr.Size = new System.Drawing.Size(256, 78);
            this.button_Load_Centr.TabIndex = 20;
            this.button_Load_Centr.Text = "🕹️ Загрузить поле (централизованный)";
            this.button_Load_Centr.TextColor = System.Drawing.Color.Black;
            this.button_Load_Centr.UseVisualStyleBackColor = false;
            this.button_Load_Centr.Click += new System.EventHandler(this.button_Load_Click_Centr);
            // 
            // button_BigStart_Centr
            // 
            this.button_BigStart_Centr.BackColor = System.Drawing.Color.SkyBlue;
            this.button_BigStart_Centr.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.button_BigStart_Centr.BorderColor = System.Drawing.Color.LightSlateGray;
            this.button_BigStart_Centr.BorderRadius = 10;
            this.button_BigStart_Centr.BorderSize = 1;
            this.button_BigStart_Centr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_BigStart_Centr.FlatAppearance.BorderSize = 0;
            this.button_BigStart_Centr.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_BigStart_Centr.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_BigStart_Centr.ForeColor = System.Drawing.Color.Black;
            this.button_BigStart_Centr.Location = new System.Drawing.Point(646, 318);
            this.button_BigStart_Centr.Margin = new System.Windows.Forms.Padding(4);
            this.button_BigStart_Centr.Name = "button_BigStart_Centr";
            this.button_BigStart_Centr.Size = new System.Drawing.Size(256, 98);
            this.button_BigStart_Centr.TabIndex = 21;
            this.button_BigStart_Centr.Text = "▶▶▶ Запустить все поля в папке (централизованный)";
            this.button_BigStart_Centr.TextColor = System.Drawing.Color.Black;
            this.button_BigStart_Centr.UseVisualStyleBackColor = false;
            this.button_BigStart_Centr.Click += new System.EventHandler(this.button_BigStart_Click_Centr);
            // 
            // button_BigStart_Unite
            // 
            this.button_BigStart_Unite.BackColor = System.Drawing.Color.SkyBlue;
            this.button_BigStart_Unite.BackgroundColor = System.Drawing.Color.SkyBlue;
            this.button_BigStart_Unite.BorderColor = System.Drawing.Color.LightSlateGray;
            this.button_BigStart_Unite.BorderRadius = 10;
            this.button_BigStart_Unite.BorderSize = 1;
            this.button_BigStart_Unite.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_BigStart_Unite.FlatAppearance.BorderSize = 0;
            this.button_BigStart_Unite.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_BigStart_Unite.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_BigStart_Unite.ForeColor = System.Drawing.Color.Black;
            this.button_BigStart_Unite.Location = new System.Drawing.Point(935, 318);
            this.button_BigStart_Unite.Margin = new System.Windows.Forms.Padding(4);
            this.button_BigStart_Unite.Name = "button_BigStart_Unite";
            this.button_BigStart_Unite.Size = new System.Drawing.Size(256, 98);
            this.button_BigStart_Unite.TabIndex = 22;
            this.button_BigStart_Unite.Text = "▶▶▶ Запустить все поля в папке (комбинированный)";
            this.button_BigStart_Unite.TextColor = System.Drawing.Color.Black;
            this.button_BigStart_Unite.UseVisualStyleBackColor = false;
            this.button_BigStart_Unite.Click += new System.EventHandler(this.button_BigStart_Click_Unite);
            // 
            // FormGenerateOrOpen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1406, 634);
            this.Controls.Add(this.button_BigStart_Unite);
            this.Controls.Add(this.button_BigStart_Centr);
            this.Controls.Add(this.button_Load_Centr);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.button_BigStart_Dec);
            this.Controls.Add(this.button_Load_Dec);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label_Error);
            this.Controls.Add(this.groupBox3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGenerateOrOpen";
            this.Text = "Создание или загрузка поля для алгоритма MAPF";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.FormGenerateOrOpen_HelpButtonClicked);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGenerateOrOpen_KeyDown);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox_X;
        private System.Windows.Forms.TextBox textBox_Y;
        private System.Windows.Forms.TextBox textBox_Blocks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_Units;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_Error;
        private CustomControls.Style.RJButton button_Load_Dec;
        private CustomControls.Style.RJButton button_Generation_Dec;
        private CustomControls.Style.RJButton button_BigStart_Dec;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private CustomControls.Style.RJButton button_Generation_Centr;
        private CustomControls.Style.RJButton button_Load_Centr;
        private CustomControls.Style.RJButton button_BigStart_Centr;
        private CustomControls.Style.RJButton button_BigStart_Unite;
    }
}

