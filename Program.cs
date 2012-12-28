using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using YGOPro_Launcher.CardDatabase;

namespace DevPro_CardManager
{
    static class Program
    {

        public static Dictionary<int, CardInfos> CardData = new Dictionary<int, CardInfos>();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main_frm());
        }
    }
}
