using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    static class Program
    {
        public static string archivo_txt = "C:\\Users\\alberto\\Desktop\\Extraction sauvegarde _Chiara_1.xls";
        public static string table_name = "";
        public static string archivo_acs = "C:\\Users\\alberto\\Desktop\\huolas.mdb";
        public static string [] campoL;
        public static int [] longL;
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form2 v2 = new Form2();
            v2.añadir_pestañas();
            Application.Run(v2);
        }
    }
}
