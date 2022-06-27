namespace Sistema_Parqueo
{
    partial class ReporteComprobante
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DataSetImpresionTemp = new Sistema_Parqueo.DataSetImpresionTemp();
            this.ImpresionTempBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ImpresionTempTableAdapter = new Sistema_Parqueo.DataSetImpresionTempTableAdapters.ImpresionTempTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetImpresionTemp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImpresionTempBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = this.ImpresionTempBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "Sistema_Parqueo.ReportImpresion.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(668, 374);
            this.reportViewer1.TabIndex = 0;
            // 
            // DataSetImpresionTemp
            // 
            this.DataSetImpresionTemp.DataSetName = "DataSetImpresionTemp";
            this.DataSetImpresionTemp.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ImpresionTempBindingSource
            // 
            this.ImpresionTempBindingSource.DataMember = "ImpresionTemp";
            this.ImpresionTempBindingSource.DataSource = this.DataSetImpresionTemp;
            // 
            // ImpresionTempTableAdapter
            // 
            this.ImpresionTempTableAdapter.ClearBeforeFill = true;
            // 
            // ReporteComprobante
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 374);
            this.Controls.Add(this.reportViewer1);
            this.Name = "ReporteComprobante";
            this.Text = "ReporteComprobante";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ReporteComprobante_FormClosed);
            this.Load += new System.EventHandler(this.ReporteComprobante_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DataSetImpresionTemp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ImpresionTempBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource ImpresionTempBindingSource;
        private DataSetImpresionTemp DataSetImpresionTemp;
        private DataSetImpresionTempTableAdapters.ImpresionTempTableAdapter ImpresionTempTableAdapter;
    }
}