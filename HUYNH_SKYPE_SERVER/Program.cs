using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace HUYNH_SKYPE_SERVER
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Frm_Main());
        }
    }
}
