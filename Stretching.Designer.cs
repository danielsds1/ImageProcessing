namespace ImageProcessing
{
    partial class Stretching
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
            this.LabelValorA = new System.Windows.Forms.Label();
            this.ValorA = new System.Windows.Forms.NumericUpDown();
            this.LabelValorB = new System.Windows.Forms.Label();
            this.ValorB = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.ValorA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValorB)).BeginInit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.Location = new System.Drawing.Point(99, 62);
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
            this.btnOK.Location = new System.Drawing.Point(15, 62);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 22;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // LabelValorA
            // 
            this.LabelValorA.AutoSize = true;
            this.LabelValorA.Location = new System.Drawing.Point(12, 9);
            this.LabelValorA.Name = "LabelValorA";
            this.LabelValorA.Size = new System.Drawing.Size(56, 13);
            this.LabelValorA.TabIndex = 21;
            this.LabelValorA.Text = "Valor de A";
            // 
            // ValorA
            // 
            this.ValorA.DecimalPlaces = 5;
            this.ValorA.Location = new System.Drawing.Point(99, 7);
            this.ValorA.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ValorA.Name = "ValorA";
            this.ValorA.Size = new System.Drawing.Size(75, 20);
            this.ValorA.TabIndex = 20;
            // 
            // LabelValorB
            // 
            this.LabelValorB.AutoSize = true;
            this.LabelValorB.Location = new System.Drawing.Point(12, 38);
            this.LabelValorB.Name = "LabelValorB";
            this.LabelValorB.Size = new System.Drawing.Size(56, 13);
            this.LabelValorB.TabIndex = 25;
            this.LabelValorB.Text = "Valor de B";
            // 
            // ValorB
            // 
            this.ValorB.DecimalPlaces = 5;
            this.ValorB.Location = new System.Drawing.Point(99, 33);
            this.ValorB.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this.ValorB.Name = "ValorB";
            this.ValorB.Size = new System.Drawing.Size(75, 20);
            this.ValorB.TabIndex = 24;
            // 
            // Stretching
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(190, 100);
            this.Controls.Add(this.LabelValorB);
            this.Controls.Add(this.ValorB);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.LabelValorA);
            this.Controls.Add(this.ValorA);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Stretching";
            this.Text = "Stretching";
            ((System.ComponentModel.ISupportInitialize)(this.ValorA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ValorB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label LabelValorA;
        private System.Windows.Forms.NumericUpDown ValorA;
        private System.Windows.Forms.Label LabelValorB;
        private System.Windows.Forms.NumericUpDown ValorB;
    }
}