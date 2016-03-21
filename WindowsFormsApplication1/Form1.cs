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
            int counter = 0;
            string line;
            ArrayList lineas = new ArrayList();
            string archivo = Files.Text;

            // Read the file and display it line by line.
           
            System.IO.StreamReader file = new System.IO.StreamReader(archivo);
            
            while ((line = file.ReadLine()) != null)
            {
                lineas.Add(line);
              //  MessageBox.Show(line);
                counter++;
            }
            file.Close();
         //   MessageBox.Show("Cabecera: "+lineas[0].ToString());
          
            int ncol;
            string[] bar1 = lineas[0].ToString().Split('\t');
            ncol = lineas[0].ToString().Split('\t').Length;

            string texto;
            using (StreamReader sr = new StreamReader(archivo))
            {
                // Read the stream to a string, and write the string to the console.
                texto = sr.ReadToEnd();
           //    MessageBox.Show(texto);
            }


            MessageBox.Show(ncol.ToString());
            string[] bar = texto.Split('\t');
            
            int i = 0;
            int x = 0;
            string nuevaL="";
            while(x <bar.Length){
            i++;
            nuevaL += bar[x]+" ";
                if (i == ncol)
                {
                    i = 0;
                    MessageBox.Show(nuevaL);
                    MessageBox.Show("Linea terminada");
                    nuevaL = "";
                }

                
                x++;
 
            }
            MessageBox.Show(nuevaL);
            MessageBox.Show("Linea terminada");

            


        }

        private void File_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            lista.View = View.Details;
            lista.GridLines = true;
            lista.FullRowSelect = true;

            string line;
            string archivo = Files.Text;
            int contador=0;

            // Read the file and display it line by line.

            System.IO.StreamReader file = new System.IO.StreamReader(archivo);
            int ncampos = 0;

            while ((line = file.ReadLine()) != null)
            {
                if (contador == 0)
                {
                    // Cabecera
                    string[] campos = line.ToString().Split('\t');
                    string[,] lista_arr = new string[200, 2];

                    lista.Clear();

                    lista.Columns.Add("Nombre campo", 500);
                    lista.Columns.Add("Long. Max.", 100);

                    foreach (string campo in campos)
                    {
                        lista_arr[ncampos, 0] = campo;
                        lista_arr[ncampos, 1] = "";
                        ncampos++;
                    }
                    ListViewItem itm;

           

                    for (int i = 0; i < ncampos; i++)
                    {
                        string[] fila = new string[2];

                        fila[0] = lista_arr[i, 0];
                        fila[1] = lista_arr[i, 1];
                        itm = new ListViewItem(fila);
                        lista.Items.Add(itm);
                    }
                }
                else
                {
                    // Fila de datos
                    int camposprocesados = 0;
                    string[] campos = line.ToString().Split('\t');
                    do
                    {
                        foreach (string campo in campos)
                        {
                            // AQUI VA TODA LA LOGICA DEL PROGRAMA

                            camposprocesados++;
                        }
                        line = file.ReadLine();
                        campos = line.ToString().Split('\t');
                    } while (camposprocesados < ncampos);

                }
                MessageBox.Show(line);
                contador++;
            }
            file.Close();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            lista.Items.RemoveAt(Int32.Parse(textBox1.Text));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
