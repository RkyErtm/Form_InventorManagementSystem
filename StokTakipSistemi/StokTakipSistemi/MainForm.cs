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
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private Form activeForm = null;
        private void openChildForm(Form childForm)
        {
            if (activeForm != null)
            {
                activeForm.Close();
            }

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelMain.Controls.Add(childForm);
            panelMain.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }


        private void btnKullanicilar_Click(object sender, EventArgs e)
        {
            openChildForm(new UserForm());
        }

        private void label6_Click(object sender, EventArgs e)
        {
            openChildForm(new UserForm());
        }

        private void btnMusteriler_Click(object sender, EventArgs e)
        {
            openChildForm(new CustomerForm());
        }

        private void btnKategoriler_Click(object sender, EventArgs e)
        {
            openChildForm(new CategoryForm());
        }
    }
}
