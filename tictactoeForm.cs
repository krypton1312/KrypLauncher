using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Windows.Forms;

namespace KrypLauncher
{
    public partial class tictactoeForm : Form
    {
        private string loginUser;
        public tictactoeForm(string loginUser)
        {
            InitializeComponent();
            this.loginUser = loginUser;
            WinsLabel.Text = "Ваши победы: " + resultUpdate(3);
            CompWinsLabel.Text = "Победы ИИ: " + resultUpdate(4);
        }
        Player currentPlayer;
        public enum Player
        {
            X,
            O
        }
        public void Form1_Load(object sender, EventArgs e)
        {
            Default();
        }
        void Start()
        {
            const string message = "Вы желаете сделать первый ход?";
            const string caption = "Выбор хода";

            DialogResult dialogresult = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogresult == DialogResult.No)
            {
                randomMove(moves);
                moves++;
            }
        }
        public int resultUpdate(int action)
        {

            db.openConnection();
            MySqlCommand getwins = new MySqlCommand("SELECT tictactoewins from `users` WHERE `login` = @uL ", db.getConnection());
            MySqlCommand getlose = new MySqlCommand("SELECT tictactoelose from `users` WHERE `login` = @uL ", db.getConnection());
            getwins.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            getlose.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            switch (action)
            {
                case 1:
                    int tictactoeWinsCount = Convert.ToInt32(getwins.ExecuteScalar());
                    tictactoeWinsCount++;
                    WinsLabel.Text = "Ваши победы: " + tictactoeWinsCount;
                    MySqlCommand updatewins = new MySqlCommand("UPDATE `users` SET `tictactoewins` = @newValue WHERE `login` = @uL", db.getConnection());
                    updatewins.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
                    updatewins.Parameters.AddWithValue("@newValue", tictactoeWinsCount);
                    updatewins.ExecuteNonQuery();
                    MessageBox.Show("Вы выиграли.", "Игра окончена.", MessageBoxButtons.OK);
                    db.closeConnection();
                    return tictactoeWinsCount;
                case 2:
                    int tictactoeLoseCount = Convert.ToInt32(getlose.ExecuteScalar());
                    tictactoeLoseCount++;
                    CompWinsLabel.Text = "Победы ИИ: " + tictactoeLoseCount;
                    MySqlCommand updatelose = new MySqlCommand("UPDATE `users` SET `tictactoelose` = @newValue WHERE `login` = @uL", db.getConnection());
                    updatelose.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
                    updatelose.Parameters.AddWithValue("@newValue", tictactoeLoseCount);
                    updatelose.ExecuteNonQuery();
                    MessageBox.Show("Вы проиграли.", "Игра окончена.", MessageBoxButtons.OK);
                    db.closeConnection();
                    return tictactoeLoseCount;
                case 3:
                    tictactoeWinsCount = Convert.ToInt32(getwins.ExecuteScalar());
                    Console.WriteLine(3);
                    db.closeConnection();
                    return tictactoeWinsCount;
                case 4:
                    tictactoeLoseCount = Convert.ToInt32(getlose.ExecuteScalar());
                    Console.WriteLine(4);
                    db.closeConnection();
                    return tictactoeLoseCount;
            }

            return 0;
        }

        int moves = 0;
        DB db = new();

        void CheckMoves(int a)
        {
            if (a < 6)
            {
                button1.Enabled = button2.Enabled = button3.Enabled = button13.Enabled = button14.Enabled = button15.Enabled = false;
                button1.Visible = button2.Visible = button3.Visible = button13.Visible = button14.Visible = button15.Visible = false;
            }
            else if (a == 7)
            {
                button1.Enabled = button2.Enabled = button3.Enabled = true;
                button1.Visible = button2.Visible = button3.Visible = true;
            }
            else if (a == 9)
            {
                button13.Enabled = button14.Enabled = button15.Enabled = true;
                button13.Visible = button14.Visible = button15.Visible = true;
            }
            else if (a == 15)
            {
                MessageBox.Show("Ничья", "Результат игры", MessageBoxButtons.YesNo);
            }
        }



