using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Ses;


namespace MuhStok
{
    public partial class Licence : Form
    {
        public Licence()
        {
            InitializeComponent();
        }

        private void ıconButton7_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
        INIKaydet ini = new INIKaydet(Application.StartupPath + @"\licence.ini");
        public void Alert(string msg, Notification.Notification.enumType type)
        {
            Notification.Notification frm = new Notification.Notification();
            frm.showAlert(msg, type);
        }
        Sesler bildirim = new Sesler();
        private void radButton1_Click(object sender, EventArgs e)
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

            #region Kontrol
            if (radTextBox1.Text == ini.Oku("DATA", "licencekey") && radTextBox1.Text != "")
            {
                Login lgn = new Login();
                Properties.Settings.Default.kontrol = 1;
                Properties.Settings.Default.Save();
                Alert("Lisanslama Başarılı!", Notification.Notification.enumType.Succes);
                bildirim.Success();
                this.Hide();
                lgn.Show();
            }

            else
            {
                Alert("Lisans Anahtarı Hatalı", Notification.Notification.enumType.Error);
                bildirim.Error();
            }
            #endregion

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

        }
        string key;
        string sifre;
        Forms.Developer dvp = new Forms.Developer();

        private void Licence_Load(object sender, EventArgs e)
        {
            KeyPreview = true;
            #region licence
            

            char[] dizi = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'R', 'S', 'T', 'U', 'V', 'Y', 'Z', 'X', 'W' ,'1','2','3','4','5','6','7','8','9','0'};
            Random rnd = new Random();
            if (Properties.Settings.Default.kontrol==0) {
                for (int i = 0; i <5; i++)
                {
                    for (int j = 1; j<6; j++)
                    {
                        int sayi = rnd.Next(34);
                        key += dizi[sayi].ToString();
                        if (j % 5 == 0)
                        {
                            if (i * j == 20)
                            {
                                key += "";
                            }
                            else
                             key += "-";
                        }
                        
                    }
                    
                }
                if (Properties.Settings.Default.durum == 0)
                {
                    TextWriter tw = new StreamWriter(Application.StartupPath + "//licence.ini");
                    tw.Write("");
                    string yol = Application.StartupPath + "//Yeni Metin Belgesi.txt";
                    File.Delete(yol);
                    tw.Close();

                    ini.Yaz("DATA", "licencekey", key);
                    Properties.Settings.Default.durum ++;
                    Properties.Settings.Default.Save();

                }
                else
                {
                    TextWriter tw = new StreamWriter(Application.StartupPath + "//licence.ini");
                    tw.Write("");
                    string yol = Application.StartupPath + "//Yeni Metin Belgesi.txt";
                    File.Delete(yol);
                    tw.Close();
                    Properties.Settings.Default.durum--;
                    Properties.Settings.Default.Save();
                    ini.Yaz("DATA", "licencekey", key);
                }
                
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
            }
            #endregion
            
        }

        private void Licence_KeyDown(object sender, KeyEventArgs e)
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