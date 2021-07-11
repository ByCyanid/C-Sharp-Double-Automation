using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace MuhStok.Forms
{
    public partial class Developer : Form
    {
        public Developer()
        {
            InitializeComponent();
        }

        private void ıconButton7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void radButton1_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "jpeg dosyası(*.jpg)|*.jpg|Bitmap(*.bmp)|*.bmp|*png dosyası(*.png)|*";
                pictureBox1.Image.Save(Application.StartupPath + "\\resim1111.jpeg");
            }
            catch
            {

                MessageBox.Show("Logo Seçmeden Kaydedemezsiniz!!!");
            }

        }
        Point İlkkonum;
        bool durum = false;
        private void Developer_MouseMove(object sender, MouseEventArgs e)
        {
            if (durum)
            {
                this.Left = e.X + this.Left - (İlkkonum.X);
                this.Top = e.Y + this.Top - (İlkkonum.Y);
            }
        }

        private void Developer_MouseUp(object sender, MouseEventArgs e)
        {
            durum = false;
            this.Cursor = Cursors.Default;
        }

        private void Developer_MouseDown(object sender, MouseEventArgs e)
        {
            durum = true;
            this.Cursor = Cursors.SizeAll;
            İlkkonum = e.Location;
        }

        private void radButton2_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;



        }
        INIKaydet INI = new INIKaydet(Application.StartupPath + @"\devsettings.ini");
        INIKaydet INI2 = new INIKaydet(Application.StartupPath + @"\licence.ini");

        string Qr;

        private void Developer_Load(object sender, EventArgs e)
        {

            

            #region Sifreac
            Byte[] dosya = File.ReadAllBytes(Application.StartupPath + "//devsettings.ini");
            for (int i = 0; i < dosya.Length; i++)
            {

                if ((Byte)((int)dosya[i] - 1) < 0)
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
            radButton7.Enabled = false;
            #region Açılış

            Qr = INI.Oku("DATA", "QrCode");
            ambiance_Toggle1.Toggled = Convert.ToBoolean(INI.Oku("DATA", "Stok"));
            ambiance_Toggle2.Toggled = Convert.ToBoolean(INI.Oku("DATA", "Muhasebe"));
            //QrCode
            ambiance_TextBox1.Text = Qr;
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


            radButton5.Enabled = false;
        }
        private void radButton3_Click(object sender, EventArgs e)
        {
            INI.Yaz("DATA", "QrCode", ambiance_TextBox1.Text);
        }
        private void ambiance_Toggle1_ToggledChanged()
        {
           
            if (ambiance_Toggle1.Toggled == true)
            {
                INI.Yaz("DATA", "Stok", "true");
            }

            else if (ambiance_Toggle1.Toggled == false)
            {
                INI.Yaz("DATA", "Stok", "false");
            }
        }
       

        private void ambiance_Toggle2_ToggledChanged()
        {
            
            if (ambiance_Toggle2.Toggled == true)
            {
                INI.Yaz("DATA", "Muhasebe", "true");
            }

            else if (ambiance_Toggle2.Toggled == false)
            {
                INI.Yaz("DATA", "Muhasebe", "false");
            }
        }
        
        private void radButton4_Click(object sender, EventArgs e)
        {
               Byte[] dosya = File.ReadAllBytes(Application.StartupPath + "//devsettings.ini");
               for (int i = 0; i < dosya.Length; i++)
               {
                    if ((Byte)((int)dosya[i] - 1) < 0)
                    {
                        dosya[i] = 255;
                    }
                    else
                    {
                        dosya[i] = (Byte)((int)dosya[i] - 1);
                    }
               }
            File.WriteAllBytes(Application.StartupPath + "//devsettings.ini", dosya);
            radButton4.Enabled = false;
            radButton5.Enabled = true;
        }

        private void radButton5_Click(object sender, EventArgs e)
        {
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
            radButton4.Enabled = true;
            radButton5.Enabled = false;
        }

        private void radButton6_Click(object sender, EventArgs e)
        {
            #region Sifreac
            Byte[] dosya = File.ReadAllBytes(Application.StartupPath + "//licence.ini");
            for (int i = 0; i < dosya.Length; i++)
            {

                if ((Byte)((int)dosya[i] - 1) < 0)
                {
                    dosya[i] = 255;
                }
                else
                {
                    dosya[i] = (Byte)((int)dosya[i] - 1);
                }
            }
            File.WriteAllBytes(Application.StartupPath + "//licence.ini", dosya);
            #endregion
            radButton6.Enabled = false;
            radButton7.Enabled = true;
        }

        private void radButton7_Click(object sender, EventArgs e)
        {
            #region Sifrele
            Byte[] dosya2 = File.ReadAllBytes(Application.StartupPath + "//licence.ini");
            for (int i = 0; i < dosya2.Length; i++)
            {
                dosya2[i] = (Byte)((int)dosya2[i] + 1);
                if (dosya2[i] > 255)
                {
                    dosya2[i] = 0;
                }
            }
            File.WriteAllBytes(Application.StartupPath + "//licence.ini", dosya2);
            #endregion
            radButton6.Enabled = true;
            radButton7.Enabled = false;
        }

        private void radButton8_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.kontrol = 0;
            TextWriter tw = new StreamWriter(Application.StartupPath + "//licence.ini");
            tw.Write("");
            string yol= Application.StartupPath + "//Yeni Metin Belgesi.txt";
            File.Delete(yol);
            tw.Close();
            Properties.Settings.Default.durum = 0;
            Properties.Settings.Default.Save();
            Application.Restart();
            
        }

        private void radRadioButton1_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (radRadioButton1.IsChecked==true){
                ambiance_Toggle1.Toggled = true;
                ambiance_Toggle2.Toggled = false;
                INI.Yaz("DATA", "Stok", "true");
                INI.Yaz("DATA", "Muhasebe", "false");
            }

            
           
        }

        private void radRadioButton2_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (radRadioButton2.IsChecked == true)
            {
                ambiance_Toggle1.Toggled = false;
                ambiance_Toggle2.Toggled = true;
                INI.Yaz("DATA", "Muhasebe", "true");
                INI.Yaz("DATA", "Stok", "false");
            }
           
        }

        private void radRadioButton3_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (radRadioButton3.IsChecked == true)
            {
                ambiance_Toggle1.Toggled = true;
                ambiance_Toggle2.Toggled = true;
                INI.Yaz("DATA", "Muhasebe", "true");
                INI.Yaz("DATA", "Stok", "true");
            }
            
            
        }
    }
}
