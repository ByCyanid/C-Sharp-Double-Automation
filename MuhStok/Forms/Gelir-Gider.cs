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
using System.Data.OleDb;
using Ses;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;



namespace MuhStok.Forms
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        Sesler bildirim = new Sesler();
        DialogResult aktaronay = new DialogResult();
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=muhasebe.accdb;Persist Security Info=True;Jet OLEDB:Database Password=cyanid38");
        System.Windows.Forms.Label lblguncelleid = new System.Windows.Forms.Label();
        public void Alert(string msg,Notification.Notification.enumType type)
        {
            Notification.Notification frm = new Notification.Notification();
            frm.showAlert(msg,type);
        }


        int sayi;
        private void Form2_Load(object sender, EventArgs e)
        {
            


            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("select * from ayarlar where Kimlik=@kimlik", baglanti);
            komut.Connection = baglanti;
            komut.Parameters.AddWithValue("@kimlik", "1");
            OleDbDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                if (dr["tarihsecenek"].ToString() == "")
                {
                    checkBox1.Checked = false;
                    txttarih.Text = DateTime.Today.Date.ToShortDateString();
                    txttarih2.Text = DateTime.Today.Date.ToShortDateString();
                }
                else if (dr["tarihsecenek"].ToString() == "X")
                {
                    checkBox1.Checked = true;
                    DateTime aybasi = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                    DateTime aysonu = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddDays(-1);
                    txttarih.Text = aybasi.ToShortDateString();
                    txttarih2.Text = aysonu.ToShortDateString();
                }
            }
            baglanti.Close();

            btntemizle.PerformClick();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void ıconButton2_Click(object sender, EventArgs e)
        {

        }

        private void dggider_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            rdgidersil.Checked = true;
            dggelir.ClearSelection();
        }

        private void btnggkaydet_Click(object sender, EventArgs e)
        {
            try
            {
                int sayi;
                sayi = int.Parse(txttutar.Text);
                txttutar.Text = sayi.ToString("N");
            }
            catch (Exception)
            {


            }

            if (txttarih.Text == "" || txtisadi.Text == "" || txttutar.Text == "" || comboBox1.Text == "Seçiniz" || comboBox2.Text == "Seçiniz" || comboBox3.Text == "Seçiniz" || comboBox1.Text == "" || comboBox2.Text == "" || comboBox3.Text == "")
            {
                Alert("Lütfen Tüm Alanları Doldurun!!", Notification.Notification.enumType.Warning);
                bildirim.Warning();
                if (txtisadi.Text == "")
                {
                    txtisadi.BackColor = Color.IndianRed;
                }
                else
                {
                    txtisadi.BackColor = Color.White;
                }
                if (txttarih.Text == "")
                {
                    txttarih.BackColor = Color.IndianRed;
                }
                else
                {
                    txttarih.BackColor = Color.White;
                }
                if (comboBox1.Text == "Seçiniz" || comboBox1.Text == "")
                {
                    comboBox1.BackColor = Color.IndianRed;
                }
                else
                {
                    comboBox1.BackColor = Color.White;
                }
                if (comboBox2.Text == "Seçiniz" || comboBox2.Text == "")
                {
                    comboBox2.BackColor = Color.IndianRed;
                }
                else
                {
                    comboBox2.BackColor = Color.White;
                }
                if (comboBox3.Text == "Seçiniz" || comboBox3.Text == "")
                {
                    comboBox3.BackColor = Color.IndianRed;
                }
                else
                {
                    comboBox3.BackColor = Color.White;
                }
                if (txttutar.Text == "")
                {
                    txttutar.BackColor = Color.IndianRed;
                }
                else
                {
                    txttutar.BackColor = Color.White;
                }
            }
            else
            {
                if (rdgelir.Checked || rdgider.Checked)
                {
                    if (lblguncelleid.Text == "")
                    {
                        baglanti.Open();
                        string ekle = "insert into gelirgider(islemtarih,islemadi,islemturu,kurumturu,kurumadi,gelir,gider,tutar) values (@islemtarih,@islemadi,@islemturu,@kurumadi,@kurumturu,@gelir,@gider,@tutar)";
                        OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                        komut.Parameters.AddWithValue("@islemtarih", txttarih.Text);
                        komut.Parameters.AddWithValue("@islemadi", txtisadi.Text);
                        komut.Parameters.AddWithValue("@islemturu", comboBox1.Text);
                        komut.Parameters.AddWithValue("@kurumturu", comboBox2.Text);
                        komut.Parameters.AddWithValue("@kurumadi", comboBox3.Text);

                        if (rdgelir.Checked)
                        {
                            komut.Parameters.AddWithValue("@gelir", "X");
                        }
                        else
                        {
                            komut.Parameters.AddWithValue("@gelir", "");
                        }
                        if (rdgider.Checked)
                        {
                            komut.Parameters.AddWithValue("@gider", "X");
                        }
                        else
                        {
                            komut.Parameters.AddWithValue("@gider", "");
                        }

                        komut.Parameters.AddWithValue("@tutar", txttutar.Text);
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        Alert("Kayıt Başarıyla Eklendi", Notification.Notification.enumType.Succes);
                        bildirim.Success();
                        btntemizle.PerformClick();
                        btngghepsi.PerformClick();
                    }
                    else
                    {

                        baglanti.Open();
                        string ekle = "update gelirgider set islemtarih=@islemtarih, islemadi=@islemadi, islemturu=@islemturu, kurumturu=@kurumturu, kurumadi=@kurumadi, gelir=@gelir, gider=@gider, tutar=@tutar where Kimlik = " + lblguncelleid.Text + "";
                        OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                        komut.Parameters.AddWithValue("@islemtarih", txttarih.Text);
                        komut.Parameters.AddWithValue("@islemadi", txtisadi.Text);
                        komut.Parameters.AddWithValue("@islemturu", comboBox1.Text);
                        komut.Parameters.AddWithValue("@kurumturu", comboBox2.Text);
                        komut.Parameters.AddWithValue("@kurumadi", comboBox3.Text);

                        if (rdgelir.Checked)
                        {
                            komut.Parameters.AddWithValue("@gelir", "X");
                        }
                        else
                        {
                            komut.Parameters.AddWithValue("@gelir", "");
                        }
                        if (rdgider.Checked)
                        {
                            komut.Parameters.AddWithValue("@gider", "X");
                        }
                        else
                        {
                            komut.Parameters.AddWithValue("@gider", "");
                        }

                        komut.Parameters.AddWithValue("@tutar", txttutar.Text);
                        komut.ExecuteNonQuery();
                        baglanti.Close();
                        Alert("Kayıt Başarıyla Güncellendi", Notification.Notification.enumType.Succes);
                        bildirim.Success();
                        lblguncelleid.Text = String.Empty;
                        btntemizle.PerformClick();
                        btngghepsi.PerformClick();
                    }

                }
                else
                {
                    Alert("Gelir Veya Gider Olarak Belirtmediniz", Notification.Notification.enumType.Warning);
                    bildirim.Warning();
                }
            }
        }
       
        private void btntemizle_Click(object sender, EventArgs e)
        {
            lblguncelleid.Text = String.Empty;
            rdgelir.Checked = false;
            rdgider.Checked = false;
            txtisadi.Clear();
            txttutar.Clear();
            txtisadi.BackColor = Color.White;
            txttutar.BackColor = Color.White;
            dggelir.ClearSelection();
            dggider.ClearSelection();
            btngelir.PerformClick();
            btngider.PerformClick();
            btngghepsi.PerformClick();
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox1.Items.Add("Seçiniz");
            comboBox2.Items.Add("Seçiniz");
            comboBox3.Items.Add("Seçiniz");
            comboBox1.Text = "Seçiniz";
            comboBox2.Text = "Seçiniz";
            comboBox3.Text = "Seçiniz";
            baglanti.Open();

            OleDbCommand komut = new OleDbCommand("select * from islemturleri", baglanti);
            OleDbDataReader data = komut.ExecuteReader();
            while (data.Read())
            {

                comboBox1.Items.Add(data["islemturu"]);
            }

            OleDbCommand komut2 = new OleDbCommand("select distinct kurumturu from kurumlar", baglanti);
            OleDbDataReader data2 = komut2.ExecuteReader();
            while (data2.Read())
            {
                comboBox2.Items.Add(data2["kurumturu"]);
            }

            OleDbCommand komut3 = new OleDbCommand("select distinct kurumadi from kurumlar", baglanti);
            OleDbDataReader data3 = komut3.ExecuteReader();

            while (data3.Read())
            {
                comboBox3.Items.Add(data3["kurumadi"]);
            }

            baglanti.Close();

        }

        private void rdgider_CheckedChanged(object sender, EventArgs e)
        {
            if (rdgider.Checked == true)
            {
                rdgider.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ok));
                rdgider.BackgroundImageLayout = ImageLayout.Center;
                rdgider.TextAlign = ContentAlignment.MiddleRight;
            }
            else
            {
                rdgider.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.oknone));
                rdgider.TextAlign = ContentAlignment.MiddleCenter;
            }
        }

        private void rdgelir_CheckedChanged(object sender, EventArgs e)
        {

            if (rdgelir.Checked == true)
            {
                rdgelir.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ok));
                rdgelir.BackgroundImageLayout = ImageLayout.Center;
                rdgelir.TextAlign = ContentAlignment.MiddleRight;
            }
            else
            {
                rdgelir.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.oknone));
                rdgelir.TextAlign = ContentAlignment.MiddleCenter;
            }
        }

        private void txtisadi_TextChanged(object sender, EventArgs e)
        {
            if (txtisadi.BackColor == Color.IndianRed)
            {
                txtisadi.BackColor = Color.White;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.BackColor == Color.IndianRed)
            {
                comboBox1.BackColor = Color.White;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.BackColor == Color.IndianRed)
            {
                comboBox2.BackColor = Color.White;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox3.BackColor == Color.IndianRed)
            {
                comboBox3.BackColor = Color.White;
            }
        }

        private void txttutar_TextChanged(object sender, EventArgs e)
        {
            if (txttutar.BackColor == Color.IndianRed)
            {
                txttutar.BackColor = Color.White;
            }
        }

        private void txttutar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (e.KeyChar == (char)Keys.Enter)
            {

                try
                {
                    int sayi;
                    sayi = int.Parse(txttutar.Text);
                    txttutar.Text = sayi.ToString("N");
                    rdgider.Checked = true;
                    SendKeys.Send("{TAB}");
                }
                catch (Exception)
                {


                }

            }
        }

        private void txtisadi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void comboBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void txttarih_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (e.KeyChar == (char)Keys.Enter)
            {

                string sayi;
                if (txttarih.TextLength < 8)
                {
                    MessageBox.Show("Geçersiz bir tarih girdiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txttarih.Focus();
                }
                else
                {
                    try
                    {
                        if (txttarih.TextLength <= 8)
                        {
                            sayi = txttarih.Text.Substring(0, 2) + "." + txttarih.Text.Substring(2, 2) + "." + txttarih.Text.Substring(4);
                            txttarih.Text = sayi.ToString();
                            SendKeys.Send("{TAB}");
                        }
                        else
                        {
                            SendKeys.Send("{TAB}");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Geçersiz bir tarih girdiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txttarih.Focus();
                    }
                }

            }
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            txtisadi.Focus();
        }

        private void btnggsil_Click(object sender, EventArgs e)
        {
            if (rdgelirsil.Checked || rdgidersil.Checked)
            {
                aktaronay = MessageBox.Show("Silmek istediğinizden emin misiniz? \n\nDikkat: Geri dönüşümü yoktur...", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (aktaronay == DialogResult.Yes)
                {

                    if (rdgelirsil.Checked == true)
                    {
                        try
                        {
                            baglanti.Open();
                            string ekle = "DELETE FROM gelirgider WHERE gelir='X' and Kimlik = " + dggelir.CurrentRow.Cells["Kimlik"].Value + "";
                            OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                            komut.ExecuteNonQuery();
                            baglanti.Close();
                            btngelir.PerformClick();
                            Alert("Gelir Başarıyla Silindi", Notification.Notification.enumType.Succes);
                            bildirim.Success();

                        }
                        catch (Exception)
                        { }
                    }
                    if (rdgidersil.Checked == true)
                    {
                        try
                        {
                            baglanti.Open();
                            string ekle = "DELETE FROM gelirgider WHERE gider='X' and Kimlik = " + dggider.CurrentRow.Cells["Kimlik"].Value + "";
                            OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                            komut.ExecuteNonQuery();
                            baglanti.Close();
                            btngider.PerformClick();
                            Alert("Gider Başarıyla Silindi", Notification.Notification.enumType.Succes);
                            bildirim.Success();

                        }
                        catch (Exception)
                        { }

                    }
                    btngghepsi.PerformClick();
                }
            }
            else
            {
                Alert("Seçim Yapmadınız", Notification.Notification.enumType.Warning);
                bildirim.Warning();
            }
        }

        private void btngghepsi_Click(object sender, EventArgs e)
        {
            btngelir.PerformClick();
            btngider.PerformClick();

            baglanti.Open();
            OleDbCommand cmdd = new OleDbCommand("select * from kasadurumm where Kimlik=1", baglanti);
            OleDbDataReader rd = cmdd.ExecuteReader();


            while (rd.Read())
            {
                sayi = int.Parse(rd["kasadurum"].ToString());
                label13.Text = sayi.ToString("N") + " " + "TL";
            }
            baglanti.Close();

            if (dggelir.RowCount > 0 || dggider.RowCount > 0)
            {
                decimal gelirtoplam = 0;
                decimal gidertoplam = 0;
                for (int gelir = 0; gelir <= dggelir.RowCount - 1; gelir++)
                {
                    gelirtoplam += Convert.ToDecimal(dggelir.Rows[gelir].Cells[5].Value);
                }

                for (int gider = 0; gider <= dggider.RowCount - 1; gider++)
                {
                    gidertoplam += Convert.ToDecimal(dggider.Rows[gider].Cells[5].Value);
                }

                int sonuc;
                sonuc = Convert.ToInt32(gelirtoplam) - Convert.ToInt32(gidertoplam);
                if (sonuc > 0)
                {
                    lblkalan.ForeColor = Color.CadetBlue;
                    lblkalan.Text = sonuc.ToString("N") + " " + "TL";
                }
                else
                {
                    lblkalan.ForeColor = Color.Red;
                    lblkalan.Text = sonuc.ToString("N") + " " + "TL";
                }
            }
            else
            {
                if (dggelir.RowCount < 0)
                {
                    lblgelirtoplam.Text = "0 TL";
                }

                if (dggider.RowCount < 0)
                {
                    lblgidertoplam.Text = "0 TL";
                }
            }
        }

        private void dggelir_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dggider.ClearSelection();
            rdgelirsil.Checked = true;
        }

        private void dggelir_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dggider.ClearSelection();
            rdgelirsil.Checked = true;
        }

        private void dggider_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dggelir.ClearSelection();

            rdgidersil.Checked = true;
        }

        private void btngider_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            string komut = "select Kimlik,islemtarih,islemadi,islemturu,kurumadi,tutar FROM gelirgider where islemtarih between '" + txttarih.Text.ToString() + "' and '" + txttarih2.Text.ToString() + "' and gider='X' ORDER by islemtarih ASC";
            OleDbDataAdapter sda = new OleDbDataAdapter(komut, baglanti);
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Clear();
            sda.Fill(dt);
            dggider.DataSource = dt;
            dggider.Columns[0].Visible = false;
            dggider.Columns[1].HeaderText = "Tarih";
            dggider.Columns[2].HeaderText = "Açıklama";
            dggider.Columns[3].HeaderText = "İşlem-türü";
            dggider.Columns[4].HeaderText = "Kurum-adı";
            dggider.Columns[5].HeaderText = "Tutar";
            dggider.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dggider.AutoResizeColumns();
            dggider.Columns[0].Width = 25;
            baglanti.Close();
            if (dggider.RowCount > 0)
            {
                dggider.ClearSelection();
                decimal Total = 0;

                for (int index = 0; index <= dggider.RowCount - 1; index++)
                {
                    Total += Convert.ToDecimal(dggider.Rows[index].Cells[5].Value);
                }
                lblgidertoplam.Text = Total.ToString("N") + " " + "TL";
            }
            else
            {
                lblgidertoplam.Text = "0 TL";
            }
        }

        private void btngelir_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                DateTime aybasi = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime aysonu = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddDays(-1);
                txttarih.Text = aybasi.ToShortDateString();
                txttarih2.Text = aysonu.ToShortDateString();
                dateTimePicker1.Value = DateTime.Today;
            }

            baglanti.Open();
            string komut = "select Kimlik,islemtarih,islemadi,islemturu,kurumadi,tutar FROM gelirgider where islemtarih between '" + txttarih.Text.ToString() + "' and '" + txttarih2.Text.ToString() + "' and gelir='X' ORDER by islemtarih ASC";
            OleDbDataAdapter sda = new OleDbDataAdapter(komut, baglanti);
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Clear();
            sda.Fill(dt);
            dggelir.DataSource = dt;
            dggelir.Columns[0].Visible = false;
            dggelir.Columns[1].HeaderText = "Tarih";
            dggelir.Columns[2].HeaderText = "Açıklama";
            dggelir.Columns[3].HeaderText = "İşlem-türü";
            dggelir.Columns[4].HeaderText = "Kurum-adı";
            dggelir.Columns[5].HeaderText = "Tutar";
            dggelir.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dggelir.AutoResizeColumns();
            dggelir.Columns[0].Width = 25;
            baglanti.Close();
            
            if (dggelir.RowCount > 0)
            {
                dggelir.ClearSelection();

                decimal Total = 0;

                for (int index = 0; index <= dggelir.RowCount - 1; index++)
                {
                    Total += Convert.ToDecimal(dggelir.Rows[index].Cells[5].Value);

                }
                lblgelirtoplam.Text = Total.ToString("N") + " " + "TL";
            }
            else
            {
                lblgelirtoplam.Text = "0 TL";
            }
        }

        private void düzenleToolStripMenuItem_Click(object sender, EventArgs e)
        {

            
        }

        private void dggelir_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            rdgelirsil.Checked = true;

            dggider.ClearSelection();
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
                if (!c.Selected)
                {
                    c.DataGridView.ClearSelection();
                    c.DataGridView.CurrentCell = c;
                    c.Selected = true;
                }
            }
        }

        private void dggider_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            dggelir.ClearSelection();

            rdgidersil.Checked = true;
            if (e.ColumnIndex != -1 && e.RowIndex != -1 && e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                DataGridViewCell c = (sender as DataGridView)[e.ColumnIndex, e.RowIndex];
                if (!c.Selected)
                {
                    c.DataGridView.ClearSelection();
                    c.DataGridView.CurrentCell = c;
                    c.Selected = true;
                }
            }
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Form.ActiveForm.Refresh();
                btntemizle.PerformClick();
                btngghepsi.PerformClick();
            }
        }

        private void checkBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                baglanti.Open();
                string ekle = "update ayarlar set tarihsecenek='X' where Kimlik=@Kimlik";
                OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                komut.Parameters.AddWithValue("@Kimlik", "1");
                komut.ExecuteNonQuery();
                baglanti.Close();

                DateTime aybasi = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime aysonu = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddDays(-1);
                txttarih.Text = aybasi.ToShortDateString();
                txttarih2.Text = aysonu.ToShortDateString();
                btntemizle.PerformClick();
                btngghepsi.PerformClick();
            }
            else if (checkBox1.Checked == false)
            {
                baglanti.Open();
                string ekle = "update ayarlar set tarihsecenek='' where Kimlik=@Kimlik";
                OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                komut.Parameters.AddWithValue("@Kimlik", "1");
                komut.ExecuteNonQuery();
                baglanti.Close();
                txttarih.Text = DateTime.Today.Date.ToShortDateString();
                txttarih2.Text = DateTime.Today.Date.ToShortDateString();
                btntemizle.PerformClick();
                btngghepsi.PerformClick();
            }
        }

        private void txttarih2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            if (e.KeyChar == (char)Keys.Enter)
            {

                string sayi;
                if (txttarih2.TextLength < 8)
                {
                    MessageBox.Show("Geçersiz bir tarih girdiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txttarih2.Focus();
                }
                else
                {
                    try
                    {
                        if (txttarih2.TextLength <= 8)
                        {
                            sayi = txttarih2.Text.Substring(0, 2) + "." + txttarih2.Text.Substring(2, 2) + "." + txttarih2.Text.Substring(4);
                            txttarih2.Text = sayi.ToString();
                            SendKeys.Send("{TAB}");
                        }
                        else
                        {
                            SendKeys.Send("{TAB}");
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Geçersiz bir tarih girdiniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txttarih2.Focus();
                    }
                }

            }
        }

        private void dateTimePicker1_ValueChanged_1(object sender, EventArgs e)
        {
            if (dateTimePicker1.Value.Date > dateTimePicker2.Value.Date)
            {
                MessageBox.Show("Tarih seçimi ilk tarihten küçük olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                txttarih.Text = dateTimePicker1.Value.Date.ToShortDateString();

            }
        }

        private void dateTimePicker2_ValueChanged_1(object sender, EventArgs e)
        {
            if (dateTimePicker2.Value.Date < dateTimePicker1.Value.Date)
            {
                MessageBox.Show("Tarih seçimi ilk tarihten küçük olamaz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                txttarih2.Text = dateTimePicker2.Value.Date.ToShortDateString();

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (rdgelirsil.Checked || rdgidersil.Checked)
            {
                if (rdgelirsil.Checked == true)
                {

                    baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("select * from gelirgider where Kimlik=@kimlik", baglanti);
                    komut.Connection = baglanti;
                    komut.Parameters.AddWithValue("@kimlik", dggelir.CurrentRow.Cells["Kimlik"].Value);
                    OleDbDataReader dr = komut.ExecuteReader();
                    if (dr.Read())
                    {
                        txtisadi.Text = dr["islemadi"].ToString();
                        comboBox1.Text = dr["islemturu"].ToString();
                        comboBox3.Items.Clear();
                        comboBox3.Items.Add(dr["kurumadi"].ToString());
                        comboBox3.Text = dr["kurumadi"].ToString();
                        txttutar.Text = dr["tutar"].ToString();
                        lblguncelleid.Text = dggelir.CurrentRow.Cells["Kimlik"].Value.ToString();
                        if (dr["gelir"].ToString() == "X")
                        {
                            rdgelir.Checked = true;
                        }
                    }
                    baglanti.Close();


                }
                if (rdgidersil.Checked == true)
                {

                    baglanti.Open();
                    OleDbCommand komut = new OleDbCommand("select * from gelirgider where Kimlik=@kimlik", baglanti);
                    komut.Connection = baglanti;
                    komut.Parameters.AddWithValue("@kimlik", dggider.CurrentRow.Cells["Kimlik"].Value);
                    OleDbDataReader dr = komut.ExecuteReader();
                    if (dr.Read())
                    {
                        txtisadi.Text = dr["islemadi"].ToString();
                        comboBox1.Text = dr["islemturu"].ToString();
                        comboBox3.Items.Clear();
                        comboBox3.Items.Add(dr["kurumadi"].ToString());
                        comboBox3.Text = dr["kurumadi"].ToString();
                        txttutar.Text = dr["tutar"].ToString();
                        lblguncelleid.Text = dggider.CurrentRow.Cells["Kimlik"].Value.ToString();
                        if (dr["gider"].ToString() == "X")
                        {
                            rdgider.Checked = true;
                        }
                    }
                    baglanti.Close();


                }


            }
            else
            {
                Alert("Gelir Veya Gider Seçmediniz", Notification.Notification.enumType.Error);
                bildirim.Error();
            }
        }
        int veri;
        private void button1_Click(object sender, EventArgs e)
        {

            
            veri = Convert.ToInt32(textBox1.Text);
            

            
                baglanti.Open();
                OleDbCommand cmd12 = new OleDbCommand("update kasadurumm set kasadurum='" + veri + "' where Kimlik=1", baglanti);
                cmd12.Connection = baglanti;
                cmd12.ExecuteNonQuery();
                baglanti.Close();
                btntemizle.PerformClick();
                btngghepsi.PerformClick();
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = true;
            if (radioButton2.Checked == true)
            {
                radioButton2.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ok));
                radioButton2.BackgroundImageLayout = ImageLayout.Center;
                radioButton2.TextAlign = ContentAlignment.MiddleRight;
                
            }
            else
            {
                radioButton2.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.oknone));
                radioButton2.TextAlign = ContentAlignment.MiddleCenter;
            }

            if (checkBox1.Checked == true)
            {
                baglanti.Open();
                string ekle = "update ayarlar set tarihsecenek='X' where Kimlik=@Kimlik";
                OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                komut.Parameters.AddWithValue("@Kimlik", "1");
                komut.ExecuteNonQuery();
                baglanti.Close();

                DateTime aybasi = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime aysonu = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddDays(-1);
                txttarih.Text = aybasi.ToShortDateString();
                txttarih2.Text = aysonu.ToShortDateString();
                btntemizle.PerformClick();
                btngghepsi.PerformClick();
            }
            else if (checkBox1.Checked == false)
            {
                baglanti.Open();
                string ekle = "update ayarlar set tarihsecenek='' where Kimlik=@Kimlik";
                OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                komut.Parameters.AddWithValue("@Kimlik", "1");
                komut.ExecuteNonQuery();
                baglanti.Close();
                txttarih.Text = DateTime.Today.Date.ToShortDateString();
                txttarih2.Text = DateTime.Today.Date.ToShortDateString();
                btntemizle.PerformClick();
                btngghepsi.PerformClick();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = false;
            if (radioButton1.Checked == true)
            {
                radioButton1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.ok));
                radioButton1.BackgroundImageLayout = ImageLayout.Center;
                radioButton1.TextAlign = ContentAlignment.MiddleRight;
                
            }
            else
            {
                radioButton1.BackgroundImage = ((System.Drawing.Image)(Properties.Resources.oknone));
                radioButton1.TextAlign = ContentAlignment.MiddleCenter;
            }

            if (checkBox1.Checked == true)
            {
                baglanti.Open();
                string ekle = "update ayarlar set tarihsecenek='X' where Kimlik=@Kimlik";
                OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                komut.Parameters.AddWithValue("@Kimlik", "1");
                komut.ExecuteNonQuery();
                baglanti.Close();

                DateTime aybasi = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateTime aysonu = new DateTime(DateTime.Now.Year, DateTime.Now.Month + 1, 1).AddDays(-1);
                txttarih.Text = aybasi.ToShortDateString();
                txttarih2.Text = aysonu.ToShortDateString();
                btntemizle.PerformClick();
                btngghepsi.PerformClick();
            }
            else if (checkBox1.Checked == false)
            {
                baglanti.Open();
                string ekle = "update ayarlar set tarihsecenek='' where Kimlik=@Kimlik";
                OleDbCommand komut = new OleDbCommand(ekle, baglanti);
                komut.Parameters.AddWithValue("@Kimlik", "1");
                komut.ExecuteNonQuery();
                baglanti.Close();
                txttarih.Text = DateTime.Today.Date.ToShortDateString();
                txttarih2.Text = DateTime.Today.Date.ToShortDateString();
                btntemizle.PerformClick();
                btngghepsi.PerformClick();
            }
        }

        private void ıconButton1_Click(object sender, EventArgs e)
        {
            if (rdgelirsil.Checked == true)
            {
                if (radioButton1.Checked == true || radioButton2.Checked == true)
                {
                    Excel.Application excel = new Excel.Application();
                    excel.Visible = true;
                    object Missing = Type.Missing;
                    Excel.Workbook workbook = excel.Workbooks.Add(Missing);
                    Excel.Worksheet sheet1 = (Excel.Worksheet)workbook.Sheets[1];
                    int StartCol = 1;
                    int StartRow = 1;
                    for (int j = 0; j < dggelir.Columns.Count; j++)
                    {
                        Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                        myRange.Value2 = dggelir.Columns[j].HeaderText;
                    }
                    StartRow++;
                    for (int i = 0; i < dggelir.Rows.Count; i++)
                    {
                        for (int j = 0; j < dggelir.Columns.Count; j++)
                        {

                            Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                            myRange.Value2 = dggelir[j, i].Value == null ? "" : dggelir[j, i].Value;
                            myRange.Select();


                        }
                    }
                    Alert("İşlem Başarılı", Notification.Notification.enumType.Succes);
                    bildirim.Success();
                }

                else
                {
                    Alert("Günlük/Aylık Seçmediniz!!", Notification.Notification.enumType.Warning);
                    bildirim.Warning();
                }
            }

            else if (rdgidersil.Checked == true)
            {

                if (radioButton1.Checked == true || radioButton2.Checked == true)
                {
                    Excel.Application excel = new Excel.Application();
                    excel.Visible = true;
                    object Missing = Type.Missing;
                    Excel.Workbook workbook = excel.Workbooks.Add(Missing);
                    Excel.Worksheet sheet1 = (Excel.Worksheet)workbook.Sheets[1];
                    int StartCol = 1;
                    int StartRow = 1;

                    for (int j = 0; j < dggider.Columns.Count; j++)
                    {
                        Range myRange = (Range)sheet1.Cells[StartRow, StartCol + j];
                        myRange.Value2 = dggider.Columns[j].HeaderText;
                    }
                    StartRow++;
                    for (int i = 0; i < dggider.Rows.Count; i++)
                    {
                        for (int j = 0; j < dggider.Columns.Count; j++)
                        {

                            Range myRange = (Range)sheet1.Cells[StartRow + i, StartCol + j];
                            myRange.Value2 = dggider[j, i].Value == null ? "" : dggider[j, i].Value;
                            myRange.Select();


                        }
                    }
                    Alert("İşlem Başarılı", Notification.Notification.enumType.Succes);
                    bildirim.Success();
                }
                else
                {
                    Alert("Günlük/Aylık Seçmediniz!!", Notification.Notification.enumType.Warning);
                    bildirim.Warning();
                }
            }

            else
            {
                Alert("Tablodan Veri Seçmediniz!!", Notification.Notification.enumType.Warning);
                bildirim.Warning();


            }



        }
    }
}
