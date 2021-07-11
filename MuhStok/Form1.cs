using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using FontAwesome.Sharp;
using Ses;
using System.IO;

namespace MuhStok
{
    public partial class Form1 : Form
    {
        private IconButton currentBtn;
        private Panel leftBorderBtn;
        private Form currentChildForm;


        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=muhasebe.accdb;Persist Security Info=True;Jet OLEDB:Database Password=cyanid38");
        OleDbCommand cmd1;
        OleDbDataReader dr;

        OleDbCommand cmd2;
        OleDbCommand cmd3;
        OleDbDataReader dr2;
        OleDbDataReader dr3;
        
        //Ayar Verisinin Tutulduğu Class
        INIKaydet INI = new INIKaydet(Application.StartupPath + @"\devsettings.ini");

        public void Alert(string msg, Notification.Notification.enumType type)
        {
            Notification.Notification frm = new Notification.Notification();
            frm.showAlert(msg, type);
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            if (ıconButton2.Enabled == false&& ıconButton6.Enabled == true)
            {
                radLabel4.Text = "Sınırsız-Tek(Stok)";
            }
            else if (ıconButton2.Enabled==true&&ıconButton6.Enabled==false)
            {
                radLabel4.Text = "Sınırsız-Tek(Gelir-Gider)";
            }

            else
            {
                radLabel4.Text = "Sınırsız-Çift(Full Paket)";
            }
            
            timer1.Start();
            ıconButton1.Enabled = false;
           
            #region Sifreac
            Byte[] dosya = File.ReadAllBytes(Application.StartupPath + "//devsettings.ini");
            for (int i = 0; i < dosya.Length; i++)
            {
                
                if ((Byte)((int)dosya[i] - 1)<0)
                {
                    dosya[i] = 255;
                }
                else
                {
                    dosya[i] = (Byte)((int)dosya[i] - 1);
                }
            }
            File.WriteAllBytes(Application.StartupPath + "//devsettings.ini", dosya);
            #endregion

            #region AyarYukle
            pictureBox1.ImageLocation = Application.StartupPath + "//resim1111.jpeg";
            radBarcode1.Value = INI.Oku("DATA", "QrCode");
            
            ıconButton6.Visible = Convert.ToBoolean(INI.Oku("DATA", "Stok"));
            ıconButton2.Visible = Convert.ToBoolean(INI.Oku("DATA", "Muhasebe"));

            #endregion

            #region Sifrele
            Byte[] dosya2 = File.ReadAllBytes(Application.StartupPath + "//devsettings.ini");
            for (int i = 0; i < dosya2.Length; i++)
            {
                dosya2[i] = (Byte)((int)dosya2[i] + 1);
                if (dosya2[i] > 255)
                {
                    dosya2[i] = 0;
                }
            }
            File.WriteAllBytes(Application.StartupPath + "//devsettings.ini", dosya2);
            #endregion


            //Card Kayıt İşlemleri
            baglanti.Open();
            cmd1 = new OleDbCommand("Select SUM(kasadurum) as kasa From kasadurumm", baglanti);
            cmd1.ExecuteNonQuery();
            dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                xuıCard1.Text2 = dr["kasa"].ToString()+" "+ "₺";
            }
            baglanti.Close();

            baglanti.Open();
          

            cmd2 = new OleDbCommand("SELECT Sum(tutar) as toplam from gelirgider", baglanti);
            
            cmd2.ExecuteNonQuery();
            dr2 = cmd2.ExecuteReader();

            while (dr2.Read())
            {
                
                xuıCard2.Text2 = dr2["toplam"].ToString() + " " + "₺";
            }
            baglanti.Close();

            baglanti.Open();
            cmd3 = new OleDbCommand("SELECT count(gelir) as adet from gelirgider", baglanti);

            cmd3.ExecuteNonQuery();
            dr3 = cmd3.ExecuteReader();

            while (dr3.Read())
            {

                xuıCard3.Text2 = dr3["adet"].ToString() + " " + "Adet Kayıt Bulunmaktadır";
            }
            baglanti.Close();

            

           
            

        }

        private struct RgbColors
        {
            public static Color color1 = Color.FromArgb(172, 126, 241);
            public static Color color2 = Color.FromArgb(249, 118, 176);
            public static Color color3 = Color.FromArgb(254, 138, 114);
            public static Color color4 = Color.FromArgb(95, 77, 221);
            public static Color color5 = Color.FromArgb(249, 88, 155);
            public static Color color6 = Color.FromArgb(24, 161, 251);

        }
        private void Reset()
        {
            DisableBtn();
            leftBorderBtn.Visible = false;
            ıconPictureBox1.IconChar = IconChar.Home;
            label1.Text = "Anasayfa";
            ıconPictureBox1.IconColor = Color.MediumPurple;

        }

