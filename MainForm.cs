using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace KrypLauncher
{
    public partial class MainForm : Form
    {
        private string loginUser;
        public MainForm(string loginUser)
        {
            InitializeComponent();
            this.loginUser = loginUser;
        }

        private void pictureBox2048_Click(object sender, EventArgs e)
        {
            int matrixRows = 4; int matrixCells= 4; Size tileSize= new Size(60, 60); int Int32ervalBetweenTiles =10; int borderInt32erval = 10 ; Color backColor =Color.Black;
            Options2048Form options2048Form = new Options2048Form( matrixRows, matrixCells, tileSize, Int32ervalBetweenTiles, borderInt32erval, backColor, loginUser);
            this.Hide();
            options2048Form.Show();

        }
        private void pictureBoxTicTacToe_Click(object sender, EventArgs e)
        {
            tictactoeForm TictactoeForm = new tictactoeForm(loginUser);
            this.Hide();
            TictactoeForm.Show();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
