using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
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
        public double zoomFactor = 1.0;
        /*public struct ImagemBool
        {
            public int Width, Height;
            public bool[,] matriz;
        };*/
        //private MenuItem cZoom;
        //public ManipuladorImagem imagemA = new ManipuladorImagem();
        public List<Imagem> imagens = new List<Imagem>();
        //public ManipuladorImagem imagemA = new ManipuladorImagem();
        public ImageProcessing()
        {

            InitializeComponent();
            oDlg = new OpenFileDialog
            {
                RestoreDirectory = true,
                //InitialDirectory = "C:\\Users\\",
                FilterIndex = 1,
                Filter = "Arquivo de Imagem|*.jpg; *.gif; *.png;*.PNG;*.bmp"
            }; // Open Dialog Initialization
            editarMenu.Enabled = false;
            salvarArquivo.Enabled = false;
            /*************************/
            sDlg = new SaveFileDialog
            {
                RestoreDirectory = true,
                //InitialDirectory = "C:\\Users\\",
                FilterIndex = 1,
                Filter = "jpg Files (*.jpg)|*.jpg|gif Files (*.gif)|*.gif|png Files (*.png)|*.png;*.PNG |bmp Files (*.bmp)|*.bmp"
            }; // Save Dialog Initialization
            /*************************/
            //cZoom = menuItemZoom100; // Current Zoom Percentage = 100%
        }

        private void SalvarArquivo_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == sDlg.ShowDialog())
            {
                imagens[0].SaveBitmap(sDlg.FileName);
            }
        }

        private void AbrirArquivo_Click(object sender, EventArgs e)
        {
            Imagem imagemA = new Imagem();
            oDlg.Multiselect = false;
            oDlg.Title = "Abrir Imagem";
            if (DialogResult.OK == oDlg.ShowDialog())
            {
                //zoomFactor = 1.0;
                imagemA.BitmapAtual = (Bitmap)Bitmap.FromFile(oDlg.FileName);

                if (imagens.Count() > 0)
                {
                    imagens.Clear();
                }

                imagemA.BitmapAtual = (Bitmap)Bitmap.FromFile(oDlg.FileName);
                //imagemA.BitmapPreProcess = imagemA.BitmapAtual;
                imagemA.BitmapCaminho = oDlg.FileName;
                imagens.Add(imagemA);

                //imagens[0].BitmapAtual = (Bitmap)Bitmap.FromFile(oDlg.FileName);
                //imagens[0].BitmapPreProcess = imagens[0].BitmapAtual;
                //imagens[0].BitmapCaminho = oDlg.FileName;

                if (imagens[0] != null)
                {
                    editarMenu.Enabled = true;
                }
                //this.Invalidate();

                if (imagens[0].BitmapAtual.Height > MinHeight) zoomFactor = Convert.ToDouble(MinHeight) / imagens[0].BitmapAtual.Height;
                pictureBox1.Size = new Size(Convert.ToInt32(imagens[0].BitmapAtual.Width * zoomFactor), Convert.ToInt32(imagens[0].BitmapAtual.Height * zoomFactor));
                pictureBox1.Image = (Image)imagens[0].BitmapAtual;
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            }
        }
        private void AbrirVarios_Click(object sender, EventArgs e)
        {
            //ManipuladorImagem imagemA = new ManipuladorImagem();
            if (imagens.Count() > 0)
            {
                imagens.Clear();
            }
            oDlg.Multiselect = true;
            oDlg.Title = "Abrir Várias Imagens";
            if (DialogResult.OK == oDlg.ShowDialog())
            {
                //imagemA.BitmapAtual = (Bitmap)Bitmap.FromFile(oDlg.FileNames[0]);
                //int z = 0;
                //List < Visualizador > = new List<Visualizador>(numImagens);
                foreach (String file in oDlg.FileNames)
                {
                    // Create a PictureBox.
                    try
                    {
                        Imagem imagemA = new Imagem();
                        //Visualizador janela = new Visualizador();
                        imagemA.BitmapAtual = (Bitmap)Bitmap.FromFile(file);
                        //imagemA.BitmapPreProcess = imagemA.BitmapAtual;
                        imagemA.BitmapCaminho = file;

                        PictureBox pb = new PictureBox
                        {

                            //Image loadedImage = Image.FromFile(file);
                            Height = imagemA.BitmapAtual.Height,
                            Width = imagemA.BitmapAtual.Width,
                            Image = imagemA.BitmapAtual
                        };
                        imagens.Add(imagemA);
                        Visualizador visualizador = new Visualizador();
                        //imagens.Add(imagemA);
                        visualizador.Controls.Add(pb);
                        visualizador.Show();
                        //imagens.Add(imagemA);
                        //this.Controls.Add(pb);
                        //pictureBox1.Image= (Image)imagens[z].BitmapAtual;
                        //z++;

                        //flowLayoutPanel1.Controls.Add(pb);
                    }
                    catch (SecurityException ex)
                    {
                        // The user lacks appropriate permissions to read files, discover paths, etc.
                        MessageBox.Show("Security error. Please contact your administrator for details.\n\n" +
                            "Error message: " + ex.Message + "\n\n" +
                            "Details (send to Support):\n\n" + ex.StackTrace
                        );
                    }
                    catch (Exception ex)
                    {
                        // Could not load the image - probably related to Windows file system permissions.
                        MessageBox.Show("Cannot display the image: " + file.Substring(file.LastIndexOf('\\'))
                            + ". You may not have permission to read the file, or " +
                            "it may be corrupt.\n\nReported error: " + ex.Message);
                    }
                }

                if (imagens[0] != null)
                {
                    editarMenu.Enabled = true;
                }
            }

        }
        private void Sair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Adicao_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].MathOp(MathOperationType.adicaoLimiar, GetImagemB());
        }

        private void AdicaoMedia_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].MathOp(MathOperationType.adicaoMedia, GetImagemB());
        }

        private void SubtracaoLimiar_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].MathOp(MathOperationType.subtracaoLimiar, GetImagemB());
        }

        private void SubtracaoMedia_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].MathOp(MathOperationType.subtracaoMedia, GetImagemB());
        }

        private void Multiplicacao_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].MathOp(MathOperationType.multiplicacao, GetImagemB());
        }

        private void Divisao_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].MathOp(MathOperationType.divisao, GetImagemB());
        }

        private void LogicNot_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].LogicOp(LogicOperationType.not, null);
        }

        private void LogicOr_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].LogicOp(LogicOperationType.or, GetImagemB());
        }

        private void LogicAnd_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].LogicOp(LogicOperationType.and, GetImagemB());
        }

        private void LogicXor_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].LogicOp(LogicOperationType.xor, GetImagemB());
        }

        private void LogicSub_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].LogicOp(LogicOperationType.sub, GetImagemB());
        }

        public Imagem GetImagemB()
        {
            Imagem imagemB = new Imagem();

            if (DialogResult.OK == oDlg.ShowDialog())
            {
                imagemB.BitmapAtual = (Bitmap)Bitmap.FromFile(oDlg.FileName);
                imagemB.BitmapCaminho = oDlg.FileName;
                if (imagemB.BitmapAtual.Width > imagens[0].BitmapAtual.Width)
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
            pictureBox1.Image = imagens[0].FiltroMedia();
        }

        private void FiltroMediana_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].FiltroMediana();
        }

        private void Desfazer_Click(object sender, EventArgs e)
        {
            imagens[0].BitmapAtual = imagens[0].BitmapPreProcess;
            pictureBox1.Image = imagens[0].BitmapAtual;
        }

        private void Histograma_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].CorrecaoHistograma();
        }

        private void FecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(1, 1);
            imagens.Clear();
            editarMenu.Enabled = false;
            salvarArquivo.Enabled = false;
        }

        private void MediaImagens_Click(object sender, EventArgs e)
        {
            //_ = new ManipuladorImagem();
            Imagem imagemA = imagens[0];
            var numImg = imagens.Count();
            int MaxW = imagemA.BitmapAtual.Width;
            int MaxH = imagemA.BitmapAtual.Height;
            int x, y, z, r = 0, g = 0, b = 0;
            foreach (Imagem imagem in imagens)
                imagem.ToInt();

            for (x = 0; x < MaxW; x++)
            {
                for (y = 0; y < MaxH; y++)
                {
                    for (z = 0; z < numImg; z++)
                    {
                        r += imagens[z].MatrizInt.Matriz[x, y, 0];
                        g += imagens[z].MatrizInt.Matriz[x, y, 1];
                        b += imagens[z].MatrizInt.Matriz[x, y, 2];
                    }

                    imagemA.MatrizInt.Matriz[x, y, 0] = r / numImg;
                    imagemA.MatrizInt.Matriz[x, y, 1] = g / numImg;
                    imagemA.MatrizInt.Matriz[x, y, 2] = b / numImg;
                    r = 0; g = 0; b = 0;
                }
            }
            imagemA.ToImage();
            pictureBox1.Image = (Image)imagemA.BitmapAtual;

        }

        private void OpQuantizacao_Click(object sender, EventArgs e)
        {
            Quantizacao quant = new Quantizacao();

            if (quant.ShowDialog() == DialogResult.OK)
            {
                imagens[0].ToQuant(quant.niveis);
                imagens[0].ToImage();
            }
            //imagens[0].ToImage();
            pictureBox1.Image = imagens[0].BitmapAtual;

        }

        private void TonsDeCinzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imagens[0].ToGray();
            pictureBox1.Image = imagens[0].BitmapAtual;
        }

        private void SobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].BordasSobel();
        }

        private void InverterCores_Click(object sender, EventArgs e)
        {
            imagens[0].InverterCores();
            pictureBox1.Image = imagens[0].BitmapAtual;
        }

        private void PrewittToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].BordasPrewitt();
        }

        private void RobertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].BordasRoberts();
        }

        private void BordasIsotropico_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].BordasIsotropico();
        }

        private void LaplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[0].BordasLaplace();
        }

        private void StLinear_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();

            if (stretching.ShowDialog() == DialogResult.OK)
            {
                //imagens[0].ToGray();
                imagens[0].ToInt();

                int x, y, h = imagens[0].MatrizInt.Height, w = imagens[0].MatrizInt.Width, r = 0, g = 0, b = 0;

                for (x = 0; x < w; x++)
                {
                    for (y = 0; y < h; y++)
                    {
                        r = (int)(imagens[0].MatrizInt.Matriz[x, y, 0] * stretching.A + stretching.B);
                        g = (int)(imagens[0].MatrizInt.Matriz[x, y, 1] * stretching.A + stretching.B);
                        b = (int)(imagens[0].MatrizInt.Matriz[x, y, 2] * stretching.A + stretching.B);
                        imagens[0].MatrizInt.Matriz[x, y, 0] = (r > 255) ? 255 : (r < 0) ? 0 : r;
                        imagens[0].MatrizInt.Matriz[x, y, 1] = (g > 255) ? 255 : (g < 0) ? 0 : g;
                        imagens[0].MatrizInt.Matriz[x, y, 2] = (b > 255) ? 255 : (g < 0) ? 0 : b;
                    }
                }
            }
            imagens[0].ToImage();
            pictureBox1.Image = imagens[0].BitmapAtual;
        }

        private void StQuadrado_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();

            if (stretching.ShowDialog() == DialogResult.OK)
            {
                //imagens[0].ToGray();
                if(imagens[0].imageType!=ImageType.integer) imagens[0].ToInt();

                int x, y, h = imagens[0].MatrizInt.Height, w = imagens[0].MatrizInt.Width, r = 0, g = 0, b = 0;

                for (x = 0; x < w; x++)
                {
                    for (y = 0; y < h; y++)
                    {
                        r = (int)((double)stretching.A * Math.Pow(imagens[0].MatrizInt.Matriz[x, y, 0], 2));
                        g = (int)((double)stretching.A * Math.Pow(imagens[0].MatrizInt.Matriz[x, y, 1], 2));
                        b = (int)((double)stretching.A * Math.Pow(imagens[0].MatrizInt.Matriz[x, y, 2], 2));
                        imagens[0].MatrizInt.Matriz[x, y, 0] = (r > 255) ? 255 : (r < 0) ? 0 : r;
                        imagens[0].MatrizInt.Matriz[x, y, 1] = (g > 255) ? 255 : (g < 0) ? 0 : g;
                        imagens[0].MatrizInt.Matriz[x, y, 2] = (b > 255) ? 255 : (g < 0) ? 0 : b;
                    }
                }
            }
            imagens[0].ToImage();
            pictureBox1.Image = imagens[0].BitmapAtual;
        }

        private void StRaizQuadrada_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();

            if (stretching.ShowDialog() == DialogResult.OK)
            {
                //imagens[0].ToGray();
                imagens[0].ToInt();

                int x, y, h = imagens[0].MatrizInt.Height, w = imagens[0].MatrizInt.Width, r = 0, g = 0, b = 0;

                for (x = 0; x < w; x++)
                {
                    for (y = 0; y < h; y++)
                    {
                        r = (int)((double)stretching.A * Math.Sqrt(imagens[0].MatrizInt.Matriz[x, y, 0]));
                        g = (int)((double)stretching.A * Math.Sqrt(imagens[0].MatrizInt.Matriz[x, y, 1]));
                        b = (int)((double)stretching.A * Math.Sqrt(imagens[0].MatrizInt.Matriz[x, y, 2]));
                        imagens[0].MatrizInt.Matriz[x, y, 0] = (r > 255) ? 255 : (r < 0) ? 0 : r;
                        imagens[0].MatrizInt.Matriz[x, y, 1] = (g > 255) ? 255 : (g < 0) ? 0 : g;
                        imagens[0].MatrizInt.Matriz[x, y, 2] = (b > 255) ? 255 : (g < 0) ? 0 : b;
                    }
                }
            }
            imagens[0].ToImage();
            pictureBox1.Image = imagens[0].BitmapAtual;
        }

        private void StLogaritmico_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();

            if (stretching.ShowDialog() == DialogResult.OK)
            {
                //imagens[0].ToGray();
                imagens[0].ToInt();

                int x, y, h = imagens[0].MatrizInt.Height, w = imagens[0].MatrizInt.Width, r = 0, g = 0, b = 0;

                for (x = 0; x < w; x++)
                {
                    for (y = 0; y < h; y++)
                    {
                        r = (int)(stretching.A * Math.Log10(imagens[0].MatrizInt.Matriz[x, y, 0] + 1));
                        g = (int)(stretching.A * Math.Log10(imagens[0].MatrizInt.Matriz[x, y, 1] + 1));
                        b = (int)(stretching.A * Math.Log10(imagens[0].MatrizInt.Matriz[x, y, 2] + 1));
                        imagens[0].MatrizInt.Matriz[x, y, 0] = (r > 255) ? 255 : (r < 0) ? 0 : r;
                        imagens[0].MatrizInt.Matriz[x, y, 1] = (g > 255) ? 255 : (g < 0) ? 0 : g;
                        imagens[0].MatrizInt.Matriz[x, y, 2] = (b > 255) ? 255 : (g < 0) ? 0 : b;
                    }
                }
            }
            imagens[0].ToImage();
            pictureBox1.Image = imagens[0].BitmapAtual;
        }

        private void StNegativo_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();

            if (stretching.ShowDialog() == DialogResult.OK)
            {
                //imagens[0].ToGray();
                imagens[0].ToInt();

                int x, y, h = imagens[0].MatrizInt.Height, w = imagens[0].MatrizInt.Width, r = 0, g = 0, b = 0;

                for (x = 0; x < w; x++)
                {
                    for (y = 0; y < h; y++)
                    {
                        r = (int)(-(imagens[0].MatrizInt.Matriz[x, y, 0] * stretching.A + stretching.B));
                        g = (int)(-(imagens[0].MatrizInt.Matriz[x, y, 1] * stretching.A + stretching.B));
                        b = (int)(-(imagens[0].MatrizInt.Matriz[x, y, 2] * stretching.A + stretching.B));
                        imagens[0].MatrizInt.Matriz[x, y, 0] = (r > 255) ? 255 : (r < 0) ? 0 : r;
                        imagens[0].MatrizInt.Matriz[x, y, 1] = (g > 255) ? 255 : (g < 0) ? 0 : g;
                        imagens[0].MatrizInt.Matriz[x, y, 2] = (b > 255) ? 255 : (g < 0) ? 0 : b;
                    }
                }
            }
            imagens[0].ToImage();
            pictureBox1.Image = imagens[0].BitmapAtual;
        }
    }
}
