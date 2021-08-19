using MuhStok.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace MuhStok.Notification
{
    public partial class Notification : Form
    {
        public Notification()
        {
            InitializeComponent();
        }
        public enum enumType
        {
            Succes,
            Warning,
            Error,
            Info
        }
        public enum enumAction
        {
            wait,
            start,
            close
        }
        public int x, y;
        public enumAction action;
        public void showAlert(string msg, enumType type)
        {
            this.Opacity = 0.0;
            this.StartPosition = FormStartPosition.Manual;
            string fname;


            for (int i = 1; i < 10; i++)
            {
                fname = "alert" + i.ToString();
                Notification frm = (Notification)Application.OpenForms[fname];
                if (frm == null)
                {
                    this.Name = fname;
                    this.x = Screen.PrimaryScreen.WorkingArea.Width - this.Width + 15;
                    this.y = Screen.PrimaryScreen.WorkingArea.Height - this.Height * i - 5 * i;
                    this.Location = new Point(this.x, this.y);
                    break;
                }
            }
            this.x = Screen.PrimaryScreen.WorkingArea.Width - base.Width - 5;
            switch (type)
            {
                case enumType.Succes:
                    this.pictureBox1.Image = Resources.succes;
                    this.BackColor = Color.SeaGreen;
                    break;

                case enumType.Error:
                    this.pictureBox1.Image = Resources.error;
                    this.BackColor = Color.DarkRed;
                    break;


                case enumType.Info:
                    this.pictureBox1.Image = Resources.icons8_info_45px;
                    this.BackColor = Color.RoyalBlue;
                    break;


                case enumType.Warning:
                    this.pictureBox1.Image = Resources.Warning;
                    this.BackColor = Color.DarkOrange;
                    break;

            }
            this.label1.Text = msg;
            this.Show();
            this.action = enumAction.start;
            this.timer1.Interval = 1;
            timer1.Start();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1;
            action = enumAction.close;
        }

        private void Notification_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1;
            action = enumAction.close;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (this.action)
            {
                case enumAction.wait:
                    timer1.Interval = 2000;
                    action = enumAction.close;
                    break;
                case enumAction.start:
                    timer1.Interval = 1;
                    this.Opacity += 0.1;
                    if (this.x < this.Location.X)
                    {
                        this.Left--;
                    }
                    else
                    {
                        if (this.Opacity == 1.0)
                        {
                            action = enumAction.wait;
                        }
                    }
                    break;
                case enumAction.close:
                    timer1.Interval = 1;
                    this.Opacity -= 0.1;
                    this.Left -= 3;
                    if (base.Opacity == 0.0)
                    {
                        base.Close();
                    }
                    break;
            }
        }
    }
}
