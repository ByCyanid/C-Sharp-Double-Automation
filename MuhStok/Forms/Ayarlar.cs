using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ses;
using System.Data.OleDb;


namespace MuhStok.Forms
{
    public partial class Ayarlar : Form
    {
        public Ayarlar()
        {
            InitializeComponent();
        }

        public void Alert(string msg, Notification.Notification.enumType type)
        {
            Notification.Notification frm = new Notification.Notification();
            frm.showAlert(msg, type);
        }

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=muhasebe.accdb;Persist Security Info=True;Jet OLEDB:Database Password=cyanid38");
        Sesler bildirim = new Sesler();
        OleDbCommand cmd;
        OleDbCommand cmd2;
        OleDbDataReader dr2;
        string kulladi;
       
        private void radButton1_Click(object sender, EventArgs e)
        {
            
            cmd = new OleDbCommand();
            
            baglanti.Open();
            cmd.Connection = baglanti;
            cmd.CommandText = "update login set sifre='" + radTextBox2.Text + "' where kull_adi='" + radTextBox1.Text + "'";
            cmd.ExecuteNonQuery();

             Alert("Güncelleme Başarılı", Notification.Notification.enumType.Succes);
             bildirim.Success();
            

            baglanti.Close();
        }

        private void Ayarlar_Load(object sender, EventArgs e)
        {
            
            cmd2 = new OleDbCommand();
            baglanti.Open();
            cmd2.Connection = baglanti;
            cmd2.CommandText = "select * from login";
            dr2 = cmd2.ExecuteReader();
            while (dr2.Read())
            {
                kulladi = dr2["kull_adi"].ToString();
            }
            radTextBox1.Text = kulladi;
            baglanti.Close();
        }
       
        private void Ayarlar_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
    }
}
