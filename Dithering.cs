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
    public partial class Dithering : Form
    {
        public Dithering()
        {
            InitializeComponent();
        }
        public int limiar;
        public int rsup;
        public int rinf;
        public int dispersao;
        private void BtnOK_Click(object sender, EventArgs e)
        {
            rsup = (int)ValorRSup.Value;
            rinf = (int)ValorRInf.Value;
            limiar = (int)ValorLimiar.Value;
            dispersao = (int)DtDim.Value;
            this.Invalidate();
            this.Close();
        }
    }
}
