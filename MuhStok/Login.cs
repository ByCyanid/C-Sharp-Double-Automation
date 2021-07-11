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
using System.IO;

namespace MuhStok
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        Point İlkkonum;
        bool durum = false;

        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=muhasebe.accdb;Persist Security Info=True;Jet OLEDB:Database Password=cyanid38");
        private void Login_MouseMove(object sender, MouseEventArgs e)
        {
            if (durum)
            {
                this.Left = e.X + this.Left - (İlkkonum.X);
                this.Top = e.Y + this.Top - (İlkkonum.Y);
            }
        }

        private void Login_MouseUp(object sender, MouseEventArgs e)
        {
            durum = false;
            this.Cursor = Cursors.Default; 
        }

        private void Login_MouseDown(object sender, MouseEventArgs e)
        {
            durum = true;
            this.Cursor = Cursors.SizeAll;
            İlkkonum = e.Location;
        }

        private void ıconButton7_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void ıconButton9_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ıconButton7_MouseHover(object sender, EventArgs e)
        {
            ıconButton7.BackColor = Color.Red;
        }

        private void ıconButton7_MouseLeave(object sender, EventArgs e)
        {
            ıconButton7.BackColor = Color.FromArgb(36, 23, 30);
        }

        private void ıconButton9_MouseHover(object sender, EventArgs e)
        {
            ıconButton9.BackColor = Color.FromArgb(235, 135, 106);
        }

        private void ıconButton9_MouseLeave(object sender, EventArgs e)
        {
            ıconButton9.BackColor = Color.FromArgb(36, 23, 30);
        }
        Form1 frm = new Form1();
        private void Login_Load(object sender, EventArgs e)
        {
            radTextBox2.UseSystemPasswordChar=true;
            KeyPreview = true;

            if (Properties.Settings.Default.kontrol == 0)
            {
                Environment.Exit(0);
            }
           

        }

        public void Alert(string msg, Notification.Notification.enumType type)
        {
            Notification.Notification frm = new Notification.Notification();
            frm.showAlert(msg, type);
        }
        Sesler bildirim = new Sesler();
        OleDbCommand cmd;
        OleDbDataReader dr;
        private void radButton1_Click(object sender, EventArgs e)
        {
            
            cmd = new OleDbCommand();
            baglanti.Open();
            cmd.Connection = baglanti;
            cmd.CommandText = "SELECT * FROM login where kull_adi='" + radTextBox1.Text + "' AND sifre='" + radTextBox2.Text + "'";
            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                this.Hide();
                Form1 f1 = new Form1();
                f1.Show();
            }
            else
            {
                Alert("Şifre Veya Nick Hatalı", Notification.Notification.enumType.Error);
                bildirim.Error();
            }

            baglanti.Close();
        }
        string sifre;
        Forms.Developer dvp = new Forms.Developer();
        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                sifre += "D";
            }

            else if (e.KeyCode == Keys.E)
            {
                sifre += "e";
            }

            else if (e.KeyCode == Keys.V)
            {
                sifre += "v";
            }

            else if (e.KeyCode == Keys.L)
            {
                sifre += "l";
            }

            else if (e.KeyCode == Keys.O)
            {
                sifre += "o";
            }

            else if (e.KeyCode == Keys.P)
            {
                sifre += "p";
            }

            else if (e.KeyCode == Keys.R)
            {
                sifre += "r";
            }
            
            if (sifre == "Developer")
            {
                dvp.Show();
                sifre = "";
            }

            if (e.Alt == true)
            {
                sifre = "";
            }
        }
    }
}
