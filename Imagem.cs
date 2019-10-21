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
        public ImageType imageType = ImageType.color;
        public int niveisCinza = 256;
        public ImagemBool MatrizBool;
        public ImagemInt MatrizCor;
        public ImagemGray MatrizGray;
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
                imageType = ImageType.color;
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
            if (this.imageType != ImageType.integer) { this.ToInt(); }
            for (x = 0; x < this.MatrizCor.Width; x++)
            {
                for (y = 0; y < this.MatrizCor.Height; y++)
                {
                    for (canal = 0; canal < 3; canal++)
                    {
                        MatrizCor.Matriz[x, y, canal] = (255 - MatrizCor.Matriz[x, y, canal]);
                    }
                }
            }
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
            if (this.imageType == ImageType.color)
                this.ToInt();
            if (this.imageType == ImageType.binary)
                this.ToGray();
            if (this.imageType == ImageType.gray)
            {
                int x, y, h = MatrizGray.Height, w = MatrizGray.Width;
                int max = MatrizGray.Matriz.Cast<int>().Max();
                int min = MatrizGray.Matriz.Cast<int>().Min();
                if (max > 255 || min < 0)
                {
                    if (tipo == Correcao.limiar)
                        for (x = 0; x < w; x++)
                            for (y = 0; y < h; y++)
                            {
                                if (MatrizGray.Matriz[x, y] > 255)
                                    MatrizGray.Matriz[x, y] = 255;
                                if (MatrizGray.Matriz[x, y] < 0)
                                    MatrizGray.Matriz[x, y] = 0;
                            }
                    else
                    {
                        for (x = 0; x < w; x++)
                            for (y = 0; y < h; y++)
                                MatrizGray.Matriz[x, y] = (MatrizGray.Matriz[x, y] - min) * 255 / (max - min);
                    }
                }
            }
            else
            {
                int canal, x, y, h = MatrizCor.Height, w = MatrizCor.Width;
                int[,] minmaxRGB = GetMinMax();
                bool cond = false;
                if (minmaxRGB[0, 0] > 255 || minmaxRGB[0, 1] > 255 || minmaxRGB[0, 2] > 255 || minmaxRGB[1, 0] < 0 || minmaxRGB[1, 1] < 0 || minmaxRGB[1, 2] < 0)
                {
                    cond = true;
                }
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
            this.imageType = ImageType.integer;

        }
        public void ToBool()
        {
            if (this.imageType != ImageType.binary)
            {
                int x, y;
                this.ToGray();
                MatrizBool.Matriz = new bool[MatrizGray.Width, MatrizGray.Height];
                MatrizBool.Width = MatrizGray.Width;
                MatrizBool.Height = MatrizGray.Height;
                int w = MatrizGray.Width, h = MatrizGray.Height;
                for (x = 0; x < w; x++)
                    for (y = 0; y < h; y++)
                        MatrizBool.Matriz[x, y] = (MatrizGray.Matriz[x, y] > 127) ? true : false;
                imageType = ImageType.binary;
                niveisCinza = 2;
                this.ClearMatriz();
            }
        }
        public void ToInt()
        {
            if (imageType != ImageType.integer)
            {
                int x, y;
                int w = new int[] { ImagemBMP.Width, MatrizBool.Width, MatrizGray.Width }.Max(), h = new int[] { ImagemBMP.Height, MatrizBool.Height, MatrizGray.Height }.Max();
                MatrizCor.Matriz = new int[w, h, 3];
                MatrizCor.Width = w;
                MatrizCor.Height = h;
                int canal;
                switch (this.imageType)
                {
                    case ImageType.color:
                        Color color;
                        for (x = 0; x < ImagemBMP.Width; x++)
                            for (y = 0; y < ImagemBMP.Height; y++)
                            {
                                color = ImagemBMP.GetPixel(x, y);
                                MatrizCor.Matriz[x, y, 0] = (int)color.R;
                                MatrizCor.Matriz[x, y, 1] = (int)color.G;
                                MatrizCor.Matriz[x, y, 2] = (int)color.B;
                            }
                        break;
                    case ImageType.binary:
                        for (x = 0; x < MatrizCor.Width; x++)
                            for (y = 0; y < MatrizCor.Height; y++)
                                for (canal = 0; canal < 3; canal++)
                                    MatrizCor.Matriz[x, y, canal] = MatrizBool.Matriz[x, y] ? 255 : 0;
                        break;
                    case ImageType.gray:
                        for (x = 0; x < MatrizCor.Width; x++)
                            for (y = 0; y < MatrizCor.Height; y++)
                                for (canal = 0; canal < 3; canal++)
                                    MatrizCor.Matriz[x, y, canal] = MatrizGray.Matriz[x, y];
                        break;
                }
                imageType = ImageType.integer;
                this.ClearMatriz();
            }
        }
        public void ToImage()
        {
            int r, x, y;
            switch (imageType)
            {
                case ImageType.binary:
                    ImagemBMP = new Bitmap(MatrizBool.Width, MatrizBool.Height);
                    for (x = 0; x < MatrizBool.Width; x++)
                        for (y = 0; y < MatrizBool.Height; y++)
                        {
                            r = MatrizBool.Matriz[x, y] ? 255 : 0;
                            ImagemBMP.SetPixel(x, y, Color.FromArgb(r, r, r));
                        }
                    break;
                case ImageType.integer:
                    ImagemBMP = new Bitmap(MatrizCor.Width, MatrizCor.Height);
                    for (x = 0; x < MatrizCor.Width; x++)
                        for (y = 0; y < MatrizCor.Height; y++)
                            ImagemBMP.SetPixel(x, y, Color.FromArgb(MatrizCor.Matriz[x, y, 0], MatrizCor.Matriz[x, y, 1], MatrizCor.Matriz[x, y, 2]));
                    break;
                case ImageType.gray:
                    ImagemBMP = new Bitmap(MatrizGray.Width, MatrizGray.Height);
                    for (x = 0; x < MatrizGray.Width; x++)
                        for (y = 0; y < MatrizGray.Height; y++)
                            ImagemBMP.SetPixel(x, y, Color.FromArgb(MatrizGray.Matriz[x, y], MatrizGray.Matriz[x, y], MatrizGray.Matriz[x, y]));
                    break;
            }

            imageType = ImageType.color;
            this.ClearMatriz();
        }
        public void ToGray()
        {
            if (this.imageType != ImageType.gray)
            {
                this.ToInt();
                int gray = 0, canal, i, j;
                double[] factors = { 0.299, 0.587, 0.144 };
                MatrizGray.Matriz = new int[MatrizCor.Width, MatrizCor.Height];
                MatrizGray.Width = MatrizCor.Width;
                MatrizGray.Height = MatrizCor.Height;
                for (i = 0; i < this.MatrizCor.Width; i++)
                    for (j = 0; j < this.MatrizCor.Height; j++)
                    {
                        for (canal = 0; canal < 3; canal++)
                            gray += (int)(factors[canal] * (double)MatrizCor.Matriz[i, j, canal]);
                        if (gray <= 255)
                            MatrizGray.Matriz[i, j] = gray;
                        else
                            MatrizGray.Matriz[i, j] = 255;
                        gray = 0;
                    }
                imageType = ImageType.gray;
                this.ClearMatriz();
            }
        }
        private void ClearMatriz()
        {
            switch (imageType)
            {
                case ImageType.binary:
                    MatrizCor.Matriz = null;
                    MatrizGray.Matriz = null;
                    ImagemBMP = null;
                    imageType = ImageType.binary;

                    break;
                case ImageType.integer:
                    MatrizGray.Matriz = null;
                    MatrizBool.Matriz = null;
                    ImagemBMP = null;
                    imageType = ImageType.integer;
                    break;
                case ImageType.gray:
                    MatrizBool.Matriz = null;
                    MatrizCor.Matriz = null;
                    ImagemBMP = null;
                    imageType = ImageType.gray;
                    break;
                case ImageType.color:
                    MatrizBool.Matriz = null;
                    MatrizCor.Matriz = null;
                    MatrizGray.Matriz = null;
                    imageType = ImageType.color;
                    break;
            }
        }
        public void ToQuant(int niveis)
        {
            int canal, x, y, h = Math.Max(this.MatrizCor.Height, this.MatrizGray.Height), w = Math.Max(this.MatrizCor.Width, this.MatrizGray.Width);
            switch (this.imageType)
            {
                case ImageType.integer:
                    for (x = 0; x < w; x++)
                        for (y = 0; y < h; y++)
                            for (canal = 0; canal < 3; canal++)
                            {
                                this.MatrizCor.Matriz[x, y, canal] /= (256 / niveis);
                                this.MatrizCor.Matriz[x, y, canal] *= (256 / niveis);
                            }
                    break;
                case ImageType.gray:
                    for (x = 0; x < w; x++)
                        for (y = 0; y < h; y++)
                        {
                            this.MatrizGray.Matriz[x, y] /= (256 / niveis);
                            this.MatrizGray.Matriz[x, y] *= (256 / niveis);
                        }
                    break;
            }
            this.niveisCinza = niveis;

        }
        public void FiltroMediana(int raio)
        {
            if (this.imageType != ImageType.integer) { this.ToInt(); }
            int size = ((2 * raio + 1) * (2 * raio + 1));
            int[] r = new int[size];
            int x, y, i, j, pos = 0, canal;
            int w = MatrizCor.Width - 1;
            int h = MatrizCor.Height - 1;
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
            if (this.imageType != ImageType.integer) { this.ToInt(); }
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
            if (this.imageType != ImageType.integer) { this.ToInt(); }
            int[,] maskX = new int[3, 3] { { -1, -1, -1 },
                                           { -1, 8, -1 },
                                           { -1, -1, -1 } };

            int rx = 0, x, y, i, j, k = 0, l = 0, c = 0;

            int width = MatrizCor.Width, height = MatrizCor.Height;
            int[,,] G = new int[width, height, 3];

            for (x = 1; x < width - 1; x++)
                for (y = 1; y < height - 1; y++)
                    for (c = 0; c <= 2; c++)
                    {
                        for (i = x - 1; i < x + 2; i++)
                        {
                            for (j = y - 1; j < y + 2; j++)
                            {
                                rx += (MatrizCor.Matriz[i, j, c] * maskX[k, l]);
                                k++;
                            }
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
            this.ToInt();
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
            this.ToBool();
            if (imagemB == null)
                imagemB = this;
            imagemB.ToBool();
            int x, y;
            for (x = 0; x < imagemB.MatrizBool.Width; x++)
                for (y = 0; y < imagemB.MatrizBool.Height; y++)
                    switch (operation)
                    {
                        case LogicOperationType.not:
                            this.MatrizBool.Matriz[x, y] = !this.MatrizBool.Matriz[x, y];
                            break;
                        case LogicOperationType.and:
                            this.MatrizBool.Matriz[x, y] = this.MatrizBool.Matriz[x, y] && imagemB.MatrizBool.Matriz[x, y];
                            break;
                        case LogicOperationType.or:
                            this.MatrizBool.Matriz[x, y] = this.MatrizBool.Matriz[x, y] || imagemB.MatrizBool.Matriz[x, y];
                            break;
                        case LogicOperationType.sub:
                            this.MatrizBool.Matriz[x, y] = this.MatrizBool.Matriz[x, y] && !imagemB.MatrizBool.Matriz[x, y];
                            break;
                        case LogicOperationType.xor:
                            this.MatrizBool.Matriz[x, y] = (this.MatrizBool.Matriz[x, y] && !imagemB.MatrizBool.Matriz[x, y]) || (!this.MatrizBool.Matriz[x, y] && imagemB.MatrizBool.Matriz[x, y]);
                            break;
                    }
        }

        public void MathOp(MathOperationType operation, Imagem imagemB)
        {
            if (this.imageType != ImageType.integer)
                this.ToInt();
            if (imagemB != null)
            {
                imagemB.ToInt();
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

        public void CorrecaoHistograma()
        {
            if (this.imageType != ImageType.integer) { this.ToInt(); }
            int[] histograma = new int[256];
            int[] histogramaAc = new int[256];
            int[] histogramaCalc = new int[256];
            int x, y, width = this.MatrizCor.Width, height = this.MatrizCor.Height;
            for (x = 0; x < width; x++)
                for (y = 0; y < height; y++)
                    histograma[MatrizCor.Matriz[x, y, 0]]++;
            histogramaAc[0] = histograma[0];
            for (x = 1; x < 256; x++)
                histogramaAc[x] = histogramaAc[x - 1] + histograma[x];
            for (x = 0; x < 256; x++)
                histogramaCalc[x] = (int)(255 * histogramaAc[x] / (width * height));
            int aux;
            for (x = 0; x < width; x++)

                for (y = 0; y < height; y++)
                {
                    aux = histogramaCalc[MatrizCor.Matriz[x, y, 0]];
                    MatrizCor.Matriz[x, y, 0] = aux;
                    MatrizCor.Matriz[x, y, 1] = aux;
                    MatrizCor.Matriz[x, y, 2] = aux;
                }
        }

        public void ToLimiar(int limiar)
        {
            this.ToGray();
            int x, y, h = MatrizGray.Height, w = MatrizGray.Width;
            for (x = 0; x < w; x++)
                for (y = 0; y < h; y++)
                    MatrizGray.Matriz[x, y] = (MatrizGray.Matriz[x, y] > limiar) ? 255 : 0;

        }
        public void ToLimiarAleatorio(int limiar, int limRandomInf, int limRandomSup)
        {
            this.ToGray();
            int x, y, h = MatrizGray.Height, w = MatrizGray.Width;
            Random rnd = new Random();
            int rand, temp;
            for (x = 0; x < w; x++)
                for (y = 0; y < h; y++)
                {
                    rand = rnd.Next(limRandomInf, limRandomSup + 1);
                    temp = MatrizGray.Matriz[x, y] + rand;
                    MatrizGray.Matriz[x, y] = (temp > limiar) ? 255 : 0;
                }
        }

        public void ToPeriodicoDispersao(int dispersao)
        {
            int[,] D2 = new int[,] { { 0 * 64, 2 * 64 },
                                     { 3 * 64, 1 * 64 } };

            int[,] D3 = new int[,] { { 06 * 28, 04 * 28, 08 * 28 },
                                     { 01 * 28, 00 * 28, 03 * 28 },
                                     { 05 * 28, 02 * 28, 07 * 28 } };

            int[,] D4 = new int[,] { { 00 * 16, 08 * 16, 02 * 16, 10 * 16 },
                                     { 12 * 16, 04 * 16, 14 * 16, 06 * 16 },
                                     { 03 * 16, 11 * 16, 01 * 16, 09 * 16 },
                                     { 15 * 16, 07 * 16, 13 * 16, 05 * 16 } };

            List<int[,]> D = new List<int[,]> { D2, D3, D4 };
            this.ToGray();
            int i, j, x, y, h = MatrizGray.Height, w = MatrizGray.Width;
            for (x = 0; x < w; x++)
                for (y = 0; y < h; y++)
                {
                    i = x % dispersao;
                    j = y % dispersao;
                    if (MatrizGray.Matriz[x, y] > D[dispersao - 2][i, j])
                        MatrizGray.Matriz[x, y] = 255;
                    else
                        MatrizGray.Matriz[x, y] = 0;
                }
        }

        public void ToAperiodicoDispersao(int vizinhos)
        {
            this.ToGray();
            int x, y, h = MatrizGray.Height - 1, w = MatrizGray.Width - 1, erro;
            int media = GetMedia();
            if (vizinhos == 3)
                for (y = 0; y < h; y++)
                    for (x = 0; x < w; x++)
                    {
                        if (MatrizGray.Matriz[x, y] < media)
                        {
                            erro = MatrizGray.Matriz[x, y];
                            MatrizGray.Matriz[x, y] = 0;
                        }
                        else
                        {
                            erro = MatrizGray.Matriz[x, y] - 255;
                            MatrizGray.Matriz[x, y] = 255;
                        }
                        MatrizGray.Matriz[x + 1, y] += (erro * 3 / 8);
                        MatrizGray.Matriz[x, y + 1] += (erro * 3 / 8);
                        MatrizGray.Matriz[x + 1, y + 1] += (erro * 2 / 8);
                    }
            else
                for (y = 0; y < h; y++)
                {
                    for (x = 1; x < w; x++)
                    {
                        if (MatrizGray.Matriz[x, y] < media)
                        {
                            erro = MatrizGray.Matriz[x, y];
                            MatrizGray.Matriz[x, y] = 0;
                        }
                        else
                        {
                            erro = MatrizGray.Matriz[x, y] - 255;
                            MatrizGray.Matriz[x, y] = 255;
                        }
                        MatrizGray.Matriz[x + 1, y] += (erro * 7 / 16);
                        MatrizGray.Matriz[x - 1, y + 1] += (erro * 3 / 16);
                        MatrizGray.Matriz[x, y + 1] += (erro * 5 / 16);
                        MatrizGray.Matriz[x + 1, y + 1] += (erro * 1 / 16);
                    }
                }
            this.CorrecaoMinMax(Correcao.proporcao);
        }
        public void ToPeriodicoAglomeracao(int dispersao)
        {
            int[,] D2 = new int[,] { { 0 * 64, 2 * 64 },
                                     { 3 * 64, 1 * 64 } };

            int[,] D3 = new int[,] { { 06 * 28, 04 * 28, 08 * 28 },
                                     { 01 * 28, 00 * 28, 03 * 28 },
                                     { 05 * 28, 02 * 28, 07 * 28 } };

            int[,] D4 = new int[,] { { 00 * 16, 08 * 16, 02 * 16, 10 * 16 },
                                     { 12 * 16, 04 * 16, 14 * 16, 06 * 16 },
                                     { 03 * 16, 11 * 16, 01 * 16, 09 * 16 },
                                     { 15 * 16, 07 * 16, 13 * 16, 05 * 16 } };

            List<int[,]> D = new List<int[,]> { D2, D3, D4 };
            if (this.imageType == ImageType.gray)
            {
                var Matriz = new int[MatrizGray.Width * dispersao, MatrizGray.Width * dispersao];
                int i, j, x, y, h = MatrizGray.Height * dispersao, w = MatrizGray.Width * dispersao;
                for (x = 0; x < w; x++)
                    for (y = 0; y < h; y++)
                    {
                        i = x % dispersao;
                        j = y % dispersao;
                        if (MatrizGray.Matriz[x / dispersao, y / dispersao] > D[dispersao - 2][i, j])
                            Matriz[x, y] = 255;
                        else
                            Matriz[x, y] = 0;
                    }
                MatrizGray.Matriz = Matriz;
                MatrizGray.Width *= dispersao;
                MatrizGray.Height *= dispersao;
            }
            else
            {
                this.ToInt();
                var Matriz = new int[MatrizCor.Width * dispersao, MatrizCor.Width * dispersao, 3];
                int i, j, x, y, c, h = MatrizCor.Height * dispersao, w = MatrizCor.Width * dispersao;
                for (x = 0; x < w; x++)
                    for (y = 0; y < h; y++)
                    {
                        i = x % dispersao;
                        j = y % dispersao;
                        for (c = 0; c < 3; c++)
                            if (MatrizCor.Matriz[x / dispersao, y / dispersao, c] > D[dispersao - 2][i, j])
                                Matriz[x, y, c] = 255;
                            else
                                Matriz[x, y, c] = 0;
                    }

                MatrizCor.Matriz = Matriz;
                MatrizCor.Width *= dispersao;
                MatrizCor.Height *= dispersao;
            }
        }
        public int GetMedia()
        {
            int x, y, h = MatrizGray.Height, w = MatrizGray.Width;
            long m = 0;
            for (x = 0; x < w; x++)
                for (y = 0; y < h; y++)
                    m += (long)MatrizGray.Matriz[x, y];
            return (int)(m / (w * h));
        }
        public int[,] GetMinMax()
        {
            int[,] RGB = { { 255, 255, 255 }, { 0, 0, 0 } };
            int x, y, c;

            for (x = 0; x < MatrizCor.Width; x++)
                for (y = 0; y < MatrizCor.Height; y++)
                    for (c = 0; c <= 2; c++)
                    {
                        if (MatrizCor.Matriz[x, y, c] > RGB[0, c])
                            RGB[0, c] = MatrizCor.Matriz[x, y, c];
                        if (MatrizCor.Matriz[x, y, c] < RGB[1, c])
                            RGB[0, c] = MatrizCor.Matriz[x, y, c];
                    }
            return RGB;
        }
        public void Stretching(StretchingType stretching, double A, double B)
        {
            this.ToInt();
            int x, y, c, h = MatrizCor.Height, w = MatrizCor.Width;
            for (x = 0; x < w; x++)
                for (y = 0; y < h; y++)
                    for (c = 0; c <= 2; c++)
                        switch (stretching)
                        {
                            case StretchingType.linear:
                                MatrizCor.Matriz[x, y, c] = (int)(MatrizCor.Matriz[x, y, c] * A + B);
                                break;
                            case StretchingType.log:
                                MatrizCor.Matriz[x, y, c] = (int)(A * Math.Log10(MatrizCor.Matriz[x, y, 0] + 1));
                                break;
                            case StretchingType.neg:
                                MatrizCor.Matriz[x, y, c] = (int)(-(MatrizCor.Matriz[x, y, 0] * A + B));
                                break;
                            case StretchingType.quad:
                                MatrizCor.Matriz[x, y, c] = (int)(A * Math.Pow(MatrizCor.Matriz[x, y, c], 2));
                                break;
                            case StretchingType.srqt:
                                MatrizCor.Matriz[x, y, c] = (int)(A * Math.Sqrt(MatrizCor.Matriz[x, y, c]));
                                break;
                        }
        }
    }
}
