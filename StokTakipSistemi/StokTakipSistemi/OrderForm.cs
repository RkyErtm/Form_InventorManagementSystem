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
            int i = 0;
            dgvOrder.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbOrder", conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvOrder.Rows.Add(i, reader[0].ToString(), Convert.ToDateTime(reader[1].ToString()).ToString("dd/MM/yyyy"), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
            }
            reader.Close();
            conn.Close();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            OrderModuleForm moduleForm = new OrderModuleForm();
            moduleForm.btnEkle.Enabled = true;
            moduleForm.btnGuncelle.Enabled = false;
            moduleForm.ShowDialog();
            LoadOrder();
        }

        private void dgvProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dgvOrder.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                OrderModuleForm orderModule = new OrderModuleForm();
                orderModule.lblSiparisId.Text = dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString();
                orderModule.datetime.Text = dgvOrder.Rows[e.RowIndex].Cells[2].Value.ToString();
                orderModule.txtUrunId.Text = dgvOrder.Rows[e.RowIndex].Cells[3].Value.ToString();
                orderModule.txtMusteriId.Text = dgvOrder.Rows[e.RowIndex].Cells[4].Value.ToString();
                orderModule.numUrunAdet.Text = dgvOrder.Rows[e.RowIndex].Cells[5].Value.ToString();
                orderModule.txtUrunFiyat.Text = dgvOrder.Rows[e.RowIndex].Cells[6].Value.ToString();

                orderModule.btnEkle.Enabled = false;
                orderModule.btnGuncelle.Enabled = true;
                orderModule.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Kullanıcı Silinsin mi", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("Delete from tbOrder where siparidId LIKE '" + dgvOrder.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Sipariş silindi!");
                }
            }
            LoadOrder();
        }
    }
}
