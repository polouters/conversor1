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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /* Procesar */
        private void button1_Click(object sender, EventArgs e)
        {
            /* Procesar */


            try
            {
                /* Al pulsar el boton procesar, recojemos los datos del ListView y con ellos 
                    creamos una tabla en access en un fichero dado por el usuario o uno nuevo. 
                    */
                //Asi saco cualquier valor del List View  
                // prueba con for
             //   for (int i = 0; i < lista.Items.Count; i++)
               // {
                    // MessageBox.Show(lista.Items[i].SubItems[0].Text+ " tiene un tamaño de " +lista.Items[i].SubItems[1].Text);
                //}
                //fin del for

                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + textBox1.Text;
                MessageBox.Show(conn.ConnectionString);
                conn.Open();
                string tableName = "[" + textBox2.Text.Replace(".", "_") + "]";
                OleDbCommand cmmd = new OleDbCommand("", conn);
                cmmd.CommandText = "CREATE TABLE " + tableName + "(";
                for (int i = 0; i < lista.Items.Count; i++)
                {
                    //camabiamos codificacion del texto para quitar posibles errores al llegar en ASCI
                    string myString = lista.Items[i].SubItems[0].Text.Replace(".", "_");
                    string longitu = lista.Items[i].SubItems[1].Text;
                    int longuitu = Int32.Parse(longitu);
                    Console.Write(longuitu);
                    if (i == 0)
                    {
                        //[id_default]  NUMBER NOT NULL PRIMARY KEY,
                        if (longuitu < 50)
                        {
                            cmmd.CommandText += "" + "[" + myString + "] Text,";
                        }else if(longuitu > 50)
                        {
                            cmmd.CommandText += "" + "[" + myString + "] Text,";
                        }
                      
                    }
                    else
                    {
                        if (i == lista.Items.Count - 1)
                        {
                            if (longuitu < 50)
                            {
                               
                                cmmd.CommandText +=  "[" + myString + "] Text)";
                            }
                            else if (longuitu > 50)
                            {
                              
                                cmmd.CommandText +=  "[" + myString + "] Text)";
                            }
                           
                        }
                        else
                        {
                            if (longuitu < 50)
                            {
                                cmmd.CommandText +=  "[" + myString + "] Text,";
                            }
                            else if (longuitu > 50)
                            {
                                
                                cmmd.CommandText +=  "[" + myString + "] Text,";
                            }
                           
                        }

                    }
                    // MessageBox.Show(lista.Items[i].SubItems[0].Text + " tiene un tamaño de " + lista.Items[i].SubItems[1].Text);
                }

                // cmmd.CommandText = "CREATE TABLE " + tableName + "( [I D] Counter Primary Key, [FirstName] Text, [LastName] Text, [Gender] Text, [Phone] Text, [CellPhone] Text, [FriendsFirstName] Text, [FriendsLastName] Text, [RegisterDate] Text, [Size] Text, [Description] Text)";
                if (conn.State == ConnectionState.Open)
                {
                    try
                    {
                        //ejecutamos query de creacion de tabla
                        cmmd.ExecuteNonQuery();
                        MessageBox.Show("Add!");
                        // ejecutamos query de insert
                        ejecutar_inserts(tableName, conn);

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
            catch (Exception ex)
            {
                MessageBox.Show("Algo va mal"+ ex);
                
            }


        }
        public void ejecutar_inserts(string tableName,OleDbConnection conn)
        {
            try
            {
                //variables


                string archivo = Files.Text;
                System.IO.StreamReader file = new System.IO.StreamReader(archivo, Encoding.GetEncoding(1252));
                
                int ncampos = 0;

                string[] lines = file.ReadToEnd().Split('\n');
                int contador1 = 0;
                int camposprocesados = 0;
                ArrayList registro = new ArrayList();
                foreach (string line in lines)
                {
                    if (contador1 == 0)
                    {// si es la primera vez que entramos cojemos la cabecera y guardamos el numero de campos
                        string[] campos = line.ToString().Replace("'", "\'").Replace('"', '\"').Split('\t');
                        ncampos = campos.Length;
                        contador1++;
                    }
                    else
                    { // ahora seguimos con el resto de lineas y vamos haciendo las inserts
                       
                        int concatenar = 0;
                       
                        //procesado de datos
                        string line1 = line.Replace(@"""", @"""""");
                        if (camposprocesados > 0 && camposprocesados < ncampos)
                        {
                            concatenar = 1; //Esta línea es una continuación de la anterior
                            camposprocesados--;
                        }
                        else concatenar = 0;

                        string [] campos = line1.ToString().Split('\t');
                        foreach (string campo in campos)
                        {
                            if (concatenar == 1)
                            {
                                registro[registro.Count - 1] += "\r\n" + campo;
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

                        if (camposprocesados == ncampos)
                        {
                            //Antes del siguiente registro hacemos una repetitiva para revisar el tema del Tamaño
                            int z = 0;
                            //'null',
                            string inserta = "INSERT INTO " + tableName + " VALUES (";
                            foreach (string campo in registro)
                            {
                                //vamos preparando la insert
                                inserta += @"""" + campo + @"""";
                                if (z == registro.Count - 1)
                                {
                                    inserta += ")";
                                }
                                else
                                {
                                    inserta += ",";
                                }
                                z++;
                            }
  
                            OleDbCommand cmd = new OleDbCommand(inserta, conn);
                            insertatxt.Text = inserta;
                            cmd.ExecuteNonQuery();//registro terminado
                            registro = new ArrayList(); // formateamos el ArrayList
                            camposprocesados = 0; // La siguiente línea es un registro nuevo
                        }



                    }


                }

                MessageBox.Show("Registros guardados");
            }
            catch (Exception ex1)
            {
                MessageBox.Show(""+ ex1);
            }

        }
        private void File_TextChanged(object sender, EventArgs e)
        {

        }
        /* Analizar */
        private void button2_Click(object sender, EventArgs e) 
        {
            /* Analizar */
            try
            {
                // Esenciales ListView
                lista.View = System.Windows.Forms.View.Details;
                lista.GridLines = true;
                lista.FullRowSelect = true;
                // Variables globales dentro de la funcion
                string line;
                string archivo = Files.Text;
                System.IO.StreamReader file = new System.IO.StreamReader(archivo, Encoding.GetEncoding(1252));
                int ncampos = 0;
                // ArrayLists para llenar el ListView 
                ArrayList nombreC = new ArrayList();
                ArrayList longC = new ArrayList();
                ArrayList regLogS = new ArrayList();
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
                ArrayList rlog = new ArrayList();
         
                while ((line = file.ReadLine()) != null)
                {
                    if (camposprocesados > 0 && camposprocesados < ncampos)
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
                            registro[registro.Count - 1] += "\n\r" + campo.Replace("'", "\'").Replace('"', '\"');
                            //MessageBox.Show("Concatenar con la anterior");
                            concatenar = 0;
                        }
                        else
                        {
                            registro.Add(campo.Replace("'", "\'").Replace('"', '\"'));
                        }
                        //    Console.WriteLine(camposprocesados.ToString() + " - " + campo);
                        camposprocesados++;
                    }
                    // Console.WriteLine(camposprocesados + "/" + ncampos);

                    if (camposprocesados == ncampos)
                    {
                        //Antes del siguiente registro hacemos una repetitiva para revisar el tema del Tamaño
                        int z = 0;
                      
                        foreach (string campo in registro)
                        {
                          
                            if (campo.Length > longC[z].ToString().Length)
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

        


                // inserccion al listview


                ListViewItem itm;
                   
                for (int i = 0; i < ncampos; i++)
                {
                    string[] fila = new string[3+ regLogS.Count];
                    Boolean igh = true;
                    int cont = 1;
                    for (int z = 0; z <=i; z++)
                    {
                        if(nombreC[i].ToString() == nombreC[z].ToString() && i != z)
                        {
                            cont++;
                            igh = false;
                        }
                        
                    }
                    if(igh == true)
                    {
                        fila[0] = nombreC[i].ToString();
                    }
                    else
                    {
                        fila[0] = nombreC[i].ToString() + cont;
                    }
                       


                    fila[1] = longC[i].ToString();
                
                
                    itm = new ListViewItem(fila);
                    lista.Items.Add(itm);
                }
                file.Close();
            }
            catch (Exception ex)
            {

                MessageBox.Show("Algo va mal "+ ex);
            }
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
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ADOX.Catalog cat = new ADOX.Catalog();
                //ADOX.CatalogClass cat = new ADOX.CatalogClass();
                string archivo = textBox3.Text;
                string cusu = textBox4.Text;
                cat.Create("Provider=Microsoft.Jet.OLEDB.4.0;" +
                       "Data Source=" + cusu + "\\" + archivo + ".mdb;" +
                       "Jet OLEDB:Engine Type=5");

                MessageBox.Show("Database Created Successfully");

                cat = null;
            }catch(Exception ex)
            {
                MessageBox.Show("Se ha producido un error" + ex);
            }
           


        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void label9_Click(object sender, EventArgs e)
        {

        }
        private void label10_Click(object sender, EventArgs e)
        {

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
        private void button6_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = folderBrowserDialog1.SelectedPath;
            }
        }

    }
}
