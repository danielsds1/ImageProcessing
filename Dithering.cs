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
        public int Limiar { get; set; }
        public int Rsup { get; set; }
        public int Rinf { get; set; }
        public int Dispersao { get; set; }
        public int Vizinhos { get; set; }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Rsup = (int)ValorRSup.Value;
            Rinf = (int)ValorRInf.Value;
            Limiar = (int)ValorLimiar.Value;
            Dispersao = (int)DtDim.Value;
            Vizinhos = (int)dtVizinhos.Value;
            this.Invalidate();
            this.Close();
        }
    }
}
