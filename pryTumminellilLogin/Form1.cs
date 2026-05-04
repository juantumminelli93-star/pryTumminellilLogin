using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pryTumminellilLogin
{
    public partial class login : Form
    {
        // Simple user store and failure counter
        private Dictionary<string, UserInfo> users;
        private int failedAttempts = 0;

        private class UserInfo
        {
            public string Password { get; }
            public string[] Modules { get; }

            public UserInfo(string password, string[] modules)
            {
                Password = password;
                Modules = modules;
            }
        }

        public login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Configure text boxes according to requirements
            // Username textbox: max 10 chars, blue font
            textBox2.MaxLength = 10;
            textBox2.ForeColor = Color.Blue;

            // Password textbox: max 10 chars and show '#' as password char
            textBox1.MaxLength = 10;
            textBox1.PasswordChar = '#';

            // Populate modules ComboBox
            comboBox1.Items.Clear();
            comboBox1.Items.Add("ADM - Administración");
            comboBox1.Items.Add("SIST - Sistemas");
            comboBox1.Items.Add("COM - Compras");
            comboBox1.Items.Add("VTA - Ventas");
            comboBox1.SelectedIndex = 0;

            // Initialize users (case-insensitive usernames)
            users = new Dictionary<string, UserInfo>(StringComparer.OrdinalIgnoreCase)
            {
                { "Adm",  new UserInfo("@1a",  new[] { "ADM", "COM", "VTA" }) },
                { "John", new UserInfo("*2b",  new[] { "SIST" }) },
                { "Ceci", new UserInfo("*@3c", new[] { "ADM", "VTA" }) },
                { "God",  new UserInfo("*@#4d", new[] { "ADM", "SIST", "COM", "VTA" }) },
            };
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Toggle between password masking and visible text
            if (checkBoxShowPassword.Checked)
            {
                textBox1.PasswordChar = '\0'; // no masking
            }
            else
            {
                textBox1.PasswordChar = '#';
            }
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            string user = (textBox2.Text ?? string.Empty).Trim();
            string pass = textBox1.Text ?? string.Empty;

            // Determine selected module code (extract first token before space or '-').
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un módulo.");
                return;
            }

            string selected = comboBox1.SelectedItem.ToString();
            string moduleCode = selected.Split(new[] { ' ', '-' }, StringSplitOptions.RemoveEmptyEntries)[0];

            // Validate user
            if (!users.TryGetValue(user, out var info) || info.Password != pass || System.Array.IndexOf(info.Modules, moduleCode) < 0)
            {
                failedAttempts++;
                MessageBox.Show("Usuario y/o contraseña incorrectos para el módulo seleccionado");
                if (failedAttempts >= 2)
                {
                    // Close the login form after two consecutive failures
                    this.Close();
                }
                return;
            }

            // Success
            failedAttempts = 0;

            // Open bienvenida form and hide login
            var bienvenida = new BienvenidaForm(user, moduleCode);
            bienvenida.Show();
            this.Hide();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
