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
    public partial class Quantizacao : Form
    {
        public Quantizacao()
        {
            InitializeComponent();
        }

        public int niveis { get; set; }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            niveis = (int)niveisCinza.Value;
            this.Invalidate();
            this.Close();

        }
    }
    }
