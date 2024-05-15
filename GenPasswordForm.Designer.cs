namespace KrypLauncher
{
    partial class GenPasswordForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            outTextBox = new TextBox();
            generateBut = new Button();
            NumbersBox = new CheckBox();
            TextLittleBox = new CheckBox();
            TextBigBox = new CheckBox();
            SpecialSymbolsBox = new CheckBox();
            LenghtLabel = new Label();
            numericUpDown1 = new NumericUpDown();
            label1 = new Label();
            label2 = new Label();
            textBox1 = new TextBox();
            radioButton3 = new RadioButton();
            radioButton4 = new RadioButton();
            groupBox2 = new GroupBox();
            radioButton1 = new RadioButton();
            radioButton2 = new RadioButton();
            groupBox1 = new GroupBox();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // outTextBox
            // 
            outTextBox.Location = new Point(143, 59);
            outTextBox.Name = "outTextBox";
            outTextBox.ReadOnly = true;
            outTextBox.Size = new Size(200, 23);
            outTextBox.TabIndex = 0;
            // 
            // generateBut
            // 
            generateBut.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            generateBut.Location = new Point(143, 287);
            generateBut.Name = "generateBut";
            generateBut.Size = new Size(200, 50);
            generateBut.TabIndex = 1;
            generateBut.Text = "Сгенерировать пароль";
            generateBut.UseVisualStyleBackColor = true;
            generateBut.Click += generateBut_Click;
            // 
            // NumbersBox
            // 
            NumbersBox.AutoSize = true;
            NumbersBox.Location = new Point(20, 152);
            NumbersBox.Name = "NumbersBox";
            NumbersBox.Size = new Size(83, 19);
            NumbersBox.TabIndex = 3;
            NumbersBox.Text = "checkBox1";
            NumbersBox.UseVisualStyleBackColor = true;
            // 
            // TextLittleBox
            // 
            TextLittleBox.AutoSize = true;
            TextLittleBox.Location = new Point(20, 177);
            TextLittleBox.Name = "TextLittleBox";
            TextLittleBox.Size = new Size(83, 19);
            TextLittleBox.TabIndex = 4;
            TextLittleBox.Text = "checkBox2";
            TextLittleBox.UseVisualStyleBackColor = true;
            // 
            // TextBigBox
            // 
            TextBigBox.AutoSize = true;
            TextBigBox.Location = new Point(20, 202);
            TextBigBox.Name = "TextBigBox";
            TextBigBox.Size = new Size(83, 19);
            TextBigBox.TabIndex = 5;
            TextBigBox.Text = "checkBox3";
            TextBigBox.UseVisualStyleBackColor = true;
            // 
            // SpecialSymbolsBox
            // 
            SpecialSymbolsBox.AutoSize = true;
            SpecialSymbolsBox.Location = new Point(20, 227);
            SpecialSymbolsBox.Name = "SpecialSymbolsBox";
            SpecialSymbolsBox.Size = new Size(83, 19);
            SpecialSymbolsBox.TabIndex = 6;
            SpecialSymbolsBox.Text = "checkBox4";
            SpecialSymbolsBox.UseVisualStyleBackColor = true;
            // 
            // LenghtLabel
            // 
            LenghtLabel.AutoSize = true;
            LenghtLabel.Location = new Point(20, 105);
            LenghtLabel.Name = "LenghtLabel";
            LenghtLabel.Size = new Size(153, 15);
            LenghtLabel.TabIndex = 7;
            LenghtLabel.Text = "Длина желаемого пароля:";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(66, 123);
            numericUpDown1.Minimum = new decimal(new int[] { 8, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(50, 23);
            numericUpDown1.TabIndex = 8;
            numericUpDown1.Value = new decimal(new int[] { 8, 0, 0, 0 });
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(281, 100);
            label1.Name = "label1";
            label1.Size = new Size(155, 15);
            label1.TabIndex = 9;
            label1.Text = "Сохранять пароли в файл?";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(282, 153);
            label2.Name = "label2";
            label2.Size = new Size(166, 15);
            label2.TabIndex = 12;
            label2.Text = "Выберите куда их сохранять:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(282, 227);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(166, 23);
            textBox1.TabIndex = 15;
            textBox1.Visible = false;
            // 
            // radioButton3
            // 
            radioButton3.AutoSize = true;
            radioButton3.Location = new Point(6, 7);
            radioButton3.Name = "radioButton3";
            radioButton3.Size = new Size(99, 19);
            radioButton3.TabIndex = 13;
            radioButton3.TabStop = true;
            radioButton3.Text = "Дефолт файл";
            radioButton3.UseVisualStyleBackColor = true;
            radioButton3.CheckedChanged += radioButton3_CheckedChanged;
            // 
            // radioButton4
            // 
            radioButton4.AutoSize = true;
            radioButton4.Location = new Point(6, 31);
            radioButton4.Name = "radioButton4";
            radioButton4.Size = new Size(83, 19);
            radioButton4.TabIndex = 14;
            radioButton4.TabStop = true;
            radioButton4.Text = "Свой путь:";
            radioButton4.UseVisualStyleBackColor = true;
            radioButton4.CheckedChanged += radioButton4_CheckedChanged;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(radioButton4);
            groupBox2.Controls.Add(radioButton3);
            groupBox2.FlatStyle = FlatStyle.Flat;
            groupBox2.Location = new Point(282, 171);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(131, 55);
            groupBox2.TabIndex = 17;
            groupBox2.TabStop = false;
            // 
            // radioButton1
            // 
            radioButton1.AutoSize = true;
            radioButton1.Location = new Point(16, 12);
            radioButton1.Name = "radioButton1";
            radioButton1.Size = new Size(39, 19);
            radioButton1.TabIndex = 10;
            radioButton1.TabStop = true;
            radioButton1.Text = "Да";
            radioButton1.UseVisualStyleBackColor = true;
            radioButton1.CheckedChanged += radioButton1_CheckedChanged;
            // 
            // radioButton2
            // 
            radioButton2.AutoSize = true;
            radioButton2.Location = new Point(72, 12);
            radioButton2.Name = "radioButton2";
            radioButton2.Size = new Size(45, 19);
            radioButton2.TabIndex = 11;
            radioButton2.TabStop = true;
            radioButton2.Text = "Нет";
            radioButton2.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButton2);
            groupBox1.Controls.Add(radioButton1);
            groupBox1.FlatStyle = FlatStyle.Flat;
            groupBox1.Location = new Point(282, 118);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(128, 35);
            groupBox1.TabIndex = 16;
            groupBox1.TabStop = false;
            // 
            // GenPasswordForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(484, 361);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(numericUpDown1);
            Controls.Add(LenghtLabel);
            Controls.Add(SpecialSymbolsBox);
            Controls.Add(TextBigBox);
            Controls.Add(TextLittleBox);
            Controls.Add(NumbersBox);
            Controls.Add(generateBut);
            Controls.Add(outTextBox);
            Name = "GenPasswordForm";
            Text = "Генерация желаемого пароля";
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox outTextBox;
        private Button generateBut;
        private CheckBox NumbersBox;
        private CheckBox TextLittleBox;
        private CheckBox TextBigBox;
        private CheckBox SpecialSymbolsBox;
        private Label LenghtLabel;
        private NumericUpDown numericUpDown1;
        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private RadioButton radioButton3;
        private RadioButton radioButton4;
        private GroupBox groupBox2;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private GroupBox groupBox1;
    }
}