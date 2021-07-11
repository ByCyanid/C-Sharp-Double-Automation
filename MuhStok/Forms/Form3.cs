using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using Ses;

namespace MuhStok.Forms
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }
        public void Alert(string msg, Notification.Notification.enumType type)
        {
            Notification.Notification frm = new Notification.Notification();
            frm.showAlert(msg, type);
        }
        Sesler bildirim = new Sesler();
        private void button1_Click(object sender, EventArgs e)
        {
            if (txtisadi.Text == "" || txtkrmadi.Text == "")
            {
                MessageBox.Show("Lütfen tüm alanları doldurunuz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string vtyolu = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=muhasebe.accdb;Persist Security Info=True;Jet OLEDB:Database Password=cyanid38";
                OleDbConnection baglanti = new OleDbConnection(vtyolu);
                baglanti.Open();
                string ekle = "insert into kurumlar(kurumturu,kurumadi) values (@kurumturu,@kurumadi)";
                OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                komut.Parameters.AddWithValue("@kurumturu", txtisadi.Text);
                komut.Parameters.AddWithValue("@kurumadi", txtkrmadi.Text);
                komut.ExecuteNonQuery();
                baglanti.Close();
                Alert("Kayıt Başarıyla Eklendi", Notification.Notification.enumType.Succes);
                bildirim.Success();
                button2.PerformClick();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            string vtyolu = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=muhasebe.accdb;Persist Security Info=True;Jet OLEDB:Database Password=cyanid38";
            OleDbConnection baglanti = new OleDbConnection(vtyolu);

            baglanti.Open();
            string komut = "select kimlik,kurumturu,kurumadi FROM kurumlar ORDER by kimlik ASC";
            OleDbDataAdapter sda = new OleDbDataAdapter(komut, baglanti);
            System.Data.DataTable dt = new System.Data.DataTable();
            sda.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
            dataGridView1.Columns[0].HeaderText = "ID";
            dataGridView1.Columns[1].HeaderText = "Kurum türü";
            dataGridView1.Columns[2].HeaderText = "Kurum adı";

            dataGridView1.Columns[0].Width = 25;
            label1.Text = dataGridView1.CurrentRow.Cells["Kimlik"].Value.ToString();
            
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            button2.PerformClick();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            label1.Text = dataGridView1.CurrentRow.Cells["Kimlik"].Value.ToString();
        }

        DialogResult aktaronay = new DialogResult();

        private void button3_Click(object sender, EventArgs e)
        {
            aktaronay = MessageBox.Show("Silmek istediğinizden emin misiniz? \n\nDikkat: Geri dönüşümü yoktur...", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (aktaronay == DialogResult.Yes)
            {
                string vtyolu = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=muhasebe.accdb;Persist Security Info=True";
                OleDbConnection baglanti = new OleDbConnection(vtyolu);
                baglanti.Open();
                string ekle = "DELETE FROM kurumlar WHERE Kimlik = " + dataGridView1.CurrentRow.Cells["Kimlik"].Value + "";
                OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                button2.PerformClick();
                Alert("Kayıt Başarıyla Silindi", Notification.Notification.enumType.Succes);
                bildirim.Success();
            }
        }

        
    }
}
