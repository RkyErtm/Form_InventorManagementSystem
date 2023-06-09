using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace StokTakipSistemi
{
    public partial class UserModuleForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\VKFGLB\Documents\dbStokTakip.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        public UserModuleForm()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSifre.Text != txtSifreOnayla.Text)
                {
                    MessageBox.Show("Şifre Eşleşmiyor.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Kaydedilsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("Insert into tbUser(AdSoyad,KullaniciAdi,Sifre,Telefon)Values(@AdSoyad,@KullaniciAdi,@Sifre,@Telefon)", conn);
                    cmd.Parameters.AddWithValue("@AdSoyad", txtAdSoyad.Text);
                    cmd.Parameters.AddWithValue("@KullaniciAdi", txtKullaniciAdi.Text);
                    cmd.Parameters.AddWithValue("Sifre", txtSifre.Text);
                    cmd.Parameters.AddWithValue("@Telefon", txtTel.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Kullanıcı Başarıyla Kaydedildi.");
                    Clear();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            txtAdSoyad.Clear();
            txtKullaniciAdi.Clear();
            txtSifre.Clear();
            txtSifreOnayla.Clear();
            txtTel.Clear();
        }

        private void bntSil_Click(object sender, EventArgs e)
        {
            Clear();
            btnEkle.Enabled = true;
            btnGuncelle.Enabled = false;
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSifre.Text != txtSifreOnayla.Text)
                {
                    MessageBox.Show("Şifre Eşleşmiyor.", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Güncellensin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("Update tbUser set AdSoyad=@AdSoyad,Sifre=@Sifre,Telefon=@Telefon where KullaniciAdi LIKE '" + txtKullaniciAdi.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@AdSoyad", txtAdSoyad.Text);
                    cmd.Parameters.AddWithValue("Sifre", txtSifre.Text);
                    cmd.Parameters.AddWithValue("@Telefon", txtTel.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Kullanıcı Başarıyla Güncellendi.");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void UserModuleForm_Load(object sender, EventArgs e)
        {

        }
    }
}
