using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using KrypLauncher;

namespace KrypLauncher
{
    public partial class Options2048Form : Form
    {
        private bool options = false;
        private Main2048Form mf;
        string loginUser;
        public Options2048Form(int matrixRows, int matrixCells, Size tileSize, int Int32ervalBetweenTiles, int borderInt32erval, Color backColor, string loginUser)
        {
            InitializeComponent();
            this.loginUser = loginUser; 
            nudRows.Value = matrixRows;
            nudCells.Value = matrixCells;
            nudTileSize.Value = tileSize.Width;
            nudInterval1.Value = Int32ervalBetweenTiles;
            nudInterval2.Value = borderInt32erval;
        }

        private void OnOptions()
        {
            options = true;
            Show();
            if (mf != null) mf.Enabled = false;
        }
        private void ReadSettings()
        {
            try
            {
                using (BinaryReader br = new BinaryReader(new FileStream("data.2048", FileMode.OpenOrCreate)))
                {
                    nudRows.Value = br.ReadInt32();
                    nudCells.Value = br.ReadInt32();
                    nudTileSize.Value = br.ReadInt32();
                    nudInterval1.Value = br.ReadInt32();
                    nudInterval2.Value = br.ReadInt32();
                    cbEllipse.Checked = br.ReadBoolean();
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка чтения файла!", "Error");
            }
            catch
            {
                MessageBox.Show("Ошибка!", "Error");
            }
        }

        private void StartForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (options)
            {
                if (mf != null)
                    mf.Enabled = true;
                e.Cancel = true;
                options = false;
                Hide();
            }
        }
        private void StartForm_Load(object sender, EventArgs e)
        {
            ReadSettings();
        }
        private void bOK_Click(object sender, EventArgs e)
        {
            Size tileSize = new Size(Convert.ToInt32(nudTileSize.Value), Convert.ToInt32(nudTileSize.Value));
            int rows = Convert.ToInt32(nudRows.Value);
            int cells = Convert.ToInt32(nudCells.Value);
            int borderInt32erval = Convert.ToInt32(nudInterval2.Value);
            int Int32erval = Convert.ToInt32(nudInterval1.Value);

            if (mf != null) mf.Close();
            mf = new Main2048Form(rows, cells, tileSize, Int32erval, borderInt32erval, cbEllipse.Checked, Color.Silver, loginUser);
            Hide();
            mf.Show();
            mf.OptionsEvent += OnOptions;
        }
        private void bClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainform = new MainForm(loginUser);
            mainform.Show();
        }
        private void OptionsForm_MouseDown(object sender, MouseEventArgs e)
        {
            Capture = false;
            Message m = Message.Create(this.Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
            WndProc(ref m);
        }

        private void bInfo_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"
Гарячі клавіші:
F1 – допомога;
Ecs – вихід;
F11 – скинути поточний рекорд;
F12 – скинути всі рекорди.
", "2048", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}