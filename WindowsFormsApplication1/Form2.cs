﻿using System;
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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public void añadir_pestañas()
        {
            tabControl1.TabPages.Add(new MyTabPage(new Form1()));
            tabControl1.TabPages.Add(new MyTabPage(new Procesar()));
            tabControl1.TabPages.Add(new MyTabPage(new Relacionar()));
            tabControl1.TabPages.Add(new MyTabPage(new Ajustes()));

        }
    }
}
