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
    public partial class ProductModuleForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\VKFGLB\Documents\dbStokTakip.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader rd;
        public ProductModuleForm()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            cmd = new SqlCommand("Select KategoriAd from tbCategory", conn);
            conn.Open();
            rd = cmd.ExecuteReader();
            while (rd.Read())
            {
                cbxCtg.Items.Add(rd[0].ToString());
            }
            rd.Close();
            conn.Close();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Kaydedilsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("Insert into tbProduct(ad,adet,fiyat,aciklama,pkategori)Values(@ad,@adet,@fiyat,@aciklama,@pkategori)", conn);
                    cmd.Parameters.AddWithValue("@ad", txtUrunAd.Text);
                    cmd.Parameters.AddWithValue("@adet", Convert.ToInt16(txtAdet.Text));
                    cmd.Parameters.AddWithValue("@fiyat", Convert.ToInt16(txtFiyat.Text));
                    cmd.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
                    cmd.Parameters.AddWithValue("@pkategori", cbxCtg.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Ürün Başarıyla Kaydedildi.");
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
            txtUrunAd.Clear();
            txtAdet.Clear();
            txtFiyat.Clear();
            txtAciklama.Clear();
            cbxCtg.Text = "";

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
                    cmd = new SqlCommand("Update tbProduct set ad=@ad,adet=@adet,fiyat=@fiyat,aciklama=@aciklama,pkategori=@pkategori where id LIKE '" + lblProductId.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@ad", txtUrunAd.Text);
                    cmd.Parameters.AddWithValue("@adet", Convert.ToInt16(txtAdet.Text));
                    cmd.Parameters.AddWithValue("@fiyat", Convert.ToInt16(txtFiyat.Text));
                    cmd.Parameters.AddWithValue("@aciklama", txtAciklama.Text);
                    cmd.Parameters.AddWithValue("@pkategori", cbxCtg.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Ürün Başarıyla Güncellendi.");
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
