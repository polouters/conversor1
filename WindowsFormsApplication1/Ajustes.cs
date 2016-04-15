using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Ajustes : MyTabPage.MyFormPage
    {
        public Ajustes()
        {
            InitializeComponent();
            this.pn1 = panel1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Files.Text = openFileDialog1.FileName;
                Program.archivo_txt = Files.Text;

            }
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName;
                Program.archivo_acs = Files.Text;
            }
                
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = folderBrowserDialog1.SelectedPath;
            }
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se ha producido un error" + ex);
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Program.table_name = textBox2.Text;
        }
    }
}
