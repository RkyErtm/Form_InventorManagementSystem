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
    public partial class CategoryModuleForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\VKFGLB\Documents\dbStokTakip.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        public CategoryModuleForm()
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
                if (MessageBox.Show("Kaydedilsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("Insert into tbCategory(KategoriAd)Values(@KategoriAd)", conn);
                    cmd.Parameters.AddWithValue("@KategoriAd", txtKategori.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Kategori Başarıyla Kaydedildi.");
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
            txtKategori.Clear();
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
                if (MessageBox.Show("Güncellensin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("Update tbCategory set KategoriAd=@KategoriAd where KategoriId LIKE '" + lblMusterId.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@KategoriAd", txtKategori.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Kategori Başarıyla Güncellendi.");
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
