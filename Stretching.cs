using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageProcessing
{
    public partial class Stretching : Form
    {
        public Stretching()
        {
            InitializeComponent();
        }

        public double A { get; set; }
        public double B { get; set; }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            A = (double)ValorA.Value;
            B = (double)ValorB.Value;
            this.Invalidate();
            this.Close();
        }
    }
}
