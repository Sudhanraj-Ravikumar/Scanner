using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Windows.Forms;

namespace ScannerDisplay
{
    public partial class Login : Form
    {
        public delegate void LoginSuccess(object sender, User user);

        public event LoginSuccess OnSuccess;

        public Login()
        {
            InitializeComponent();

            InitConfig();

            LoadConfig();
        }

        private static string Username
        { get; set; }

        private static string Password
        { get; set; }

        private void InitConfig()
        {
            try
            {
                Username = ConfigurationManager.AppSettings["Login.Username"];
            }
            catch (ConfigurationErrorsException)
            {
                return;
                //todo
            }

            try
            {
                Password = ConfigurationManager.AppSettings["Login.Password"];
            }
            catch (ConfigurationErrorsException)
            {
                return;
                //todo
            }
        }

        private void LoadConfig()
        {
            comboBoxLoginUsername.SelectedIndex = comboBoxLoginUsername.Items.IndexOf(Username);
        }

        private void comboBoxLoginUsername_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatePasswordInput(sender);
        }

        private void UpdatePasswordInput(object sender)
        {
            ComboBox box = (ComboBox)sender;

            if (box.SelectedIndex == 0)
            {
                textBoxLoginPassword.Enabled = true;
            }
            else if (box.SelectedIndex == 1)
            {
                textBoxLoginPassword.Enabled = false;
            }
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (comboBoxLoginUsername.SelectedIndex == 1)
            {
                OnLoginSuccess(this, User.Oberserver);
            }
            else if (comboBoxLoginUsername.SelectedIndex == 0)
            {
                if (textBoxLoginPassword.Text == Password)
                {
                    OnLoginSuccess(this, User.Admin);
                }
                else
                {
                    MessageBox.Show("Password incorrect, please try again.");
                    textBoxLoginPassword.Text = String.Empty;
                }
            }
        }

        protected void OnLoginSuccess(object sender, User user)
        {
            OnSuccess?.Invoke(sender, user);
            Close();
        }

        private void textBoxLoginPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonLogin_Click(null, EventArgs.Empty);
            }
        }
    }
}
