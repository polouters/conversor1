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
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // Files
            // 
            this.Files.Location = new System.Drawing.Point(135, 46);
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
            this.button1.Location = new System.Drawing.Point(83, 97);
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
            this.Analizar.Location = new System.Drawing.Point(643, 97);
            this.Analizar.Name = "Analizar";
            this.Analizar.Size = new System.Drawing.Size(310, 116);
            this.Analizar.TabIndex = 2;
            this.Analizar.Text = "ANALIZAR";
            this.Analizar.UseVisualStyleBackColor = false;
            this.Analizar.Click += new System.EventHandler(this.button2_Click);
            // 
            // lista
            // 
            this.lista.Location = new System.Drawing.Point(49, 244);
            this.lista.Name = "lista";
            this.lista.Size = new System.Drawing.Size(945, 460);
            this.lista.TabIndex = 3;
            this.lista.UseCompatibleStateImageBehavior = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(467, 206);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 23);
            this.progressBar1.TabIndex = 4;
            this.progressBar1.StyleChanged += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.WindowFrame;
            this.ClientSize = new System.Drawing.Size(1038, 716);
            this.Controls.Add(this.progressBar1);
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
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}

