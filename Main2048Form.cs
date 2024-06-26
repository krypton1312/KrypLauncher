﻿#define SettingsFromFile

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using G_2048 = Game_2048._2048;
using MySql.Data.MySqlClient;
using Microsoft.VisualBasic.Logging;

namespace KrypLauncher
{
    public delegate void OptionsEventHandler();
    public partial class Main2048Form : Form
    {
        DB db = new DB();
        private string loginUser;
        string cheater;
        string gameover;
        string infoBox;
        string bbest;
        string best;
        public event OptionsEventHandler OptionsEvent;
        private int score;                        // Очки
        private int bestScore;                    // Рекорд
        private int minValue = 2;                 // Минимально выпадающий блок
        private int matrixRows = 4;               // Количество строк
        private int matrixCells = 4;              // Количество столбцов
        private int intervalBetweenTiles = 10;    // Интервал между блоками (тайлами)
        private int borderInterval = 10;          // Интервал до края формы
        private bool gameOver = true;
        private bool lockOn = false;             // Задержка между сдвигами
        private bool ellipseTile = false;        // Круговые тайлы
        /*****/
        private Button[] tiles;                     // Массив тайлов (кнопок)
        /*****/
        private G_2048 game;
        private Size tileSize = new Size(60, 60);
        private Size normalFormSize = new Size(323, 466);
        private Dictionary<int, Color> colors;    // Цвета плиток
        Dictionary<string, int> records = new Dictionary<string, int>();        // Рекорды для каждого размера поля
        private string wanna_leave;
        private string resent_current;
        private string reseted;
        private string resetall;
        private string allreseted;

        public Main2048Form(int _matrixRows, int _matrixCells, Size _tileSize, int _Int32ervalBetweenTiles, int _borderInt32erval, bool _ellipseTile, Color backColor, string loginUser)
        {
            this.loginUser = loginUser;
            if (_matrixRows < 2 || _matrixCells < 2 || _tileSize.Width < 1 || _tileSize.Height < 1 || _Int32ervalBetweenTiles < 0 || _borderInt32erval < 0)
                return;
            matrixRows = _matrixRows;
            matrixCells = _matrixCells;
            intervalBetweenTiles = _Int32ervalBetweenTiles;
            borderInterval = _borderInt32erval;
            ellipseTile = _ellipseTile;
            tileSize = new Size(_tileSize.Width, _tileSize.Height);
            InitializeComponent();
            BackColor = backColor;
            /*Подгон размеров всех элементов*/
            // Панель с матрицей.
            pMatrix.Size = new Size(matrixCells * tileSize.Width + (intervalBetweenTiles * (matrixCells + 1)), matrixRows * tileSize.Height + (intervalBetweenTiles * (matrixRows + 1)));
            pMatrix.Location = new Point(borderInterval, borderInterval);

            // Создание плиток.
            CreateMatrix();

            // Предполагаемые размеры формы.
            Size newFormSize = new Size();
            int supposedFormWidth = borderInterval * 2 + pMatrix.Size.Width;
            int supposedFormHeight = pMenu.Size.Height + (borderInterval * 2 + pMatrix.Size.Height);
            newFormSize.Width = normalFormSize.Width < supposedFormWidth ?
                supposedFormWidth : normalFormSize.Width;
            newFormSize.Height = normalFormSize.Height < supposedFormHeight ?
                supposedFormHeight : normalFormSize.Height;

            ClientSize = new Size(newFormSize.Width, newFormSize.Height);
            pMatrix.Location = new Point(pField.Size.Width / 2 - pMatrix.Size.Width / 2, pField.Size.Height / 2 - pMatrix.Size.Height / 2);
        }

        public Main2048Form(string loginUser)
        {
            this.loginUser = loginUser;
        }

