using System;
using System.Collections;
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
using ADOX;

/* 
@autor Ruben Polo
@version v1.0
*/
namespace WindowsFormsApplication1
{
    public partial class Form1 : MyTabPage.MyFormPage
    {
        
        public static string[] nombreC;
        public Form1()
        {
            InitializeComponent();
            this.pn1 = panel1;
        }

        /* Analizar */
        private void button2_Click(object sender, EventArgs e) 
        {
            try
            {
                //variables
                string archivo = Program.archivo_txt;
                System.IO.StreamReader file = new System.IO.StreamReader(archivo, Encoding.GetEncoding(1252));
                string line;
                int ncampos = 0;
                //nos saltamos la cabecera pero nos quedamos  con el ncampos
                line = file.ReadLine().Replace("'", "\'").Replace('"', '\"');
                string[] campos = line.ToString().Split('\t');
                nombreC = campos;
                ncampos = campos.Length;
                int[] longC = new int[ncampos];
                // ahora seguimos con el resto de lineas y vamos haciendo las inserts
                //procesado de datos
                int camposprocesados = 0;
                int concatenar = 0;
                ArrayList registro = new ArrayList();
                while ((line = file.ReadLine()) != null)
                {
                    line = line.Replace(@"""", @"""""");
                    if (camposprocesados > 0 && camposprocesados < ncampos) { concatenar = 1; camposprocesados--; } else { concatenar = 0; }
                    campos = line.ToString().Split('\t');
                    foreach (string campo in campos)
                    {
                        if (concatenar == 1)
                        { registro[registro.Count - 1] += "\r\n" + campo; concatenar = 0; }
                        else
                        { registro.Add(campo); }
                        camposprocesados++;
                    }
                    if (camposprocesados == ncampos)
                    {
                        int z = 0;
                        foreach (string campo in registro)
                        {
                            if (longC[z] < campo.Length)
                            {
                                longC[z] = campo.Length;
                            }
                            z++;
                        }
                        registro = new ArrayList(); // formateamos el ArrayList
                        camposprocesados = 0; // La siguiente línea es un registro nuevo
                    }
                }
                llenar(ncampos, longC);

                file.Close();
            }
            catch (Exception ex1) { MessageBox.Show("" + ex1); }
        }

        public void llenar(int ncampos, int[] longC)
        {
            lista.View = System.Windows.Forms.View.Details;
            lista.GridLines = true;
            lista.FullRowSelect = true;
            lista.Clear();
            lista.Columns.Add("Nombre campo", 500);
            lista.Columns.Add("Long. Max.", 100);
            // inserccion al listview

            ListViewItem itm;

            for (int i = 0; i < ncampos; i++)
            {
                string[] fila = new string[2];
                Boolean igh = true;
                int cont = 1;
                for (int z = 0; z <= i; z++)
                {
                    if (nombreC[i].ToString() == nombreC[z].ToString() && i != z)
                    {
                        cont++;
                        igh = false;
                    }

                }
                if (igh == true)
                {
                    fila[0] = nombreC[i];
                }
                else
                {
                    fila[0] = nombreC[i] + cont;
                }



                fila[1] = longC[i].ToString();


                itm = new ListViewItem(fila);
                lista.Items.Add(itm);
                Program.campoL = nombreC;
                Program.longL = longC;
            }
        }


        private void button5_Click(object sender, EventArgs e)
        {
            Clipboard.Clear();
            string tex1t = "";
            for (int i = 0; i < lista.Items.Count; i++)
            {
                string ntabla = lista.Items[i].SubItems[0].Text;
                string nlong = lista.Items[i].SubItems[1].Text;

                tex1t += ntabla + "\t" + nlong + "\t \n";

                }
            Clipboard.SetText(tex1t);
            }
       

    }
}
