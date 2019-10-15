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
    public partial class RaioFiltro : Form
    {
        public RaioFiltro()
        {
            InitializeComponent();
        }
        public int raio;
        private void BtnOK_Click(object sender, EventArgs e)
        {
            raio = (int)tamanhoRaio.Value;
        }
    }
}
