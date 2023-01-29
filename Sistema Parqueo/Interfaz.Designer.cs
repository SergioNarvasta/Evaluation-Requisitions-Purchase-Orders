namespace Sistema_Parqueo
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.label1 = new System.Windows.Forms.Label();
            this.btnResgCli = new System.Windows.Forms.Button();
            this.btnGenerCompr = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Georgia", 15.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(137, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(399, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "SISTEMA DE CONTROL DEL PARQUEO";
            // 
            // btnResgCli
            // 
            this.btnResgCli.Location = new System.Drawing.Point(13, 3);
            this.btnResgCli.Name = "btnResgCli";
            this.btnResgCli.Size = new System.Drawing.Size(168, 56);
            this.btnResgCli.TabIndex = 2;
            this.btnResgCli.Text = "REGISTRAR CLIENTE";
            this.btnResgCli.UseVisualStyleBackColor = true;
            this.btnResgCli.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnGenerCompr
            // 
            this.btnGenerCompr.Location = new System.Drawing.Point(208, 3);
            this.btnGenerCompr.Name = "btnGenerCompr";
            this.btnGenerCompr.Size = new System.Drawing.Size(173, 56);
            this.btnGenerCompr.TabIndex = 3;
            this.btnGenerCompr.Text = "GENERAR COMPRABANTE";
            this.btnGenerCompr.UseVisualStyleBackColor = true;
            this.btnGenerCompr.Click += new System.EventHandler(this.button2_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnGenerCompr);
            this.panel1.Controls.Add(this.btnResgCli);
            this.panel1.Location = new System.Drawing.Point(142, 253);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(394, 68);
            this.panel1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::Sistema_Parqueo.Properties.Resources.descarga;
            this.pictureBox1.InitialImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.InitialImage")));
            this.pictureBox1.Location = new System.Drawing.Point(187, 77);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(313, 157);
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.BackColor = System.Drawing.Color.CadetBlue;
            this.ClientSize = new System.Drawing.Size(676, 375);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnResgCli;
        private System.Windows.Forms.Button btnGenerCompr;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

