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
    public partial class OrderForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\VKFGLB\Documents\dbStokTakip.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public OrderForm()
        {
            InitializeComponent();
            LoadOrder();
        }

        public void LoadOrder()
        {
            double toplam = 0;
            int i = 0;
            dgvOrder.Rows.Clear();
            cmd = new SqlCommand("SELECT o.siparisId,o.date,o.productId,p.ad,o.customerId,c.MusteriAdSoyad,o.adet,o.fiyat,o.toplam FROM tbOrder as o join tbCustomer as c on o.customerId=c.MusteriId join tbProduct as p on o.productId=p.id where concat(o.siparisId,o.date,o.productId,p.ad,o.customerId,c.MusteriAdSoyad,o.adet,o.fiyat) like '%" + txtArama.Text + "%'", conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, reader[0].ToString(), Convert.ToDateTime(reader[1].ToString()).ToString("dd/MM/yyyy"), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString());
                toplam += Convert.ToInt32(reader[8].ToString());
            }
            reader.Close();
            conn.Close();

            lblAdet.Text = i.ToString();
            lblToplam.Text = toplam.ToString();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            OrderModuleForm moduleForm = new OrderModuleForm();
            moduleForm.ShowDialog();
            LoadOrder();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Kullanıcı Silinsin mi", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("Delete from tbOrder where siparisId LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Sipariş silindi!");

                    cmd = new SqlCommand("Update tbProduct set adet=(adet+@adet) where id LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString() + "'", conn);
                    cmd.Parameters.AddWithValue("@adet", Convert.ToInt16(dgvOrder.Rows[e.RowIndex].Cells[5].Value.ToString()));

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();


                }
            }
            LoadOrder();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            LoadOrder();
        }
    }
}