        private void ActiveButton(object senderBtn,Color color)
        {
            if(senderBtn!=null)
            {
                DisableBtn();

                currentBtn = (IconButton)senderBtn;
                currentBtn.BackColor = Color.FromArgb(33, 20, 24);
                currentBtn.ForeColor = color;
                currentBtn.TextAlign = ContentAlignment.MiddleCenter;
                currentBtn.IconColor = color;
                currentBtn.TextImageRelation = TextImageRelation.TextBeforeImage;
                currentBtn.ImageAlign = ContentAlignment.MiddleRight;

                leftBorderBtn.BackColor = color;
                leftBorderBtn.Location = new Point(0, currentBtn.Location.Y);
                leftBorderBtn.Visible = true;
                leftBorderBtn.BringToFront();

                ıconPictureBox1.IconChar = currentBtn.IconChar;
                ıconPictureBox1.IconColor = color;
            }
        }
        private void DisableBtn()
        {
            if(currentBtn!=null)
            {
                currentBtn.BackColor = Color.FromArgb(33, 20, 24);
                currentBtn.ForeColor = Color.Gainsboro;
                currentBtn.TextAlign = ContentAlignment.MiddleLeft;
                currentBtn.IconColor = Color.Gainsboro;
                currentBtn.TextImageRelation = TextImageRelation.ImageBeforeText;
                currentBtn.ImageAlign = ContentAlignment.MiddleLeft;

            }
        }

        public Form1()
        {
            InitializeComponent();
            leftBorderBtn = new Panel();
            leftBorderBtn.Size = new Size(7, 100);
            panel1.Controls.Add(leftBorderBtn);
            
            
            
           
        }
        private void OpenChildForm(Form childForm)
        {
            if(currentChildForm!=null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panelDesktop.Controls.Add(childForm);
            panelDesktop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            label1.Text = childForm.Text;

        }

        public void ResimDegistir(Image resim)
        {
            pictureBox1.Image = resim;
        }


        private void ıconButton1_Click(object sender, EventArgs e)
        {
           
                ActiveButton(sender, RgbColors.color1);
                currentChildForm.Close(); 
            
        }

        private void ıconButton2_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RgbColors.color2);
            OpenChildForm(new Forms.Form2());
            ıconButton1.Enabled = true;
           
        }

        private void ıconButton3_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RgbColors.color3);
            OpenChildForm(new Forms.Form3());
            ıconButton1.Enabled = true;
        }

        private void ıconButton4_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RgbColors.color4);
            OpenChildForm(new Forms.Form4());
            ıconButton1.Enabled = true;
        }

        

        private void ıconButton6_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RgbColors.color6);
            OpenChildForm(new Forms.Stok());
            ıconButton1.Enabled = true;
        }

       

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd,int wmsg,int wParam,int lParam);


        private void panel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

       

        private void ıconButton9_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void ıconButton8_Click(object sender, EventArgs e)
        {
            if(this.WindowState==FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
                
                ıconButton8.IconChar = IconChar.WindowRestore;

            }
            else if (this.WindowState == FormWindowState.Maximized)
            {
                this.WindowState = FormWindowState.Normal;
                ıconButton8.IconChar = IconChar.WindowMaximize;
            }
        }

        private void ıconButton7_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void ıconButton7_MouseHover(object sender, EventArgs e)
        {
            ıconButton7.BackColor = Color.Red;
        }

        private void ıconButton7_MouseLeave(object sender, EventArgs e)
        {
            ıconButton7.BackColor = Color.FromArgb(28,15,18);
        }

        private void ıconButton8_MouseHover(object sender, EventArgs e)
        {
            ıconButton8.BackColor = Color.FromArgb(91, 89, 145);
        }

        private void ıconButton8_MouseLeave(object sender, EventArgs e)
        {
            ıconButton8.BackColor = Color.FromArgb(28, 15, 18);
        }

        private void ıconButton9_MouseHover(object sender, EventArgs e)
        {
            ıconButton9.BackColor = Color.FromArgb(235, 135, 106);

        }

        private void ıconButton9_MouseLeave(object sender, EventArgs e)
        {
            ıconButton9.BackColor = Color.FromArgb(28, 15, 18);
        }

        private void ıconButton5_Click(object sender, EventArgs e)
        {
            ActiveButton(sender, RgbColors.color5);
            OpenChildForm(new Forms.Ayarlar());
        }

        private void xuıCard1_Click(object sender, EventArgs e)
        {

        }

        private void panelDesktop_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                xuıBatteryPercentageAPI1.Start();
                radLabel1.Text = "%" + xuıBatteryPercentageAPI1.Value.ToString();
                xuıFlatProgressBar2.Value = xuıBatteryPercentageAPI1.Value;

                if (xuıFlatProgressBar2.Value <= 30)
                {
                    Alert("Lütfen Bilgisayarınızı Şarza Takın", Notification.Notification.enumType.Info);
                    
                }
            }
            catch
            {
                xuıBatteryPercentageAPI1.Stop();
                radLabel1.Visible = false;
                radLabel2.Visible = false;
                xuıFlatProgressBar2.Visible = false;
                timer1.Stop();
            }
            

            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }
       
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void radBarcode1_Click(object sender, EventArgs e)
        {

        }

        private void panelDesktop_Paint_1(object sender, PaintEventArgs e)
        {

        }
    }
}
