using System;
using System.Drawing;
using System.Windows.Forms;

namespace pryTumminellilLogin
{
    public class BienvenidaForm : Form
    {
        private Label lblWelcome;

        public BienvenidaForm(string user, string module)
        {
            InitializeComponent();
            lblWelcome.Text = $"Bienvenido {user} - Módulo: {module}";
        }

        private void InitializeComponent()
        {
            this.lblWelcome = new Label();
            this.SuspendLayout();
            // 
            // lblWelcome
            // 
            this.lblWelcome.AutoSize = true;
            this.lblWelcome.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.lblWelcome.Location = new Point(20, 20);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new Size(200, 20);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "Bienvenido";
            // 
            // BienvenidaForm
            // 
            this.ClientSize = new Size(400, 120);
            this.Controls.Add(this.lblWelcome);
            this.Name = "BienvenidaForm";
            this.Text = "Bienvenida - Sintepart SRL";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
