using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Procesar : MyTabPage.MyFormPage
    {
        public Procesar()
        {
            InitializeComponent();
            this.pn1 = panel1;
        }
        

       private void button1_Click(object sender, EventArgs e)
        {
            /* Procesar */

            string [] nombreC = Program.campoL;
            int[] longL = Program.longL;
            try
            {


                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Program.archivo_acs;
                MessageBox.Show(conn.ConnectionString);
                conn.Open();
                string tableName = "[" + Program.table_name.Replace(".", "_") + "]";
                OleDbCommand cmmd = new OleDbCommand("", conn);
                cmmd.CommandText = "CREATE TABLE " + tableName + "(";
                for (int i = 0; i < nombreC.Length; i++)
                {
                    //camabiamos codificacion del texto para quitar posibles errores al llegar en ASCI
                    string myString = nombreC[i].ToString().Replace(".", "_");
                    string longitu = longL[i].ToString();
                    int longuitu = Int32.Parse(longitu);
                    Console.Write(longuitu);
                    if (i == 0)
                    {
                    

                        cmmd.CommandText += "" + "[" + myString + "] Text,";
                    }
                    else
                    {
                        if (i == nombreC.Length - 1)
                        {
                           cmmd.CommandText += "[" + myString + "] Text)";
                        }
                        else
                        {
                                cmmd.CommandText += "[" + myString + "] Text,";   
                        }

                    }
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
                MessageBox.Show("Algo va mal" + ex);

            }
        }
       public void ejecutar_inserts(string tableName, OleDbConnection conn)
        {
            try
            {
                //variables


                string archivo = Program.archivo_txt;
                System.IO.StreamReader file = new System.IO.StreamReader(archivo, Encoding.GetEncoding(1252));

                int ncampos = 0;

                string[] lines = file.ReadToEnd().Split('\n');
                int contador1 = 0;
                int camposprocesados = 0;
                ArrayList registro = new ArrayList();
                foreach (string line in lines)
                {
                    if (camposprocesados > ncampos)
                    {
                        MessageBox.Show("Error, el numero de campos ha sido superdado" + line + "  / " + camposprocesados);
                    }
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

                        string[] campos = line1.ToString().Split('\t');
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
                            if (camposprocesados > ncampos)
                            {
                                MessageBox.Show("Error, el numero de campos ha sido superdado" + line + "  / " + camposprocesados);
                            }
                            if(camposprocesados == ncampos)
                            {

                            }
                            else
                            {
                                camposprocesados++;
                            }
                            
                            
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
                MessageBox.Show("" + ex1);
            }

        }
    }
}
