﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MuhStok
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 frm = new Form1();
            if (Properties.Settings.Default.kontrol == 0)
            {
                Application.Run(new Licence());
            }
            else
            {
                Application.Run(new Login());
            }



        }
    }
}
