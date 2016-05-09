using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class prueba : MyTabPage.MyFormPage
    {
        public int cases = 1;
        public ArrayList id = new ArrayList();
        public ArrayList rTb1 = new ArrayList();
        public prueba()
        {
            InitializeComponent();
            this.pn1 = panel1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button3_Clic();
            button2_Clic();
        }
        public ArrayList primerLlenado(ArrayList resultado, int posCampo, int posCampoC)
        {
            //variables
            string archivo = Program.archivo_txt;
            System.IO.StreamReader file = new System.IO.StreamReader(archivo, Encoding.GetEncoding(1252));
            string line;
            int ncampos = 0;
            string[] campos;
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
                        if (z == posCampo)
                        {
                            nResult.Add(campo);
                        }
                        if (z == posCampoC)
                        {
                            id.Add(campo);
                        }
                        z++;
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
            string pattern = @"[a-z]\/";
            if (resultado.Count == 0)
            {
                return resultado;
            }
            else
            {

                ArrayList nResult = new ArrayList();
                ArrayList idN = new ArrayList();
                int y = 0;
                foreach (string reg in resultado)
                {
                    string input = @""+reg+"";

                   
                    string[] campos = Regex.Split(input, pattern);
                    for (int x = 0; x < campos.Length; x++)
                    {
                        nResult.Add(campos[x]);
                        idN.Add(id[y]);

                    }
                    y++;
                }
                id = idN;
                return nResult;
            }

        }
        private void button3_Clic()
        {
            bool normal = true;
            string[] nombreC = Program.campoL;
            int posCampo = 0;
            int posCampoC = 0;
            string campo = "3.2.4 Cote du versement";
            string campoC = "Clé dossier";
            int[] pos = new int[4];
           
            for (posCampo = 0; posCampo < nombreC.Length && nombreC[posCampo] != campo; posCampo++) { }

            for (posCampoC = 0; posCampoC < nombreC.Length && nombreC[posCampoC] != campoC; posCampoC++) { }

            ArrayList resultado = new ArrayList();
          
           
             //llenado inicial
             resultado = primerLlenado(resultado, posCampo, posCampoC);
            

            //ahora sacamos de cada registro el resto de registros
            resultado = segundoLlenado(resultado);

           

            rTb1 = resultado;
            MessageBox.Show("terminado");
        }
        private void button2_Clic()
        {
          
            swich(id, rTb1);



        }
        public void swich(ArrayList col1, ArrayList col2)
        {
            switch (cases)
            {
                case 1:
                    case1(col1, col2);
                    break;

                default:
                    Console.WriteLine("Default case");
                    break;
            }

        }
        public void case1(ArrayList col1, ArrayList col2)
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
                cmmd.CommandText += "" + "[Cle_dossier] Text,";
                cmmd.CommandText += "[3_2_4_Cote_du_versement] Text)";
                if (conn.State == ConnectionState.Open)
                {
                    try
                    {
                        //ejecutamos query de creacion de tabla
                        cmmd.ExecuteNonQuery();
                        MessageBox.Show("Add!");
                        // ejecutamos query de insert
                        ejecutar_inserts(tableName, conn, col1, col2);

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
        public void ejecutar_inserts(string tableName, OleDbConnection conn, ArrayList col1, ArrayList col2)
        {
            try
            {

                //variables

                int ncampos = col1.Count;

                int z = 0;
                foreach (string campo in col2)
                {
                    string inserta = "INSERT INTO " + tableName + " VALUES (";
                    inserta += @"""" + col1[z].ToString() + @"""";
                    inserta += ",";
                    inserta += @"""" + campo + @"""";
                    inserta += ")";
                   
                    OleDbCommand cmd = new OleDbCommand(inserta, conn);
                    cmd.ExecuteNonQuery();
                    z++;
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