        private void ButtonClick(object sender, EventArgs e)
        {
            try
            {
                var button = (Button)sender;
                currentPlayer = Player.X;
                button.Text = currentPlayer.ToString();
                button.Enabled = false;
                moves++;
                CheckMoves(moves);
                LableMoves.Text = ("Количество ходов: " + Convert.ToString(moves));
                if (!CheckWin_USER()) return;
                playAI(moves); CheckMoves(moves); CheckWin_AI();
                Console.WriteLine(moves);
            }
            catch { }
        }
        void playAI(int a)
        {
            moves++;
            LableMoves.Text = ("Количество ходов: " + Convert.ToString(moves));
            if (Convert.ToBoolean(CheckAttack(a))) return;
            if (Convert.ToBoolean(CheckDefend(a))) return;
            randomMove(a);
        }
        bool CheckWin_AI()
        {
            if (button4.Text == "O" && button5.Text == "O" && button6.Text == "O")
            {
                button4.BackColor = Color.Red;
                button5.BackColor = Color.Red;
                button6.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button6.Text == "O" && button8.Text == "O" && button10.Text == "O")
            {
                button6.BackColor = Color.Red;
                button8.BackColor = Color.Red;
                button10.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button7.Text == "O" && button8.Text == "O" && button9.Text == "O")
            {
                button7.BackColor = Color.Red;
                button8.BackColor = Color.Red;
                button9.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button4.Text == "O" && button7.Text == "O" && button10.Text == "O")
            {
                button4.BackColor = Color.Red;
                button7.BackColor = Color.Red;
                button10.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button5.Text == "O" && button8.Text == "O" && button11.Text == "O")
            {
                button5.BackColor = Color.Red;
                button8.BackColor = Color.Red;
                button11.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button6.Text == "O" && button9.Text == "O" && button12.Text == "O")
            {
                button6.BackColor = Color.Red;
                button9.BackColor = Color.Red;
                button12.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button4.Text == "O" && button8.Text == "O" && button12.Text == "O")
            {
                button4.BackColor = Color.Red;
                button8.BackColor = Color.Red;
                button12.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button13.Text == "O" && button14.Text == "O" && button15.Text == "O")
            {
                button13.BackColor = Color.Red;
                button14.BackColor = Color.Red;
                button15.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button1.Text == "O" && button4.Text == "O" && button7.Text == "O")
            {
                button1.BackColor = Color.Red;
                button4.BackColor = Color.Red;
                button7.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button2.Text == "O" && button5.Text == "O" && button8.Text == "O")
            {
                button2.BackColor = Color.Red;
                button5.BackColor = Color.Red;
                button8.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button3.Text == "O" && button6.Text == "O" && button9.Text == "O")
            {
                button3.BackColor = Color.Red;
                button6.BackColor = Color.Red;
                button9.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button1.Text == "O" && button5.Text == "O" && button9.Text == "O")
            {
                button1.BackColor = Color.Red;
                button5.BackColor = Color.Red;
                button9.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button3.Text == "O" && button5.Text == "O" && button7.Text == "O")
            {
                button3.BackColor = Color.Red;
                button5.BackColor = Color.Red;
                button7.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button7.Text == "O" && button10.Text == "O" && button13.Text == "O")
            {
                button7.BackColor = Color.Red;
                button10.BackColor = Color.Red;
                button13.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button9.Text == "O" && button12.Text == "O" && button15.Text == "O")
            {
                button9.BackColor = Color.Red;
                button12.BackColor = Color.Red;
                button15.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button7.Text == "O" && button10.Text == "O" && button13.Text == "O")
            {
                button7.BackColor = Color.Red;
                button10.BackColor = Color.Red;
                button13.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button9.Text == "O" && button11.Text == "O" && button13.Text == "O")
            {
                button9.BackColor = Color.Red;
                button11.BackColor = Color.Red;
                button13.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else if (button10.Text == "O" && button11.Text == "O" && button12.Text == "O")
            {
                button10.BackColor = Color.Red;
                button11.BackColor = Color.Red;
                button12.BackColor = Color.Red;
                resultUpdate(2);
                return false;
            }
            else
            {
                return true;
            }
        }
        bool CheckWin_USER()
        {
            if (button4.Text == "X" && button5.Text == "X" && button6.Text == "X")
            {
                button4.BackColor = Color.Green;
                button5.BackColor = Color.Green;
                button6.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button6.Text == "X" && button8.Text == "X" && button10.Text == "X")
            {
                button6.BackColor = Color.Green;
                button8.BackColor = Color.Green;
                button10.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button10.Text == "X" && button11.Text == "X" && button12.Text == "X")
            {
                button10.BackColor = Color.Green;
                button11.BackColor = Color.Green;
                button12.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button7.Text == "X" && button8.Text == "X" && button9.Text == "X")
            {
                button7.BackColor = Color.Green;
                button8.BackColor = Color.Green;
                button9.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button4.Text == "X" && button7.Text == "X" && button10.Text == "X")
            {
                button4.BackColor = Color.Green;
                button7.BackColor = Color.Green;
                button10.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button5.Text == "X" && button8.Text == "X" && button11.Text == "X")
            {
                button5.BackColor = Color.Green;
                button8.BackColor = Color.Green;
                button11.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button6.Text == "X" && button9.Text == "X" && button12.Text == "X")
            {
                button6.BackColor = Color.Green;
                button9.BackColor = Color.Green;
                button12.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button4.Text == "X" && button8.Text == "X" && button12.Text == "X")
            {
                button4.BackColor = Color.Green;
                button8.BackColor = Color.Green;
                button12.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button13.Text == "X" && button14.Text == "X" && button15.Text == "X")
            {
                button13.BackColor = Color.Green;
                button14.BackColor = Color.Green;
                button15.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button1.Text == "X" && button4.Text == "X" && button7.Text == "X")
            {
                button13.BackColor = Color.Green;
                button14.BackColor = Color.Green;
                button15.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button2.Text == "X" && button5.Text == "X" && button8.Text == "X")
            {
                button2.BackColor = Color.Green;
                button5.BackColor = Color.Green;
                button8.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button3.Text == "X" && button6.Text == "X" && button9.Text == "X")
            {
                button3.BackColor = Color.Green;
                button6.BackColor = Color.Green;
                button9.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button1.Text == "X" && button5.Text == "X" && button9.Text == "X")
            {
                button1.BackColor = Color.Green;
                button5.BackColor = Color.Green;
                button9.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button3.Text == "X" && button5.Text == "X" && button7.Text == "X")
            {
                button3.BackColor = Color.Green;
                button5.BackColor = Color.Green;
                button7.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button7.Text == "X" && button10.Text == "X" && button13.Text == "X")
            {
                button7.BackColor = Color.Green;
                button10.BackColor = Color.Green;
                button13.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button9.Text == "X" && button12.Text == "X" && button15.Text == "X")
            {
                button9.BackColor = Color.Green;
                button12.BackColor = Color.Green;
                button15.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button7.Text == "X" && button10.Text == "X" && button13.Text == "X")
            {
                button7.BackColor = Color.Green;
                button10.BackColor = Color.Green;
                button13.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else if (button9.Text == "X" && button11.Text == "X" && button13.Text == "X")
            {
                button9.BackColor = Color.Green;
                button11.BackColor = Color.Green;
                button13.BackColor = Color.Green;
                resultUpdate(1);
                return false;
            }
            else
            {
                return true;
            }
        }
        int CheckDefend(int a)
        {
            if (a < 7)
            {
                if (button4.Text == "X" && button5.Text == " " && button6.Text == "X")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;

                }
                else if (button4.Text == " " && button5.Text == "X" && button6.Text == "X")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "X" && button5.Text == "X" && button6.Text == " ")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button7.Text == "X" && button8.Text == " " && button9.Text == "X")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button7.Text == " " && button8.Text == "X" && button9.Text == "X")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button7.Text == "X" && button8.Text == "X" && button9.Text == " ")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button10.Text == " " && button11.Text == "X" && button12.Text == "X")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }
                else if (button10.Text == "X" && button11.Text == " " && button12.Text == "X")
                {
                    button11.Text = "O";
                    button11.Enabled = false;
                    return 1;
                }
                else if (button10.Text == "X" && button11.Text == "X" && button12.Text == " ")
                {
                    button12.Text = "O";
                    button12.Enabled = false;
                    return 1;
                }
                else if (button4.Text == " " && button7.Text == "X" && button10.Text == "X")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "X" && button7.Text == " " && button10.Text == "X")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "X" && button7.Text == "X" && button10.Text == " ")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }

                else if (button5.Text == " " && button8.Text == "X" && button11.Text == "X")
                {
                    button5.Text = "O";
                    button5.Enabled = false; ;
                    return 1;
                }
                else if (button5.Text == "X" && button8.Text == " " && button11.Text == "X")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button5.Text == "X" && button8.Text == "X" && button11.Text == " ")
                {
                    button11.Text = "O";
                    button11.Enabled = false;
                    return 1;
                }

                else if (button6.Text == " " && button9.Text == "X" && button12.Text == "X")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "X" && button9.Text == " " && button12.Text == "X")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "X" && button9.Text == "X" && button12.Text == " ")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }

                else if (button4.Text == " " && button8.Text == "X" && button12.Text == "X")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "X" && button8.Text == " " && button12.Text == "X")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "X" && button8.Text == "X" && button12.Text == " ")
                {
                    button12.Text = "O";
                    button12.Enabled = false;
                    return 1;
                }
                else if (button6.Text == " " && button8.Text == "X" && button10.Text == "X")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "X" && button8.Text == " " && button10.Text == "X")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "X" && button8.Text == "X" && button10.Text == " ")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }
                return 0;
            }
            else if (a >= 8)
            {
                if (button4.Text == "X" && button5.Text == " " && button6.Text == "X")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;

                }
                else if (button4.Text == " " && button5.Text == "X" && button6.Text == "X")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "X" && button5.Text == "X" && button6.Text == " ")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button7.Text == "X" && button8.Text == " " && button9.Text == "X")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button7.Text == " " && button8.Text == "X" && button9.Text == "X")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button7.Text == "X" && button8.Text == "X" && button9.Text == " ")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button10.Text == " " && button11.Text == "X" && button12.Text == "X")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }
                else if (button10.Text == "X" && button11.Text == " " && button12.Text == "X")
                {
                    button11.Text = "O";
                    button11.Enabled = false;
                    return 1;
                }
                else if (button10.Text == "X" && button11.Text == "X" && button12.Text == " ")
                {
                    button12.Text = "O";
                    button12.Enabled = false;
                    return 1;
                }
                else if (button4.Text == " " && button7.Text == "X" && button10.Text == "X")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "X" && button7.Text == " " && button10.Text == "X")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "X" && button7.Text == "X" && button10.Text == " ")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }

                else if (button5.Text == " " && button8.Text == "X" && button11.Text == "X")
                {
                    button5.Text = "O";
                    button5.Enabled = false; ;
                    return 1;
                }
                else if (button5.Text == "X" && button8.Text == " " && button11.Text == "X")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button5.Text == "X" && button8.Text == "X" && button11.Text == " ")
                {
                    button11.Text = "O";
                    button11.Enabled = false;
                    return 1;
                }

                else if (button6.Text == " " && button9.Text == "X" && button12.Text == "X")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "X" && button9.Text == " " && button12.Text == "X")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "X" && button9.Text == "X" && button12.Text == " ")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }

                else if (button4.Text == " " && button8.Text == "X" && button12.Text == "X")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "X" && button8.Text == " " && button12.Text == "X")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "X" && button8.Text == "X" && button12.Text == " ")
                {
                    button12.Text = "O";
                    button12.Enabled = false;
                    return 1;
                }
                else if (button6.Text == " " && button8.Text == "X" && button10.Text == "X")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "X" && button8.Text == " " && button10.Text == "X")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "X" && button8.Text == "X" && button10.Text == " ")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }
                else if (button1.Text == " " && button4.Text == "X" && button7.Text == "X")
                {
                    button1.Text = "O";
                    button1.Enabled = false; return 1;
                }
                else if (button1.Text == "X" && button4.Text == " " && button7.Text == "X")
                {
                    button4.Text = "O";
                    button4.Enabled = false; return 1;
                }
                else if (button1.Text == "X" && button4.Text == "X" && button7.Text == " ")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button2.Text == " " && button5.Text == "X" && button8.Text == "X")
                {
                    button2.Text = "O";
                    button2.Enabled = false;
                    return 1;
                }
                else if (button2.Text == "X" && button5.Text == " " && button8.Text == "X")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;
                }
                else if (button2.Text == "X" && button5.Text == "X" && button8.Text == " ")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button3.Text == " " && button6.Text == "X" && button9.Text == "X")
                {
                    button3.Text = "O";
                    button3.Enabled = false;
                    return 1;
                }
                else if (button3.Text == "X" && button6.Text == " " && button9.Text == "X")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button3.Text == "X" && button6.Text == "X" && button9.Text == " ")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button1.Text == " " && button5.Text == "X" && button9.Text == "X")
                {
                    button1.Text = "O";
                    button1.Enabled = false;
                    return 1;
                }
                else if (button1.Text == "X" && button5.Text == " " && button9.Text == "X")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;
                }
                else if (button1.Text == "X" && button5.Text == "X" && button9.Text == " ")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button3.Text == " " && button5.Text == "X" && button7.Text == "X")
                {
                    button3.Text = "O";
                    button3.Enabled = false;
                    return 1;
                }
                else if (button3.Text == "X" && button5.Text == " " && button7.Text == "X")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;
                }
                else if (button3.Text == "X" && button5.Text == "X" && button7.Text == " ")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                return 0;
            }
            else if (a >= 12)
            {
                if (button4.Text == "X" && button5.Text == " " && button6.Text == "X")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;

                }
                else if (button4.Text == " " && button5.Text == "X" && button6.Text == "X")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "X" && button5.Text == "X" && button6.Text == " ")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button7.Text == "X" && button8.Text == " " && button9.Text == "X")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button7.Text == " " && button8.Text == "X" && button9.Text == "X")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button7.Text == "X" && button8.Text == "X" && button9.Text == " ")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button10.Text == " " && button11.Text == "X" && button12.Text == "X")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }
                else if (button10.Text == "X" && button11.Text == " " && button12.Text == "X")
                {
                    button11.Text = "O";
                    button11.Enabled = false;
                    return 1;
                }
                else if (button10.Text == "X" && button11.Text == "X" && button12.Text == " ")
                {
                    button12.Text = "O";
                    button12.Enabled = false;
                    return 1;
                }
                else if (button4.Text == " " && button7.Text == "X" && button10.Text == "X")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "X" && button7.Text == " " && button10.Text == "X")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "X" && button7.Text == "X" && button10.Text == " ")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }
                else if (button5.Text == " " && button8.Text == "X" && button11.Text == "X")
                {
                    button5.Text = "O";
                    button5.Enabled = false; ;
                    return 1;
                }
                else if (button5.Text == "X" && button8.Text == " " && button11.Text == "X")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button5.Text == "X" && button8.Text == "X" && button11.Text == " ")
                {
                    button11.Text = "O";
                    button11.Enabled = false;
                    return 1;
                }
                else if (button6.Text == " " && button9.Text == "X" && button12.Text == "X")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "X" && button9.Text == " " && button12.Text == "X")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "X" && button9.Text == "X" && button12.Text == " ")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button4.Text == " " && button8.Text == "X" && button12.Text == "X")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "X" && button8.Text == " " && button12.Text == "X")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "X" && button8.Text == "X" && button12.Text == " ")
                {
                    button12.Text = "O";
                    button12.Enabled = false;
                    return 1;
                }
                else if (button6.Text == " " && button8.Text == "X" && button10.Text == "X")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "X" && button8.Text == " " && button10.Text == "X")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "X" && button8.Text == "X" && button10.Text == " ")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }
                else if (button1.Text == " " && button4.Text == "X" && button7.Text == "X")
                {
                    button1.Text = "O";
                    button1.Enabled = false; return 1;
                }
                else if (button1.Text == "X" && button4.Text == " " && button7.Text == "X")
                {
                    button4.Text = "O";
                    button4.Enabled = false; return 1;
                }
                else if (button1.Text == "X" && button4.Text == "X" && button7.Text == " ")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button2.Text == " " && button5.Text == "X" && button8.Text == "X")
                {
                    button2.Text = "O";
                    button2.Enabled = false;
                    return 1;
                }
                else if (button2.Text == "X" && button5.Text == " " && button8.Text == "X")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;
                }
                else if (button2.Text == "X" && button5.Text == "X" && button8.Text == " ")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button3.Text == " " && button6.Text == "X" && button9.Text == "X")
                {
                    button3.Text = "O";
                    button3.Enabled = false;
                    return 1;
                }
                else if (button3.Text == "X" && button6.Text == " " && button9.Text == "X")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button3.Text == "X" && button6.Text == "X" && button9.Text == " ")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button1.Text == " " && button5.Text == "X" && button9.Text == "X")
                {
                    button1.Text = "O";
                    button1.Enabled = false;
                    return 1;
                }
                else if (button1.Text == "X" && button5.Text == " " && button9.Text == "X")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;
                }
                else if (button1.Text == "X" && button5.Text == "X" && button9.Text == " ")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button3.Text == " " && button5.Text == "X" && button7.Text == "X")
                {
                    button3.Text = "O";
                    button3.Enabled = false;
                    return 1;
                }
                else if (button3.Text == "X" && button5.Text == " " && button7.Text == "X")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;
                }
                else if (button3.Text == "X" && button5.Text == "X" && button7.Text == " ")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button13.Text == " " && button14.Text == "X" && button15.Text == "X")
                {
                    button13.Text = "O";
                    button13.Enabled = false;
                    return 1;
                }
                else if (button13.Text == "X" && button14.Text == " " && button15.Text == "X")
                {
                    button14.Text = "O";
                    button14.Enabled = false;
                    return 1;
                }
                else if (button13.Text == "X" && button14.Text == "X" && button15.Text == " ")
                {
                    button15.Text = "O";
                    button15.Enabled = false;
                    return 1;
                }
                else if (button7.Text == " " && button10.Text == "X" && button13.Text == "X")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button7.Text == "X" && button10.Text == " " && button13.Text == "X")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }
                else if (button7.Text == "X" && button10.Text == "X" && button13.Text == " ")
                {
                    button13.Text = "O";
                    button13.Enabled = false;
                    return 1;
                }
                else if (button9.Text == " " && button12.Text == "X" && button15.Text == "X")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button9.Text == "X" && button12.Text == " " && button15.Text == "X")
                {
                    button12.Text = "O";
                    button12.Enabled = false;
                    return 1;
                }
                else if (button9.Text == "X" && button12.Text == "X" && button15.Text == " ")
                {
                    button15.Text = "O";
                    button15.Enabled = false;
                    return 1;
                }
                else if (button9.Text == " " && button11.Text == "X" && button13.Text == "X")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button9.Text == "X" && button11.Text == " " && button13.Text == "X")
                {
                    button11.Text = "O";
                    button11.Enabled = false;
                    return 1;
                }
                else if (button9.Text == "X" && button11.Text == "X" && button13.Text == " ")
                {
                    button13.Text = "O";
                    button13.Enabled = false;
                    return 1;
                }
                return 0;
            }
            return 0;


        }
        int CheckAttack(int a)
        {
            if (a < 7)
            {
                if (button4.Text == "O" && button5.Text == " " && button6.Text == "O")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;
                }
                else if (button4.Text == " " && button5.Text == "O" && button6.Text == "O")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "O" && button5.Text == "O" && button6.Text == " ")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button7.Text == "O" && button8.Text == " " && button9.Text == "O")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button7.Text == " " && button8.Text == "O" && button9.Text == "O")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button7.Text == "O" && button8.Text == "O" && button9.Text == " ")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button10.Text == " " && button11.Text == "O" && button12.Text == "O")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }
                else if (button10.Text == "O" && button11.Text == " " && button12.Text == "O")
                {
                    button11.Text = "O";
                    button11.Enabled = false;
                    return 1;
                }
                else if (button10.Text == "O" && button11.Text == "O" && button12.Text == " ")
                {
                    button12.Text = "O";
                    button12.Enabled = false;
                    return 1;
                }
                else if (button4.Text == " " && button7.Text == "O" && button10.Text == "O")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "O" && button7.Text == " " && button10.Text == "O")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "O" && button7.Text == "O" && button10.Text == " ")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }

                else if (button5.Text == " " && button8.Text == "O" && button11.Text == "O")
                {
                    button5.Text = "O";
                    button5.Enabled = false; ;
                    return 1;
                }
                else if (button5.Text == "O" && button8.Text == " " && button11.Text == "O")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button5.Text == "O" && button8.Text == "O" && button11.Text == " ")
                {
                    button11.Text = "O";
                    button11.Enabled = false;
                    return 1;
                }

                else if (button6.Text == " " && button9.Text == "O" && button12.Text == "O")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "O" && button9.Text == " " && button12.Text == "O")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "O" && button9.Text == "O" && button12.Text == " ")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }

                else if (button4.Text == " " && button8.Text == "O" && button12.Text == "O")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "O" && button8.Text == " " && button12.Text == "O")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "O" && button8.Text == "O" && button12.Text == " ")
                {
                    button12.Text = "O";
                    button12.Enabled = false;
                    return 1;
                }
                else if (button6.Text == " " && button8.Text == "O" && button10.Text == "O")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "O" && button8.Text == " " && button10.Text == "O")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "O" && button8.Text == "O" && button10.Text == " ")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }
                else
                {
                    return 0;
                }
                return 3;
            }
            else if (a >= 8)
            {
                if (button4.Text == "O" && button5.Text == " " && button6.Text == "O")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;

                }
                else if (button4.Text == " " && button5.Text == "O" && button6.Text == "O")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "O" && button5.Text == "O" && button6.Text == " ")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button7.Text == "O" && button8.Text == " " && button9.Text == "O")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button7.Text == " " && button8.Text == "O" && button9.Text == "O")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button7.Text == "O" && button8.Text == "O" && button9.Text == " ")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button10.Text == " " && button11.Text == "O" && button12.Text == "O")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }
                else if (button10.Text == "O" && button11.Text == " " && button12.Text == "O")
                {
                    button11.Text = "O";
                    button11.Enabled = false;
                    return 1;
                }
                else if (button10.Text == "O" && button11.Text == "O" && button12.Text == " ")
                {
                    button12.Text = "O";
                    button12.Enabled = false;
                    return 1;
                }
                else if (button4.Text == " " && button7.Text == "O" && button10.Text == "O")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "O" && button7.Text == " " && button10.Text == "O")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "O" && button7.Text == "O" && button10.Text == " ")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }

                else if (button5.Text == " " && button8.Text == "O" && button11.Text == "O")
                {
                    button5.Text = "O";
                    button5.Enabled = false; ;
                    return 1;
                }
                else if (button5.Text == "O" && button8.Text == " " && button11.Text == "O")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button5.Text == "O" && button8.Text == "O" && button11.Text == " ")
                {
                    button11.Text = "O";
                    button11.Enabled = false;
                    return 1;
                }

                else if (button6.Text == " " && button9.Text == "O" && button12.Text == "O")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "O" && button9.Text == " " && button12.Text == "O")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "O" && button9.Text == "O" && button12.Text == " ")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }

                else if (button4.Text == " " && button8.Text == "O" && button12.Text == "O")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "O" && button8.Text == " " && button12.Text == "O")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "O" && button8.Text == "O" && button12.Text == " ")
                {
                    button12.Text = "O";
                    button12.Enabled = false;
                    return 1;
                }
                else if (button6.Text == " " && button8.Text == "O" && button10.Text == "O")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "O" && button8.Text == " " && button10.Text == "O")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "O" && button8.Text == "O" && button10.Text == " ")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }
                else if (button1.Text == " " && button4.Text == "O" && button7.Text == "O")
                {
                    button1.Text = "O";
                    button1.Enabled = false; return 1;
                }
                else if (button1.Text == "O" && button4.Text == " " && button7.Text == "O")
                {
                    button4.Text = "O";
                    button4.Enabled = false; return 1;
                }
                else if (button1.Text == "O" && button4.Text == "O" && button7.Text == " ")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button2.Text == " " && button5.Text == "O" && button8.Text == "O")
                {
                    button2.Text = "O";
                    button2.Enabled = false;
                    return 1;
                }
                else if (button2.Text == "O" && button5.Text == " " && button8.Text == "O")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;
                }
                else if (button2.Text == "O" && button5.Text == "O" && button8.Text == " ")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button3.Text == " " && button6.Text == "O" && button9.Text == "O")
                {
                    button3.Text = "O";
                    button3.Enabled = false;
                    return 1;
                }
                else if (button3.Text == "O" && button6.Text == " " && button9.Text == "O")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button3.Text == "O" && button6.Text == "O" && button9.Text == " ")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button1.Text == " " && button5.Text == "O" && button9.Text == "O")
                {
                    button1.Text = "O";
                    button1.Enabled = false;
                    return 1;
                }
                else if (button1.Text == "O" && button5.Text == " " && button9.Text == "O")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;
                }
                else if (button1.Text == "O" && button5.Text == "O" && button9.Text == " ")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button3.Text == " " && button5.Text == "O" && button7.Text == "O")
                {
                    button3.Text = "O";
                    button3.Enabled = false;
                    return 1;
                }
                else if (button3.Text == "O" && button5.Text == " " && button7.Text == "O")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;
                }
                else if (button3.Text == "O" && button5.Text == "O" && button7.Text == " ")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else
                {
                    return 0;
                }
                return 0;
            }
            else if (a >= 12)
            {
                if (button4.Text == "O" && button5.Text == " " && button6.Text == "O")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;

                }
                else if (button4.Text == " " && button5.Text == "O" && button6.Text == "O")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "O" && button5.Text == "O" && button6.Text == " ")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button7.Text == "O" && button8.Text == " " && button9.Text == "O")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button7.Text == " " && button8.Text == "O" && button9.Text == "O")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button7.Text == "O" && button8.Text == "O" && button9.Text == " ")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button10.Text == " " && button11.Text == "O" && button12.Text == "O")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }
                else if (button10.Text == "O" && button11.Text == " " && button12.Text == "O")
                {
                    button11.Text = "O";
                    button11.Enabled = false;
                    return 1;
                }
                else if (button10.Text == "O" && button11.Text == "O" && button12.Text == " ")
                {
                    button12.Text = "O";
                    button12.Enabled = false;
                    return 1;
                }
                else if (button4.Text == " " && button7.Text == "O" && button10.Text == "O")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "O" && button7.Text == " " && button10.Text == "O")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "O" && button7.Text == "O" && button10.Text == " ")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }

                else if (button5.Text == " " && button8.Text == "O" && button11.Text == "O")
                {
                    button5.Text = "O";
                    button5.Enabled = false; ;
                    return 1;
                }
                else if (button5.Text == "O" && button8.Text == " " && button11.Text == "O")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button5.Text == "O" && button8.Text == "O" && button11.Text == " ")
                {
                    button11.Text = "O";
                    button11.Enabled = false;
                    return 1;
                }

                else if (button6.Text == " " && button9.Text == "O" && button12.Text == "O")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "O" && button9.Text == " " && button12.Text == "O")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "O" && button9.Text == "O" && button12.Text == " ")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }

                else if (button4.Text == " " && button8.Text == "O" && button12.Text == "O")
                {
                    button4.Text = "O";
                    button4.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "O" && button8.Text == " " && button12.Text == "O")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button4.Text == "O" && button8.Text == "O" && button12.Text == " ")
                {
                    button12.Text = "O";
                    button12.Enabled = false;
                    return 1;
                }
                else if (button6.Text == " " && button8.Text == "O" && button10.Text == "O")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "O" && button8.Text == " " && button10.Text == "O")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button6.Text == "O" && button8.Text == "O" && button10.Text == " ")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }
                else if (button1.Text == " " && button4.Text == "O" && button7.Text == "O")
                {
                    button1.Text = "O";
                    button1.Enabled = false; return 1;
                }
                else if (button1.Text == "O" && button4.Text == " " && button7.Text == "O")
                {
                    button4.Text = "O";
                    button4.Enabled = false; return 1;
                }
                else if (button1.Text == "O" && button4.Text == "O" && button7.Text == " ")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button2.Text == " " && button5.Text == "O" && button8.Text == "O")
                {
                    button2.Text = "O";
                    button2.Enabled = false;
                    return 1;
                }
                else if (button2.Text == "O" && button5.Text == " " && button8.Text == "O")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;
                }
                else if (button2.Text == "O" && button5.Text == "O" && button8.Text == " ")
                {
                    button8.Text = "O";
                    button8.Enabled = false;
                    return 1;
                }
                else if (button3.Text == " " && button6.Text == "O" && button9.Text == "O")
                {
                    button3.Text = "O";
                    button3.Enabled = false;
                    return 1;
                }
                else if (button3.Text == "O" && button6.Text == " " && button9.Text == "O")
                {
                    button6.Text = "O";
                    button6.Enabled = false;
                    return 1;
                }
                else if (button3.Text == "O" && button6.Text == "O" && button9.Text == " ")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button1.Text == " " && button5.Text == "O" && button9.Text == "O")
                {
                    button1.Text = "O";
                    button1.Enabled = false;
                    return 1;
                }
                else if (button1.Text == "O" && button5.Text == " " && button9.Text == "O")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;
                }
                else if (button1.Text == "O" && button5.Text == "O" && button9.Text == " ")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button3.Text == " " && button5.Text == "O" && button7.Text == "O")
                {
                    button3.Text = "O";
                    button3.Enabled = false;
                    return 1;
                }
                else if (button3.Text == "O" && button5.Text == " " && button7.Text == "O")
                {
                    button5.Text = "O";
                    button5.Enabled = false;
                    return 1;
                }
                else if (button3.Text == "O" && button5.Text == "O" && button7.Text == " ")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button13.Text == " " && button14.Text == "O" && button15.Text == "O")
                {
                    button13.Text = "O";
                    button13.Enabled = false;
                    return 1;
                }
                else if (button13.Text == "O" && button14.Text == " " && button15.Text == "O")
                {
                    button14.Text = "O";
                    button14.Enabled = false;
                    return 1;
                }
                else if (button13.Text == "O" && button14.Text == "O" && button15.Text == " ")
                {
                    button15.Text = "O";
                    button15.Enabled = false;
                    return 1;
                }
                else if (button7.Text == " " && button10.Text == "O" && button13.Text == "O")
                {
                    button7.Text = "O";
                    button7.Enabled = false;
                    return 1;
                }
                else if (button7.Text == "O" && button10.Text == " " && button13.Text == "O")
                {
                    button10.Text = "O";
                    button10.Enabled = false;
                    return 1;
                }
                else if (button7.Text == "O" && button10.Text == "O" && button13.Text == " ")
                {
                    button13.Text = "O";
                    button13.Enabled = false;
                    return 1;
                }
                else if (button9.Text == " " && button12.Text == "O" && button15.Text == "O")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button9.Text == "O" && button12.Text == " " && button15.Text == "O")
                {
                    button12.Text = "O";
                    button12.Enabled = false;
                    return 1;
                }
                else if (button9.Text == "O" && button12.Text == "O" && button15.Text == " ")
                {
                    button15.Text = "O";
                    button15.Enabled = false;
                    return 1;
                }
                else if (button9.Text == " " && button11.Text == "O" && button13.Text == "O")
                {
                    button9.Text = "O";
                    button9.Enabled = false;
                    return 1;
                }
                else if (button9.Text == "O" && button11.Text == " " && button13.Text == "O")
                {
                    button11.Text = "O";
                    button11.Enabled = false;
                    return 1;
                }
                else if (button9.Text == "O" && button11.Text == "O" && button13.Text == " ")
                {
                    button13.Text = "O";
                    button13.Enabled = false;
                    return 1;
                }
                return 0;
            }
            else
            {
                return 0;
            }

        }
        void randomMove(int a)
        {
            List<Button> buttons;
            currentPlayer = Player.O;
            buttons = new List<Button> { button4, button5, button6, button7, button8, button9, button10, button11, button12 };
            if (a == 8)
            {
                buttons.Add(button1);
                buttons.Add(button2);
                buttons.Add(button3);
                Console.WriteLine("added new buttons");
            }
            else if (a == 10)
            {
                buttons.Add(button13);
                buttons.Add(button14);
                buttons.Add(button15);
                Console.WriteLine("were added more");
            }
            int k = buttons.Count - 1;
            const int cj = 0;
            int j = cj;
            while (-1 < k)
            {
                if (buttons[k].Enabled == false)
                {
                    buttons.RemoveAt(k);
                    k--;
                }
                else
                {
                    k--;
                }
            }
            Random r = new Random();
            int value;
            value = r.Next(0, buttons.Count - 1);
            buttons[value].Text = currentPlayer.ToString();
            buttons[value].Enabled = false;
        }
        void Default()
        {
            foreach (Control x in this.Controls)
            {
                if (x is Button && x.Tag == "Play")
                {
                    ((Button)x).Enabled = false;
                    ((Button)x).Text = " ";
                    ((Button)x).BackColor = default(Color);
                }
                if (x is Button && x.Tag == "Play1")
                {
                    ((Button)x).Visible = false;
                    ((Button)x).Enabled = false;
                    ((Button)x).Text = " ";
                    ((Button)x).BackColor = default(Color);
                }
            }
        }
        private void restBut_Click(object sender, EventArgs e)
        {
            moves = 0;
            LableMoves.Text = " ";
            Default();
        }
        private void startbut_Click(object sender, EventArgs e)
        {
            foreach (Control x in this.Controls)
            {
                if (x is Button && x.Tag == "Play")
                {
                    ((Button)x).Enabled = true;
                    ((Button)x).Text = " ";
                    ((Button)x).BackColor = default(Color);
                }
            }
            Start();
        }

        private void tictactoeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainForm mainForm = new MainForm(loginUser);
            mainForm.Show();
        }
    }
}