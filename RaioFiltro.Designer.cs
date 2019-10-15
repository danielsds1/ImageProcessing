namespace ImageProcessing
{
    partial class RaioFiltro
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.labelRaioFiltro = new System.Windows.Forms.Label();
            this.tamanhoRaio = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.tamanhoRaio)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(139, 59);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 23;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(58, 59);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 22;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // labelRaioFiltro
            // 
            this.labelRaioFiltro.AutoSize = true;
            this.labelRaioFiltro.Location = new System.Drawing.Point(55, 14);
            this.labelRaioFiltro.Name = "labelRaioFiltro";
            this.labelRaioFiltro.Size = new System.Drawing.Size(92, 13);
            this.labelRaioFiltro.TabIndex = 21;
            this.labelRaioFiltro.Text = "Tamanho do Raio";
            // 
            // tamanhoRaio
            // 
            this.tamanhoRaio.Location = new System.Drawing.Point(153, 12);
            this.tamanhoRaio.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.tamanhoRaio.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tamanhoRaio.Name = "tamanhoRaio";
            this.tamanhoRaio.Size = new System.Drawing.Size(75, 20);
            this.tamanhoRaio.TabIndex = 20;
            this.tamanhoRaio.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // RaioFiltro
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(272, 94);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.labelRaioFiltro);
            this.Controls.Add(this.tamanhoRaio);
            this.Name = "RaioFiltro";
            this.Text = "RaioFiltro";
            ((System.ComponentModel.ISupportInitialize)(this.tamanhoRaio)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label labelRaioFiltro;
        private System.Windows.Forms.NumericUpDown tamanhoRaio;
    }
}