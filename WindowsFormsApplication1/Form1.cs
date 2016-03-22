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

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* Al pulsar el boton procesar, recojemos los datos del ListView y con ellos 
            creamos una tabla en access en un fichero dado por el usuario o uno nuevo. 
            */
           //Asi saco cualquier valor del List View  
            // prueba con for
            for (int i = 0; i < lista.Items.Count; i++)
            {
             MessageBox.Show(lista.Items[i].SubItems[0].Text+ " tiene un tamaño de " +lista.Items[i].SubItems[1].Text);
            }
            //fin del for




        }

        private void File_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Esenciales ListView
            lista.View = View.Details;
            lista.GridLines = true;
            lista.FullRowSelect = true;
            // Variables globales dentro de la funcion
            string line;
            string archivo = Files.Text;
            System.IO.StreamReader file = new System.IO.StreamReader(archivo);
            int ncampos = 0;
            // ArrayLists para llenar el ListView 
            ArrayList nombreC = new ArrayList();
            ArrayList longC = new ArrayList();
           

            //   string[,] lista_arr = new string[ncampos, 2];



            // Cabecera
            line = file.ReadLine();
            string[] campos = line.ToString().Split('\t');
            lista.Clear();
            lista.Columns.Add("Nombre campo", 500);
            lista.Columns.Add("Long. Max.", 100);
            foreach (string campo in campos)
            {
                nombreC.Add(campo);
                longC.Add(0);
                ncampos++;
            }
            // Fin Cabecera

            // Proceso filas de datos
            int camposprocesados = 0;
            int concatenar = 0;
            ArrayList registro = new ArrayList();
           
            while ((line = file.ReadLine()) != null)
            {
                if (camposprocesados>0 && camposprocesados < ncampos)
                {
                    concatenar = 1; //Esta línea es una continuación de la anterior
                    camposprocesados--;
                }
                else concatenar = 0;
                
                campos = line.ToString().Split('\t');
                foreach (string campo in campos)
                {
                    if (concatenar == 1)
                    {
                        registro[registro.Count-1] += "\n" + campo; 
                        Console.WriteLine("Concatenar con la anterior");
                        concatenar = 0;
                    }
                    else
                    {
                        registro.Add(campo);
                    }
                    Console.WriteLine(camposprocesados.ToString() + " - " + campo);
                    camposprocesados++;
                }
               Console.WriteLine(camposprocesados + "/" + ncampos);
                
                if (camposprocesados == ncampos) {
                    //Antes del siguiente registro hacemos una repetitiva para revisar el tema del Tamaño
                    int z = 0;
                    foreach ( string campo in registro)
                    {
                        if(campo.Length > longC[z].ToString().Length)
                        {
                            longC[z] = campo.Length;
                        }
                        z++;
                    }
                    registro = new ArrayList(); // formateamos el ArrayList
                    camposprocesados = 0; // La siguiente línea es un registro nuevo
                } 
              //  MessageBox.Show(line);
              
            }







            ListViewItem itm;
            for (int i = 0; i < ncampos; i++)
            {
                string[] fila = new string[2];

                fila[0] = nombreC[i].ToString();
                fila[1] = longC[i].ToString();
                itm = new ListViewItem(fila);
                lista.Items.Add(itm);
            }
            file.Close();
        }

        

        
    }
}
