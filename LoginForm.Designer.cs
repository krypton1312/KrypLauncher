namespace KrypLauncher
{
    partial class LoginForm
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
            AuthLabel = new Label();
            LoginTextBox = new TextBox();
            PasswordTextBox = new TextBox();
            GeneragePasswordLabel = new LinkLabel();
            LoginButton = new Button();
            LangBox = new ComboBox();
            SuspendLayout();
            // 
            // AuthLabel
            // 
            AuthLabel.AutoSize = true;
            AuthLabel.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold, GraphicsUnit.Point);
            AuthLabel.Location = new Point(117, 9);
            AuthLabel.Name = "AuthLabel";
            AuthLabel.Size = new Size(161, 32);
            AuthLabel.TabIndex = 0;
            AuthLabel.Text = "Авторизация";
            // 
            // LoginTextBox
            // 
            LoginTextBox.CausesValidation = false;
            LoginTextBox.Location = new Point(105, 60);
            LoginTextBox.Name = "LoginTextBox";
            LoginTextBox.PlaceholderText = "Логин";
            LoginTextBox.Size = new Size(186, 23);
            LoginTextBox.TabIndex = 1;
            LoginTextBox.TabStop = false;
            // 
            // PasswordTextBox
            // 
            PasswordTextBox.Location = new Point(105, 89);
            PasswordTextBox.Name = "PasswordTextBox";
            PasswordTextBox.PlaceholderText = "Пароль";
            PasswordTextBox.Size = new Size(186, 23);
            PasswordTextBox.TabIndex = 2;
            PasswordTextBox.TabStop = false;
            // 
            // GeneragePasswordLabel
            // 
            GeneragePasswordLabel.AutoSize = true;
            GeneragePasswordLabel.Location = new Point(129, 115);
            GeneragePasswordLabel.Name = "GeneragePasswordLabel";
            GeneragePasswordLabel.Size = new Size(139, 15);
            GeneragePasswordLabel.TabIndex = 3;
            GeneragePasswordLabel.TabStop = true;
            GeneragePasswordLabel.Text = "Сгенериaровать пароль";
            GeneragePasswordLabel.LinkClicked += GeneragePasswordLabel_LinkClicked;
            // 
            // LoginButton
            // 
            LoginButton.Location = new Point(117, 145);
            LoginButton.Name = "LoginButton";
            LoginButton.Size = new Size(161, 32);
            LoginButton.TabIndex = 4;
            LoginButton.Text = "Войти";
            LoginButton.UseVisualStyleBackColor = true;
            LoginButton.Click += LoginButton_Click;
            // 
            // LangBox
            // 
            LangBox.FormattingEnabled = true;
            LangBox.Items.AddRange(new object[] { "Русский ", "Українська", "English", "Espanol" });
            LangBox.Location = new Point(297, 18);
            LangBox.Name = "LangBox";
            LangBox.Size = new Size(111, 23);
            LangBox.TabIndex = 2;
            LangBox.SelectedIndexChanged += LangBox_SelectedIndexChanged;
            // 
            // LoginForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(420, 211);
            Controls.Add(LangBox);
            Controls.Add(LoginButton);
            Controls.Add(GeneragePasswordLabel);
            Controls.Add(PasswordTextBox);
            Controls.Add(LoginTextBox);
            Controls.Add(AuthLabel);
            Name = "LoginForm";
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label AuthLabel;
        private TextBox LoginTextBox;
        private TextBox PasswordTextBox;
        private LinkLabel GeneragePasswordLabel;
        private Button LoginButton;
        public ComboBox LangBox;
        private ComboBox comboBox1;
    }
}