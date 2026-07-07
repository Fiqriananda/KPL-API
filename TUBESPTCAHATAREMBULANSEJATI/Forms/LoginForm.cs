using System;
using System.Drawing;
using System.Windows.Forms;
using TUBESPTCAHATAREMBULANSEJATI.Services;

namespace TUBESPTCAHATAREMBULANSEJATI.Forms
{
    public class LoginForm : Form
    {
        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnLogin;
        private LinkLabel lnkRegister;
        private Label lblMessage;

        public LoginForm()
        {
            InitializeUI();
        }

        private void InitializeUI()
        {
            this.Text = "Login - PT CAHAYA REMBULAN SEJATI";
            this.Size = new Size(400, 390);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            Label lblTitle = new Label
            {
                Text = "LOGIN",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(150, 30),
                AutoSize = true
            };

            Label lblUsername = new Label { Text = "Username:", Location = new Point(50, 90) };
            txtUsername = new TextBox { Location = new Point(50, 110), Width = 280 };

            Label lblPassword = new Label { Text = "Password:", Location = new Point(50, 150) };
            txtPassword = new TextBox { Location = new Point(50, 170), Width = 280, UseSystemPasswordChar = true };

            btnLogin = new Button
            {
                Text = "Login",
                Location = new Point(50, 220),
                Width = 280,
                Height = 35,
                BackColor = Color.DodgerBlue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnLogin.Click += BtnLogin_Click;

            lnkRegister = new LinkLabel
            {
                Text = "Don't have an account? Register here.",
                Location = new Point(90, 270),
                AutoSize = true
            };
            lnkRegister.LinkClicked += LnkRegister_LinkClicked;

            LinkLabel lnkTrack = new LinkLabel
            {
                Text = "Lacak Paket Tanpa Login",
                Location = new Point(130, 305),
                AutoSize = true
            };
            lnkTrack.LinkClicked += LnkTrack_LinkClicked;

            lblMessage = new Label
            {
                Location = new Point(50, 70),
                Width = 280,
                ForeColor = Color.Red,
                TextAlign = ContentAlignment.MiddleCenter,
                Visible = false
            };

            this.Controls.Add(lblTitle);
            this.Controls.Add(lblUsername);
            this.Controls.Add(txtUsername);
            this.Controls.Add(lblPassword);
            this.Controls.Add(txtPassword);
            this.Controls.Add(btnLogin);
            this.Controls.Add(lnkRegister);
            this.Controls.Add(lnkTrack);
            this.Controls.Add(lblMessage);
        }

        private async void BtnLogin_Click(object? sender, EventArgs e)
        {
            lblMessage.Visible = false;
            btnLogin.Enabled = false;

            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                ShowError("Please enter username and password.");
                btnLogin.Enabled = true;
                return;
            }

            var result = await ApiClient.LoginAsync(txtUsername.Text, txtPassword.Text);
            
            if (result.Success)
            {
                var dashboard = new MainDashboardForm();
                this.Hide();
                dashboard.ShowDialog();
                this.Close();
            }
            else
            {
                ShowError(result.Message);
                btnLogin.Enabled = true;
            }
        }

        private void LnkRegister_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            var registerForm = new RegisterForm();
            this.Hide();
            registerForm.ShowDialog();
            this.Show();
        }

        private void LnkTrack_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            var trackingForm = new TrackingForm();
            this.Hide();
            trackingForm.ShowDialog();
            this.Show();
        }

        private void ShowError(string message)
        {
            lblMessage.Text = message;
            lblMessage.Visible = true;
        }
    }
}
