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
            };
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
            if (imagemA != null)
            {
                if (imagemA.imageType != ImageType.color)
                {
                    imagemA.ToImage();
                }
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
        }
        private void Visualizar(Imagem imagemA, string text)
        {
            if (imagemA != null)
            {
                if (imagemA.imageType != ImageType.color)
                {
                    imagemA.ToImage();
                }
                PictureBox pb = new PictureBox
                {
                    Height = imagemA.ImagemBMP.Height,
                    Width = imagemA.ImagemBMP.Width,
                    Image = imagemA.ImagemBMP,
                    SizeMode = PictureBoxSizeMode.AutoSize
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
        }
        private void Sair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Adicao_Click(object sender, EventArgs e)
        {
            Imagem imagemA = imagens[0];
            if (count == 1)
                Visualizar(GetImagemB());
            foreach (Imagem imagem in imagens)
                imagemA.MathOp(MathOperationType.adicao, imagem);
            imagemA.CorrecaoMinMax(Correcao.limiar);
            Visualizar(imagemA, "Soma Limiar");
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void AdicaoMedia_Click(object sender, EventArgs e)
        {
            Imagem imagemA = imagens[0];
            if (count == 1)
                Visualizar(GetImagemB());
            foreach (Imagem imagem in imagens)
                imagemA.MathOp(MathOperationType.adicao, imagem);
            imagemA.CorrecaoMinMax(Correcao.proporcao);
            Visualizar(imagemA, "Soma Corrigida");
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void SubtracaoLimiar_Click(object sender, EventArgs e)
        {
            Imagem imagemA = imagens[0];
            if (count == 1)
                Visualizar(GetImagemB());
            foreach (Imagem imagem in imagens)
                imagemA.MathOp(MathOperationType.subtracao, imagem);
            imagemA.CorrecaoMinMax(Correcao.limiar);
            Visualizar(imagemA, "Subtração Limiar");
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void SubtracaoMedia_Click(object sender, EventArgs e)
        {
            Imagem imagemA = imagens[0];
            if (count == 1)
                Visualizar(GetImagemB());
            foreach (Imagem imagem in imagens)
                imagemA.MathOp(MathOperationType.subtracao, imagem);
            imagemA.CorrecaoMinMax(Correcao.proporcao);
            Visualizar(imagemA, "Subtração Corrigida");
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void Multiplicacao_Click(object sender, EventArgs e)
        {
            Imagem imagemA = imagens[0];
            if (count == 1)
                Visualizar(GetImagemB());
            foreach (Imagem imagem in imagens)
                imagemA.MathOp(MathOperationType.multiplicacao, imagem);
            imagemA.CorrecaoMinMax(Correcao.proporcao);
            Visualizar(imagemA, "Multiplicação");
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void Divisao_Click(object sender, EventArgs e)
        {
            Imagem imagemA = imagens[0];
            if (count == 1)
                Visualizar(GetImagemB());
            foreach (Imagem imagem in imagens)
                imagemA.MathOp(MathOperationType.divisao, imagem);
            imagemA.CorrecaoMinMax(Correcao.proporcao);
            Visualizar(imagemA, "Divisão");
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void LogicNot_Click(object sender, EventArgs e)
        {
            var count2 = count;
            for (int k = 0; k < count2; k++)
            {
                var imagem = imagens[k];
                imagem.LogicOp(LogicOperationType.not, null);
                Visualizar(imagem, "NOT " + imagem.NomeArquivo());
            }

            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void LogicOr_Click(object sender, EventArgs e)
        {
            Imagem imagemA = imagens[0];
            if (count == 1)
                Visualizar(GetImagemB());
            for (int k = 1; k < count; k++)
                imagemA.LogicOp(LogicOperationType.or, imagens[k]);
            Visualizar(imagemA, "OR");
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
            //pictureBox1.Image = imagens[count - 1].LogicOp(LogicOperationType.or, GetImagemB());
        }

        private void LogicAnd_Click(object sender, EventArgs e)
        {
            Imagem imagemA = imagens[0];
            if (count == 1)
                Visualizar(GetImagemB());
            for (int k = 1; k < count; k++)
                imagemA.LogicOp(LogicOperationType.and, imagens[k]);
            Visualizar(imagemA, "AND");
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void LogicXor_Click(object sender, EventArgs e)
        {
            Imagem imagemA = imagens[0];
            if (count == 1)
                Visualizar(GetImagemB());
            for (int k = 1; k < count; k++)
                imagemA.LogicOp(LogicOperationType.xor, imagens[k]);
            Visualizar(imagemA, "XOR");
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void LogicSub_Click(object sender, EventArgs e)
        {
            Imagem imagemA = imagens[0];
            if (count == 1)
                Visualizar(GetImagemB());
            for (int k = 1; k < count; k++)
                imagemA.LogicOp(LogicOperationType.sub, imagens[k]);
            Visualizar(imagemA, "SUB");
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
            //pictureBox1.Image = imagens[count - 1].LogicOp(LogicOperationType.sub, GetImagemB());
        }

        public Imagem GetImagemB()
        {
            Imagem imagemB = new Imagem();

            if (DialogResult.OK == oDlg.ShowDialog())
            {
                imagemB.ImagemBMP = (Bitmap)Bitmap.FromFile(oDlg.FileName);
                imagemB.BitmapCaminho = oDlg.FileName;
                int width = 0, height = 0;

                switch (imagens[0].imageType)
                {
                    case ImageType.binary:
                        width = imagens[0].MatrizBool.Width;
                        height = imagens[0].MatrizBool.Height;
                        break;
                    case ImageType.color:
                        width = imagens[0].ImagemBMP.Width;
                        height = imagens[0].ImagemBMP.Height;
                        break;
                    case ImageType.gray:
                        width = imagens[0].MatrizGray.Width;
                        height = imagens[0].MatrizGray.Height;
                        break;
                    case ImageType.integer:
                        width = imagens[0].MatrizCor.Width;
                        height = imagens[0].MatrizCor.Height;
                        break;
                }

                if (imagemB.ImagemBMP.Width > width || imagemB.ImagemBMP.Height > height)
                {
                    MessageBox.Show("A imagem de destino é maior do que a de origem!", "Erro!");
                    return null;
                }
                return imagemB;
            }
            return null;

        }
        private void Desfazer_Click(object sender, EventArgs e)
        {
            if (count > 1)
            {

                imagens.RemoveAt(count - 1);
                count--;
                imagens[count - 1].ToImage();
                pictureBox1.Image = imagens[count - 1].ImagemBMP;
                imagens[count - 1].ToInt();
            }
            else
            {
                pictureBox1.Image = new Bitmap(1, 1);
                imagens.Clear();
                count = 0;
                editarMenu.Enabled = false;
                salvarArquivo.Enabled = false;
            }
        }
        private void FiltroMedia_Click(object sender, EventArgs e)
        {
            var raioFiltro = new RaioFiltro();
            if (raioFiltro.ShowDialog() == DialogResult.OK)
            {
                var imagem = imagens[count - 1];
                imagem.FiltroMedia(raioFiltro.raio);
                Visualizar(imagem);
                pictureBox1.Image = imagens[count - 1].ImagemBMP;
            }
        }

        private void FiltroMediana_Click(object sender, EventArgs e)
        {
            var raioFiltro = new RaioFiltro();
            if (raioFiltro.ShowDialog() == DialogResult.OK)
            {
                var imagem = imagens[count - 1];
                imagem.FiltroMediana(raioFiltro.raio);
                Visualizar(imagem);
                pictureBox1.Image = imagens[count - 1].ImagemBMP;
            }
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
            bool cond = true;
            int k = 1;
            while (k < count && cond)
                cond = imagemA.MatrizCor.Width >= imagens[k].MatrizCor.Width && imagemA.MatrizCor.Height >= imagens[k++].MatrizCor.Height;
            if (cond)
            {
                for (k = 1; k < count; k++)
                    imagemA.MathOp(MathOperationType.adicao, imagens[k]);
                Imagem divisor = new Imagem();
                divisor.CreatePlainImage(imagemA.MatrizCor.Width, imagemA.MatrizCor.Height, count);
                imagemA.MathOp(MathOperationType.divisao, divisor);
                Visualizar(imagemA, "Média de " + (count) + " imagens");
                pictureBox1.Image = imagens[count - 1].ImagemBMP;
            }
            else
            {
                MessageBox.Show("Imagens possuem resoluções diferentes!");
            }
        }

        private void OpQuantizacao_Click(object sender, EventArgs e)
        {
            Quantizacao quant = new Quantizacao();
            if (quant.ShowDialog() == DialogResult.OK)
            {
                var imagem = imagens[count - 1];
                imagem.ToQuant(quant.niveis);
                Visualizar(imagem);
            }
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void TonsDeCinzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var imagem = imagens[count - 1];
            imagem.ToGray();
            Visualizar(imagem);
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void SobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Imagem imagem = imagens[count - 1];
            imagem.Bordas(EdgeDetection.Sobel);
            Visualizar(imagem, "Bordas Sobel " + imagem.NomeArquivo());
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void InverterCores_Click(object sender, EventArgs e)
        {
            var imagem = imagens[count - 1];
            imagem.InverterCores();
            Visualizar(imagem, "Inverso " + imagem.NomeArquivo());
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }
        private void PassaAltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var imagem = imagens[count - 1];
            imagem.FiltroPassaAlta(); ;
            Visualizar(imagem, "Filtro Passa-Alta " + imagem.NomeArquivo());
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }
        private void PrewittToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var imagem = imagens[count - 1];
            imagem.Bordas(EdgeDetection.Prewitt);
            Visualizar(imagem, "Bordas Prewitt " + imagem.NomeArquivo());
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void RobertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var imagem = imagens[count - 1];
            imagem.Bordas(EdgeDetection.Roberts);
            Visualizar(imagem, "Bordas Roberts " + imagem.NomeArquivo());
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void BordasIsotropico_Click(object sender, EventArgs e)
        {
            var imagem = imagens[count - 1];
            imagem.Bordas(EdgeDetection.Isotropico);
            Visualizar(imagem, "Bordas Isotrópico " + imagem.NomeArquivo());
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void LaplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var imagem = imagens[count - 1];
            imagem.Bordas(EdgeDetection.Laplace);
            Visualizar(imagem, "Bordas Laplace " + imagem.NomeArquivo());
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void StLinear_Click(object sender, EventArgs e)
        {

            Stretching stretching = new Stretching();

            if (stretching.ShowDialog() == DialogResult.OK)
            {
                var imagem = imagens[count - 1];
                imagem.Stretching(StretchingType.linear, stretching.A, stretching.B);
                if (stretching.correcaoProporcional)
                    imagem.CorrecaoMinMax(Correcao.proporcao);
                else
                    imagem.CorrecaoMinMax(Correcao.limiar);
                Visualizar(imagem, "Stretching Linear [" + stretching.A + "X + " + stretching.B + "] " + imagem.NomeArquivo());
                pictureBox1.Image = imagens[count - 1].ImagemBMP;
            }
        }

        private void StQuadrado_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();

            if (stretching.ShowDialog() == DialogResult.OK)
            {
                var imagem = imagens[count - 1];
                imagem.Stretching(StretchingType.quad, stretching.A, stretching.B);
                if (stretching.correcaoProporcional)
                    imagem.CorrecaoMinMax(Correcao.proporcao);
                else
                    imagem.CorrecaoMinMax(Correcao.limiar);
                Visualizar(imagem, "Stretching Quadrado " + imagem.NomeArquivo());
                pictureBox1.Image = imagens[count - 1].ImagemBMP;
            }
        }

        private void StRaizQuadrada_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();
            if (stretching.ShowDialog() == DialogResult.OK)
            {
                var imagem = imagens[count - 1];
                imagem.Stretching(StretchingType.linear, stretching.A, stretching.B);
                if (stretching.correcaoProporcional)
                    imagem.CorrecaoMinMax(Correcao.proporcao);
                else
                    imagem.CorrecaoMinMax(Correcao.limiar);
                Visualizar(imagem, "Stretching Raiz Quadrada " + imagem.NomeArquivo());
                pictureBox1.Image = imagens[count - 1].ImagemBMP;
            }
        }

        private void StLogaritmico_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();

            if (stretching.ShowDialog() == DialogResult.OK)
            {
                var imagem = imagens[count - 1];
                imagem.Stretching(StretchingType.linear, stretching.A, stretching.B);
                if (stretching.correcaoProporcional)
                    imagem.CorrecaoMinMax(Correcao.proporcao);
                else
                    imagem.CorrecaoMinMax(Correcao.limiar);
                Visualizar(imagem, "Stretching Logarítmico [" + stretching.A + "*log(X)] " + imagem.NomeArquivo());
                pictureBox1.Image = imagens[count - 1].ImagemBMP;
            }
        }

        private void StNegativo_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();

            if (stretching.ShowDialog() == DialogResult.OK)
            {
                var imagem = imagens[count - 1];
                imagem.Stretching(StretchingType.linear, stretching.A, stretching.B);
                if (stretching.correcaoProporcional)
                    imagem.CorrecaoMinMax(Correcao.proporcao);
                else
                    imagem.CorrecaoMinMax(Correcao.limiar);
                Visualizar(imagem, "Stretching Linear [-" + stretching.A + "X - " + stretching.B + "] " + imagem.NomeArquivo());
                pictureBox1.Image = imagens[count - 1].ImagemBMP;
            }
            imagens[count - 1].ToImage();
            pictureBox1.Image = imagens[count - 1].ImagemBMP;
        }

        private void Limiar_Click(object sender, EventArgs e)
        {
            Dithering dithering = new Dithering();
            Imagem B = new Imagem
            {
                MatrizCor = imagens[count - 1].MatrizCor,
                imageType = ImageType.integer
            };

            if (dithering.ShowDialog() == DialogResult.OK)
            {
                B.ToLimiar(dithering.Limiar);
                Visualizar(B);
                pictureBox1.Image = imagens[count - 1].ImagemBMP;
            }
        }

        private void LimiarComRuido_Click(object sender, EventArgs e)
        {
            Dithering dithering = new Dithering();
            Imagem B = new Imagem
            {
                MatrizCor = imagens[count - 1].MatrizCor,
                imageType = ImageType.integer
            };
            if (dithering.ShowDialog() == DialogResult.OK)
            {
                B.ToLimiarAleatorio(dithering.Limiar, dithering.Rinf, dithering.Rsup);
                Visualizar(B);
                pictureBox1.Image = imagens[count - 1].ImagemBMP;
            }
        }

        private void DtPeriodicoDispersão_Click(object sender, EventArgs e)
        {
            Dithering dithering = new Dithering();
            Imagem B = new Imagem
            {
                MatrizCor = imagens[count - 1].MatrizCor,
                imageType = ImageType.integer
            };

            if (dithering.ShowDialog() == DialogResult.OK)
            {
                B.ToPeriodicoDispersao(dithering.Dispersao);
                Visualizar(B, "Dithering " + dithering.Dispersao + "X" + dithering.Dispersao + " " + imagens[count - 1].NomeArquivo() + imagens[count - 1].ExtensaoArquivo());
                pictureBox1.Image = imagens[count - 1].ImagemBMP;
            }
        }

        private void DtAperiodicoDispersao_Click(object sender, EventArgs e)
        {
            Dithering dithering = new Dithering();
            Imagem B = new Imagem
            {
                MatrizCor = imagens[count - 1].MatrizCor,
                imageType = ImageType.integer
            };

            if (dithering.ShowDialog() == DialogResult.OK)
            {
                B.ToAperiodicoDispersao(dithering.Vizinhos);
                Visualizar(B, "Dithering " + dithering.Dispersao + "X" + dithering.Dispersao + " " + imagens[count - 1].NomeArquivo() + imagens[count - 1].ExtensaoArquivo());
                pictureBox1.Image = imagens[count - 1].ImagemBMP;
            }
        }

        private void Histograma_Click_1(object sender, EventArgs e)
        {
            pictureBox1.Image = imagens[count - 1].CorrecaoHistograma();
        }

        private void PeriodicoPorAglomeracao_Click(object sender, EventArgs e)
        {
            Dithering dithering = new Dithering();


            if (dithering.ShowDialog() == DialogResult.OK)
            {
                Imagem B = new Imagem
                {
                    MatrizCor = imagens[count - 1].MatrizCor,
                    imageType = ImageType.integer
                };
                B.ToGray();
                B.ToQuant(dithering.Dispersao * dithering.Dispersao + 1);
                B.ToPeriodicoAglomeracao(dithering.Dispersao);
                Visualizar(B, "Dithering Aglomeração" + dithering.Dispersao + "X" + dithering.Dispersao + " " + imagens[count - 1].NomeArquivo() + imagens[count - 1].ExtensaoArquivo());
                pictureBox1.Image = imagens[count - 1].ImagemBMP;
            }
        }
    }
}
