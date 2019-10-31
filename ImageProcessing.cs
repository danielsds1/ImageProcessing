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
            morfologia.Enabled = false;
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
                var imagem = imagens[count - 1];
                imagens[count - 1].SaveBitmap(sDlg.FileName);
                imagens.Clear();
                imagens.Add(imagem);
                count = 1;
            }
        }

        private void AbrirArquivo_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            oDlg.Multiselect = false;
            oDlg.Title = "Abrir Imagem";
            if (DialogResult.OK == oDlg.ShowDialog())
            {
                imagem.ImagemBMP = (Bitmap)Bitmap.FromFile(oDlg.FileName);
                imagem.BitmapCaminho = oDlg.FileName;
                Visualizar(imagem);

                if (imagens[count - 1] != null)
                {
                    imagens[count - 1].ToInt();
                    salvarArquivo.Enabled = true;
                    editarMenu.Enabled = true;
                    morfologia.Enabled = true;
                }
            }
        }
        private void AbrirVarios_Click(object sender, EventArgs e)
        {
            if (imagens.Count() > 0)
                imagens.Clear();
            oDlg.Multiselect = true;
            oDlg.Title = "Abrir Várias Imagens";
            if (DialogResult.OK == oDlg.ShowDialog())
            {
                foreach (String file in oDlg.FileNames)
                {
                    try
                    {
                        Imagem imagem = new Imagem();
                        imagem.ImagemBMP = (Bitmap)Bitmap.FromFile(file);
                        imagem.BitmapCaminho = file;
                        Visualizar(imagem);
                        imagem.ToInt();
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
                    editarMenu.Enabled = true;
                else
                    editarMenu.Enabled = false;
            }
        }
        private void Visualizar(Imagem imagem)
        {
            if (imagem != null)
            {
                if (imagem.MatrizCor.Matriz != null)
                    imagem.ToImage();
                PictureBox pb = new PictureBox
                {
                    Height = imagem.ImagemBMP.Height,
                    Width = imagem.ImagemBMP.Width,
                    Image = imagem.ImagemBMP,
                    MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height),
                    SizeMode = PictureBoxSizeMode.Zoom,
                };
                imagens.Add(imagem);
                count++;
                Visualizador visualizador = new Visualizador
                {
                    Text = imagem.NomeArquivo(),
                    AutoSize = true
                };
                visualizador.Controls.Add(pb);
                visualizador.Show();
                pictureBox1.Image = imagens[count - 1].ImagemBMP;
            }
        }
        private void Visualizar(Imagem imagem, string text)
        {
            if (imagem != null)
            {
                if (imagem.MatrizCor.Matriz != null)
                    imagem.ToImage();
                PictureBox pb = new PictureBox
                {
                    Height = imagem.ImagemBMP.Height,
                    Width = imagem.ImagemBMP.Width,
                    Image = imagem.ImagemBMP,
                    MaximumSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height),
                    SizeMode = PictureBoxSizeMode.Zoom
                };
                imagens.Add(imagem);
                count++;
                Visualizador visualizador = new Visualizador
                {
                    Text = text,
                    AutoSize = true
                };
                visualizador.Controls.Add(pb);
                visualizador.Show();
                pictureBox1.Image = imagens[count - 1].ImagemBMP;
            }
        }
        private void Sair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Adicao_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[0]);
            if (count == 1)
                Visualizar(GetImagemB());
            for (int k = 1; k < count; k++)
                imagem.MathOp(MathOperationType.adicao, imagens[k]);
            imagem.CorrecaoMinMax(Correcao.limiar);
            Visualizar(imagem, "Soma Limiar");
        }

        private void AdicaoMedia_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[0]);
            if (count == 1)
                Visualizar(GetImagemB());
            for (int k = 1; k < count; k++)
                imagem.MathOp(MathOperationType.adicao, imagens[k]);
            imagem.CorrecaoMinMax(Correcao.proporcao);
            Visualizar(imagem, "Soma Corrigida");
        }
        private void SubtracaoLimiar_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[0]);
            if (count == 1)
                Visualizar(GetImagemB());
            for (int k = 1; k < count; k++)
                imagem.MathOp(MathOperationType.subtracao, imagens[k]);
            imagem.CorrecaoMinMax(Correcao.limiar);
            Visualizar(imagem, "Subtração Limiar");
        }

        private void SubtracaoMedia_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[0]);
            if (count == 1)
                Visualizar(GetImagemB());
            for (int k = 1; k < count; k++)
                imagem.MathOp(MathOperationType.subtracao, imagens[k]);
            imagem.CorrecaoMinMax(Correcao.proporcao);
            Visualizar(imagem, "Subtração Corrigida");
        }

        private void Multiplicacao_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[0]);
            if (count == 1)
                Visualizar(GetImagemB());
            for (int k = 1; k < count; k++)
                imagem.MathOp(MathOperationType.multiplicacao, imagens[k]);
            imagem.CorrecaoMinMax(Correcao.proporcao);
            Visualizar(imagem, "Multiplicação");
        }

        private void Divisao_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[0]);
            if (count == 1)
                Visualizar(GetImagemB());
            for (int k = 1; k < count; k++)
                imagem.MathOp(MathOperationType.divisao, imagens[k]);
            imagem.CorrecaoMinMax(Correcao.proporcao);
            Visualizar(imagem, "Divisão");
        }

        private void LogicNot_Click(object sender, EventArgs e)
        {
            var count2 = count;
            for (int k = 0; k < count2; k++)
            {
                Imagem imagem = new Imagem();
                imagem.Clone(imagens[k]);
                imagem.LogicOp(LogicOperationType.not, null);
                Visualizar(imagem, "NOT " + imagem.NomeArquivo());
            }
        }

        private void LogicOr_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[0]);
            if (count == 1)
                Visualizar(GetImagemB());
            for (int k = 1; k < count; k++)
                imagem.LogicOp(LogicOperationType.or, imagens[k]);
            Visualizar(imagem, "OR");
        }

        private void LogicAnd_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[0]);
            if (count == 1)
                Visualizar(GetImagemB());
            for (int k = 1; k < count; k++)
                imagem.LogicOp(LogicOperationType.and, imagens[k]);
            Visualizar(imagem, "AND");
        }

        private void LogicXor_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[0]);
            if (count == 1)
                Visualizar(GetImagemB());
            for (int k = 1; k < count; k++)
                imagem.LogicOp(LogicOperationType.xor, imagens[k]);
            Visualizar(imagem, "XOR");
        }

        private void LogicSub_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[0]);
            if (count == 1)
                Visualizar(GetImagemB());
            for (int k = 1; k < count; k++)
                imagem.LogicOp(LogicOperationType.sub, imagens[k]);
            Visualizar(imagem, "SUB");
        }

        public Imagem GetImagemB()
        {
            Imagem imagemB = new Imagem();
            if (DialogResult.OK == oDlg.ShowDialog())
            {
                imagemB.ImagemBMP = (Bitmap)Bitmap.FromFile(oDlg.FileName);
                imagemB.BitmapCaminho = oDlg.FileName;
                int width = imagens[0].MatrizCor.Width, height = imagens[0].MatrizCor.Height;
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
                morfologia.Enabled = false;
            }
        }
        private void FiltroMedia_Click(object sender, EventArgs e)
        {
            var raioFiltro = new RaioFiltro();
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            if (raioFiltro.ShowDialog() == DialogResult.OK)
            {
                imagem.FiltroMedia(raioFiltro.raio);
                Visualizar(imagem);
            }
        }

        private void FiltroMediana_Click(object sender, EventArgs e)
        {
            var raioFiltro = new RaioFiltro();
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            if (raioFiltro.ShowDialog() == DialogResult.OK)
            {
                imagem.FiltroMediana(raioFiltro.raio);
                Visualizar(imagem);
            }
        }

        private void FecharToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = new Bitmap(1, 1);
            imagens.Clear();
            count = 0;
            editarMenu.Enabled = false;
            salvarArquivo.Enabled = false;
            morfologia.Enabled = false;
        }
        private void MediaImagens_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            bool cond = true;
            int k = 1;
            while (k < count && cond)
                cond = imagem.MatrizCor.Width >= imagens[k].MatrizCor.Width && imagem.MatrizCor.Height >= imagens[k++].MatrizCor.Height;
            if (cond)
            {
                for (k = 1; k < count; k++)
                    imagem.MathOp(MathOperationType.adicao, imagens[k]);
                Imagem divisor = new Imagem();
                divisor.CreatePlainImage(imagem.MatrizCor.Width, imagem.MatrizCor.Height, count);
                imagem.MathOp(MathOperationType.divisao, divisor);
                Visualizar(imagem, "Média de " + (count) + " imagens");
            }
            else
                MessageBox.Show("Imagens possuem resoluções diferentes!");
        }

        private void OpQuantizacao_Click(object sender, EventArgs e)
        {
            Quantizacao quant = new Quantizacao();
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            if (quant.ShowDialog() == DialogResult.OK)
            {
                imagem.ToQuant(quant.niveis);
                Visualizar(imagem);
            }
        }

        private void TonsDeCinzaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            imagem.ToGray();
            Visualizar(imagem);

        }

        private void SobelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            imagem.Bordas(EdgeDetection.Sobel);
            Visualizar(imagem, "Bordas Sobel " + imagem.NomeArquivo());
        }

        private void InverterCores_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            imagem.InverterCores();
            Visualizar(imagem, "Inverso " + imagem.NomeArquivo());

        }
        private void PassaAltaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            imagem.FiltroPassaAlta(); ;
            Visualizar(imagem, "Filtro Passa-Alta " + imagem.NomeArquivo());
        }
        private void PrewittToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            imagem.Bordas(EdgeDetection.Prewitt);
            Visualizar(imagem, "Bordas Prewitt " + imagem.NomeArquivo());
        }

        private void RobertsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            imagem.Bordas(EdgeDetection.Roberts);
            Visualizar(imagem, "Bordas Roberts " + imagem.NomeArquivo());
        }

        private void BordasIsotropico_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            imagem.Bordas(EdgeDetection.Isotropico);
            Visualizar(imagem, "Bordas Isotrópico " + imagem.NomeArquivo());
        }

        private void LaplaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            imagem.Bordas(EdgeDetection.Laplace);
            Visualizar(imagem, "Bordas Laplace " + imagem.NomeArquivo());
        }

        private void StLinear_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            if (stretching.ShowDialog() == DialogResult.OK)
            {
                imagem.Stretching(StretchingType.linear, stretching.A, stretching.B);
                if (stretching.correcaoProporcional)
                    imagem.CorrecaoMinMax(Correcao.proporcao);
                else
                    imagem.CorrecaoMinMax(Correcao.limiar);
                Visualizar(imagem, "Stretching Linear [" + stretching.A + "X + " + stretching.B + "] " + imagem.NomeArquivo());
            }
        }

        private void StQuadrado_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            if (stretching.ShowDialog() == DialogResult.OK)
            {
                imagem.Stretching(StretchingType.quad, stretching.A, stretching.B);
                if (stretching.correcaoProporcional)
                    imagem.CorrecaoMinMax(Correcao.proporcao);
                else
                    imagem.CorrecaoMinMax(Correcao.limiar);
                Visualizar(imagem, "Stretching Quadrado " + imagem.NomeArquivo());
            }
        }

        private void StRaizQuadrada_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            if (stretching.ShowDialog() == DialogResult.OK)
            {
                imagem.Stretching(StretchingType.linear, stretching.A, stretching.B);
                if (stretching.correcaoProporcional)
                    imagem.CorrecaoMinMax(Correcao.proporcao);
                else
                    imagem.CorrecaoMinMax(Correcao.limiar);
                Visualizar(imagem, "Stretching Raiz Quadrada " + imagem.NomeArquivo());
            }
        }

        private void StLogaritmico_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            if (stretching.ShowDialog() == DialogResult.OK)
            {
                imagem.Stretching(StretchingType.linear, stretching.A, stretching.B);
                if (stretching.correcaoProporcional)
                    imagem.CorrecaoMinMax(Correcao.proporcao);
                else
                    imagem.CorrecaoMinMax(Correcao.limiar);
                Visualizar(imagem, "Stretching Logarítmico [" + stretching.A + "*log(X)] " + imagem.NomeArquivo());
            }
        }

        private void StNegativo_Click(object sender, EventArgs e)
        {
            Stretching stretching = new Stretching();
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            if (stretching.ShowDialog() == DialogResult.OK)
            {
                imagem.Stretching(StretchingType.neg, stretching.A, stretching.B);
                if (stretching.correcaoProporcional)
                    imagem.CorrecaoMinMax(Correcao.proporcao);
                else
                    imagem.CorrecaoMinMax(Correcao.limiar);
                Visualizar(imagem, "Stretching Linear [-" + stretching.A + "X - " + stretching.B + "] " + imagem.NomeArquivo());

            }
        }

        private void Limiar_Click(object sender, EventArgs e)
        {
            Dithering dithering = new Dithering();
            Imagem B = new Imagem();
            B.Clone(imagens[count - 1]);
            if (dithering.ShowDialog() == DialogResult.OK)
            {
                B.ToLimiar(dithering.Limiar);
                Visualizar(B);
            }
        }

        private void LimiarComRuido_Click(object sender, EventArgs e)
        {
            Dithering dithering = new Dithering();
            Imagem B = new Imagem();
            B.Clone(imagens[count - 1]);
            if (dithering.ShowDialog() == DialogResult.OK)
            {
                B.ToLimiarAleatorio(dithering.Limiar, dithering.Rinf, dithering.Rsup);
                Visualizar(B);
            }
        }

        private void DtPeriodicoDispersão_Click(object sender, EventArgs e)
        {
            Dithering dithering = new Dithering();
            Imagem B = new Imagem();
            B.Clone(imagens[count - 1]);
            if (dithering.ShowDialog() == DialogResult.OK)
            {
                B.ToPeriodicoDispersao(dithering.Dispersao);
                Visualizar(B, "Dithering " + dithering.Dispersao + "X" + dithering.Dispersao + " " + imagens[count - 1].NomeArquivo() + imagens[count - 1].ExtensaoArquivo());
            }
        }

        private void DtAperiodicoDispersao_Click(object sender, EventArgs e)
        {
            Dithering dithering = new Dithering();
            Imagem B = new Imagem();
            B.Clone(imagens[count - 1]);
            if (dithering.ShowDialog() == DialogResult.OK)
            {
                B.ToAperiodicoDispersao(dithering.Vizinhos);
                Visualizar(B, "Dithering " + dithering.Dispersao + "X" + dithering.Dispersao + " " + imagens[count - 1].NomeArquivo() + imagens[count - 1].ExtensaoArquivo());
            }
        }

        private void Histograma_Click_1(object sender, EventArgs e)
        {
            Imagem imagem = new Imagem();
            imagem.Clone(imagens[count - 1]);
            imagem.CorrecaoHistograma();
            Visualizar(imagem, "Histograma Corrigido " + imagem.NomeArquivo());
        }

        private void PeriodicoPorAglomeracao_Click(object sender, EventArgs e)
        {
            Dithering dithering = new Dithering();
            Imagem B = new Imagem();
            B.Clone(imagens[count - 1]);
            if (dithering.ShowDialog() == DialogResult.OK)
            {
                B.ToQuant(dithering.Dispersao * dithering.Dispersao + 1);
                B.ToPeriodicoAglomeracao(dithering.Dispersao);
                Visualizar(B, "Dithering Aglomeração" + dithering.Dispersao + "X" + dithering.Dispersao + " " + imagens[count - 1].NomeArquivo() + imagens[count - 1].ExtensaoArquivo());
            }
        }

        private void Erosao_Click(object sender, EventArgs e)
        {
            Imagem B = new Imagem();
            B.Clone(imagens[count - 1]);
            B.Erosao(ElEst.quadrado, 1, 1, null);
            Visualizar(B, "Erosão " + B.NomeArquivo());
        }

        private void DilataçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Imagem B = new Imagem();
            B.Clone(imagens[count - 1]);
            B.Dilatacao(ElEst.quadrado, 1, 1, null);
            Visualizar(B, "Dilatação " + B.NomeArquivo());
        }

        private void BordaInterna_Click(object sender, EventArgs e)
        {
            Imagem erod = new Imagem();
            erod.Clone(imagens[count - 1]);
            erod.Erosao(ElEst.quadrado, 1, 1, null);
            Visualizar(erod, "Erosão " + erod.NomeArquivo());
            Imagem borda = new Imagem();
            borda.Clone(imagens[count - 2]);
            borda.MathOp(MathOperationType.subtracao, imagens[count - 1]);
            borda.CorrecaoMinMax(Correcao.limiar);
            Visualizar(borda, "Borda Interna " + borda.NomeArquivo());
        }

        private void BordaExterna_Click(object sender, EventArgs e)
        {
            Imagem dil = new Imagem();
            dil.Clone(imagens[count - 1]);
            dil.Dilatacao(ElEst.quadrado, 1, 1, null);
            Visualizar(dil, "Dilatação " + dil.NomeArquivo());
            Imagem borda = new Imagem();
            borda.Clone(imagens[count - 1]);
            borda.MathOp(MathOperationType.subtracao, imagens[count - 2]);
            borda.CorrecaoMinMax(Correcao.limiar);
            Visualizar(borda, "Borda Externa " + borda.NomeArquivo());
        }

        private void Abertura_Click(object sender, EventArgs e)
        {
            Imagem erod = new Imagem();
            erod.Clone(imagens[count - 1]);
            erod.Erosao(ElEst.quadrado, 1, 1, null);
            Visualizar(erod, "Erosão " + erod.NomeArquivo());
            Imagem dil = new Imagem();
            dil.Clone(imagens[count - 1]);
            dil.Dilatacao(ElEst.quadrado, 1, 1, null);
            dil.CorrecaoMinMax(Correcao.limiar);
            Visualizar(dil, "Abertura " + dil.NomeArquivo());
        }

        private void Fechamento_Click(object sender, EventArgs e)
        {
            Imagem dil = new Imagem();
            dil.Clone(imagens[count - 1]);
            dil.Dilatacao(ElEst.quadrado, 1, 1, null);
            dil.CorrecaoMinMax(Correcao.limiar);
            Visualizar(dil, "Dilatação " + dil.NomeArquivo());
            Imagem erod = new Imagem();
            erod.Clone(imagens[count - 1]);
            erod.Erosao(ElEst.quadrado, 1, 1, null);
            Visualizar(erod, "Fechamento " + erod.NomeArquivo());
        }

        private void AcertoEErro_Click(object sender, EventArgs e)
        {
            Imagem A = new Imagem();
            A.Clone(imagens[count - 1]);
            Imagem X = GetImagemB();
            Imagem W = new Imagem();
            W.CreatePlainImage(X.MatrizCor.Width + 2, X.MatrizCor.Height + 2, 255);
            W.MathOp(MathOperationType.subtracao, X, 1);
            Visualizar(X, "Teste");
            A.Dilatacao(ElEst.quadrado, 1, 1, null);
            A.CorrecaoMinMax(Correcao.limiar);
            //Visualizar(A, "Dilatação " + A.NomeArquivo());

        }

        private void Esqueleto_Click(object sender, EventArgs e)
        {
            Imagem process = new Imagem();
            Imagem entrada = new Imagem();
            Imagem op = new Imagem();
            Imagem saida = new Imagem();
            saida.CreatePlainImage(imagens[count - 1].MatrizCor.Width, imagens[count - 1].MatrizCor.Height, 0);

            process.Clone(imagens[count - 1]);//armazena a saida
            //origem.Clone(imagens[count - 1]);//armazena a entrada
            entrada.Clone(imagens[count - 1]);//armazena a entrada erodida
            op.Clone(imagens[count - 1]); //Imagem que sofre abertura

            while (!entrada.IsNull())
            {
                
                op.Erosao(ElEst.quadrado, 1, 1, null); op.Dilatacao(ElEst.quadrado, 1, 1, null);
                //Visualizar(op);
                process.MathOp(MathOperationType.subtracao, op);
                saida.LogicOp(LogicOperationType.or, process);
                entrada.Erosao(ElEst.quadrado, 1, 1, null);
                process.Clone(entrada);
                op.Clone(entrada);
            }
            Imagem combo = new Imagem();
            combo.Clone(imagens[count-1]);
            Visualizar(saida, "Esqueletização " + saida.NomeArquivo());
            combo.MathOp(MathOperationType.subtracao, saida);
            combo.CorrecaoMinMax(Correcao.limiar);
            Visualizar(combo, "Esqueletização " + saida.NomeArquivo());




        }
    }
}
