﻿using System;
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
    public partial class CostumerModuleForm : Form
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\VKFGLB\Documents\dbStokTakip.mdf;Integrated Security=True;Connect Timeout=30");
        SqlCommand cmd = new SqlCommand();
        public CostumerModuleForm()
        {
            InitializeComponent();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Kaydedilsin mi?", "UYARI", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cmd = new SqlCommand("Insert into tbCustomer(MusteriAdSoyad,MusteriTelefon)Values(@MusteriAdSoyad,@MusteriTelefon)", conn);
                    cmd.Parameters.AddWithValue("@MusteriAdSoyad", txtMAdSoyad.Text);
                    cmd.Parameters.AddWithValue("@MusteriTelefon", txtMTel.Text);
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
            txtMAdSoyad.Clear();
            txtMTel.Clear();
        }

        private void bntSil_Click(object sender, EventArgs e)
        {
            Clear();
            btnEkle.Enabled = true;
            btnGuncelle.Enabled = false;
        }
    }
}
