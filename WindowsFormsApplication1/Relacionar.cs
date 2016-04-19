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
        public ArrayList rTb1 = new ArrayList();
        public ArrayList id2 = new ArrayList();
        public ArrayList rTb2 = new ArrayList();
        public Relacionar()
        {
            InitializeComponent();
            this.pn1 = panel1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool ck1 = checkBox1.Checked;
            bool ck2 = checkBox2.Checked;
            bool ck3 = checkBox3.Checked;
            bool ck4 = checkBox4.Checked;
            ArrayList col1 = new ArrayList();
            ArrayList col2 = new ArrayList();
            if (ck1 == true && ck4 == true)
            {
                int x = 0;
                foreach (string campo1 in rTb1)
                {
                    foreach(string clave2 in id2)
                    {
                        if(campo1 == clave2)
                        {
                            col1.Add(id[x].ToString());
                            col2.Add(clave2);  
                        }
                    }
                    x++;
                }
            }
            if (ck1 == true && ck3 == true)
            {
                int x = 0;
                foreach (string campo1 in rTb1)
                {
                    int z = 0;
                    foreach (string campo2 in rTb2)
                    {
                        if (campo1 == campo2)
                        {
                            col1.Add(id[x].ToString());
                            col2.Add(id2[z].ToString());
                        }
                        z++;
                    }
                    x++;
                }
            }
            if (ck2 == true && ck4 == true)
            {
                foreach (string clave1 in id)
                {
                    foreach (string clave2 in id2)
                    {
                        if (clave1 == clave2)
                        {
                            col1.Add(clave1);
                            col2.Add(clave2);
                        }
                    }
                }
            }
            if (ck2 == true && ck3 == true)
            {
                foreach (string clave1 in id)
                {
                    int z = 0;
                    foreach (string campo2 in rTb2)
                    {
                        if (clave1 == campo2)
                        {
                            col1.Add(clave1);
                            col2.Add(id2[z].ToString());
                        }
                        z++;
                    }
                }
            }

            swich(col1, col2);
            


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
                        z++;
                    }
                    registro = new ArrayList(); // formateamos el ArrayList
                    camposprocesados = 0; // La siguiente línea es un registro nuevo
                }
            }
            

            file.Close();

            return nResult;
        }
        public ArrayList primerLlenado(ArrayList resultado, int posCampo, int [] posCampoC)
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
                    string campoF = "";
                    string[] campoFA =new string[posCampoC.Length];
                    int z = 0;
                    foreach (string campo in registro)
                    {
                        if (z == posCampo)
                        {
                            nResult.Add(campo);
                        }
                        int w = 0;
                        foreach(int pos in posCampoC)
                        {
                            if (z == pos)
                            {
                                campoFA[w] = campo;
                            }
                            w++;
                        }

                        z++;
                    }
                    foreach(string str in campoFA)
                    {
                        campoF += str;
                    }
                    id.Add(campoF);
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
                Char[] c = cs1Tb1.Text.ToCharArray();
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
        public ArrayList tercerLlenado(ArrayList resultado)
        {
            ArrayList nResult = new ArrayList();
            int y = 0;
            foreach (string reg in resultado)
            {
                Char[] c = cs2Tb1.Text.ToCharArray();
                string[] campos = reg.Split(c);
                if(rbSi.Checked == true)
                {
                    nResult.Add(campos[0].Replace(tbq1.Text,""));
                }
                else
                {
                    nResult.Add(campos[0]);
                }
               
                    
                
                y++;
            }
            return nResult;
        }
        public void swich(ArrayList col1, ArrayList col2)
        {
            switch (cases)
            {
                case 1:
                    case1(col1,col2);
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
                cmmd.CommandText += "" + "[" + ccTb1.Text.Replace(".", "_") + "] Text,";
                cmmd.CommandText += "[" + cTb2.Text.Replace(".", "_") + "] Text)";
                if (conn.State == ConnectionState.Open)
                {
                    try
                    {
                        //ejecutamos query de creacion de tabla
                        cmmd.ExecuteNonQuery();
                        MessageBox.Show("Add!");
                        // ejecutamos query de insert
                        ejecutar_inserts(tableName, conn,col1, col2);

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
        public void ejecutar_inserts(string tableName, OleDbConnection conn,ArrayList col1,ArrayList col2)
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
        private void button3_Click(object sender, EventArgs e)
        {
            bool normal = true;
            string[] nombreC = Program.campoL;
            int posCampo = 0;
            int posCampoC = 0;
            string campo = cTb1.Text;
            string campoC = ccTb1.Text;
            int[] pos = new int[4];
            if (campoC.IndexOf("+") != -1)
            {
             pos =    otrocamino(campoC);
                normal = false;
                
            }

            for (posCampo = 0; posCampo < nombreC.Length && nombreC[posCampo] != campo; posCampo++) { }

            for (posCampoC = 0; posCampoC < nombreC.Length && nombreC[posCampoC] != campoC; posCampoC++) { }

            ArrayList resultado = new ArrayList();
            if (normal == false)
            {
                //llenado inicial
                resultado = primerLlenado(resultado, posCampo, pos);
            }
            else
            {
                //llenado inicial
                resultado = primerLlenado(resultado, posCampo, posCampoC);
            }
            
            //ahora sacamos de cada registro el resto de registros
            resultado = segundoLlenado(resultado);

            //ahora separamos la clave del campo
            resultado = tercerLlenado(resultado);

            rTb1 = resultado;
            MessageBox.Show("terminado");
        }
        public int[]  otrocamino(string campoC)
        {
            // con esto conseguimos las posiciones de las tres claves
            int posCampoC = 0;
            string[] nombreC = Program.campoL;
            int x = 0;
            string[] camposCC = campoC.Split('+');
            int[] pos = new int[camposCC.Length];
            foreach ( string cam in camposCC)
            {
                for (posCampoC = 0; posCampoC < nombreC.Length && nombreC[posCampoC] != cam; posCampoC++) { }
                pos[x] = posCampoC;
                x++;
            }
            return pos;

            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            bool normal = true;
            string[] nombreC = Program.campoL;
            int posCampo = 0;
            int posCampoC = 0;
            string campo = cTb2.Text;
            string campoC = ccTb2.Text;
            int[] pos= new int[4];
            if (campoC.IndexOf("+") != -1)
            {
                pos = otrocamino(campoC);
                normal = false;

            }
            else {
                for (posCampoC = 0; posCampoC < nombreC.Length && nombreC[posCampoC] != campoC; posCampoC++) { }
            }

            for (posCampo = 0; posCampo < nombreC.Length && nombreC[posCampo] != campo; posCampo++) { }

           // for (posCampoC = 0; posCampoC < nombreC.Length && nombreC[posCampoC] != campoC; posCampoC++) { }

            ArrayList resultado = new ArrayList();
            if (normal == false)
            {
                //llenado inicial
                resultado = primerLlenado2(resultado, posCampo, pos);
            }
            else
            {
                //llenado inicial
                resultado = primerLlenado2(resultado, posCampo, posCampoC);
            }
           

            //ahora sacamos de cada registro el resto de registros
            resultado = segundoLlenado2(resultado);

            //ahora separamos la clave del campo
            resultado = tercerLlenado2(resultado);

            rTb2 = resultado;
            MessageBox.Show("terminado");

        }
        public ArrayList primerLlenado2(ArrayList resultado, int posCampo, int posCampoC)
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
                        if (z == posCampo)
                        {
                            nResult.Add(campo);
                        }
                        if (z == posCampoC)
                        {
                            id2.Add(campo);
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
        public ArrayList primerLlenado2(ArrayList resultado, int posCampo, int[] posCampoC)
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
                    string campoF = "";
                    string[] campoFA = new string[posCampoC.Length];
                    int z = 0;
                    foreach (string campo in registro)
                    {
                        if (z == posCampo)
                        {
                            nResult.Add(campo);
                        }
                        int w = 0;
                        foreach (int pos in posCampoC)
                        {
                            if (z == pos)
                            {
                                campoFA[w] = campo;
                            }
                            w++;
                        }

                        z++;
                    }
                    foreach (string str in campoFA)
                    {
                        campoF += str;
                    }
                    id2.Add(campoF);
                    registro = new ArrayList(); // formateamos el ArrayList
                    camposprocesados = 0; // La siguiente línea es un registro nuevo
                }
            }


            file.Close();

            return nResult;
        }
        public ArrayList segundoLlenado2(ArrayList resultado)
        {
            ArrayList nResult = new ArrayList();
            ArrayList idN = new ArrayList();
            int y = 0;
            foreach (string reg in resultado)
            {
                Char[] c = cs1Tb2.Text.ToCharArray();
                string[] campos = reg.Split(c);
                for (int x = 0; x < campos.Length; x++)
                {
                    nResult.Add(campos[x]);
                    idN.Add(id2[y]);

                }
                y++;
            }
            id2 = idN;
            return nResult;
        }
        public ArrayList tercerLlenado2(ArrayList resultado)
        {
            ArrayList nResult = new ArrayList();
            int y = 0;
            foreach (string reg in resultado)
            {
                Char[] c = cs2Tb2.Text.ToCharArray();
                string[] campos = reg.Split(c);

                if (rbSi.Checked == true)
                {
                    nResult.Add(campos[0].Replace(tbq2.Text, ""));
                }
                else
                {
                    nResult.Add(campos[0]);
                }

                y++;
            }
            return nResult;
        }
    }
}
