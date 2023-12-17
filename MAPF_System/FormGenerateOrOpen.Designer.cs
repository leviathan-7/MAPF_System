
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
            this.button_Generation = new System.Windows.Forms.Button();
            this.textBox_X = new System.Windows.Forms.TextBox();
            this.textBox_Y = new System.Windows.Forms.TextBox();
            this.textBox_Blocks = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_Units = new System.Windows.Forms.TextBox();
            this.label_Error = new System.Windows.Forms.Label();
            this.button_Load = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // button_Generation
            // 
            this.button_Generation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Generation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_Generation.Location = new System.Drawing.Point(271, 42);
            this.button_Generation.Name = "button_Generation";
            this.button_Generation.Size = new System.Drawing.Size(158, 63);
            this.button_Generation.TabIndex = 0;
            this.button_Generation.Text = "Сгенерировать поле с юнитами";
            this.button_Generation.UseVisualStyleBackColor = true;
            this.button_Generation.Click += new System.EventHandler(this.button_Generation_Click);
            // 
            // textBox_X
            // 
            this.textBox_X.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBox_X.Location = new System.Drawing.Point(54, 77);
            this.textBox_X.Name = "textBox_X";
            this.textBox_X.Size = new System.Drawing.Size(132, 24);
            this.textBox_X.TabIndex = 1;
            // 
            // textBox_Y
            // 
            this.textBox_Y.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBox_Y.Location = new System.Drawing.Point(54, 154);
            this.textBox_Y.Name = "textBox_Y";
            this.textBox_Y.Size = new System.Drawing.Size(132, 24);
            this.textBox_Y.TabIndex = 2;
            // 
            // textBox_Blocks
            // 
            this.textBox_Blocks.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBox_Blocks.Location = new System.Drawing.Point(54, 232);
            this.textBox_Blocks.Name = "textBox_Blocks";
            this.textBox_Blocks.Size = new System.Drawing.Size(132, 24);
            this.textBox_Blocks.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label2.Location = new System.Drawing.Point(51, 128);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(158, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "Размер поля Y <=50 :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label3.Location = new System.Drawing.Point(51, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(186, 18);
            this.label3.TabIndex = 6;
            this.label3.Text = "Количество препятствий:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label1.Location = new System.Drawing.Point(51, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "Размер поля X <=50 :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label4.Location = new System.Drawing.Point(52, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(151, 18);
            this.label4.TabIndex = 10;
            this.label4.Text = "Количество юнитов:";
            // 
            // textBox_Units
            // 
            this.textBox_Units.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.textBox_Units.Location = new System.Drawing.Point(55, 305);
            this.textBox_Units.Name = "textBox_Units";
            this.textBox_Units.Size = new System.Drawing.Size(132, 24);
            this.textBox_Units.TabIndex = 9;
            // 
            // label_Error
            // 
            this.label_Error.AutoSize = true;
            this.label_Error.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.label_Error.ForeColor = System.Drawing.Color.Red;
            this.label_Error.Location = new System.Drawing.Point(268, 311);
            this.label_Error.Name = "label_Error";
            this.label_Error.Size = new System.Drawing.Size(0, 18);
            this.label_Error.TabIndex = 11;
            // 
            // button_Load
            // 
            this.button_Load.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_Load.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.button_Load.Location = new System.Drawing.Point(271, 154);
            this.button_Load.Name = "button_Load";
            this.button_Load.Size = new System.Drawing.Size(158, 63);
            this.button_Load.TabIndex = 12;
            this.button_Load.Text = "Загрузить поле";
            this.button_Load.UseVisualStyleBackColor = true;
            this.button_Load.Click += new System.EventHandler(this.button_Load_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(236)))), ((int)(((byte)(236)))));
            this.groupBox1.Location = new System.Drawing.Point(-19, -11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1099, 14);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            // 
            // FormGenerateOrOpen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 477);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button_Load);
            this.Controls.Add(this.label_Error);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_Units);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_Blocks);
            this.Controls.Add(this.textBox_Y);
            this.Controls.Add(this.textBox_X);
            this.Controls.Add(this.button_Generation);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGenerateOrOpen";
            this.Text = "Создание или загрузка поля";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.FormGenerateOrOpen_HelpButtonClicked);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGenerateOrOpen_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Generation;
        private System.Windows.Forms.TextBox textBox_X;
        private System.Windows.Forms.TextBox textBox_Y;
        private System.Windows.Forms.TextBox textBox_Blocks;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_Units;
        private System.Windows.Forms.Label label_Error;
        private System.Windows.Forms.Button button_Load;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}

