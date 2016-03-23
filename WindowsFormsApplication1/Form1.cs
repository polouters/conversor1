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
            // MessageBox.Show(lista.Items[i].SubItems[0].Text+ " tiene un tamaño de " +lista.Items[i].SubItems[1].Text);
            }
            //fin del for

            OleDbConnection conn = new OleDbConnection();
            conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + textBox1.Text;
            MessageBox.Show(conn.ConnectionString);
            conn.Open();
            string tableName = "["+textBox2.Text.Replace(".","_")+"]";
            OleDbCommand cmmd = new OleDbCommand("", conn);
            cmmd.CommandText = "CREATE TABLE " + tableName + "(";
            for (int i = 0; i < lista.Items.Count; i++)
            {
                //camabiamos codificacion del texto para quitar posibles errores al llegar en ASCI
                string myString = lista.Items[i].SubItems[0].Text.Replace(".", "_");
                byte[] bytes = Encoding.Default.GetBytes(myString);
                myString = Encoding.UTF8.GetString(bytes);
                
                if (i == 0)
                {
                    cmmd.CommandText += "[" + myString + "] Text Primary Key,";
                }
                else
                {if(i == lista.Items.Count - 1)
                    {
                        cmmd.CommandText += "[" + myString + "] Text)";
                    }
                    else
                    {
                        cmmd.CommandText += "[" + myString + "] Text,";
                    }
                   
                }
                // MessageBox.Show(lista.Items[i].SubItems[0].Text + " tiene un tamaño de " + lista.Items[i].SubItems[1].Text);
            }
         
           // cmmd.CommandText = "CREATE TABLE " + tableName + "( [I D] Counter Primary Key, [FirstName] Text, [LastName] Text, [Gender] Text, [Phone] Text, [CellPhone] Text, [FriendsFirstName] Text, [FriendsLastName] Text, [RegisterDate] Text, [Size] Text, [Description] Text)";
            if (conn.State == ConnectionState.Open)
            {
                try
                {
                    cmmd.ExecuteNonQuery();
                    MessageBox.Show("Add!");
                    conn.Close();
                }
                catch (OleDbException expe)
                {
                    MessageBox.Show(expe.Message);
                    conn.Close();
                }
            }
            else
            {
                MessageBox.Show("Error!");
            }


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
                  //      Console.WriteLine("Concatenar con la anterior");
                        concatenar = 0;
                    }
                    else
                    {
                        registro.Add(campo);
                    }
                //    Console.WriteLine(camposprocesados.ToString() + " - " + campo);
                    camposprocesados++;
                }
              // Console.WriteLine(camposprocesados + "/" + ncampos);
                
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

        private void button2_Click_1(object sender, EventArgs e)
        {
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                Files.Text = openFileDialog1.FileName;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                textBox1.Text = openFileDialog1.FileName;
        }
    }
}
