using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;
using System.Windows.Forms;
using System.IO;




namespace Ses
{
    public class Sesler
    {
        SoundPlayer sesler = new SoundPlayer();
        string dizin;
        public void Error()
        {
            dizin = Application.StartupPath + "\\ses\\Error.wav";
            sesler.SoundLocation = dizin;
            sesler.Play();
        }

        public void Information()
        {
            dizin = Application.StartupPath + "\\ses\\Information.wav";
            sesler.SoundLocation = dizin;
            sesler.Play();
        }

        public void Success()
        {
            dizin = Application.StartupPath + "\\ses\\Success.wav";
            sesler.SoundLocation = dizin;
            sesler.Play();
        }


        public void Warning()
        {
            dizin = Application.StartupPath + "\\ses\\Alerts.wav";
            sesler.SoundLocation = dizin;
            sesler.Play();
        }
    }
}
