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
    }
}
