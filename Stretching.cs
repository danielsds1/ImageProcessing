﻿using System;
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

        public int A { get; set; }
        public int B { get; set; }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            A = (int)ValorA.Value;
            B = (int)ValorB.Value;
            this.Invalidate();
            this.Close();
        }
    }
}
