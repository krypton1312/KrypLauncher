namespace KrypLauncher
{
    partial class MainForm
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
            pictureBox2048 = new PictureBox();
            chooseLabel = new Label();
            pictureBoxTicTacToe = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox2048).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTicTacToe).BeginInit();
            SuspendLayout();
            // 
            // pictureBox2048
            // 
            pictureBox2048.Image = Properties.Resources._1024x1024bb;
            pictureBox2048.InitialImage = Properties.Resources._1024x1024bb;
            pictureBox2048.Location = new Point(79, 83);
            pictureBox2048.Name = "pictureBox2048";
            pictureBox2048.Size = new Size(185, 181);
            pictureBox2048.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2048.TabIndex = 0;
            pictureBox2048.TabStop = false;
            // 
            // chooseLabel
            // 
            chooseLabel.AutoSize = true;
            chooseLabel.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            chooseLabel.Location = new Point(29, 24);
            chooseLabel.Name = "chooseLabel";
            chooseLabel.Size = new Size(584, 32);
            chooseLabel.TabIndex = 2;
            chooseLabel.Text = "Выберите игру, в которую вы бы хотели поиграть:";
            // 
            // pictureBoxTicTacToe
            // 
            pictureBoxTicTacToe.Image = Properties.Resources.tic_tac_toe_icon_2048x2048_g58f0u84;
            pictureBoxTicTacToe.InitialImage = null;
            pictureBoxTicTacToe.Location = new Point(347, 83);
            pictureBoxTicTacToe.Name = "pictureBoxTicTacToe";
            pictureBoxTicTacToe.Size = new Size(185, 181);
            pictureBoxTicTacToe.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxTicTacToe.TabIndex = 3;
            pictureBoxTicTacToe.TabStop = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(637, 341);
            Controls.Add(pictureBoxTicTacToe);
            Controls.Add(chooseLabel);
            Controls.Add(pictureBox2048);
            Name = "MainForm";
            Text = "KrypLauncher";
            ((System.ComponentModel.ISupportInitialize)pictureBox2048).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxTicTacToe).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox2048;
        private Label chooseLabel;
        private PictureBox pictureBoxTicTacToe;
    }
}