        #region Methods
        // Создание плиток.
        private void CreateMatrix()
        {
            tiles = CreateButtons(matrixRows * matrixCells, tileSize, new Font("Microsoft Sans Serif", 20, FontStyle.Bold), Color.WhiteSmoke);

            Point location = new Point(intervalBetweenTiles, intervalBetweenTiles);
            for (int i = 0; i < matrixRows; i++)
            {
                for (int j = 0; j < matrixCells; j++)
                {
                    tiles[matrixCells * i + j].Location = location;
                    tiles[matrixCells * i + j].Name = "bM" + i + j;
                    tiles[matrixCells * i + j].FlatStyle = FlatStyle.Flat;
                    tiles[matrixCells * i + j].Enabled = false;
                    tiles[matrixCells * i + j].FlatAppearance.BorderSize = 0;
                    if (ellipseTile)
                    {
                        GraphicsPath myPath = new GraphicsPath();
                        myPath.AddEllipse(0, 0, tiles[matrixCells * i + j].Width, tiles[matrixCells * i + j].Height);
                        Region myRegion = new Region(myPath);
                        tiles[matrixCells * i + j].Region = myRegion;
                    }
                    this.pMatrix.Controls.Add(tiles[matrixCells * i + j]);
                    location.X += tileSize.Width + intervalBetweenTiles;
                }
                location.X = intervalBetweenTiles;
                location.Y += tileSize.Height + intervalBetweenTiles;
            }
        }
        private Button[] CreateButtons(int count, Size size, Font font, Color backColor)
        {
            if (font == null || backColor == null) return null;

            Button[] buttons = new Button[count];
            for (int i = 0; i < count; i++)
            {
                buttons[i] = new Button()
                {
                    Size = size,
                    Font = font,
                    BackColor = backColor
                };
            }
            return buttons;
        }

