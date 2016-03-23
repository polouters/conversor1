namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.Files = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Analizar = new System.Windows.Forms.Button();
            this.lista = new System.Windows.Forms.ListView();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Files
            // 
            this.Files.Location = new System.Drawing.Point(165, 12);
            this.Files.Name = "Files";
            this.Files.Size = new System.Drawing.Size(771, 20);
            this.Files.TabIndex = 0;
            this.Files.Text = "M:\\PROYECTO\\75-European Projects\\7501 CDRGREFFE122015 Gestión de Archivos\\Entrega" +
    "do por CoR\\Extraccion EXCEL CLARA Marzo 2016\\Extraction sauvegarde entree_805 RU" +
    "BEN.xls";
            this.Files.TextChanged += new System.EventHandler(this.File_TextChanged);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.RosyBrown;
            this.button1.Location = new System.Drawing.Point(82, 132);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(310, 116);
            this.button1.TabIndex = 1;
            this.button1.Text = "PROCESAR";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Analizar
            // 
            this.Analizar.BackColor = System.Drawing.Color.LightGreen;
            this.Analizar.Location = new System.Drawing.Point(626, 132);
            this.Analizar.Name = "Analizar";
            this.Analizar.Size = new System.Drawing.Size(310, 116);
            this.Analizar.TabIndex = 2;
            this.Analizar.Text = "ANALIZAR";
            this.Analizar.UseVisualStyleBackColor = false;
            this.Analizar.Click += new System.EventHandler(this.button2_Click);
            // 
            // lista
            // 
            this.lista.Location = new System.Drawing.Point(51, 280);
            this.lista.Name = "lista";
            this.lista.Size = new System.Drawing.Size(945, 460);
            this.lista.TabIndex = 3;
            this.lista.UseCompatibleStateImageBehavior = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(165, 47);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(432, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "C:\\Users\\alberto\\Documents\\database1.accdb";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.label1.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 18);
            this.label1.TabIndex = 6;
            this.label1.Text = "Archivo a procesar";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(6, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 18);
            this.label2.TabIndex = 7;
            this.label2.Text = "Archivo Access";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(165, 85);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(574, 20);
            this.textBox2.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Gill Sans Ultra Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(6, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 18);
            this.label3.TabIndex = 9;
            this.label3.Text = "Titulo de la tabla";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(942, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(26, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_2);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(603, 46);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(26, 23);
            this.button3.TabIndex = 11;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(1048, 766);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.lista);
            this.Controls.Add(this.Analizar);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Files);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Files;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button Analizar;
        private System.Windows.Forms.ListView lista;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

