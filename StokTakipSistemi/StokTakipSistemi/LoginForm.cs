using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StokTakipSistemi
{
    public partial class LoginForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\VKFGLB\Documents\dbStokTakip.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
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

        private void btn_giris_Click(object sender, EventArgs e)
        {
            try
            {
                cmd = new SqlCommand("Select * from tbUser where KullaniciAdi=@KullaniciAdi and Sifre=@Sifre", conn);
                cmd.Parameters.AddWithValue("@KullaniciAdi", txtUser.Text);
                cmd.Parameters.AddWithValue("@Sifre", txtPass.Text);
                conn.Open();
                reader=cmd.ExecuteReader();
                reader.Read();
                if (reader.HasRows)
                {
                    MessageBox.Show("Hoşgeldiniz"+ " | " +reader["AdSoyad"].ToString() , "Erişim İzni Verildi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MainForm main = new MainForm();
                    this.Hide();
                    main.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Geçersiz kullanıcı adı veya şifre", "Erişim İzni Engellendi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                conn.Close();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
