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
    public partial class Relacionar : MyTabPage.MyFormPage
    {
        public int cases=1;
        public ArrayList id = new ArrayList();
        public Relacionar()
        {
            InitializeComponent();
            this.pn1 = panel1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //variables inciales
            string[] nombreC = Program.campoL;
            int posCampo=0;
            int posCampoC=0;
            string campo = textBox1.Text;
            string campoC = textBox4.Text;
            
            for(posCampo=0;posCampo<nombreC.Length && nombreC[posCampo] != campo; posCampo++) { }

            for (posCampoC = 0; posCampoC < nombreC.Length && nombreC[posCampoC] != campoC; posCampoC++) { }


            //ahora crearemos el arrayList inicial 
            ArrayList resultado = new ArrayList();

            //llenado inicial
            resultado = primerLlenado(resultado,posCampo,posCampoC);

            //ahora sacamos de cada registro el resto de registros
            resultado = segundoLlenado(resultado);

            //por ultimo lo mandamos a un swich donde se ejecuta la opcion correspondiente
            swich(resultado);


        }
        public ArrayList primerLlenado(ArrayList resultado, int posCampo,int posCampoC)
        {
            //variables
            string archivo = Program.archivo_txt;
            System.IO.StreamReader file = new System.IO.StreamReader(archivo, Encoding.GetEncoding(1252));
            string line;
            int ncampos = 0;
            string []campos;
            //nos saltamos la cabecera pero nos quedamos  con el ncampos
            line = file.ReadLine().Replace("'", "\'").Replace('"', '\"');
            string[] nombreC = Program.campoL;
            ncampos = nombreC.Length;
            int[] longC = new int[ncampos];
            // ahora seguimos con el resto de lineas y vamos haciendo las inserts
            //procesado de datos
            int camposprocesados = 0;
            int concatenar = 0;
            ArrayList nResult = new ArrayList();
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
                       if(z == posCampo)
                        {
                            nResult.Add(campo);
                        }
                        if (z == posCampoC)
                        {
                            id.Add(campo);
                        }
                    }
                    registro = new ArrayList(); // formateamos el ArrayList
                    camposprocesados = 0; // La siguiente línea es un registro nuevo
                }
            }
            

            file.Close();

            return nResult;
        }
        public ArrayList segundoLlenado(ArrayList resultado)
        {
            ArrayList nResult = new ArrayList();
            ArrayList idN = new ArrayList();
            int y = 0;
            foreach(string reg in resultado)
            {
                Char[] c = textBox2.Text.ToCharArray();
                string[] campos = reg.Split(c);
                for(int x = 0; x < campos.Length; x++)
                {
                    nResult.Add(campos[x]);
                    idN.Add(id[y]);

                }
                y++;
            }
            id = idN;
            return nResult;
        }
        public void swich(ArrayList resultado)
        {
            switch (cases)
            {
                case 1:
                    case1(resultado);
                    break;
               
                default:
                    Console.WriteLine("Default case");
                    break;
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                cases = 1;
            }
        }
        public void case1(ArrayList resultado)
        {
            
            try
            {

                OleDbConnection conn = new OleDbConnection();
                conn.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Program.archivo_acs;
                MessageBox.Show(conn.ConnectionString);
                conn.Open();
                string tableName = "[" + Program.table_name.Replace(".", "_") + "]";
                OleDbCommand cmmd = new OleDbCommand("", conn);
                cmmd.CommandText = "CREATE TABLE " + tableName + "(";
                cmmd.CommandText += "" + "[" + textBox4.Text.Replace(".", "_") + "] Text,";
                cmmd.CommandText += "[" + textBox1.Text.Replace(".", "_") + "] Text)";
                if (conn.State == ConnectionState.Open)
                {
                    try
                    {
                        //ejecutamos query de creacion de tabla
                        cmmd.ExecuteNonQuery();
                        MessageBox.Show("Add!");
                        // ejecutamos query de insert
                        ejecutar_inserts(tableName, conn,resultado);

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
        public void ejecutar_inserts(string tableName, OleDbConnection conn,ArrayList resultado)
        {
            try
            {
                //variables

                int ncampos = resultado.Count;
                int contador1 = 0;
                int camposprocesados = 0;
                ArrayList registro = new ArrayList();
             /*   foreach (string line in )
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


                }*/

                MessageBox.Show("Registros guardados");
            }
            catch (Exception ex1)
            {
                MessageBox.Show("" + ex1);
            }

        }
    }
}
