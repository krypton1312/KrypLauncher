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
        string infoBox;
        public Options2048Form(int matrixRows, int matrixCells, Size tileSize, int Int32ervalBetweenTiles, int borderInt32erval, Color backColor, string loginUser)
        {
            InitializeComponent();
            changelang();
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
            MessageBox.Show(infoBox, "2048", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        void changelang()
        {
            switch (LangChoose.langindex)
            {
                case 1:
                    lMatrixSize.Text = "Размер игрового поля:";
                    lRows.Text = "Строки:";
                    lCells.Text = "Столбцы:";
                    lTileSize.Text = "Размер игровой ячейки:";
                    lInt32erval1.Text = "Интервал между ячейками:";
                    lInt32erval2.Text = "Интервал до матрицы";
                    cbEllipse.Text = "Круглые плитки";
                    infoBox = "Горячие клавиши:\r\nF1 – помощь;\r\nEsc – выход;\r\nF11 – сброс текущего рекорда;\r\nF12 – сброс всех рекордов.";
                    break;
                case 2:
                    lMatrixSize.Text = "Розмір ігрового поля:";
                    lRows.Text = "Рядки:";
                    lCells.Text = "Стовпці:";
                    lTileSize.Text = "Розмір ігрової клітини:";
                    lInt32erval1.Text = "Інтерваaл між клітинами:";
                    lInt32erval2.Text = "Інтервал до матриці";
                    cbEllipse.Text = "Круглі плитaки";
                    infoBox = "Гарячі клавіші:\r\nF1 – допомога;\r\nEcs – вихід;\r\nF11 – скинути поточний рекорд;\r\nF12 – скинути всі рекорди.";
                    break;
                case 3:
                    lMatrixSize.Text = "Playing field size:";
                    lRows.Text = "Lines:";
                    lCells.Text = "Columns:";
                    lTileSize.Text = "Tile size:";
                    lInt32erval1.Text = "Interval between cells:";
                    lInt32erval2.Text = "Interval to the matrix";
                    cbEllipse.Text = "Circular tiles";
                    infoBox = "Hotkeys:\r\nF1 - help;\r\nEsc - exit;\r\nF11 - reset current record;\r\nF12 - reset all records.";
                    break;
                case 4:
                    lMatrixSize.Text = "Tamaño del campo de juego:";
                    lRows.Text = "Líneas:";
                    lCells.Text = "Columnas:";
                    lTileSize.Text = "Tamaño de la ficha:";
                    lInt32erval1.Text = "Intervalo entre celdas:";
                    lInt32erval2.Text = "Intervalo hasta la matriz";
                    cbEllipse.Text = "Fichas circulares";
                    infoBox = "Atajos de teclado:\r\nF1 - ayuda;\r\nEsc - salida;\r\nF11 - reiniciar el récord actual;\r\nF12 - reiniciar todos los registros.";
                    break;

            }
        }
    }
}