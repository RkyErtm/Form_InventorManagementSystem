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
    public partial class ProductForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\VKFGLB\Documents\dbStokTakip.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public ProductForm()
        {
            InitializeComponent();
            LoadProduct();
        }


        public void LoadProduct()
        {
            int i = 0;
            dgvProduct.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbProduct where concat(ad,adet,fiyat,aciklama,pkategori) like '%" + txtArama.Text + "%'", conn);
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

        private void btn_Add_Click(object sender, EventArgs e)
        {
            ProductModuleForm frm = new ProductModuleForm();
            frm.btnGuncelle.Enabled = false;
            frm.btnEkle.Enabled = true;
            frm.ShowDialog();
            LoadProduct();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvProduct.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                ProductModuleForm module = new ProductModuleForm();
                module.lblProductId.Text = dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString();
                module.txtUrunAd.Text = dgvProduct.Rows[e.RowIndex].Cells[2].Value.ToString();
                module.txtAdet.Text = dgvProduct.Rows[e.RowIndex].Cells[3].Value.ToString();
                module.txtFiyat.Text = dgvProduct.Rows[e.RowIndex].Cells[4].Value.ToString();
                module.txtAciklama.Text = dgvProduct.Rows[e.RowIndex].Cells[5].Value.ToString();
                module.cbxCtg.Text = dgvProduct.Rows[e.RowIndex].Cells[6].Value.ToString();

                module.btnEkle.Enabled = false;
                module.btnGuncelle.Enabled = true;
                module.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Ürün Silinsin mi", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("Delete from tbProduct where id LIKE '" + dgvProduct.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Ürün silindi!");
                }
            }
            LoadProduct();
        }

        private void txtArama_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }
    }
}
