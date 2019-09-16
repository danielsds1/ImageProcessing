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
            oDlg.RestoreDirectory = true;
            oDlg.InitialDirectory = "C:\\Users\\";
            oDlg.FilterIndex = 1;
            editarMenu.Enabled = false;
            //oDlg.Filter = "jpg Files (*.jpg)|*.jpg|gif Files (*.gif)|*.gif|png Files (*.png)|*.png;*.PNG |bmp Files (*.bmp)|*.bmp";
            oDlg.Filter = "Arquivo de Imagem|*.jpg; *.gif; *.png;*.PNG;*.bmp";
            /*************************/
            sDlg = new SaveFileDialog(); // Save Dialog Initialization
            sDlg.RestoreDirectory = true;
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
            imagemA = MathOp(MathOperationType.subtracaoMedia);
            pictureBox1.Image = imagemA.BitmapAtual;
        }

        private void Multiplicacao_Click(object sender, EventArgs e)
        {
            imagemA = MathOp(MathOperationType.multiplicacao);
            pictureBox1.Image = imagemA.BitmapAtual;
        }

        private void Divisao_Click(object sender, EventArgs e)
        {
            imagemA = MathOp(MathOperationType.divisao);
            pictureBox1.Image = imagemA.BitmapAtual;
        }

        private void LogicNot_Click(object sender, EventArgs e)
        {
            imagemA = LogicOp(LogicOperationType.not);
            pictureBox1.Image = imagemA.BitmapAtual;
        }

        private void LogicOr_Click(object sender, EventArgs e)
        {
                imagemA = LogicOp(LogicOperationType.or);
                pictureBox1.Image = imagemA.BitmapAtual;   
        }

        private void LogicAnd_Click(object sender, EventArgs e)
        {
            imagemA = LogicOp(LogicOperationType.and);
            pictureBox1.Image = imagemA.BitmapAtual;
        }

        private void LogicXor_Click(object sender, EventArgs e)
        {
            imagemA = LogicOp(LogicOperationType.xor);
            pictureBox1.Image = imagemA.BitmapAtual;
        }

        private void LogicSub_Click(object sender, EventArgs e)
        {
            imagemA = LogicOp(LogicOperationType.sub);
            pictureBox1.Image = imagemA.BitmapAtual;
        }

        public enum LogicOperationType
        {
            not = 0,
            and = 1,
            or = 2,
            xor = 3,
            sub = 4
        }
        public enum MathOperationType
        {
            adicaoLimiar = 1,
            adicaoMedia = 2,
            subtracaoLimiar = 3,
            subtracaoMedia = 4,
            multiplicacao = 5,
            divisao = 6
        }
        public enum ImageType
        {
            binary = 2,
            gray = 256,
            color = 0
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
        public ManipuladorImagem LogicOp(LogicOperationType operation)
        {
            ManipuladorImagem imagemB;
            imagemA.ToBool();
            if (operation!= LogicOperationType.not)
            {
                imagemB = GetImagemB();
            }
            else
            {
                imagemB = imagemA;
            }
            if (imagemB != null&& imagemA != null)
            {
                imagemB.ToBool();
                int x, y;
                for (x = 0; x < imagemB.MatrizBool.Width; x++)
                {
                    for (y = 0; y < imagemB.MatrizBool.Height; y++)
                    {
                        switch (operation)
                        {
                            case LogicOperationType.not:
                                imagemA.MatrizBool.Matriz[x, y] = !imagemB.MatrizBool.Matriz[x, y];
                                break;
                            case LogicOperationType.and:
                                imagemA.MatrizBool.Matriz[x, y] = imagemA.MatrizBool.Matriz[x, y] && imagemB.MatrizBool.Matriz[x, y];
                                break;
                            case LogicOperationType.or:
                                imagemA.MatrizBool.Matriz[x, y] = imagemA.MatrizBool.Matriz[x, y] || imagemB.MatrizBool.Matriz[x, y];
                                break;
                            case LogicOperationType.sub:
                                imagemA.MatrizBool.Matriz[x, y] = imagemA.MatrizBool.Matriz[x, y] && !imagemB.MatrizBool.Matriz[x, y];
                                break;
                            case LogicOperationType.xor:
                                imagemA.MatrizBool.Matriz[x, y] = imagemA.MatrizBool.Matriz[x, y] && !imagemB.MatrizBool.Matriz[x, y] || !imagemA.MatrizBool.Matriz[x, y] && imagemA.MatrizBool.Matriz[x, y];
                                break;
                        }
                    }
                }
                imagemA.ToImage();
            }
            return imagemA;
        }

        
        

        private void FiltroMedia_Click(object sender, EventArgs e)
        {
            int r = 0, g = 0, b = 0, x, y, i, k;

            for (x = 1; x < imagemA.BitmapAtual.Width - 1; x++)
            {

                for (y = 1; y < imagemA.BitmapAtual.Height - 1; y++)
                {
                    for (i = x - 1; i < x + 2; i++)
                    {
                        for (k = y - 1; k < y + 2; k++)
                        {
                            r += (int)imagemA.BitmapAtual.GetPixel(i, k).R;
                            g += (int)imagemA.BitmapAtual.GetPixel(i, k).G;
                            b += (int)imagemA.BitmapAtual.GetPixel(i, k).B;
                        }
                    }

                    r = (int)(r / 9);
                    g = (int)(g / 9);
                    b = (int)(b / 9);

                    imagemA.BitmapAtual.SetPixel(x, y, Color.FromArgb(r, g, b));
                    r = 0; g = 0; b = 0;
                }
            }
            pictureBox1.Image = (Image)imagemA.BitmapAtual;
        }

        private void FiltroMediana_Click(object sender, EventArgs e)
        {
            int[] r = new int[9];
            int[] g = new int[9];
            int[] b = new int[9];
            int x, y, i, k, pos = 0;

            for (x = 1; x < imagemA.BitmapAtual.Width - 1; x++)
            {
                for (y = 1; y < imagemA.BitmapAtual.Height - 1; y++)
                {
                    for (i = x - 1; i < x + 2; i++)
                    {
                        for (k = y - 1; k < y + 2; k++)
                        {
                            r[pos] = (int)imagemA.BitmapAtual.GetPixel(i, k).R;
                            g[pos] = (int)imagemA.BitmapAtual.GetPixel(i, k).G;
                            b[pos] = (int)imagemA.BitmapAtual.GetPixel(i, k).B;
                            pos++;
                        }
                    }
                    Array.Sort(r);
                    Array.Sort(g);
                    Array.Sort(b);
                    imagemA.BitmapAtual.SetPixel(x, y, Color.FromArgb(r[4], g[4], b[4]));
                    pos = 0;
                }
            }
            pictureBox1.Image = (Image)imagemA.BitmapAtual;
        }

        private void Desfazer_Click(object sender, EventArgs e)
        {
            imagemA.BitmapAtual=imagemA.BitmapPreProcess;
            pictureBox1.Image = imagemA.BitmapAtual;
        }
    }
}
