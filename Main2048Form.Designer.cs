namespace KrypLauncher
{
    partial class Main2048Form
    {
        /// <summary>
        /// Требуется переменная конструктора.
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
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            pMenu = new Panel();
            bClose = new NonFocusButton();
            bUndo = new NonFocusButton();
            bOptions = new NonFocusButton();
            bBest = new NonFocusButton();
            bScore = new NonFocusButton();
            bNewGame = new NonFocusButton();
            pField = new Panel();
            pMatrix = new Panel();
            pMenu.SuspendLayout();
            pField.SuspendLayout();
            SuspendLayout();
            // 
            // pMenu
            // 
            pMenu.BackColor = Color.Silver;
            pMenu.Controls.Add(bClose);
            pMenu.Controls.Add(bUndo);
            pMenu.Controls.Add(bOptions);
            pMenu.Controls.Add(bBest);
            pMenu.Controls.Add(bScore);
            pMenu.Controls.Add(bNewGame);
            pMenu.Dock = DockStyle.Top;
            pMenu.Location = new Point(0, 0);
            pMenu.Margin = new Padding(4, 3, 4, 3);
            pMenu.Name = "pMenu";
            pMenu.Size = new Size(370, 145);
            pMenu.TabIndex = 1;
            pMenu.Paint += pMenu_Paint;
            // 
            // bClose
            // 
            bClose.BackColor = Color.Gainsboro;
            bClose.BackgroundImage = Properties.Resources.SquareClose;
            bClose.BackgroundImageLayout = ImageLayout.Stretch;
            bClose.FlatAppearance.BorderSize = 4;
            bClose.FlatAppearance.MouseDownBackColor = Color.Silver;
            bClose.FlatAppearance.MouseOverBackColor = Color.Silver;
            bClose.FlatStyle = FlatStyle.Flat;
            bClose.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Pixel);
            bClose.Location = new Point(14, 14);
            bClose.Margin = new Padding(4, 3, 4, 3);
            bClose.Name = "bClose";
            bClose.Size = new Size(58, 58);
            bClose.TabIndex = 1;
            bClose.UseVisualStyleBackColor = false;
            bClose.Click += bClose_Click;
            // 
            // bUndo
            // 
            bUndo.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            bUndo.BackColor = Color.Tomato;
            bUndo.FlatAppearance.BorderSize = 0;
            bUndo.FlatAppearance.MouseDownBackColor = Color.Red;
            bUndo.FlatAppearance.MouseOverBackColor = Color.Red;
            bUndo.FlatStyle = FlatStyle.Flat;
            bUndo.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Pixel);
            bUndo.Location = new Point(240, 93);
            bUndo.Margin = new Padding(4, 3, 4, 3);
            bUndo.Name = "bUndo";
            bUndo.Size = new Size(117, 38);
            bUndo.TabIndex = 0;
            bUndo.Text = "Undo";
            bUndo.UseVisualStyleBackColor = false;
            bUndo.Click += bUndo_Click;
            // 
            // bOptions
            // 
            bOptions.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            bOptions.BackColor = Color.Tomato;
            bOptions.FlatAppearance.BorderSize = 0;
            bOptions.FlatAppearance.MouseDownBackColor = Color.Red;
            bOptions.FlatAppearance.MouseOverBackColor = Color.Red;
            bOptions.FlatStyle = FlatStyle.Flat;
            bOptions.Font = new Font("Microsoft Sans Serif", 16F, FontStyle.Bold, GraphicsUnit.Pixel);
            bOptions.Location = new Point(118, 93);
            bOptions.Margin = new Padding(4, 3, 4, 3);
            bOptions.Name = "bOptions";
            bOptions.Size = new Size(117, 38);
            bOptions.TabIndex = 0;
            bOptions.Text = "Options";
            bOptions.UseVisualStyleBackColor = false;
            bOptions.Click += bOptions_Click;
            // 
            // bBest
            // 
            bBest.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bBest.BackColor = Color.LightSteelBlue;
            bBest.Enabled = false;
            bBest.FlatAppearance.BorderSize = 0;
            bBest.FlatStyle = FlatStyle.Flat;
            bBest.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Pixel);
            bBest.Location = new Point(240, 14);
            bBest.Margin = new Padding(4, 3, 4, 3);
            bBest.Name = "bBest";
            bBest.Size = new Size(117, 74);
            bBest.TabIndex = 0;
            bBest.Text = "Best:";
            bBest.TextAlign = ContentAlignment.TopCenter;
            bBest.UseVisualStyleBackColor = false;
            // 
            // bScore
            // 
            bScore.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            bScore.BackColor = Color.LightSteelBlue;
            bScore.Enabled = false;
            bScore.FlatAppearance.BorderSize = 0;
            bScore.FlatStyle = FlatStyle.Flat;
            bScore.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Pixel);
            bScore.Location = new Point(117, 14);
            bScore.Margin = new Padding(4, 3, 4, 3);
            bScore.Name = "bScore";
            bScore.Size = new Size(117, 73);
            bScore.TabIndex = 0;
            bScore.Text = "Score:";
            bScore.TextAlign = ContentAlignment.TopCenter;
            bScore.UseVisualStyleBackColor = false;
            // 
            // bNewGame
            // 
            bNewGame.BackColor = Color.Gainsboro;
            bNewGame.BackgroundImage = Properties.Resources.refresh_page_option;
            bNewGame.BackgroundImageLayout = ImageLayout.Zoom;
            bNewGame.FlatAppearance.BorderSize = 4;
            bNewGame.FlatAppearance.MouseDownBackColor = Color.Silver;
            bNewGame.FlatAppearance.MouseOverBackColor = Color.Silver;
            bNewGame.FlatStyle = FlatStyle.Flat;
            bNewGame.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Bold, GraphicsUnit.Pixel);
            bNewGame.ForeColor = SystemColors.ControlDark;
            bNewGame.Image = Properties.Resources.refresh_page_option;
            bNewGame.Location = new Point(13, 81);
            bNewGame.Margin = new Padding(4, 3, 4, 3);
            bNewGame.Name = "bNewGame";
            bNewGame.Size = new Size(58, 58);
            bNewGame.TabIndex = 0;
            bNewGame.UseVisualStyleBackColor = false;
            bNewGame.Click += bNewGame_Click;
            // 
            // pField
            // 
            pField.BackColor = Color.Silver;
            pField.Controls.Add(pMatrix);
            pField.Dock = DockStyle.Fill;
            pField.Location = new Point(0, 145);
            pField.Margin = new Padding(4, 3, 4, 3);
            pField.Name = "pField";
            pField.Size = new Size(370, 365);
            pField.TabIndex = 2;
            // 
            // pMatrix
            // 
            pMatrix.BackColor = Color.Gainsboro;
            pMatrix.Font = new Font("Microsoft Sans Serif", 20.25F, FontStyle.Bold, GraphicsUnit.Point);
            pMatrix.Location = new Point(13, 13);
            pMatrix.Margin = new Padding(4, 3, 4, 3);
            pMatrix.Name = "pMatrix";
            pMatrix.Size = new Size(344, 340);
            pMatrix.TabIndex = 9;
            // 
            // Main2048Form
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlLightLight;
            ClientSize = new Size(370, 510);
            Controls.Add(pField);
            Controls.Add(pMenu);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Main2048Form";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "\"2048\"";
            FormClosing += Main2048Form_FormClosing;
            Load += Main2048Form_Load;
            KeyDown += Main2048Form_KeyDown;
            pMenu.ResumeLayout(false);
            pField.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel pMenu;
        private Panel pField;
        private Panel pMatrix;
        private NonFocusButton bNewGame;
        private NonFocusButton bScore;
        private NonFocusButton bBest;
        private NonFocusButton bOptions;
        private NonFocusButton bUndo;
        private NonFocusButton bClose;
    }
}