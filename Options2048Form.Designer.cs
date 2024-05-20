namespace KrypLauncher

{
    partial class Options2048Form
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Options2048Form));
            bOK = new Button();
            lMatrixSize = new Label();
            nudRows = new NumericUpDown();
            lRows = new Label();
            lCells = new Label();
            nudCells = new NumericUpDown();
            lTileSize = new Label();
            nudTileSize = new NumericUpDown();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            nudInterval2 = new NumericUpDown();
            lInt32erval2 = new Label();
            nudInterval1 = new NumericUpDown();
            lInt32erval1 = new Label();
            cbEllipse = new CheckBox();
            toolTip1 = new ToolTip(components);
            bInfo = new Button();
            bClose = new Button();
            ((System.ComponentModel.ISupportInitialize)nudRows).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudCells).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudTileSize).BeginInit();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudInterval2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudInterval1).BeginInit();
            SuspendLayout();
            // 
            // bOK
            // 
            bOK.FlatAppearance.BorderSize = 3;
            bOK.FlatStyle = FlatStyle.System;
            bOK.Font = new Font("Tahoma", 12.75F, FontStyle.Bold, GraphicsUnit.Point);
            bOK.Location = new Point(572, 410);
            bOK.Margin = new Padding(4, 3, 4, 3);
            bOK.Name = "bOK";
            bOK.Size = new Size(105, 38);
            bOK.TabIndex = 0;
            bOK.Text = "OK";
            toolTip1.SetToolTip(bOK, "Предупреждение: при нажатии кнопки \"ОК\", предыдущая игра будет завершена без сохранения. Чтобы вернуться к игре, используйте крестик.");
            bOK.UseVisualStyleBackColor = true;
            bOK.Click += bOK_Click;
            // 
            // lMatrixSize
            // 
            lMatrixSize.AutoSize = true;
            lMatrixSize.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            lMatrixSize.Location = new Point(36, 16);
            lMatrixSize.Margin = new Padding(4, 0, 4, 0);
            lMatrixSize.Name = "lMatrixSize";
            lMatrixSize.Size = new Size(161, 18);
            lMatrixSize.TabIndex = 1;
            lMatrixSize.Text = "Розмір ігрового поля:";
            // 
            // nudRows
            // 
            nudRows.BackColor = Color.Gainsboro;
            nudRows.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            nudRows.Location = new Point(132, 40);
            nudRows.Margin = new Padding(4, 3, 4, 3);
            nudRows.Maximum = new decimal(new int[] { 25, 0, 0, 0 });
            nudRows.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            nudRows.Name = "nudRows";
            nudRows.Size = new Size(66, 26);
            nudRows.TabIndex = 2;
            nudRows.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // lRows
            // 
            lRows.AutoSize = true;
            lRows.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lRows.Location = new Point(36, 42);
            lRows.Margin = new Padding(4, 0, 4, 0);
            lRows.Name = "lRows";
            lRows.Size = new Size(49, 16);
            lRows.TabIndex = 3;
            lRows.Text = "Рядки:";
            // 
            // lCells
            // 
            lCells.AutoSize = true;
            lCells.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            lCells.Location = new Point(36, 80);
            lCells.Margin = new Padding(4, 0, 4, 0);
            lCells.Name = "lCells";
            lCells.Size = new Size(61, 16);
            lCells.TabIndex = 5;
            lCells.Text = "Стовпці:";
            // 
            // nudCells
            // 
            nudCells.BackColor = Color.Gainsboro;
            nudCells.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            nudCells.Location = new Point(132, 78);
            nudCells.Margin = new Padding(4, 3, 4, 3);
            nudCells.Maximum = new decimal(new int[] { 25, 0, 0, 0 });
            nudCells.Minimum = new decimal(new int[] { 2, 0, 0, 0 });
            nudCells.Name = "nudCells";
            nudCells.Size = new Size(66, 26);
            nudCells.TabIndex = 4;
            nudCells.Value = new decimal(new int[] { 4, 0, 0, 0 });
            // 
            // lTileSize
            // 
            lTileSize.AutoSize = true;
            lTileSize.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            lTileSize.Location = new Point(54, 32);
            lTileSize.Margin = new Padding(4, 0, 4, 0);
            lTileSize.Name = "lTileSize";
            lTileSize.Size = new Size(166, 18);
            lTileSize.TabIndex = 6;
            lTileSize.Text = "Розмір ігрової клітини:";
            // 
            // nudTileSize
            // 
            nudTileSize.BackColor = Color.Gainsboro;
            nudTileSize.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point);
            nudTileSize.Location = new Point(98, 65);
            nudTileSize.Margin = new Padding(4, 3, 4, 3);
            nudTileSize.Maximum = new decimal(new int[] { 200, 0, 0, 0 });
            nudTileSize.Minimum = new decimal(new int[] { 50, 0, 0, 0 });
            nudTileSize.Name = "nudTileSize";
            nudTileSize.Size = new Size(66, 26);
            nudTileSize.TabIndex = 7;
            nudTileSize.Value = new decimal(new int[] { 70, 0, 0, 0 });
            // 
            // panel1
            // 
            panel1.BackColor = Color.LightSteelBlue;
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(nudRows);
            panel1.Controls.Add(lRows);
            panel1.Controls.Add(lCells);
            panel1.Controls.Add(lMatrixSize);
            panel1.Controls.Add(nudCells);
            panel1.Location = new Point(14, 14);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(281, 151);
            panel1.TabIndex = 8;
            // 
            // panel2
            // 
            panel2.BackColor = Color.LightSteelBlue;
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(lTileSize);
            panel2.Controls.Add(nudTileSize);
            panel2.Location = new Point(359, 14);
            panel2.Margin = new Padding(4, 3, 4, 3);
            panel2.Name = "panel2";
            panel2.Size = new Size(281, 151);
            panel2.TabIndex = 9;
            // 
            // panel3
            // 
            panel3.BackColor = Color.LightSteelBlue;
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(nudInterval2);
            panel3.Controls.Add(lInt32erval2);
            panel3.Controls.Add(nudInterval1);
            panel3.Controls.Add(lInt32erval1);
            panel3.Location = new Point(182, 174);
            panel3.Margin = new Padding(4, 3, 4, 3);
            panel3.Name = "panel3";
            panel3.Size = new Size(281, 151);
            panel3.TabIndex = 10;
            // 
            // nudInterval2
            // 
            nudInterval2.BackColor = Color.Gainsboro;
            nudInterval2.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            nudInterval2.Location = new Point(106, 102);
            nudInterval2.Margin = new Padding(4, 3, 4, 3);
            nudInterval2.Name = "nudInterval2";
            nudInterval2.Size = new Size(66, 22);
            nudInterval2.TabIndex = 10;
            nudInterval2.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lInt32erval2
            // 
            lInt32erval2.AutoSize = true;
            lInt32erval2.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lInt32erval2.Location = new Point(71, 81);
            lInt32erval2.Margin = new Padding(4, 0, 4, 0);
            lInt32erval2.Name = "lInt32erval2";
            lInt32erval2.Size = new Size(126, 15);
            lInt32erval2.TabIndex = 9;
            lInt32erval2.Text = "Інтервал до матриці";
            // 
            // nudInterval1
            // 
            nudInterval1.BackColor = Color.Gainsboro;
            nudInterval1.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            nudInterval1.Location = new Point(106, 40);
            nudInterval1.Margin = new Padding(4, 3, 4, 3);
            nudInterval1.Name = "nudInterval1";
            nudInterval1.Size = new Size(66, 22);
            nudInterval1.TabIndex = 8;
            nudInterval1.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // lInt32erval1
            // 
            lInt32erval1.AutoSize = true;
            lInt32erval1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            lInt32erval1.Location = new Point(57, 20);
            lInt32erval1.Margin = new Padding(4, 0, 4, 0);
            lInt32erval1.Name = "lInt32erval1";
            lInt32erval1.Size = new Size(156, 15);
            lInt32erval1.TabIndex = 7;
            lInt32erval1.Text = "Інтерваaл між клітинами:";
            // 
            // cbEllipse
            // 
            cbEllipse.AutoSize = true;
            cbEllipse.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            cbEllipse.Location = new Point(231, 332);
            cbEllipse.Margin = new Padding(4, 3, 4, 3);
            cbEllipse.Name = "cbEllipse";
            cbEllipse.Size = new Size(149, 25);
            cbEllipse.TabIndex = 12;
            cbEllipse.Text = "Круглі плитaки";
            cbEllipse.UseVisualStyleBackColor = true;
            // 
            // bInfo
            // 
            bInfo.FlatAppearance.BorderSize = 3;
            bInfo.FlatStyle = FlatStyle.System;
            bInfo.Font = new Font("Tahoma", 12.75F, FontStyle.Bold, GraphicsUnit.Point);
            bInfo.Location = new Point(14, 410);
            bInfo.Margin = new Padding(4, 3, 4, 3);
            bInfo.Name = "bInfo";
            bInfo.Size = new Size(105, 38);
            bInfo.TabIndex = 13;
            bInfo.Text = "Info";
            toolTip1.SetToolTip(bInfo, "Предупреждение: при нажатии кнопки \"ОК\", предыдущая игра будет завершена без сохранения. Чтобы вернуться к игре, используйте крестик.");
            bInfo.UseVisualStyleBackColor = true;
            bInfo.Click += bInfo_Click;
            // 
            // bClose
            // 
            bClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bClose.BackColor = Color.Transparent;
            bClose.BackgroundImage = Properties.Resources.SquareClose;
            bClose.BackgroundImageLayout = ImageLayout.Stretch;
            bClose.FlatAppearance.BorderSize = 0;
            bClose.FlatStyle = FlatStyle.Flat;
            bClose.Location = new Point(653, 8);
            bClose.Margin = new Padding(4, 3, 4, 3);
            bClose.Name = "bClose";
            bClose.Size = new Size(29, 29);
            bClose.TabIndex = 11;
            bClose.UseVisualStyleBackColor = false;
            bClose.Click += bClose_Click;
            // 
            // Options2048Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Silver;
            ClientSize = new Size(691, 462);
            Controls.Add(bInfo);
            Controls.Add(cbEllipse);
            Controls.Add(bClose);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(bOK);
            FormBorderStyle = FormBorderStyle.None;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Options2048Form";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Настройки";
            FormClosing += StartForm_FormClosing;
            Load += StartForm_Load;
            MouseDown += OptionsForm_MouseDown;
            ((System.ComponentModel.ISupportInitialize)nudRows).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudCells).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudTileSize).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudInterval2).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudInterval1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bOK;
        private Label lMatrixSize;
        private NumericUpDown nudRows;
        private Label lRows;
        private Label lCells;
        private NumericUpDown nudCells;
        private Label lTileSize;
        private NumericUpDown nudTileSize;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Label lInt32erval1;
        private NumericUpDown nudInterval1;
        private NumericUpDown nudInterval2;
        private Label lInt32erval2;
        private Button bClose;
        private CheckBox cbEllipse;
        private ToolTip toolTip1;
        private Button bInfo;
    }
}