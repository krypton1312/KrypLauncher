using MySql.Data.MySqlClient;
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
    public partial class LoginForm : Form
    {
        public String loginUser;
        public String PassUser;
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            loginUser = LoginTextBox.Text;
            PassUser = PasswordTextBox.Text;

            DB db = new DB();

            DataTable table = new DataTable();

            MySqlDataAdapter adapter = new MySqlDataAdapter();

            MySqlCommand command = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @uL AND `password` = @pL", db.getConnection());
            command.Parameters.Add("@uL", MySqlDbType.VarChar).Value = loginUser;
            command.Parameters.Add("@pL", MySqlDbType.VarChar).Value = PassUser;


            adapter.SelectCommand = command;
            adapter.Fill(table);

            if (table.Rows.Count > 0)
            {
                MainForm mainForm = new MainForm(loginUser);
                mainForm.Show();
                this.Hide();
            }
            else
            {
                if (!ifUserExist(loginUser))
                {
                    DialogResult result = MessageBox.Show("Учетная запись не существует.\nЖелаете зарегестрировать новую?", "Ошибка", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                    if (result == DialogResult.Yes)
                    {
                        if (loginUser != null)
                        {
                            if (PassUser.Length > 7)
                            {
                                addNewUser(loginUser, PassUser);
                                MainForm mainForm = new MainForm(loginUser);
                                mainForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Пароль должен состоять как минимум из 8 символов.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show("Поле логина не может быть пустым.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    MessageBox.Show("Вы ввели неверный пароль\nПопробуйте снова.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            void addNewUser(string loginUser, string PassUser)
            {
                db.openConnection();
                MySqlCommand insert = new MySqlCommand("INSERT INTO `users` (login, password) VALUES (@newuL, @newpL)", db.getConnection());
                insert.Parameters.Add("@newuL", MySqlDbType.VarChar).Value = loginUser;
                insert.Parameters.Add("@newpL", MySqlDbType.VarChar).Value = PassUser;
                insert.ExecuteNonQuery();
                db.closeConnection();
            }
            bool ifUserExist(string loginUser)
            {
                db.openConnection();
                MySqlCommand checkuser = new MySqlCommand("SELECT * FROM `users` WHERE `login` = @checkuL", db.getConnection());
                checkuser.Parameters.Add("@checkuL", MySqlDbType.VarChar).Value = loginUser;
                MySqlDataReader reader = checkuser.ExecuteReader();
                bool result = reader.HasRows;
                reader.Close();
                db.closeConnection();
                return result;
            }
        }

        private void GeneragePasswordLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            GenPasswordForm genPasswordForm = new GenPasswordForm();
            genPasswordForm.Show();
        }
    }
}
