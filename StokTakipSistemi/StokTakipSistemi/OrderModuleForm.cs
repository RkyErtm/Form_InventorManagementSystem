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
    public partial class OrderModuleForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\VKFGLB\Documents\dbStokTakip.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        int adet = 0;
        public OrderModuleForm()
        {
            InitializeComponent();
            LoadCustomer();
            LoadProduct();
        }

        public void LoadCustomer()
        {
            int i = 0;
            dgvCustomer.Rows.Clear();
            cmd = new SqlCommand("SELECT MusteriId,MusteriAdSoyad FROM tbCustomer where concat(MusteriId,MusteriAdSoyad) like '%" + txtMusteriAra.Text + "%'", conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvCustomer.Rows.Add(i, reader[0].ToString(), reader[1].ToString());
            }
            reader.Close();
            conn.Close();
        }

        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbProduct where CONCAT(ad,adet,fiyat,aciklama,pkategori) like '%" + txtUrunAra.Text + "%'", conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvProduct.Rows.Add(i, reader[0].ToString(), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
            }
            reader.Close();
            conn.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
            LoadCustomer();
        }

        private void txtMusteriAra_TextChanged(object sender, EventArgs e)
        {
            LoadCustomer();
        }

        private void txtUrunAra_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }


        private void numUrunAdet_ValueChanged(object sender, EventArgs e)
        {
            GetQty();
            if (Convert.ToInt16(numUrunAdet.Value) > adet)
            {
                MessageBox.Show("Stok Sınırını Aştınız!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numUrunAdet.Value = numUrunAdet.Value - 1;
                return;
            }
            if (Convert.ToInt16(numUrunAdet.Value) > 0)
            {
                int total = Convert.ToInt16(txtUrunFiyat.Text) * Convert.ToInt16(numUrunAdet.Value);
                txtToplam.Text = total.ToString();
            }

        }

        private void dgvCustomer_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMusteriId.Text = dgvCustomer.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtMusteriAd.Text = dgvCustomer.Rows[e.RowIndex].Cells[2].Value.ToString();

        }

        private void dgvProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtUrunId.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtUrunAd.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtUrunFiyat.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
            //adet = Convert.ToInt16(dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString());
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            try
            {
                if (txtMusteriId.Text == "")
                {
                    MessageBox.Show("Lütfen müşteri seçiniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtUrunId.Text == "")
                {
                    MessageBox.Show("Lütfen ürün seçiniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("Kaydedilsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("Insert into tbOrder(date,productId,customerId,adet,fiyat,toplam)Values(@date,@productId,@customerId,@adet,@fiyat,@toplam)", conn);
                    cmd.Parameters.AddWithValue("@date", datetime.Value);
                    cmd.Parameters.AddWithValue("@productId", Convert.ToInt16(txtUrunId.Text));
                    cmd.Parameters.AddWithValue("@customerId", Convert.ToInt16(txtMusteriId.Text));
                    cmd.Parameters.AddWithValue("@adet", Convert.ToInt16(numUrunAdet.Text));
                    cmd.Parameters.AddWithValue("@fiyat", Convert.ToInt16(txtUrunFiyat.Text));
                    cmd.Parameters.AddWithValue("@toplam", Convert.ToInt16(txtToplam.Text));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Sipariş Oluşturuldu.");


                    cmd = new SqlCommand("Update tbProduct set adet=(adet-@adet) where id LIKE '" + txtUrunId.Text + "'", conn);
                    cmd.Parameters.AddWithValue("@adet", Convert.ToInt16(numUrunAdet.Value));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Clear();
                    LoadProduct();

                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        public void Clear()
        {
            txtMusteriId.Clear();
            txtMusteriAd.Clear();

            txtUrunId.Clear();
            txtUrunAd.Clear();

            txtUrunFiyat.Clear();
            numUrunAdet.Value = 0;
            txtToplam.Clear();
            datetime.Value = DateTime.Now;
        }

        private void bntSil_Click(object sender, EventArgs e)
        {
            Clear();
            btnEkle.Enabled = true;
            btnGuncelle.Enabled = false;
        }

        public void GetQty()
        {
            dgvProduct.Rows.Clear();
            cmd = new SqlCommand("SELECT adet FROM tbProduct where urunId= '" + txtUrunId.Text + "'", conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                adet = Convert.ToInt32(reader[0].ToString());
            }
            reader.Close();
            conn.Close();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {

        }
    }
}
