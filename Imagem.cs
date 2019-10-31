using System;
using System.IO;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace ImageProcessing
{
    public class Imagem
    {
        private Bitmap _imagemBMP;
        public int niveisCinza = 256;
        public ImagemInt MatrizCor;

        public Imagem()
        {

        }

        public Bitmap ImagemBMP
        {
            get
            {
                if (_imagemBMP == null)
                    _imagemBMP = new Bitmap(1, 1);
                return _imagemBMP;
            }
            set
            {
                _imagemBMP = value;
            }
        }

        public string BitmapCaminho { get; set; }
        public string NomeArquivo()
        {
            return Path.GetFileNameWithoutExtension(BitmapCaminho);
        }
        public string ExtensaoArquivo()
        {
            return Path.GetExtension(BitmapCaminho);
        }

        public enum ColorFilterTypes
        {
            Vermelho,
            Verde,
            Azul
        };

        public void SaveBitmap(string saveFilePath)
        {
            BitmapCaminho = saveFilePath;
            if (File.Exists(saveFilePath))
                File.Delete(saveFilePath);
            _imagemBMP.Save(saveFilePath);
        }

        public void InverterCores()
        {
            int x, y, canal;
            for (x = 0; x < this.MatrizCor.Width; x++)
                for (y = 0; y < this.MatrizCor.Height; y++)
                    for (canal = 0; canal < 3; canal++)
                        MatrizCor.Matriz[x, y, canal] = (255 - MatrizCor.Matriz[x, y, canal]);
        }
        public void SetColorFilter(ColorFilterTypes colorFilterType)
        {
            Bitmap temp = (Bitmap)_imagemBMP;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int nPixelR = 0;
                    int nPixelG = 0;
                    int nPixelB = 0;
                    if (colorFilterType == ColorFilterTypes.Vermelho)
                    {
                        nPixelR = c.R;
                        nPixelG = c.G - 255;
                        nPixelB = c.B - 255;
                    }
                    else if (colorFilterType == ColorFilterTypes.Verde)
                    {
                        nPixelR = c.R - 255;
                        nPixelG = c.G;
                        nPixelB = c.B - 255;
                    }
                    else if (colorFilterType == ColorFilterTypes.Azul)
                    {
                        nPixelR = c.R - 255;
                        nPixelG = c.G - 255;
                        nPixelB = c.B;
                    }

                    nPixelR = Math.Max(nPixelR, 0);
                    nPixelR = Math.Min(255, nPixelR);

                    nPixelG = Math.Max(nPixelG, 0);
                    nPixelG = Math.Min(255, nPixelG);

                    nPixelB = Math.Max(nPixelB, 0);
                    nPixelB = Math.Min(255, nPixelB);

                    bmap.SetPixel(i, j, Color.FromArgb((byte)nPixelR, (byte)nPixelG, (byte)nPixelB));
                }
            }
            _imagemBMP = (Bitmap)bmap.Clone();
        }
        public void CorrecaoMinMax(Correcao tipo)
        {
            int canal, x, y, h = MatrizCor.Height, w = MatrizCor.Width;
            int[,] minmaxRGB = GetMinMax();
            bool cond = false;
            if (minmaxRGB[0, 0] > 255 || minmaxRGB[0, 1] > 255 || minmaxRGB[0, 2] > 255 || minmaxRGB[1, 0] < 0 || minmaxRGB[1, 1] < 0 || minmaxRGB[1, 2] < 0)
                cond = true;
            if (cond)
            {
                if (tipo == Correcao.limiar)
                {
                    for (x = 0; x < w; x++)
                        for (y = 0; y < h; y++)
                            for (canal = 0; canal < 3; canal++)
                            {
                                if (MatrizCor.Matriz[x, y, canal] > 255)
                                    MatrizCor.Matriz[x, y, canal] = 255;
                                if (MatrizCor.Matriz[x, y, canal] < 0)
                                    MatrizCor.Matriz[x, y, canal] = 0;
                            }
                }
                else
                {
                    for (x = 0; x < w; x++)
                        for (y = 0; y < h; y++)
                            for (canal = 0; canal < 3; canal++)
                                MatrizCor.Matriz[x, y, canal] = (MatrizCor.Matriz[x, y, canal] - minmaxRGB[1, canal]) * 255 / (minmaxRGB[0, canal] - minmaxRGB[1, canal]);
                }

            }
        }
        public void CreatePlainImage(int width, int height, int value)
        {
            int x, y, c;
            MatrizCor = new ImagemInt
            {
                Height = height,
                Width = width,
                Matriz = new int[width, height, 3]
            };
            for (x = 0; x < width; x++)
                for (y = 0; y < height; y++)
                    for (c = 0; c <= 2; c++)
                        MatrizCor.Matriz[x, y, c] = value;
        }

        public void ToInt()
        {
            int x, y;
            int w = ImagemBMP.Width, h = ImagemBMP.Height;
            MatrizCor.Matriz = new int[w, h, 3];
            MatrizCor.Width = w;
            MatrizCor.Height = h;
            Color color;
            for (x = 0; x < w; x++)
                for (y = 0; y < h; y++)
                {
                    color = ImagemBMP.GetPixel(x, y);
                    MatrizCor.Matriz[x, y, 0] = (int)color.R;
                    MatrizCor.Matriz[x, y, 1] = (int)color.G;
                    MatrizCor.Matriz[x, y, 2] = (int)color.B;
                }
        }
        public void ToImage()
        {
            int x, y;
            ImagemBMP = new Bitmap(MatrizCor.Width, MatrizCor.Height);
            for (x = 0; x < MatrizCor.Width; x++)
                for (y = 0; y < MatrizCor.Height; y++)
                    ImagemBMP.SetPixel(x, y, Color.FromArgb(MatrizCor.Matriz[x, y, 0], MatrizCor.Matriz[x, y, 1], MatrizCor.Matriz[x, y, 2]));
        }
        public void ToGray()
        {
            int gray = 0, canal, i, j;
            double[] factors = { 0.299, 0.587, 0.144 };
            for (i = 0; i < this.MatrizCor.Width; i++)
                for (j = 0; j < this.MatrizCor.Height; j++)
                {
                    for (canal = 0; canal < 3; canal++)
                    {
                        gray += (int)(factors[canal] * (double)MatrizCor.Matriz[i, j, canal]);
                        if (gray > 255)
                            gray = 255;
                    }
                    for (canal = 0; canal < 3; canal++)
                        MatrizCor.Matriz[i, j, canal] = gray;
                    gray = 0;
                }
        }

        public void ToQuant(int niveis)
        {
            int canal, x, y, h = this.MatrizCor.Height, w = this.MatrizCor.Width;
            for (x = 0; x < w; x++)
                for (y = 0; y < h; y++)
                    for (canal = 0; canal < 3; canal++)
                    {
                        this.MatrizCor.Matriz[x, y, canal] /= (256 / niveis);
                        this.MatrizCor.Matriz[x, y, canal] *= (256 / niveis);
                    }
            this.niveisCinza = niveis;
        }
        public void FiltroMediana(int raio)
        {
            int size = ((2 * raio + 1) * (2 * raio + 1));
            int[] r = new int[size];
            int x, y, i, j, pos = 0, canal;
            int w = MatrizCor.Width;
            int h = MatrizCor.Height;
            var aux = this.MatrizCor;
            for (x = raio; x <= w - raio; x++)
                for (y = raio; y <= h - raio; y++)
                    for (canal = 0; canal < 3; canal++)
                    {
                        for (i = x - raio; i <= x + raio; i++)
                            for (j = y - raio; j <= y + raio; j++)
                                r[pos++] = MatrizCor.Matriz[i, j, canal];
                        Array.Sort(r);
                        aux.Matriz[x, y, canal] = r[size / 2];
                        pos = 0;
                    }
            MatrizCor = aux;
        }
        public void FiltroMedia(int raio)
        {
            int r = 0, x, y, i, j, canal;
            int width = MatrizCor.Width;
            int height = MatrizCor.Height;
            var aux = MatrizCor;
            for (x = raio; x < width - raio; x++)
                for (y = raio; y < height - raio; y++)
                    for (canal = 0; canal < 3; canal++)
                    {
                        for (i = x - raio; i <= x + raio; i++)
                            for (j = y - raio; j <= y + raio; j++)
                                r += aux.Matriz[i, j, canal];
                        r /= 9;
                        aux.Matriz[x, y, canal] = r;
                        r = 0;
                    }
            MatrizCor = aux;
        }
        public void FiltroPassaAlta()
        {
            int[,] maskX = new int[3, 3] { { -1, -1, -1 },
                                           { -1, 8, -1 },
                                           { -1, -1, -1 } };
            int rx = 0, x, y, i, j, k = 0, l = 0, c;
            int width = MatrizCor.Width, height = MatrizCor.Height;
            int[,,] G = new int[width, height, 3];
            for (x = 1; x < width - 1; x++)
                for (y = 1; y < height - 1; y++)
                    for (c = 0; c <= 2; c++)
                    {
                        for (i = x - 1; i < x + 2; i++)
                        {
                            for (j = y - 1; j < y + 2; j++)
                                rx += (MatrizCor.Matriz[i, j, c] * maskX[k++, l]);
                            k = 0;
                            l++;
                        }
                        G[x, y, c] = (int)Math.Abs(rx) / 9;
                        rx = 0;
                        l = 0;
                    }
            MatrizCor.Matriz = G;
        }
        public void Bordas(EdgeDetection borda)
        {
            double[,] maskX = new double[3, 3], maskY = new double[3, 3];
            int el = 9;
            switch (borda)
            {
                case EdgeDetection.Sobel:
                    maskX = new double[3, 3] {{ -1, 00, 01 },
                                              { -2, 00, 02 },
                                              { -1, 00, 01 }};
                    maskY = new double[3, 3] {{ -1, -2, -1 },
                                              { 00, 00, 00 },
                                              { 01, 02, 01 } };
                    break;
                case EdgeDetection.Isotropico:
                    maskX = new double[3, 3] { { -1, 00, 01 },
                                               { -1.4142135624, 00, 1.4142135624 },
                                               { -1, 00, 01 } };
                    maskY = new double[3, 3] { { -1, -1.4142135624, -1 },
                                               { 00, 00, 00 },
                                               { 01, 1.4142135624, 01 } };
                    break;
                case EdgeDetection.Laplace:
                    maskX = new double[3, 3] {{  0, -1,  0 },
                                              { -1,  4, -1 },
                                              {  0, -1,  0 }};

                    maskY = new double[3, 3] {{ -1, -1, -1 },
                                              { -1,  8, -1 },
                                              { -1, -1, -1 }};
                    break;
                case EdgeDetection.Prewitt:
                    maskX = new double[3, 3] { { -1, -1, -1 },
                                               { 00, 00, 00 },
                                               { 01, 01, 01 } };
                    maskY = new double[3, 3] { { -1, 00, 01 },
                                               { -1, 00, 01 },
                                               { -1, 00, 01 } };
                    break;

                case EdgeDetection.Roberts:
                    maskX = new double[3, 3] { { 00, 00, 00 },
                                               { 00, 01, 00 },
                                               { 00, 00, -1 } };
                    maskY = new double[3, 3] { { 00, 00, 00 },
                                               { 00, 00, 01 },
                                               { 00, -1, 00 } };
                    el = 4;
                    break;

            }
            double rx = 0, ry = 0;
            int x, y, i, j, k = 0, l = 0, canal, width = MatrizCor.Width, height = MatrizCor.Height;
            int[,,] G = new int[width, height, 3];

            for (x = 1; x < width - 1; x++)
                for (y = 1; y < height - 1; y++)
                    for (canal = 0; canal < 3; canal++)
                    {
                        for (i = x - 1; i < x + 2; i++)
                        {
                            for (j = y - 1; j < y + 2; j++)
                            {
                                rx += (MatrizCor.Matriz[i, j, canal] * maskX[k, l]);
                                ry += (MatrizCor.Matriz[i, j, canal] * maskY[k, l]);
                                k++;
                            }
                            k = 0;
                            l++;
                        }
                        G[x, y, canal] = (int)(Math.Sqrt(Math.Pow(rx, 2.0) + Math.Pow(ry, 2.0)) / el);
                        rx = 0; ry = 0;
                        l = 0;
                    }
            MatrizCor.Matriz = G;
        }
        public void LogicOp(LogicOperationType operation, Imagem imagemB)
        {

            if (imagemB == null) imagemB = this;

            int x, y, c;
            for (x = 0; x < imagemB.MatrizCor.Width; x++)
                for (y = 0; y < imagemB.MatrizCor.Height; y++)
                    for (c = 0; c < 3; c++)
                        switch (operation)
                        {
                            case LogicOperationType.not:
                                this.MatrizCor.Matriz[x, y, c] = GetInt(!GetBool(MatrizCor.Matriz[x, y, c]));
                                break;
                            case LogicOperationType.and:
                                this.MatrizCor.Matriz[x, y, c] = GetInt(GetBool(this.MatrizCor.Matriz[x, y, c]) && GetBool(imagemB.MatrizCor.Matriz[x, y, c]));
                                break;
                            case LogicOperationType.or:
                                this.MatrizCor.Matriz[x, y, c] = GetInt(GetBool(this.MatrizCor.Matriz[x, y, c]) || GetBool(imagemB.MatrizCor.Matriz[x, y, c]));
                                break;
                            case LogicOperationType.sub:
                                this.MatrizCor.Matriz[x, y, c] = GetInt(GetBool(this.MatrizCor.Matriz[x, y, c]) && !GetBool(imagemB.MatrizCor.Matriz[x, y, c]));
                                break;
                            case LogicOperationType.xor:
                                this.MatrizCor.Matriz[x, y, c] = GetInt(!GetBool(this.MatrizCor.Matriz[x, y, c]) && GetBool(imagemB.MatrizCor.Matriz[x, y, c]) || GetBool(this.MatrizCor.Matriz[x, y, c]) && !GetBool(imagemB.MatrizCor.Matriz[x, y, c]));
                                break;
                        }
        }
        public void MathOp(MathOperationType operation, Imagem imagemB)
        {

            if (imagemB != null)
            {
                int width = imagemB.MatrizCor.Width;
                int height = imagemB.MatrizCor.Height;
                int[] rgbA = { 0, 0, 0 }, rgbB = { 0, 0, 0 };
                int x, y, canal;

                for (x = 0; x < width; x++)
                    for (y = 0; y < height; y++)
                        for (canal = 0; canal < 3; canal++)
                        {
                            rgbA[canal] = this.MatrizCor.Matriz[x, y, canal];
                            rgbB[canal] = imagemB.MatrizCor.Matriz[x, y, canal];
                            switch (operation)
                            {
                                case MathOperationType.adicao:
                                    rgbA[canal] += rgbB[canal];
                                    break;
                                case MathOperationType.divisao:
                                    rgbA[canal] = (rgbA[canal] < 1) ? 1 : rgbA[canal];
                                    rgbA[canal] /= rgbB[canal];
                                    break;
                                case MathOperationType.multiplicacao:
                                    rgbA[canal] *= rgbB[canal];
                                    break;
                                case MathOperationType.subtracao:
                                    rgbA[canal] -= rgbB[canal];
                                    break;
                            }
                            this.MatrizCor.Matriz[x, y, canal] = rgbA[canal];
                        }
            }
        }
        public void MathOp(MathOperationType operation, Imagem imagemB, int r)
        {

            if (imagemB != null)
            {
                int width = this.MatrizCor.Width;
                int height = this.MatrizCor.Height;
                int[] rgbA = { 0, 0, 0 }, rgbB = { 0, 0, 0 };
                int x, y, canal;

                for (x = r; x < width - r; x++)
                    for (y = r; y < height - r; y++)
                        for (canal = 0; canal < 3; canal++)
                        {
                            rgbA[canal] = this.MatrizCor.Matriz[x, y, canal];
                            rgbB[canal] = imagemB.MatrizCor.Matriz[x - r, y - r, canal];
                            switch (operation)
                            {
                                case MathOperationType.adicao:
                                    rgbA[canal] += rgbB[canal];
                                    break;
                                case MathOperationType.divisao:
                                    rgbA[canal] = (rgbA[canal] < 1) ? 1 : rgbA[canal];
                                    rgbA[canal] /= rgbB[canal];
                                    break;
                                case MathOperationType.multiplicacao:
                                    rgbA[canal] *= rgbB[canal];
                                    break;
                                case MathOperationType.subtracao:
                                    rgbA[canal] -= rgbB[canal];
                                    break;
                            }
                            this.MatrizCor.Matriz[x, y, canal] = rgbA[canal];
                        }
            }
        }
        public void CorrecaoHistograma()
        {
            int[,] histograma = new int[256, 3];
            int[,] histogramaAc = new int[256, 3];
            int[,] histogramaCalc = new int[256, 3];
            int x, y, c, width = this.MatrizCor.Width, height = this.MatrizCor.Height;
            for (x = 0; x < width; x++)
                for (y = 0; y < height; y++)
                    for (c = 0; c < 3; c++)
                        histograma[MatrizCor.Matriz[x, y, c], c]++;
            histogramaAc[0, 0] = histograma[0, 0];
            histogramaAc[0, 1] = histograma[0, 1];
            histogramaAc[0, 2] = histograma[0, 2];
            for (x = 1; x < 256; x++)
                for (c = 0; c < 3; c++)
                    histogramaAc[x, c] = histogramaAc[x - 1, c] + histograma[x, c];
            for (x = 0; x < 256; x++)
                for (c = 0; c < 3; c++)
                    histogramaCalc[x, c] = (int)(255 * histogramaAc[x, c] / (width * height));

            for (x = 0; x < width; x++)
                for (y = 0; y < height; y++)
                    for (c = 0; c < 3; c++)
                        MatrizCor.Matriz[x, y, c] = histogramaCalc[MatrizCor.Matriz[x, y, c], c];
        }
        public void ToLimiar(int limiar)
        {
            int x, y, c, h = MatrizCor.Height, w = MatrizCor.Width;
            for (x = 0; x < w; x++)
                for (y = 0; y < h; y++)
                    for (c = 0; c < 3; c++)
                        MatrizCor.Matriz[x, y, c] = (MatrizCor.Matriz[x, y, c] > limiar) ? 255 : 0;
        }
        public void ToLimiarAleatorio(int limiar, int limRandomInf, int limRandomSup)
        {
            int x, y, c, h = MatrizCor.Height, w = MatrizCor.Width;
            Random rnd = new Random();
            int rand, temp;
            for (x = 0; x < w; x++)
                for (y = 0; y < h; y++)
                {
                    rand = rnd.Next(limRandomInf, limRandomSup + 1);
                    for (c = 0; c < 3; c++)
                    {
                        temp = MatrizCor.Matriz[x, y, c] + rand;
                        MatrizCor.Matriz[x, y, c] = (temp > limiar) ? 255 : 0;
                    }
                }
        }

        public void ToPeriodicoDispersao(int dispersao)
        {
            int[,] D = GetDispersao(dispersao);
            int i, j, x, y, c, h = MatrizCor.Height, w = MatrizCor.Width;
            for (x = 0; x < w; x++)
                for (y = 0; y < h; y++)
                    for (c = 0; c < 3; c++)
                    {
                        i = x % dispersao;
                        j = y % dispersao;
                        if (MatrizCor.Matriz[x, y, c] > D[i, j])
                            MatrizCor.Matriz[x, y, c] = 255;
                        else
                            MatrizCor.Matriz[x, y, c] = 0;
                    }
        }

        public void ToAperiodicoDispersao(int vizinhos)
        {
            int x, y, c, h = MatrizCor.Height - 1, w = MatrizCor.Width - 1, erro;
            int[] media = GetMedia();
            if (vizinhos == 3)
                for (y = 0; y < h; y++)
                    for (x = 0; x < w; x++)
                        for (c = 0; c < 3; c++)
                        {
                            if (MatrizCor.Matriz[x, y, c] < media[c])
                            {
                                erro = MatrizCor.Matriz[x, y, c];
                                MatrizCor.Matriz[x, y, c] = 0;
                            }
                            else
                            {
                                erro = MatrizCor.Matriz[x, y, c] - 255;
                                MatrizCor.Matriz[x, y, c] = 255;
                            }
                            MatrizCor.Matriz[x, y + 1, c] += (erro * 3 / 8);
                            MatrizCor.Matriz[x + 1, y, c] += (erro * 3 / 8);
                            MatrizCor.Matriz[x + 1, y + 1, c] += (erro * 2 / 8);
                        }
            else
                for (y = 0; y < h; y++)
                    for (x = 1; x < w; x++)
                        for (c = 0; c < 3; c++)
                        {
                            if (MatrizCor.Matriz[x, y, c] < media[c])
                            {
                                erro = MatrizCor.Matriz[x, y, c];
                                MatrizCor.Matriz[x, y, c] = 0;
                            }
                            else
                            {
                                erro = MatrizCor.Matriz[x, y, c] - 255;
                                MatrizCor.Matriz[x, y, c] = 255;
                            }
                            MatrizCor.Matriz[x + 1, y, c] += (erro * 7 / 16);
                            MatrizCor.Matriz[x - 1, y + 1, c] += (erro * 3 / 16);
                            MatrizCor.Matriz[x, y + 1, c] += (erro * 5 / 16);
                            MatrizCor.Matriz[x + 1, y + 1, c] += (erro * 1 / 16);
                        }

            this.CorrecaoMinMax(Correcao.proporcao);
        }
        public void ToPeriodicoAglomeracao(int dispersao)
        {
            int[,] D = GetDispersao(dispersao);
            var Matriz = new int[MatrizCor.Width * dispersao, MatrizCor.Width * dispersao, 3];
            int i, j, x, y, c, h = MatrizCor.Height * dispersao, w = MatrizCor.Width * dispersao;
            for (x = 0; x < w; x++)
                for (y = 0; y < h; y++)
                {
                    i = x % dispersao;
                    j = y % dispersao;
                    for (c = 0; c < 3; c++)
                        if (MatrizCor.Matriz[x / dispersao, y / dispersao, c] > D[i, j])
                            Matriz[x, y, c] = 255;
                        else
                            Matriz[x, y, c] = 0;
                }
            MatrizCor.Matriz = Matriz;
            MatrizCor.Width *= dispersao;
            MatrizCor.Height *= dispersao;

        }
        public int[] GetMedia()
        {
            int x, y, c, h = MatrizCor.Height, w = MatrizCor.Width;
            long[] aux = { 0, 0, 0 };
            int[] m = { 0, 0, 0 };

            for (x = 0; x < w; x++)
                for (y = 0; y < h; y++)
                    for (c = 0; c < 3; c++)
                        aux[c] += (long)MatrizCor.Matriz[x, y, c];
            for (c = 0; c < 3; c++)
            {
                m[c] = (int)(aux[c] / (w * h));

            }
            return m;
        }
        public int[,] GetMinMax()
        {
            int[,] RGB = { { 0, 0, 0 }, { 0, 0, 0 } };
            int x, y, c;
            RGB[0, 0] = MatrizCor.Matriz[0, 0, 0];
            RGB[0, 1] = MatrizCor.Matriz[0, 0, 1];
            RGB[0, 2] = MatrizCor.Matriz[0, 0, 2];
            RGB[1, 0] = MatrizCor.Matriz[0, 0, 0];
            RGB[1, 1] = MatrizCor.Matriz[0, 0, 1];
            RGB[1, 2] = MatrizCor.Matriz[0, 0, 2];

            for (x = 0; x < MatrizCor.Width; x++)
                for (y = 0; y < MatrizCor.Height; y++)
                    for (c = 0; c <= 2; c++)
                    {
                        if (MatrizCor.Matriz[x, y, c] > RGB[0, c])
                            RGB[0, c] = MatrizCor.Matriz[x, y, c];
                        if (MatrizCor.Matriz[x, y, c] < RGB[1, c])
                            RGB[1, c] = MatrizCor.Matriz[x, y, c];
                    }
            return RGB;
        }
        public void Stretching(StretchingType stretching, double A, double B)
        {
            //this.ToInt();
            int x, y, c, h = MatrizCor.Height, w = MatrizCor.Width;
            for (x = 0; x < w; x++)
                for (y = 0; y < h; y++)
                    for (c = 0; c < 3; c++)
                        switch (stretching)
                        {
                            case StretchingType.linear:
                                MatrizCor.Matriz[x, y, c] = (int)(MatrizCor.Matriz[x, y, c] * A + B);
                                break;
                            case StretchingType.log:
                                MatrizCor.Matriz[x, y, c] = (int)(A * Math.Log10(MatrizCor.Matriz[x, y, c] + 1));
                                break;
                            case StretchingType.neg:
                                MatrizCor.Matriz[x, y, c] = (int)(0 - (MatrizCor.Matriz[x, y, c] * A + B));
                                break;
                            case StretchingType.quad:
                                MatrizCor.Matriz[x, y, c] = (int)(A * Math.Pow(MatrizCor.Matriz[x, y, c], 2));
                                break;
                            case StretchingType.srqt:
                                MatrizCor.Matriz[x, y, c] = (int)(A * Math.Sqrt(MatrizCor.Matriz[x, y, c]));
                                break;
                        }
        }
        public void Clone(Imagem imagem)
        {
            int x, y, c, h = imagem.MatrizCor.Height, w = imagem.MatrizCor.Width;
            this.MatrizCor.Matriz = new int[w, h, 3];
            this.MatrizCor.Height = h;
            this.MatrizCor.Width = w;
            for (x = 0; x < w; x++)
                for (y = 0; y < h; y++)
                    for (c = 0; c < 3; c++)
                        this.MatrizCor.Matriz[x, y, c] = imagem.MatrizCor.Matriz[x, y, c];
        }
        private bool GetBool(int i)
        {
            if (i > 127)
                return true;
            else
                return false;
        }
        private int GetInt(bool i)
        {
            if (i)
                return 255;
            else
                return 0;
        }
        public void Erosao(ElEst el, int rx, int ry, Imagem B)
        {
            int x, y, i, j, c, w = MatrizCor.Width, h = MatrizCor.Height;
            int[,] ee;
            if (B == null)
                ee = GetEE(el, rx, ry);
            else
            {
                ee = GetEE(B);
                rx = B.MatrizCor.Width / 2;
                ry = B.MatrizCor.Height / 2;
            }

            bool iguais;
            var aux = new int[w, h, 3];
            for (c = 0; c < 3; c++)
                for (x = rx; x < w - rx; x++)
                    for (y = ry; y < h - ry; y++)
                    {
                        iguais = true;
                        for (i = -rx; i <= rx && iguais; i++)
                            for (j = -ry; j <= ry && iguais; j++)
                                if (ee[rx + i, ry + j] >= 0)
                                    iguais = MatrizCor.Matriz[x + i, y + j, c] == ee[rx + i, ry + j];
                        aux[x, y, c] = iguais ? 255 : 0;
                    }
            this.MatrizCor.Matriz = aux;
        }
        public void Dilatacao(ElEst el, int rx, int ry, Imagem B)
        {
            int x, y, i, j, c, w = MatrizCor.Width, h = MatrizCor.Height;
            int[,] ee;
            if (B == null)
                ee = GetEE(el, rx, ry);
            else
            {
                ee = GetEE(B);
                rx = B.MatrizCor.Width / 2;
                ry = B.MatrizCor.Height / 2;
            }
            bool iguais;
            var aux = new int[w, h, 3];
            for (c = 0; c < 3; c++)
                for (x = rx; x < w - rx; x++)
                    for (y = ry; y < h - ry; y++)
                    {
                        iguais = false;
                        for (i = -rx; i <= rx; i++)
                            for (j = -ry; j <= ry; j++)
                                if (ee[rx + i, ry + j] >= 0)
                                    iguais = iguais || (MatrizCor.Matriz[x + i, y + j, c] == ee[rx + i, ry + j]);
                        aux[x, y, c] = iguais ? 255 : 0;
                    }
            this.MatrizCor.Matriz = aux;
        }

        public int[,] GetDispersao(int dispersao)
        {
            int[,] D;
            switch (dispersao)
            {
                case 2:
                    D = new int[,] { { 0 * 64, 2 * 64 },
                                             { 3 * 64, 1 * 64 } };
                    break;
                case 3:
                    D = new int[,] { { 06 * 28, 04 * 28, 08 * 28 },
                                     { 01 * 28, 00 * 28, 03 * 28 },
                                     { 05 * 28, 02 * 28, 07 * 28 } };
                    break;
                default:
                    D = new int[,] { { 00 * 16, 08 * 16, 02 * 16, 10 * 16 },
                                     { 12 * 16, 04 * 16, 14 * 16, 06 * 16 },
                                     { 03 * 16, 11 * 16, 01 * 16, 09 * 16 },
                                     { 15 * 16, 07 * 16, 13 * 16, 05 * 16 } };
                    break;
            }
            return D;
        }
        public int[,] GetEE(ElEst el, int rx, int ry)
        {
            int lengthX = (2 * rx + 1), lengthY = (2 * ry + 1); ;

            int[,] EE = new int[lengthX, lengthY];
            switch (el)
            {
                case ElEst.circulo:
                case ElEst.cruz:
                    for (int x = 0; x < lengthX; x++)
                        for (int y = 0; y < lengthY; y++)
                            if (x == (rx - 1) / 2 || y == (ry - 1) / 2)
                                EE[x, y] = 255;
                            else
                                EE[x, y] = -1;
                    break;
                case ElEst.ponto:
                    EE[(rx - 1) / 2, (ry - 1) / 2] = 255;
                    break;
                case ElEst.quadrado:
                    for (int x = 0; x < lengthX; x++)
                        for (int y = 0; y < lengthY; y++)
                            EE[x, y] = 255;
                    break;
                case ElEst.quadradoCinza:
                    for (int x = 0; x < lengthX; x++)
                        for (int y = 0; y < lengthY; y++)
                            EE[x, y] = 1;
                    EE[rx / 2, ry / 2] = 2;
                    break;

            }
            return EE;
        }
        public int[,] GetEE(Imagem B)
        {
            int x, y;
            int[,] EE = new int[B.MatrizCor.Width, B.MatrizCor.Height];
            for (x = 0; x < B.MatrizCor.Width; x++)
                for (y = 0; y < B.MatrizCor.Height; y++)
                    EE[x, y] = B.MatrizCor.Matriz[x, y, 0];
            return EE;
        }
        public void AddBorder(int rx, int ry, int v)
        {
            int x, y, c, w = this.MatrizCor.Width + 2 * rx, h = this.MatrizCor.Height + 2 * ry;
            var aux = new Imagem();
            aux.CreatePlainImage(w, h, v);

            for (x = 0; x < MatrizCor.Width; x++)
                for (y = 0; y < MatrizCor.Width; y++)
                    for (c = 0; c < 3; c++)
                        aux.MatrizCor.Matriz[x+rx, y+ry, c] = MatrizCor.Matriz[x, y, c];

            this.MatrizCor.Matriz = aux.MatrizCor.Matriz;
            this.MatrizCor.Height = h;
            this.MatrizCor.Width = w;
        }
        public bool IsNull()
        {
            int x, y, c, w = this.MatrizCor.Width, h = this.MatrizCor.Height;
            for (x = 0; x < w; x++)
                for (y = 0; y < h; y++)
                    for (c = 0; c < 3; c++)
                        if (MatrizCor.Matriz[x, y, c] == 255)
                            return false;

            return true;

        }
        public void ErosaoCinza(ElEst el, int rx, int ry)
        {
            Imagem img = this;
            int[,] ee = GetEE(el, rx, ry);
            int h = this.MatrizCor.Height;
            int w = this.MatrizCor.Width;
            Imagem saida = new Imagem();
            saida.Clone(this);
            int x, y, c, i, j, min, aux;
            for (c = 0; c < 3; c++)
                for (x = rx; x < (w - rx); x++)
                    for (y = ry; y < (h - ry); y++)
                    {
                        aux = 255;
                        for (i = -rx; i <= rx; i++)
                            for (j = -ry; j <= ry; j++)
                            {
                                min = img.MatrizCor.Matriz[x + i, y + j, c] - (ee[rx + i, ry + j]);
                                if (min < aux)
                                    aux = min;
                            }
                        saida.MatrizCor.Matriz[x, y, c] = aux;
                    }

            this.MatrizCor = saida.MatrizCor;
        }
        public void DilatacaoCinza(ElEst el, int rx, int ry)
        {
            Imagem img = this;
            int[,] ee = GetEE(el, rx, ry);
            int h = this.MatrizCor.Height;
            int w = this.MatrizCor.Width;
            Imagem saida = new Imagem();
            saida.Clone(this);
            int x, y, c, i, j, max, aux;
            for (c = 0; c < 3; c++)
                for (x = rx; x < (w - rx); x++)
                    for (y = ry; y < (h - ry); y++)
                    {
                        aux = 0;
                        for (i = -rx; i <= rx; i++)
                            for (j = -ry; j <= ry; j++)
                            {
                                max = img.MatrizCor.Matriz[x + i, y + j, c] + (ee[rx + i, ry + j]);
                                if (max > aux)
                                    aux = max;
                            }
                        saida.MatrizCor.Matriz[x, y, c] = aux;
                    }

            this.MatrizCor = saida.MatrizCor;
        }





    }
}

