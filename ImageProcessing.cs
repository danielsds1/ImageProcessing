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
        public int MinHeight = 480;
        public int NumImg = 0;
        public double zoomFactor = 1.0;
        public int count = 0;
        public List<Imagem> imagens = new List<Imagem>();

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
                var imagemA = imagens[count - 1];
                imagens[count - 1].SaveBitmap(sDlg.FileName);
                imagens.Clear();
                imagens.Add(imagemA);
                count = 1;


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
                imagemA.ImagemBMP = (Bitmap)Bitmap.FromFile(oDlg.FileName);

                if (imagens.Count() > 0)
                {
                    imagens.Clear();
                }

                imagemA.ImagemBMP = (Bitmap)Bitmap.FromFile(oDlg.FileName);
                imagemA.BitmapCaminho = oDlg.FileName;
                Visualizar(imagemA);

                if (imagens[count - 1] != null)
                {
                    imagens[count - 1].ToInt();
                    salvarArquivo.Enabled = true;
                    editarMenu.Enabled = true;
                }
            }
        }
        private void AbrirVarios_Click(object sender, EventArgs e)
        {
            if (imagens.Count() > 0)
            {
                imagens.Clear();
            }
            oDlg.Multiselect = true;
            oDlg.Title = "Abrir Várias Imagens";
            if (DialogResult.OK == oDlg.ShowDialog())
            {
                foreach (String file in oDlg.FileNames)
                {
                    try
                    {
                        Imagem imagemA = new Imagem();
                        imagemA.ImagemBMP = (Bitmap)Bitmap.FromFile(file);
                        imagemA.BitmapCaminho = file;
                        Visualizar(imagemA);
                        imagemA.ToInt();
                        salvarArquivo.Enabled = true;
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

                if (imagens[count - 1] != null)
                {
                    editarMenu.Enabled = true;
                }
                else
                {
                    editarMenu.Enabled = false;
                }
            }

        }
        private void Visualizar(Imagem imagemA)
        {
            PictureBox pb = new PictureBox
            {
                Height = imagemA.ImagemBMP.Height,
                Width = imagemA.ImagemBMP.Width,
                Image = imagemA.ImagemBMP,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            //pb.
            imagens.Add(imagemA);
            count++;
            Visualizador visualizador = new Visualizador
            {
                //visualizador.
                Text = imagemA.NomeArquivo(),
            };
            visualizador.Controls.Add(pb);
            //visualizador.Controls.

            visualizador.Show();
        }
        private void Visualizar(Imagem imagemA, string text)
        {
            PictureBox pb = new PictureBox
            {
                Height = imagemA.ImagemBMP.Height,
                Width = imagemA.ImagemBMP.Width,
                Image = imagemA.ImagemBMP,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            //pb.
            imagens.Add(imagemA);
            count++;
            Visualizador visualizador = new Visualizador
            {
                //visualizador.
                Text = text,
            };
            visualizador.Controls.Add(pb);
            //visualizador.Controls.

            visualizador.Show();
        }
        private void Sair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Adicao_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].MathOp(MathOperationType.adicaoLimiar, GetImagemB());
        }

        private void AdicaoMedia_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].MathOp(MathOperationType.adicaoMedia, GetImagemB());
        }

        private void SubtracaoLimiar_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].MathOp(MathOperationType.subtracaoLimiar, GetImagemB());
        }

        private void SubtracaoMedia_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].MathOp(MathOperationType.subtracaoMedia, GetImagemB());
        }

        private void Multiplicacao_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].MathOp(MathOperationType.multiplicacao, GetImagemB());
        }

        private void Divisao_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].MathOp(MathOperationType.divisao, GetImagemB());
        }

        private void LogicNot_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].LogicOp(LogicOperationType.not, null);
        }

        private void LogicOr_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].LogicOp(LogicOperationType.or, GetImagemB());
        }

        private void LogicAnd_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].LogicOp(LogicOperationType.and, GetImagemB());
        }

        private void LogicXor_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].LogicOp(LogicOperationType.xor, GetImagemB());
        }

        private void LogicSub_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].LogicOp(LogicOperationType.sub, GetImagemB());
        }

        public Imagem GetImagemB()
        {
            Imagem imagemB = new Imagem();

            if (DialogResult.OK == oDlg.ShowDialog())
            {
                imagemB.ImagemBMP = (Bitmap)Bitmap.FromFile(oDlg.FileName);
                imagemB.BitmapCaminho = oDlg.FileName;
                if (imagemB.ImagemBMP.Width > imagens[count - 1].ImagemBMP.Width)
                {
                    MessageBox.Show("A imagem de destino é maior do que a de origem!", "Erro!");
                    return null;
                }
                Visualizar(imagemB);
                return imagemB;
            }
            return null;

        }

        private void FiltroMedia_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].FiltroMedia();
        }

        private void FiltroMediana_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].FiltroMediana();
        }

        private void Desfazer_Click(object sender, EventArgs e)
        {

            imagens.RemoveAt(count - 1);
            count--;
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void Histograma_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].CorrecaoHistograma();
        }

        private void FecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(1, 1);
            imagens.Clear();
            count = 0;
            editarMenu.Enabled = false;
            salvarArquivo.Enabled = false;
        }

        private void MediaImagens_Click(object sender, EventArgs e)
        {
            Imagem imagemA = imagens[0];
            var numImg = imagens.Count();
            int MaxW = imagemA.MatrizCor.Width;
            int MaxH = imagemA.MatrizCor.Height;
            int x, y, z;
            int canal;
            int[] rgb = { 0, 0, 0 };

            for (x = 0; x < MaxW; x++)
            {
                for (y = 0; y < MaxH; y++)
                {
                    for (canal = 0; canal < 3; canal++)
                    {
                        for (z = 0; z < numImg; z++)
                        {
                            rgb[canal] += imagens[z].MatrizCor.Matriz[x, y, canal];
                        }

                        imagemA.MatrizCor.Matriz[x, y, canal] = rgb[canal] / numImg;
                        rgb[canal] = 0;
                    }
                    

                    //imagemA.MatrizCor.Matriz[x, y, 0] = r / numImg;
                    //imagemA.MatrizCor.Matriz[x, y, 1] = g / numImg;
                    //imagemA.MatrizCor.Matriz[x, y, 2] = b / numImg;
                    //r = 0; g = 0; b = 0;
                }
            }
            imagemA.ToImage();
            pictureBox1.Image = (Image)imagemA.ImagemBMP;

        }

        private void OpQuantizacao_Click(object sender, EventArgs e)
        {
            Quantizacao quant = new Quantizacao();

            if (quant.ShowDialog() == DialogResult.OK)
            {
                imagens[count - 1].ToQuant(quant.niveis);
                imagens[count - 1].ToImage();
            }
            //imagens[count-1].ToImage();
            pictureBox1.Image = imagens[count - 1].ImagemBMP;

        }

        private void TonsDeCinzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            imagens[count - 1].ToGray();
            imagens[count - 1].ToImage();
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void SobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].BordasSobel();
        }

        private void InverterCores_Click(object sender, EventArgs e)
        {
            imagens[count - 1].InverterCores();
            imagens[count - 1].ToImage();
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }
        private void PassaAltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].FiltroPassaAlta();
        }
        private void PrewittToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].BordasPrewitt();
        }

        private void RobertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].BordasRoberts();
        }

        private void BordasIsotropico_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].BordasIsotropico();
        }

        private void LaplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].BordasLaplace();
        }

        private void StLinear_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();

            if (stretching.ShowDialog() == DialogResult.OK)
            {
                //imagens[count-1].ToGray();
                imagens[count - 1].ToInt();

                int x, y, h = imagens[count - 1].MatrizCor.Height, w = imagens[count - 1].MatrizCor.Width, r = 0, g = 0, b = 0;

                for (x = 0; x < w; x++)
                {
                    for (y = 0; y < h; y++)
                    {
                        r = (int)(imagens[count - 1].MatrizCor.Matriz[x, y, 0] * stretching.A + stretching.B);
                        g = (int)(imagens[count - 1].MatrizCor.Matriz[x, y, 1] * stretching.A + stretching.B);
                        b = (int)(imagens[count - 1].MatrizCor.Matriz[x, y, 2] * stretching.A + stretching.B);
                        imagens[count - 1].MatrizCor.Matriz[x, y, 0] = (r > 255) ? 255 : (r < 0) ? 0 : r;
                        imagens[count - 1].MatrizCor.Matriz[x, y, 1] = (g > 255) ? 255 : (g < 0) ? 0 : g;
                        imagens[count - 1].MatrizCor.Matriz[x, y, 2] = (b > 255) ? 255 : (g < 0) ? 0 : b;
                    }
                }
            }
            imagens[count - 1].ToImage();
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void StQuadrado_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();

            if (stretching.ShowDialog() == DialogResult.OK)
            {
                //imagens[count-1].ToGray();
                if (imagens[count - 1].imageType != ImageType.integer) imagens[count - 1].ToInt();

                int x, y, h = imagens[count - 1].MatrizCor.Height, w = imagens[count - 1].MatrizCor.Width, r = 0, g = 0, b = 0;

                for (x = 0; x < w; x++)
                {
                    for (y = 0; y < h; y++)
                    {
                        r = (int)((double)stretching.A * Math.Pow(imagens[count - 1].MatrizCor.Matriz[x, y, 0], 2));
                        g = (int)((double)stretching.A * Math.Pow(imagens[count - 1].MatrizCor.Matriz[x, y, 1], 2));
                        b = (int)((double)stretching.A * Math.Pow(imagens[count - 1].MatrizCor.Matriz[x, y, 2], 2));
                        imagens[count - 1].MatrizCor.Matriz[x, y, 0] = (r > 255) ? 255 : (r < 0) ? 0 : r;
                        imagens[count - 1].MatrizCor.Matriz[x, y, 1] = (g > 255) ? 255 : (g < 0) ? 0 : g;
                        imagens[count - 1].MatrizCor.Matriz[x, y, 2] = (b > 255) ? 255 : (g < 0) ? 0 : b;
                    }
                }
            }
            imagens[count - 1].ToImage();
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void StRaizQuadrada_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();

            if (stretching.ShowDialog() == DialogResult.OK)
            {
                //imagens[count-1].ToGray();
                imagens[count - 1].ToInt();

                int x, y, h = imagens[count - 1].MatrizCor.Height, w = imagens[count - 1].MatrizCor.Width, r = 0, g = 0, b = 0;

                for (x = 0; x < w; x++)
                {
                    for (y = 0; y < h; y++)
                    {
                        r = (int)((double)stretching.A * Math.Sqrt(imagens[count - 1].MatrizCor.Matriz[x, y, 0]));
                        g = (int)((double)stretching.A * Math.Sqrt(imagens[count - 1].MatrizCor.Matriz[x, y, 1]));
                        b = (int)((double)stretching.A * Math.Sqrt(imagens[count - 1].MatrizCor.Matriz[x, y, 2]));
                        imagens[count - 1].MatrizCor.Matriz[x, y, 0] = (r > 255) ? 255 : (r < 0) ? 0 : r;
                        imagens[count - 1].MatrizCor.Matriz[x, y, 1] = (g > 255) ? 255 : (g < 0) ? 0 : g;
                        imagens[count - 1].MatrizCor.Matriz[x, y, 2] = (b > 255) ? 255 : (g < 0) ? 0 : b;
                    }
                }
            }
            imagens[count - 1].ToImage();
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void StLogaritmico_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();

            if (stretching.ShowDialog() == DialogResult.OK)
            {
                //imagens[count-1].ToGray();
                imagens[count - 1].ToInt();

                int x, y, h = imagens[count - 1].MatrizCor.Height, w = imagens[count - 1].MatrizCor.Width, r = 0, g = 0, b = 0;

                for (x = 0; x < w; x++)
                {
                    for (y = 0; y < h; y++)
                    {
                        r = (int)(stretching.A * Math.Log10(imagens[count - 1].MatrizCor.Matriz[x, y, 0] + 1));
                        g = (int)(stretching.A * Math.Log10(imagens[count - 1].MatrizCor.Matriz[x, y, 1] + 1));
                        b = (int)(stretching.A * Math.Log10(imagens[count - 1].MatrizCor.Matriz[x, y, 2] + 1));
                        imagens[count - 1].MatrizCor.Matriz[x, y, 0] = (r > 255) ? 255 : (r < 0) ? 0 : r;
                        imagens[count - 1].MatrizCor.Matriz[x, y, 1] = (g > 255) ? 255 : (g < 0) ? 0 : g;
                        imagens[count - 1].MatrizCor.Matriz[x, y, 2] = (b > 255) ? 255 : (g < 0) ? 0 : b;
                    }
                }
            }
            imagens[count - 1].ToImage();
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void StNegativo_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();

            if (stretching.ShowDialog() == DialogResult.OK)
            {
                //imagens[count-1].ToGray();
                imagens[count - 1].ToInt();

                int x, y, h = imagens[count - 1].MatrizCor.Height, w = imagens[count - 1].MatrizCor.Width, r = 0, g = 0, b = 0;

                for (x = 0; x < w; x++)
                {
                    for (y = 0; y < h; y++)
                    {
                        r = (int)(-(imagens[count - 1].MatrizCor.Matriz[x, y, 0] * stretching.A + stretching.B));
                        g = (int)(-(imagens[count - 1].MatrizCor.Matriz[x, y, 1] * stretching.A + stretching.B));
                        b = (int)(-(imagens[count - 1].MatrizCor.Matriz[x, y, 2] * stretching.A + stretching.B));
                        imagens[count - 1].MatrizCor.Matriz[x, y, 0] = (r > 255) ? 255 : (r < 0) ? 0 : r;
                        imagens[count - 1].MatrizCor.Matriz[x, y, 1] = (g > 255) ? 255 : (g < 0) ? 0 : g;
                        imagens[count - 1].MatrizCor.Matriz[x, y, 2] = (b > 255) ? 255 : (g < 0) ? 0 : b;
                    }
                }
            }
            imagens[count - 1].ToImage();
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void Limiar_Click(object sender, EventArgs e)
        {
            Dithering dithering = new Dithering();

            if (dithering.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = imagens[count - 1].ToLimiar(dithering.limiar);
            }
            //imagens[count-1].ToImage();
            //pictureBox1.Image = imagens[count-1].BitmapAtual;
        }

        private void LimiarComRuido_Click(object sender, EventArgs e)
        {
            Dithering dithering = new Dithering();
            Imagem B = new Imagem
            {
                MatrizCor = imagens[count - 1].MatrizCor,
                ImagemBMP = (Bitmap)imagens[count - 1].ImagemBMP.Clone()
            };

            if (dithering.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = B.ToLimiarAleatorio(dithering.limiar, dithering.rinf, dithering.rsup);
                Visualizar(B);
            }
        }

        private void DtPeriodicoDispersão_Click(object sender, EventArgs e)
        {
            Dithering dithering = new Dithering();
            Imagem B = new Imagem
            {
                MatrizCor = imagens[count - 1].MatrizCor,
                ImagemBMP = (Bitmap)imagens[count - 1].ImagemBMP.Clone()
            };

            if (dithering.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = B.ToPeriodicoDispersao(dithering.dispersao);
                Visualizar(B, "Dithering " + dithering.dispersao + "X" + dithering.dispersao + " " + imagens[count - 1].NomeArquivo() + imagens[count - 1].ExtensaoArquivo());
            }
        }

        private void DtAperiodicoDispersao_Click(object sender, EventArgs e)
        {
            Dithering dithering = new Dithering();
            Imagem B = new Imagem
            {
                MatrizCor = imagens[count - 1].MatrizCor,
                ImagemBMP = (Bitmap)imagens[count - 1].ImagemBMP.Clone()
            };

            if (dithering.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = B.ToAperiodicoDispersao(dithering.dispersao);
                Visualizar(B, "Dithering " + dithering.dispersao + "X" + dithering.dispersao + " " + imagens[count - 1].NomeArquivo() + imagens[count - 1].ExtensaoArquivo());
            }
        }

        private void Histograma_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].CorrecaoHistograma();
        }
    }
}
