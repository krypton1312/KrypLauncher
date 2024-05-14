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
            Options2048Form options2048Form = new Options2048Form();
            this.Hide();
            options2048Form.Show();

        }

        private void pictureBoxTicTacToe_Click(object sender, EventArgs e)
        {
            tictactoeForm TictactoeForm = new tictactoeForm();
            this.Hide();
            TictactoeForm.Show();
        }
    }
}
