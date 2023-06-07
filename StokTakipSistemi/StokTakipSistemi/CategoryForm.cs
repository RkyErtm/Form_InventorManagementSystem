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
    public partial class CategoryForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\VKFGLB\Documents\dbStokTakip.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        SqlDataReader reader;
        public CategoryForm()
        {
            InitializeComponent();
            LoadCategory();
        }

        public void LoadCategory()
        {
            int i = 0;
            dgvCategory.Rows.Clear();
            cmd = new SqlCommand("SELECT * FROM tbCategory", conn);
            conn.Open();
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                i++;
                dgvCategory.Rows.Add(i, reader[0].ToString(), reader[1].ToString());
            }
            reader.Close();
            conn.Close();
        }

        private void dgvCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            string colName = dgvCategory.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                CategoryModuleForm frm = new CategoryModuleForm();
                frm.lblMusterId.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.txtKategori.Text = dgvCategory.Rows[e.RowIndex].Cells[2].Value.ToString();

                frm.btnEkle.Enabled = false;
                frm.btnGuncelle.Enabled = true;
                frm.ShowDialog();
            }
            else if (colName == "Delete")
            {
                if (MessageBox.Show("Kategori Silinsin mi", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    conn.Open();
                    cmd = new SqlCommand("Delete from tbCategory where KategoriId LIKE '" + dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", conn);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Kategori silindi!");
                }
            }
            LoadCategory();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            CategoryModuleForm frm = new CategoryModuleForm();
            frm.btnGuncelle.Enabled = false;
            frm.btnEkle.Enabled = true;
            frm.ShowDialog();
            LoadCategory();
        }
    }
}