        // Отображение матрицы на экране. Оптмизированная версия.
        private void ShowMatrix(int[,] oldMatrix)
        {
            int[,] matrix = game.GetMatrix();
            Random random = new Random();
            if (oldMatrix == null || oldMatrix.Length != matrix.Length) return;
            for (int i = 0; i < matrixRows; i++)
            {
                for (int j = 0; j < matrixCells; j++)
                {
                    if (matrix[i, j] == oldMatrix[i, j]) continue;
                    if (matrix[i, j] == 0)
                    {
                        tiles[matrixCells * i + j].Text = "";
                        tiles[matrixCells * i + j].BackColor = Color.WhiteSmoke;
                        tiles[matrixCells * i + j].Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                        continue;
                    }
                    else
                        tiles[matrixCells * i + j].Text = (matrix[i, j]).ToString();

                    // Установка цвета плитки.
                    int power = 0;
                    for (int number = minValue; number < matrix[i, j]; number += number, power++)
                    { }
                    if (colors.ContainsKey(power))
                        tiles[matrixCells * i + j].BackColor = colors[power];
                    else
                        tiles[matrixCells * i + j].BackColor = Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));

                    AdaptButtonFontSize(tiles[matrixCells * i + j]);
                }
            }
        }
        // Неоптимизированная.
        private void ShowMatrix()
        {
            int[,] matrix = game.GetMatrix();
            Random random = new Random();
            for (int i = 0; i < matrixRows; i++)
            {
                for (int j = 0; j < matrixCells; j++)
                {
                    if (matrix[i, j] == 0)
                    {
                        tiles[matrixCells * i + j].Text = "";
                        tiles[matrixCells * i + j].BackColor = Color.WhiteSmoke;
                        tiles[matrixCells * i + j].Font = new Font("Microsoft Sans Serif", 20, FontStyle.Bold);
                        continue;
                    }
                    else
                        tiles[matrixCells * i + j].Text = (matrix[i, j]).ToString();

                    // Установка цвета плитки.
                    int power = 0;
                    for (int number = minValue; number < matrix[i, j]; number += number, power++)
                    { }
                    if (colors.ContainsKey(power))
                        tiles[matrixCells * i + j].BackColor = colors[power];
                    else
                        tiles[matrixCells * i + j].BackColor = Color.FromArgb(random.Next(0, 256), random.Next(0, 256), random.Next(0, 256));
                    AdaptButtonFontSize(tiles[matrixCells * i + j]);
                }
            }
        }
        private void ShowScore()
        {
            score = game.Score;
            if (score == -1)
                bScore.Text = "Score:\n∞";
            else
                bScore.Text = best + score;
        }
        private void ShowBestScore()
        {
            if (bestScore == -1)
                bBest.Text = "Best:\n∞";
            else
                bBest.Text = bbest + bestScore;
        }

        private void NewGame()
        {
            game = new G_2048(matrixRows, matrixCells, minValue);
            gameOver = false;
            game.EndGameEvent += EndGameHandler;
            game.ScoreOverflowEvent += ScoreOverflowHandler;
            game.NewTileEvent += NewTileHandler;
            foreach (Button button in tiles)
            {
                button.Text = "";
            }
            ShowMatrix();
            ShowScore();
        }

        private void EndGameHandler()
        {
            MessageBox.Show(gameover, "\"2048\"");
            gameOver = true;
        }
        private void ScoreOverflowHandler()
        {
            MessageBox.Show(cheater, "2048");
            gameOver = true;
            records[matrixRows + " " + matrixCells] = -1;
            bestScore = -1;
            score = -1;
            bScore.Text = "Score:\n∞";
            bBest.Text = "Best:\n∞";
        }
        private void NewTileHandler(int x, int y)
        {
            lockOn = true;
            ShowMatrix();
            Button newTile = null;
            foreach (Button button in this.pMatrix.Controls.OfType<Button>())
            {
                if (button.Name == "bM" + x + y)
                {
                    newTile = button;
                    break;
                }
            }
            if (newTile == null) return;

            AnimationExtension(newTile, 150);
            lockOn = false;
        }

        private void AnimationExtension(Control element, int timeOnAnimation)
        {
            if (element == null)
                return;
            if (element.Size.Width != element.Size.Height)
                throw new ArgumentException("Element width must be equal to heght");
            if (timeOnAnimation <= 0)
                throw new ArgumentException("Time can't be less than 0");

            int originalWidth = element.Size.Width;
            int originalHeight = element.Size.Height;
            Point originalLocation = new Point(element.Location.X, element.Location.Y);
            Font originalFont = element.Font;

            element.Font = new Font(element.Font.Name, 0.1f, element.Font.Style);
            element.Location = new Point(element.Location.X + element.Width / 2, element.Location.Y + element.Height / 2);
            element.Size = new Size(0, 0);

            int timeOnSleep = Convert.ToInt32(Math.Floor(Convert.ToDouble(timeOnAnimation) / Convert.ToDouble(originalWidth)));
            double fontIncrement = (Convert.ToDouble(originalFont.Size * 0.1) / Convert.ToDouble(originalWidth) * 10);

            for (int i = 0; i < originalWidth; i++)
            {
                Thread.Sleep(timeOnSleep);
                element.Size = new Size(element.Size.Width + 1, element.Size.Width + 1);
                element.Font = new Font(element.Font.Name, (float)(element.Font.Size + fontIncrement), element.Font.Style);
                if (i % 2 == 0)
                    element.Location = new Point(element.Location.X - 1, element.Location.Y - 1);
                Application.DoEvents();
            }
            if (element.Location.X != originalLocation.X || element.Location.Y != originalLocation.Y)
                element.Location = originalLocation;
            element.Font = originalFont;
        }
        private void AdaptButtonFontSize(Button element, double fontSizePecrent = 0.85)
        {
            if (element == null) return;

            Size original = element.Size;
            Size textSize;
            element.Font = new Font(element.Font.Name, 1, element.Font.Style);
            while ((textSize = TextRenderer.MeasureText(element.Text, element.Font)).Width < element.ClientSize.Width * fontSizePecrent)
            {
                element.Font = new Font(element.Font.Name, element.Font.Size + 0.5f, element.Font.Style);
            }
        }

        private void ResetAllScores()
        {
            records = new Dictionary<String, int>();
            for (int i = 2; i <= 20; i++)
            {
                for (int j = 2; j <= 20; j++)
                {
                    records[i + " " + j] = 0;
                }
            }
            bestScore = 0;
        }
        private void ResetCurrentRecord()
        {
            if (records.ContainsKey(matrixRows + " " + matrixCells))
            {
                records[matrixRows + " " + matrixCells] = 0;
                bestScore = 0;
            }
        }
        private void ReadColors()
        {
            try
            {
                colors = new Dictionary<int, Color>();
                using (BinaryReader br = new BinaryReader(new FileStream("colors.2048", FileMode.OpenOrCreate)))
                {
                    int count = br.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        int key = br.ReadInt32();
                        colors[key] = Color.FromArgb(br.ReadByte(), br.ReadByte(), br.ReadByte(), br.ReadByte());
                    }

                }
            }
            catch (IOException)
            {
                MessageBox.Show("Помилка читання файлу!", "Error");
            }
            catch
            {
                MessageBox.Show("Помилка!", "Error");
            }
        }
        private void WriteColors()
        {
            try
            {
                using (BinaryWriter bw = new BinaryWriter(new FileStream("colors.2048", FileMode.Create)))
                {
                    bw.Write(colors.Count);
                    foreach (KeyValuePair<int, Color> color in colors)
                    {
                        bw.Write(color.Key);
                        bw.Write(color.Value.A);
                        bw.Write(color.Value.R);
                        bw.Write(color.Value.G);
                        bw.Write(color.Value.B);
                    }
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Помилка читання файлу!", "Error");
            }
            catch
            {
                MessageBox.Show("Помилка!", "Error");
            }
        }
        private void ReadRecords()
        {
            try
            {
                db.openConnection();
                MySqlCommand command = new MySqlCommand("SELECT `twentyfor` FROM `users` WHERE `login` = @loginUser", db.getConnection());
                command.Parameters.AddWithValue("@loginUser", loginUser);
                object result = command.ExecuteScalar();
                if (result != null)
                {
                    bestScore = Convert.ToInt32(result);
                }
                else
                {
                    bestScore = 0;
                }
                ShowBestScore();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка чтения из базы данных: " + ex.Message, "Ошибка");
            }
            finally
            {
                db.closeConnection();
            }
        }
        private void WriteRecords()
        {
            try
            {
                db.openConnection();
                MySqlCommand command = new MySqlCommand("UPDATE `users` SET `twentyfor` = @bestScore WHERE `login` = @loginUser", db.getConnection());
                command.Parameters.AddWithValue("@loginUser", loginUser);
                command.Parameters.AddWithValue("@bestScore", bestScore);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка записи в базу данных: " + ex.Message, "Ошибка");
            }
            finally
            {
                db.closeConnection();
            }
        }
        private void WriteSettings()
        {
            try
            {
                using (BinaryWriter bw = new BinaryWriter(new FileStream("data.2048", FileMode.Create)))
                {
                    bw.Write(matrixRows);
                    bw.Write(matrixCells);
                    bw.Write(tileSize.Width);
                    bw.Write(intervalBetweenTiles);
                    bw.Write(borderInterval);
                    bw.Write(ellipseTile);
                    bw.Write(BackColor.A);
                    bw.Write(BackColor.R);
                    bw.Write(BackColor.G);
                    bw.Write(BackColor.B);
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Помилка читання файлу!", "Error");
            }
            catch
            {
                MessageBox.Show("Помилка!", "Error");
            }
        }
        #endregion
        #region Events
        private void bNewGame_Click(object sender, EventArgs e)
        {
            NewGame();
        }
        private void bUndo_Click(object sender, EventArgs e)
        {
            if (game == null || gameOver) return;
            if (game.UndoAllowed())
            {
                int[,] oldMatrix = game.GetMatrix();
                game.Undo();
                ShowMatrix(oldMatrix);
                ShowScore();
            }
        }
        private void bOptions_Click(object sender, EventArgs e)
        {
            OptionsEvent();
        }

        private void Main2048Form_Load(object sender, EventArgs e)
        {
            chooselang();
            ReadColors();
            ReadRecords();
        }
        private void Main2048Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.N)
            {
                NewGame();
                return;
            }
            else if (e.KeyData == Keys.F1)
            {
                MessageBox.Show(infoBox, "2048", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else if (e.KeyData == Keys.Escape)
            {
                var result = MessageBox.Show(wanna_leave, "2048", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    this.Hide();
                    MainForm mainform = new MainForm(loginUser);
                    mainform.Show();
                }
            }
            else if (e.KeyData == Keys.F11)
            {
                var result = MessageBox.Show(resent_current, "2048", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    ResetCurrentRecord();
                    MessageBox.Show(reseted, "2048");
                    ShowBestScore();
                }
                return;
            }
            else if (e.KeyData == Keys.F12)
            {
                var result = MessageBox.Show(resetall, "2048", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    ResetAllScores();
                    MessageBox.Show(allreseted, "2048");
                    ShowBestScore();
                }
                return;
            }
            if (game == null || gameOver || lockOn) return;
            int[,] oldMatrix;
            bool moved = false;
            switch (e.KeyData)
            {
                case Keys.R:
                case Keys.B:
                    oldMatrix = game.GetMatrix();
                    game.Undo();
                    break;
                case Keys.Up:
                case Keys.W:
                    oldMatrix = game.GetMatrix();
                    moved = game.TryMoveUp();
                    break;
                case Keys.Down:
                case Keys.S:
                    oldMatrix = game.GetMatrix();
                    moved = game.TryMoveDown();
                    break;
                case Keys.Left:
                case Keys.A:
                    oldMatrix = game.GetMatrix();
                    moved = game.TryMoveLeft();
                    break;
                case Keys.Right:
                case Keys.D:
                    oldMatrix = game.GetMatrix();
                    moved = game.TryMoveRight();
                    break;
                default:
                    return;
            }
            ShowMatrix(oldMatrix);
            if (moved)
            {
                int tempScore = game.Score;
                if (bestScore != -1 && tempScore > bestScore)
                {
                    bestScore = tempScore;
                    records[matrixRows + " " + matrixCells] = tempScore;
                    ShowBestScore();
                }
                ShowScore();
            }
        }
        private void Main2048Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            WriteColors();
            WriteRecords();
            WriteSettings();
            Application.Exit();
        }
        #endregion

        private void pMenu_Paint(object sender, PaintEventArgs e)
        {

        }
        void chooselang()
        {
            switch (LangChoose.langindex)
            {
                case 1:
                    bScore.Text = "Очки:";
                    bOptions.Text = "Настройки";
                    bBest.Text = "Лучший:";
                    bUndo.Text = "Отменить";
                    best = "Очки:\n";
                    bbest = "Лучший:\n";
                    cheater = "Поздравляю. Вы превысили лимит по очкам. Вы - обманщик или читер.";
                    gameover = "Конец игры!";
                    infoBox = "Горячие клавиши:\r\nF1 - помощь;\r\nEsc - выход;\r\nF11 - сбросить текущий рекорд;\r\nF12 - сбросить все рекорды.";
                    wanna_leave = "Вы уверены, что хотите выйти? Текущая запись не сохранится.";
                    resent_current = "Сбросить текущий рекорд?";
                    reseted = "Рекорд сброшен.";
                    resetall = "Сбросить все рекорды?";
                    allreseted = "Все рекорды сброшены.";
                    break;
                case 2:
                    bScore.Text = "Очки:";
                    bOptions.Text = "Опції";
                    bBest.Text = "Найкращий:";
                    bUndo.Text = "Скасувати";
                    bUndo.Text = "Скасувати";
                    best = "Очки:\n";
                    bbest = "Найкращий:\n";
                    cheater = "Вітаю. Ви перевищили ліміт очок. Ви - шахраїн або чітер.";
                    gameover = "Гра закінчена!";
                    infoBox = "Гарячі клавіші:\r\nF1 - допомога;\r\nEsc - вихід;\r\nF11 - скинути поточний рекорд;\r\nF12 - скинути всі рекорди.";
                    wanna_leave = "Ви впевнені, що хочете вийти? Поточний рекорд не буде збережений.";
                    resent_current = "Скинути поточний рекорд?";
                    reseted = "Рекорд скинутий.";
                    resetall = "Скинути всі рекорди?";
                    allreseted = "Скинуті всі рекорди.";
                    break;

                case 3:
                    bScore.Text = "Score:";
                    bOptions.Text = "Options";
                    bBest.Text = "Best:";
                    bUndo.Text = "Undo";
                    bUndo.Text = "Undo";
                    best = "Score:\n";
                    bbest = "Best:\n";
                    cheater = "Congratulations. You've exceeded your points limit. You're a cheater or a cheater.";
                    gameover = "Game Over!";
                    infoBox = "Hotkeys:\r\nF1 - help;\r\nEsc - exit;\r\nF11 - reset current record;\r\nF12 - reset all records.";
                    wanna_leave = "Are you sure you want to quit? The current record won't hold.";
                    resent_current = "Reset current record?";
                    reseted = "Record reset.";
                    resetall = "Reset all records?";
                    allreseted = "Reset all records?";
                    break;
                case 4:
                    bScore.Text = "Puntuación:";
                    bOptions.Text = "Opciones";
                    bBest.Text = "Mejor:";
                    bbest = "Mejor:\n";
                    best = "Puntuación:\n";
                    bUndo.Text = "Deshacer";
                    cheater = "Felicidades. Has superado tu límite de puntos. Eres un tramposo o un hacker.";
                    gameover = "¡Juego terminado!";
                    infoBox = "Teclas de acceso rápido:\r\nF1 - ayuda;\r\nEsc - salir;\r\nF11 - restablecer el registro actual;\r\nF12 - restablecer todos los registros.";
                    wanna_leave = "¿Estás seguro de que quieres salir? El récord actual no se mantendrá.";
                    resent_current = "¿Restablecer el registro actual?";
                    reseted = "Registro restablecido.";
                    resetall = "¿Restablecer todos los registros?";
                    allreseted = "Todos los registros restablecidos.";
                    break;
            }
        }
        private void bClose_Click(object sender, EventArgs e)
        {
            WriteRecords();
            this.Hide();
            MainForm mainform = new MainForm(loginUser);
            mainform.Show();
        }
    }
    class NonFocusButton : Button
    {
        public NonFocusButton() => SetStyle(ControlStyles.Selectable, false);
    }
}