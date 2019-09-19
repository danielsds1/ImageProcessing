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
    public partial class ImageProcessing : Form
    {
        private int maxColorValue = 255;
        OpenFileDialog oDlg;
        SaveFileDialog sDlg;
        public int MinHeight = 360;
        public int NumImg = 0;
        double zoomFactor = 1.0;
        /*public struct ImagemBool
        {
            public int Width, Height;
            public bool[,] matriz;
        };*/
        //private MenuItem cZoom;
        public ManipuladorImagem imagemA = new ManipuladorImagem();

        public ImageProcessing()
        {
            InitializeComponent();
            oDlg = new OpenFileDialog(); // Open Dialog Initialization
            oDlg.RestoreDirectory = false;
            oDlg.InitialDirectory = "C:\\Users\\";
            oDlg.FilterIndex = 1;
            editarMenu.Enabled = false;
            //oDlg.Filter = "jpg Files (*.jpg)|*.jpg|gif Files (*.gif)|*.gif|png Files (*.png)|*.png;*.PNG |bmp Files (*.bmp)|*.bmp";
            oDlg.Filter = "Arquivo de Imagem|*.jpg; *.gif; *.png;*.PNG;*.bmp";
            /*************************/
            sDlg = new SaveFileDialog(); // Save Dialog Initialization
            sDlg.RestoreDirectory = false;
            sDlg.InitialDirectory = "C:\\Users\\";
            sDlg.FilterIndex = 1;
            sDlg.Filter = "jpg Files (*.jpg)|*.jpg|gif Files (*.gif)|*.gif|png Files (*.png)|*.png;*.PNG |bmp Files (*.bmp)|*.bmp";
            /*************************/
            //cZoom = menuItemZoom100; // Current Zoom Percentage = 100%
        }

        private void SalvarArquivo_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == sDlg.ShowDialog())
            {
                imagemA.SaveBitmap(sDlg.FileName);
            }
        }

        private void AbrirArquivo_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == oDlg.ShowDialog())
            {
                zoomFactor = 1.0;
                imagemA.BitmapAtual = (Bitmap)Bitmap.FromFile(oDlg.FileName);
                imagemA.BitmapPreProcess = imagemA.BitmapAtual;
                imagemA.BitmapCaminho = oDlg.FileName;
                if (imagemA != null)
                {
                    editarMenu.Enabled = true;
                }
                this.Invalidate();

                if (imagemA.BitmapAtual.Height > MinHeight) zoomFactor = Convert.ToDouble(MinHeight) / imagemA.BitmapAtual.Height;
                pictureBox1.Size = new Size(Convert.ToInt32(imagemA.BitmapAtual.Width * zoomFactor), Convert.ToInt32(imagemA.BitmapAtual.Height * zoomFactor));
                pictureBox1.Image = (Image)imagemA.BitmapAtual;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            }
        }

        private void Sair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Adicao_Click(object sender, EventArgs e)
        {
            imagemA = MathOp(MathOperationType.adicaoLimiar);
            pictureBox1.Image = imagemA.BitmapAtual;
        }

        private void AdicaoMedia_Click(object sender, EventArgs e)
        {
            imagemA = MathOp(MathOperationType.adicaoMedia);
            pictureBox1.Image = imagemA.BitmapAtual;
        }

        private void SubtracaoLimiar_Click(object sender, EventArgs e)
        {
            imagemA = MathOp(MathOperationType.subtracaoLimiar);
            pictureBox1.Image = imagemA.BitmapAtual;
        }

        private void SubtracaoMedia_Click(object sender, EventArgs e)
        {
            //imagemA = MathOp(MathOperationType.subtracaoMedia);
            pictureBox1.Image = imagemA.MathOp(MathOperationType.subtracaoMedia, GetImagemB());
        }

        private void Multiplicacao_Click(object sender, EventArgs e)
        {
            imagemA = MathOp(MathOperationType.multiplicacao);
            pictureBox1.Image = imagemA.BitmapAtual;
        }

        private void Divisao_Click(object sender, EventArgs e)
        {
            //imagemA = MathOp(MathOperationType.divisao);
            pictureBox1.Image = imagemA.BitmapAtual;
        }

        private void LogicNot_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagemA.LogicOp(LogicOperationType.not, null);
        }

        private void LogicOr_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagemA.LogicOp(LogicOperationType.or, GetImagemB());
        }

        private void LogicAnd_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagemA.LogicOp(LogicOperationType.and, GetImagemB());
        }

        private void LogicXor_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagemA.LogicOp(LogicOperationType.xor, GetImagemB());
        }

        private void LogicSub_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagemA.LogicOp(LogicOperationType.sub, GetImagemB());
        }


        public ManipuladorImagem MathOp(MathOperationType operation)
        {
            var imagemB = GetImagemB();
            if (imagemB != null)
            {
                int x, y, ra, ga, ba, rb, gb, bb;
                for (x = 0; x < imagemB.BitmapAtual.Width; x++)
                {
                    for (y = 0; y < imagemB.BitmapAtual.Height; y++)
                    {
                        ra = (int)imagemA.BitmapAtual.GetPixel(x, y).R;
                        rb = (int)imagemB.BitmapAtual.GetPixel(x, y).R;
                        ga = (int)imagemA.BitmapAtual.GetPixel(x, y).G;
                        gb = (int)imagemB.BitmapAtual.GetPixel(x, y).G;
                        ba = (int)imagemA.BitmapAtual.GetPixel(x, y).B;
                        bb = (int)imagemB.BitmapAtual.GetPixel(x, y).B;
                        switch (operation)
                        {
                            case MathOperationType.adicaoLimiar:
                                ra += rb; ra = (ra <= 255) ? ra : 255;
                                ga += gb; ga = (ga <= 255) ? ga : 255;
                                ba += bb; ba = (ba <= 255) ? ba : 255;
                                break;
                            case MathOperationType.adicaoMedia:
                                ra = (ra + rb) / 2;
                                ga = (ga + gb) / 2;
                                ba = (ba + bb) / 2;
                                break;
                            case MathOperationType.divisao:
                                rb = (rb == 0) ? 1 : rb;
                                gb = (gb == 0) ? 1 : gb;
                                rb = (bb == 0) ? 1 : bb;
                                ra /= rb;
                                ga /= gb;
                                ba /= bb;
                                break;
                            case MathOperationType.multiplicacao:
                                ra *= rb / maxColorValue;
                                ga *= gb / maxColorValue;
                                ba *= bb / maxColorValue;
                                break;
                            case MathOperationType.subtracaoLimiar:
                                ra -= rb; ra = (ra >= 0) ? ra : 0;
                                ga -= gb; ga = (ga >= 0) ? ga : 0;
                                ba -= bb; ba = (ba >= 0) ? ba : 0;
                                break;
                            case MathOperationType.subtracaoMedia:
                                ra = (ra - rb + maxColorValue) / 2;
                                ga = (ga - gb + maxColorValue) / 2;
                                ba = (ba - bb + maxColorValue) / 2;
                                break;
                        }

                        imagemA.BitmapAtual.SetPixel(x, y, Color.FromArgb(ra, ga, ba));


                    }
                }

            }
            return imagemA;
        }

        public ManipuladorImagem GetImagemB()
        {
            ManipuladorImagem imagemB = new ManipuladorImagem();

            if (DialogResult.OK == oDlg.ShowDialog())
            {
                imagemB.BitmapAtual = (Bitmap)Bitmap.FromFile(oDlg.FileName);
                imagemB.BitmapCaminho = oDlg.FileName;
                if (imagemB.BitmapAtual.Width > imagemA.BitmapAtual.Width)
                {
                    MessageBox.Show("A imagem de destino é maior do que a de origem!", "Erro!");
                    return null;
                }
                return imagemB;
            }
            return null;

        }

        private void FiltroMedia_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagemA.FiltroMedia();
        }

        private void FiltroMediana_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagemA.FiltroMediana();
        }

        private void Desfazer_Click(object sender, EventArgs e)
        {
            imagemA.BitmapAtual = imagemA.BitmapPreProcess;
            pictureBox1.Image = imagemA.BitmapAtual;
        }

        private void Histograma_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagemA.CorrecaoHistograma();
        }
    }
}
