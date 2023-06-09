using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokTakipSistemi
{
    public partial class WelcomeForm : Form
    {
        public WelcomeForm()
        {
            InitializeComponent();
            timer1.Start();
        }

        int start = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            start += 5;
            progressBar2.Value = start;
            if (progressBar2.Value == 100)
            {
                progressBar2.Value = 0; 
                timer1.Stop();

                LoginForm frm = new LoginForm();
                this.Hide();
                frm.ShowDialog();
            }
        }
    }
}
