﻿using System;
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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void cbx_show_CheckedChanged(object sender, EventArgs e)
        {
            if (cbx_show.Checked == false)
                txtPass.UseSystemPasswordChar = true;
            else txtPass.UseSystemPasswordChar = false;
        }

        private void lblTemizle_Click(object sender, EventArgs e)
        {
            txtPass.Clear();
            txtUser.Clear();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Uygulamadan Çıkılsın mı?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
