namespace ImageProcessing
{
    partial class Dithering
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
            this.LabelValorB = new System.Windows.Forms.Label();
            this.ValorRInf = new System.Windows.Forms.NumericUpDown();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.LabelValorA = new System.Windows.Forms.Label();
            this.ValorRSup = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.ValorLimiar = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.DtDim = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.ValorRInf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValorRSup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValorLimiar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtDim)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelValorB
            // 
            this.LabelValorB.AutoSize = true;
            this.LabelValorB.Location = new System.Drawing.Point(19, 62);
            this.LabelValorB.Name = "LabelValorB";
            this.LabelValorB.Size = new System.Drawing.Size(117, 13);
            this.LabelValorB.TabIndex = 31;
            this.LabelValorB.Text = "Random Range Inferior";
            // 
            // ValorRInf
            // 
            this.ValorRInf.Location = new System.Drawing.Point(142, 59);
            this.ValorRInf.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ValorRInf.Minimum = new decimal(new int[] {
            255,
            0,
            0,
            -2147483648});
            this.ValorRInf.Name = "ValorRInf";
            this.ValorRInf.Size = new System.Drawing.Size(75, 20);
            this.ValorRInf.TabIndex = 3;
            this.ValorRInf.Value = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(142, 118);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 29;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.Location = new System.Drawing.Point(58, 118);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 28;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // LabelValorA
            // 
            this.LabelValorA.AutoSize = true;
            this.LabelValorA.Location = new System.Drawing.Point(12, 36);
            this.LabelValorA.Name = "LabelValorA";
            this.LabelValorA.Size = new System.Drawing.Size(124, 13);
            this.LabelValorA.TabIndex = 27;
            this.LabelValorA.Text = "Random Range Superior";
            // 
            // ValorRSup
            // 
            this.ValorRSup.Location = new System.Drawing.Point(142, 33);
            this.ValorRSup.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ValorRSup.Name = "ValorRSup";
            this.ValorRSup.Size = new System.Drawing.Size(75, 20);
            this.ValorRSup.TabIndex = 2;
            this.ValorRSup.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 33;
            this.label1.Text = "Valor do Limiar";
            // 
            // ValorLimiar
            // 
            this.ValorLimiar.Location = new System.Drawing.Point(142, 7);
            this.ValorLimiar.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ValorLimiar.Name = "ValorLimiar";
            this.ValorLimiar.Size = new System.Drawing.Size(75, 20);
            this.ValorLimiar.TabIndex = 1;
            this.ValorLimiar.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Dimensão Dithering";
            // 
            // DtDim
            // 
            this.DtDim.Location = new System.Drawing.Point(142, 85);
            this.DtDim.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.DtDim.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.DtDim.Name = "DtDim";
            this.DtDim.Size = new System.Drawing.Size(75, 20);
            this.DtDim.TabIndex = 34;
            this.DtDim.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // Dithering
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 153);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.DtDim);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ValorLimiar);
            this.Controls.Add(this.LabelValorB);
            this.Controls.Add(this.ValorRInf);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.LabelValorA);
            this.Controls.Add(this.ValorRSup);
            this.Name = "Dithering";
            this.Text = "Dithering";
            ((System.ComponentModel.ISupportInitialize)(this.ValorRInf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValorRSup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValorLimiar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DtDim)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelValorB;
        private System.Windows.Forms.NumericUpDown ValorRInf;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label LabelValorA;
        private System.Windows.Forms.NumericUpDown ValorRSup;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown ValorLimiar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown DtDim;
    }
}