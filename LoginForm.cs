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
        String noexist;
        String error;
        String atleast8;
        String cannotbeempty;
        String wrongpassword;
        public LoginForm()
        {
            InitializeComponent();
            LangBox.SelectedIndex = 0;
            LangBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            loginUser = LoginTextBox.Text;
            PassUser = PasswordTextBox.Text;
            LangChoose langchoose = new();
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
                    DialogResult result = MessageBox.Show(noexist, error, MessageBoxButtons.YesNo, MessageBoxIcon.Error);
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
                                MessageBox.Show(atleast8, error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show(cannotbeempty, error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                }
                else
                {
                    MessageBox.Show(wrongpassword, error, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void LangBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            LangChoose.langindex = LangBox.SelectedIndex + 1;
            switch (LangChoose.langindex)
            {
                case 1:
                    this.Text = "Вход";
                    AuthLabel.Text = "Авторизация";
                    LoginTextBox.PlaceholderText = "Логин";
                    PasswordTextBox.PlaceholderText = "Пароль";
                    GeneragePasswordLabel.Text = "Сгенериaровать пароль";
                    LoginButton.Text = "Войти";
                    noexist = "Учетная запись не существует.\nЖелаете зарегестрировать новую?";
                    error = "Ошибка";
                    atleast8 = "Пароль должен состоять как минимум из 8 символов.";
                    cannotbeempty = "Поле логина не может быть пустым.";
                    wrongpassword = "Вы ввели неверный пароль\nПопробуйте снова.";
                    break;
                case 2:
                    this.Text = "Вхід";
                    AuthLabel.Text = "Автентифікація";
                    LoginTextBox.PlaceholderText = "Логін";
                    PasswordTextBox.PlaceholderText = "Пароль";
                    GeneragePasswordLabel.Text = "Згенерувати пароль";
                    LoginButton.Text = "Увійти";
                    noexist = "Обліковий запис не існує.\nБажаєте зареєструвати новий?";
                    error = "Помилка";
                    atleast8 = "Пароль повинен містити принаймні 8 символів.";
                    cannotbeempty = "Поле логіна не може бути порожнім.";
                    wrongpassword = "Ви ввели неправильний пароль.\nСпробуйте ще раз.";
                    break;
                case 3:
                    this.Text = "Login";
                    AuthLabel.Text = "Authentication";
                    LoginTextBox.PlaceholderText = "Login";
                    PasswordTextBox.PlaceholderText = "Password";
                    GeneragePasswordLabel.Text = "Generate Password";
                    LoginButton.Text = "Login";
                    noexist = "Account does not exist.\nWould you like to register a new one?";
                    error = "Error";
                    atleast8 = "Password must be at least 8 characters long.";
                    cannotbeempty = "Login field cannot be empty.";
                    wrongpassword = "You entered the wrong password.\nPlease try again.";
                    break;
                case 4:
                    this.Text = "Inicio de sesión";
                    AuthLabel.Text = "Autenticación";
                    LoginTextBox.PlaceholderText = "Nombre de usuario";
                    PasswordTextBox.PlaceholderText = "Contraseña";
                    GeneragePasswordLabel.Text = "Generar contraseña";
                    LoginButton.Text = "Iniciar sesión";
                    noexist = "La cuenta no existe.\n¿Quieres registrar una nueva?";
                    error = "Error";
                    atleast8 = "La contraseña debe tener al menos 8 caracteres.";
                    cannotbeempty = "El campo de inicio de sesión no puede estar vacío.";
                    wrongpassword = "Has introducido una contraseña incorrecta.\nPor favor, inténtalo de nuevo.";
                    break;
            }
        }
    }
}
