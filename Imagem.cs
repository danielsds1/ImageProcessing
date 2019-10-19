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
                int max = MatrizCor.Matriz.Cast<int>().Max();
                int min = MatrizCor.Matriz.Cast<int>().Min();
                if (max > 255 || min < 00)
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
                                    MatrizCor.Matriz[x, y, canal] = (MatrizCor.Matriz[x, y, canal] - min) * 255 / (max - min);
                    }
                }
            }
        }

        public void CreatePlainImage( int width, int height, int value)
        {
            int x, y, c;
            MatrizCor = new ImagemInt
            {
                Height = height,
                Width = width,
                Matriz = new int[width, height,3] 
            };
            for (x = 0; x < width; x++)
                for (y = 0; y < height; y++)
                    for (c = 0; c <= 2; c++)
                        MatrizCor.Matriz[x, y, c] = value;
            this.imageType = ImageType.integer;

        }
        public void ToBool()
        {
            int x, y;
            if (this.imageType != ImageType.gray) { this.ToGray(); }
            MatrizBool.Matriz = new bool[MatrizGray.Width, MatrizGray.Height];
            MatrizBool.Width = ImagemBMP.Width;
            MatrizBool.Height = ImagemBMP.Height;
            int w = MatrizGray.Width, h = MatrizGray.Height;
            for (x = 0; x < w; x++)
            {
                for (y = 0; y < h; y++)
                {
                    MatrizBool.Matriz[x, y] = (MatrizGray.Matriz[x, y] > 127) ? true : false;
                }
            }
            imageType = ImageType.binary;
            niveisCinza = 2;
            this.ClearMatriz();
        }
        public void ToInt()
        {
            int x, y;
            int canal = 0;
            if (imageType != ImageType.integer)
            {
                MatrizCor.Matriz = new int[ImagemBMP.Width, ImagemBMP.Height, 3];
                MatrizCor.Width = ImagemBMP.Width;
                MatrizCor.Height = ImagemBMP.Height;
            }
            switch (this.imageType)
            {
                case ImageType.color:
                    Color color;

                    for (x = 0; x < ImagemBMP.Width; x++)
                    {
                        for (y = 0; y < ImagemBMP.Height; y++)
                        {
                            color = ImagemBMP.GetPixel(x, y);
                            MatrizCor.Matriz[x, y, 0] = (int)color.R;
                            MatrizCor.Matriz[x, y, 1] = (int)color.G;
                            MatrizCor.Matriz[x, y, 2] = (int)color.B;
                        }
                    }
                    break;
                case ImageType.binary:

                    for (x = 0; x < MatrizCor.Width; x++)
                    {
                        for (y = 0; y < MatrizCor.Height; y++)
                        {
                            for (canal = 0; canal < 3; canal++)
                            {
                                MatrizCor.Matriz[x, y, canal] = MatrizBool.Matriz[x, y] ? 255 : 0;
                            }
                        }
                    }
                    break;
                case ImageType.gray:
                    for (x = 0; x < MatrizCor.Width; x++)
                    {
                        for (y = 0; y < MatrizCor.Height; y++)
                        {
                            for (canal = 0; canal < 3; canal++)
                            {
                                MatrizCor.Matriz[x, y, canal] = MatrizGray.Matriz[x, y];
                            }
                        }
                    }
                    break;
            }

            imageType = ImageType.integer;
            this.ClearMatriz();
        }
        public void ToImage()
        {
            int r, x, y;
            switch (imageType)
            {
                case ImageType.binary:
                    ImagemBMP = new Bitmap(MatrizBool.Width, MatrizBool.Height);
                    for (x = 0; x < MatrizBool.Width; x++)
                    {
                        for (y = 0; y < MatrizBool.Height; y++)
                        {
                            r = MatrizBool.Matriz[x, y] ? 255 : 0;
                            ImagemBMP.SetPixel(x, y, Color.FromArgb(r, r, r));
                        }
                    }
                    break;
                case ImageType.integer:
                    ImagemBMP = new Bitmap(MatrizCor.Width, MatrizCor.Height);
                    for (x = 0; x < MatrizCor.Width; x++)
                    {
                        for (y = 0; y < MatrizCor.Height; y++)
                        {
                            ImagemBMP.SetPixel(x, y, Color.FromArgb(MatrizCor.Matriz[x, y, 0], MatrizCor.Matriz[x, y, 1], MatrizCor.Matriz[x, y, 2]));
                        }
                    }
                    break;
                case ImageType.gray:
                    ImagemBMP = new Bitmap(MatrizGray.Width, MatrizGray.Height);
                    for (x = 0; x < MatrizCor.Width; x++)
                    {
                        for (y = 0; y < MatrizCor.Height; y++)
                        {
                            ImagemBMP.SetPixel(x, y, Color.FromArgb(MatrizGray.Matriz[x, y], MatrizGray.Matriz[x, y], MatrizGray.Matriz[x, y]));
                        }
                    }
                    break;
            }

            imageType = ImageType.color;
            this.ClearMatriz();
        }
        public void ToGray()
        {
            if (this.imageType != ImageType.integer)
            {
                this.ToInt();
            }
            this.ToInt();
            int gray = 0, canal, i, j;
            double[] factors = { 0.299, 0.587, 0.144 };
            MatrizGray.Matriz = new int[MatrizCor.Width, MatrizCor.Height];
            MatrizGray.Width = MatrizCor.Width;
            MatrizGray.Height = MatrizCor.Height;
            for (i = 0; i < this.MatrizCor.Width; i++)
            {
                for (j = 0; j < this.MatrizCor.Height; j++)
                {
                    for (canal = 0; canal < 3; canal++)
                    {
                        gray += (int)(factors[canal] * (double)MatrizCor.Matriz[i, j, canal]);
                    }
                    MatrizGray.Matriz[i, j] = gray;
                    gray = 0;
                }
            }

            imageType = ImageType.gray;
            this.ClearMatriz();
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
            //if (this.imageType != ImageType.gray) { this.ToGray(); }

            int canal, x, y, h = this.MatrizCor.Height, w = this.MatrizCor.Width;
            if (this.imageType == ImageType.integer)
            {
                for (x = 0; x < w; x++)
                {
                    for (y = 0; y < h; y++)
                    {
                        for (canal = 0; canal < 3; canal++)
                        {
                            this.MatrizCor.Matriz[x, y, canal] /= (256 / niveis);
                            this.MatrizCor.Matriz[x, y, canal] *= (256 / niveis);
                        }
                    }
                }
            }
            else if (this.imageType == ImageType.gray)
            {
                for (x = 0; x < w; x++)
                {
                    for (y = 0; y < h; y++)
                    {
                        this.MatrizGray.Matriz[x, y] /= (256 / niveis);
                        this.MatrizGray.Matriz[x, y] *= (256 / niveis);
                    }
                }
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

        public Image FiltroPassaAlta()
        {
            if (this.imageType != ImageType.integer) { this.ToInt(); }
            int[,] maskX = new int[3, 3] { { -1, -1, -1 },
                                           { -1, 8, -1 },
                                           { -1, -1, -1 } };

            int rx = 0, gx = 0, bx = 0, x, y, i, j, k = 0, l = 0;

            int width = ImagemBMP.Width;
            int height = ImagemBMP.Height;
            int[,,] G = new int[width, height, 3];

            for (x = 1; x < width - 1; x++)
            {
                for (y = 1; y < height - 1; y++)
                {
                    for (i = x - 1; i < x + 2; i++)
                    {
                        for (j = y - 1; j < y + 2; j++)
                        {
                            rx += (MatrizCor.Matriz[i, j, 0] * maskX[k, l]);
                            gx += MatrizCor.Matriz[i, j, 1] * maskX[k, l];
                            bx += MatrizCor.Matriz[i, j, 2] * maskX[k, l];
                            k++;
                        }
                        k = 0;
                        l++;
                    }

                    //aux= (int)Math.Sqrt(Math.Pow((double)rx,2.0) + Math.Pow((double)ry,2.0))/9;

                    G[x, y, 0] = (int)Math.Abs(rx) / 9;
                    G[x, y, 1] = (int)Math.Abs(gx) / 9;
                    G[x, y, 2] = (int)Math.Abs(bx) / 9;

                    rx = 0; gx = 0; bx = 0;
                    l = 0;
                }
            }
            MatrizCor.Matriz = G;
            this.ToImage();
            return (Image)ImagemBMP;
        }

        public Image BordasSobel()
        {
            if (this.imageType != ImageType.integer) { this.ToInt(); }
            double[,] maskX = new double[3, 3] { { -1, 00, 01 },
                                           { -2, 00, 02 },
                                           { -1, 00, 01 } };
            double[,] maskY = new double[3, 3] { { -1, -2, -1 },
                                           { 00, 00, 00 },
                                           { 01, 02, 01 } };

            double rx = 0, gx = 0, bx = 0, ry = 0, gy = 0, by = 0;
            int x, y, i, j, k = 0, l = 0;

            int width = ImagemBMP.Width;
            int height = ImagemBMP.Height;
            int[,,] G = new int[width, height, 3];

            for (x = 1; x < width - 1; x++)
            {
                for (y = 1; y < height - 1; y++)
                {
                    for (i = x - 1; i < x + 2; i++)
                    {
                        for (j = y - 1; j < y + 2; j++)
                        {
                            rx += (MatrizCor.Matriz[i, j, 0] * maskX[k, l]);
                            gx += (MatrizCor.Matriz[i, j, 1] * maskX[k, l]);
                            bx += (MatrizCor.Matriz[i, j, 2] * maskX[k, l]);
                            ry += (MatrizCor.Matriz[i, j, 0] * maskY[k, l]);
                            gy += (MatrizCor.Matriz[i, j, 1] * maskY[k, l]);
                            by += (MatrizCor.Matriz[i, j, 2] * maskY[k, l]);
                            k++;
                        }
                        k = 0;
                        l++;
                    }

                    G[x, y, 0] = (int)Math.Sqrt(Math.Pow(rx, 2.0) + Math.Pow(ry, 2.0)) / 9;
                    G[x, y, 1] = (int)Math.Sqrt(Math.Pow(gx, 2.0) + Math.Pow(gy, 2.0)) / 9;
                    G[x, y, 2] = (int)Math.Sqrt(Math.Pow(bx, 2.0) + Math.Pow(by, 2.0)) / 9;

                    rx = 0; gx = 0; bx = 0;
                    ry = 0; gy = 0; by = 0;
                    l = 0;
                }
            }
            MatrizCor.Matriz = G;
            this.ToImage();
            return (Image)ImagemBMP;
        }
        public Image BordasPrewitt()
        {
            if (this.imageType != ImageType.integer) { this.ToInt(); }
            double[,] maskX = new double[3, 3] { { -1, -1, -1 },
                                                 { 00, 00, 00 },
                                                 { 01, 01, 01 } };
            double[,] maskY = new double[3, 3] { { -1, 00, 01 },
                                                 { -1, 00, 01 },
                                                 { -1, 00, 01 } };

            double rx = 0, gx = 0, bx = 0, ry = 0, gy = 0, by = 0;
            int x, y, i, j, k = 0, l = 0;

            int width = ImagemBMP.Width;
            int height = ImagemBMP.Height;
            int[,,] G = new int[width, height, 3];

            for (x = 1; x < width - 1; x++)
            {
                for (y = 1; y < height - 1; y++)
                {
                    for (i = x - 1; i < x + 2; i++)
                    {
                        for (j = y - 1; j < y + 2; j++)
                        {
                            rx += (MatrizCor.Matriz[i, j, 0] * maskX[k, l]);
                            gx += (MatrizCor.Matriz[i, j, 1] * maskX[k, l]);
                            bx += (MatrizCor.Matriz[i, j, 2] * maskX[k, l]);
                            ry += (MatrizCor.Matriz[i, j, 0] * maskY[k, l]);
                            gy += (MatrizCor.Matriz[i, j, 1] * maskY[k, l]);
                            by += (MatrizCor.Matriz[i, j, 2] * maskY[k, l]);
                            k++;
                        }
                        k = 0;
                        l++;
                    }

                    G[x, y, 0] = (int)Math.Sqrt(Math.Pow(rx, 2.0) + Math.Pow(ry, 2.0)) / 9;
                    G[x, y, 1] = (int)Math.Sqrt(Math.Pow(gx, 2.0) + Math.Pow(gy, 2.0)) / 9;
                    G[x, y, 2] = (int)Math.Sqrt(Math.Pow(bx, 2.0) + Math.Pow(by, 2.0)) / 9;

                    rx = 0; gx = 0; bx = 0;
                    ry = 0; gy = 0; by = 0;
                    l = 0;
                }
            }
            MatrizCor.Matriz = G;
            this.ToImage();
            return (Image)ImagemBMP;
        }
        public Image BordasRoberts()
        {
            if (this.imageType != ImageType.integer) { this.ToInt(); }
            double[,] maskX = new double[2, 2] { { 1, 0},
                                                 { 0, -1}};
            double[,] maskY = new double[2, 2] { { 0, 1},
                                                 { -1, 0}};

            double rx = 0, gx = 0, bx = 0, ry = 0, gy = 0, by = 0;
            int x, y, i, j, k = 0, l = 0;

            int width = ImagemBMP.Width;
            int height = ImagemBMP.Height;
            int[,,] G = new int[width, height, 3];

            for (x = 0; x < width - 1; x++)
            {
                for (y = 0; y < height - 1; y++)
                {
                    for (i = x; i < x + 2; i++)
                    {
                        for (j = y; j < y + 2; j++)
                        {
                            rx += (MatrizCor.Matriz[i, j, 0] * maskX[k, l]);
                            gx += (MatrizCor.Matriz[i, j, 1] * maskX[k, l]);
                            bx += (MatrizCor.Matriz[i, j, 2] * maskX[k, l]);
                            ry += (MatrizCor.Matriz[i, j, 0] * maskY[k, l]);
                            gy += (MatrizCor.Matriz[i, j, 1] * maskY[k, l]);
                            by += (MatrizCor.Matriz[i, j, 2] * maskY[k, l]);
                            k++;
                        }
                        k = 0;
                        l++;
                    }

                    G[x, y, 0] = (int)Math.Sqrt(Math.Pow(rx, 2.0) + Math.Pow(ry, 2.0)) / 4;
                    G[x, y, 1] = (int)Math.Sqrt(Math.Pow(gx, 2.0) + Math.Pow(gy, 2.0)) / 4;
                    G[x, y, 2] = (int)Math.Sqrt(Math.Pow(bx, 2.0) + Math.Pow(by, 2.0)) / 4;

                    rx = 0; gx = 0; bx = 0;
                    ry = 0; gy = 0; by = 0;
                    l = 0;
                }
            }
            MatrizCor.Matriz = G;
            this.ToImage();
            return (Image)ImagemBMP;
        }
        public Image BordasIsotropico()
        {
            if (this.imageType != ImageType.integer) { this.ToInt(); }
            double[,] maskX = new double[3, 3] { { -1, 00, 01 },
                                           { -1.4142135624, 00, 1.4142135624 },
                                           { -1, 00, 01 } };
            double[,] maskY = new double[3, 3] { { -1, -1.4142135624, -1 },
                                           { 00, 00, 00 },
                                           { 01, 1.4142135624, 01 } };

            //double[,] maskX = new double[3, 3] { { -1.0, 0.0, 1.0 },{ -2‬, 0.0, -2 },{ -1.0, 0.0, 1.0 }};
            //double[,] maskY = new double[3, 3] { { -1.0, -2, -1.0 },{ 0.0, 0.0, 0.0 },{ 01, 1.4142135624, 01 }};

            double rx = 0, gx = 0, bx = 0, ry = 0, gy = 0, by = 0;
            int x, y, i, j, k = 0, l = 0;

            int width = ImagemBMP.Width;
            int height = ImagemBMP.Height;
            int[,,] G = new int[width, height, 3];

            for (x = 1; x < width - 1; x++)
            {
                for (y = 1; y < height - 1; y++)
                {
                    for (i = x - 1; i < x + 2; i++)
                    {
                        for (j = y - 1; j < y + 2; j++)
                        {
                            rx += (MatrizCor.Matriz[i, j, 0] * maskX[k, l]);
                            gx += (MatrizCor.Matriz[i, j, 1] * maskX[k, l]);
                            bx += (MatrizCor.Matriz[i, j, 2] * maskX[k, l]);
                            ry += (MatrizCor.Matriz[i, j, 0] * maskY[k, l]);
                            gy += (MatrizCor.Matriz[i, j, 1] * maskY[k, l]);
                            by += (MatrizCor.Matriz[i, j, 2] * maskY[k, l]);
                            k++;
                        }
                        k = 0;
                        l++;
                    }

                    G[x, y, 0] = (int)Math.Sqrt(Math.Pow(rx, 2.0) + Math.Pow(ry, 2.0)) / 9;
                    G[x, y, 1] = (int)Math.Sqrt(Math.Pow(gx, 2.0) + Math.Pow(gy, 2.0)) / 9;
                    G[x, y, 2] = (int)Math.Sqrt(Math.Pow(bx, 2.0) + Math.Pow(by, 2.0)) / 9;

                    rx = 0; gx = 0; bx = 0;
                    ry = 0; gy = 0; by = 0;
                    l = 0;
                }
            }
            MatrizCor.Matriz = G;
            this.ToImage();
            return (Image)ImagemBMP;
        }
        public Image BordasLaplace()
        {
            if (this.imageType != ImageType.integer) { this.ToInt(); }
            double[,] maskX = new double[3, 3] {{  0, -1,  0 },
                                                { -1,  4, -1 },
                                                {  0, -1,  0 }};

            double[,] maskY = new double[3, 3] {{ -1, -1, -1 },
                                                { -1,  8, -1 },
                                                { -1, -1, -1 }};

            double rx = 0, gx = 0, bx = 0, ry = 0, gy = 0, by = 0;
            int x, y, i, j, k = 0, l = 0;

            int width = ImagemBMP.Width;
            int height = ImagemBMP.Height;
            int[,,] G = new int[width, height, 3];

            for (x = 1; x < width - 1; x++)
            {
                for (y = 1; y < height - 1; y++)
                {
                    for (i = x - 1; i < x + 2; i++)
                    {
                        for (j = y - 1; j < y + 2; j++)
                        {
                            rx += (MatrizCor.Matriz[i, j, 0] * maskX[k, l]);
                            gx += (MatrizCor.Matriz[i, j, 1] * maskX[k, l]);
                            bx += (MatrizCor.Matriz[i, j, 2] * maskX[k, l]);
                            ry += (MatrizCor.Matriz[i, j, 0] * maskY[k, l]);
                            gy += (MatrizCor.Matriz[i, j, 1] * maskY[k, l]);
                            by += (MatrizCor.Matriz[i, j, 2] * maskY[k, l]);
                            k++;
                        }
                        k = 0;
                        l++;
                    }

                    G[x, y, 0] = (int)Math.Sqrt(Math.Pow(rx, 2.0) + Math.Pow(ry, 2.0)) / 9;
                    G[x, y, 1] = (int)Math.Sqrt(Math.Pow(gx, 2.0) + Math.Pow(gy, 2.0)) / 9;
                    G[x, y, 2] = (int)Math.Sqrt(Math.Pow(bx, 2.0) + Math.Pow(by, 2.0)) / 9;

                    rx = 0; gx = 0; bx = 0;
                    ry = 0; gy = 0; by = 0;
                    l = 0;
                }
            }
            MatrizCor.Matriz = G;
            this.ToImage();
            return (Image)ImagemBMP;
        }
        public Image LogicOp(LogicOperationType operation, Imagem imagemB)
        {
            if (this.imageType != ImageType.binary) { this.ToBool(); }

            if (imagemB == null)
                imagemB = this;

            if (imagemB.imageType != ImageType.binary) { imagemB.ToBool(); }

            int x, y;
            for (x = 0; x < imagemB.MatrizBool.Width; x++)
            {
                for (y = 0; y < imagemB.MatrizBool.Height; y++)
                {
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
                            this.MatrizBool.Matriz[x, y] = this.MatrizBool.Matriz[x, y] && !imagemB.MatrizBool.Matriz[x, y] || !this.MatrizBool.Matriz[x, y] && this.MatrizBool.Matriz[x, y];
                            break;
                    }
                }
            }
            this.ToImage();

            return this.ImagemBMP;
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

        public Image CorrecaoHistograma()
        {
            if (this.imageType != ImageType.integer) { this.ToInt(); }
            int[] histograma = new int[256];
            int[] histogramaAc = new int[256];
            int[] histogramaCalc = new int[256];
            int x, y, width = this.ImagemBMP.Width, height = this.ImagemBMP.Height;
            for (x = 0; x < width; x++)
            {
                for (y = 0; y < height; y++)
                {
                    histograma[MatrizCor.Matriz[x, y, 0]]++;
                }
            }
            histogramaAc[0] = histograma[0];
            for (x = 1; x < 256; x++)
            {
                histogramaAc[x] = histogramaAc[x - 1] + histograma[x];
            }

            for (x = 0; x < 256; x++)
            {
                histogramaCalc[x] = (int)(255 * histogramaAc[x] / (width * height));
                Console.WriteLine(histogramaCalc[x]);
            }
            int aux;
            for (x = 0; x < width; x++)
            {
                for (y = 0; y < height; y++)
                {
                    aux = histogramaCalc[MatrizCor.Matriz[x, y, 0]];
                    MatrizCor.Matriz[x, y, 0] = aux;
                    MatrizCor.Matriz[x, y, 1] = aux;
                    MatrizCor.Matriz[x, y, 2] = aux;
                }
            }
            this.ToImage();

            return (Image)ImagemBMP;
        }

        public Image ToLimiar(int limiar)
        {
            int x, y, h = MatrizCor.Height, w = MatrizCor.Width;
            if (this.imageType != ImageType.gray) { this.ToGray(); }

            for (x = 0; x < w; x++)
            {
                for (y = 0; y < h; y++)
                {
                    MatrizCor.Matriz[x, y, 0] = (MatrizCor.Matriz[x, y, 0] > limiar) ? 255 : 0;
                    //MatrizInt.Matriz[x, y, 1] = MatrizInt.Matriz[x, y, 0];
                    //MatrizInt.Matriz[x, y, 2] = MatrizInt.Matriz[x, y, 0];
                }
            }

            this.ToImage();
            return this.ImagemBMP;
        }
        public Image ToLimiarAleatorio(int limiar, int limRandomInf, int limRandomSup)
        {
            int x, y, h = MatrizCor.Height, w = MatrizCor.Width;
            Random rnd = new Random();
            int rand;
            if (this.imageType != ImageType.gray) { this.ToGray(); }
            int temp;
            for (x = 0; x < w; x++)
            {
                for (y = 0; y < h; y++)
                {
                    rand = rnd.Next(limRandomInf, limRandomSup + 1);
                    temp = MatrizCor.Matriz[x, y, 0] + rand;

                    MatrizCor.Matriz[x, y, 0] = (temp > limiar) ? 255 : 0;
                }
            }
            this.ToImage();
            return this.ImagemBMP;
        }

        public Image ToPeriodicoDispersao(int quant)
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

            List<int[,]> D = new List<int[,]>
            {
                D2,
                D3,
                D4
            };

            int i, j, x, y, h = MatrizCor.Height, w = MatrizCor.Width;
            if (this.imageType != ImageType.gray) { this.ToGray(); }

            for (x = 0; x < w; x++)
            {
                for (y = 0; y < h; y++)
                {
                    i = x % quant;
                    j = y % quant;
                    if (MatrizCor.Matriz[x, y, 0] > D[quant - 2][i, j])
                    {
                        MatrizCor.Matriz[x, y, 0] = 255;
                    }
                    else
                    {
                        MatrizCor.Matriz[x, y, 0] = 0;
                    }
                }
            }
            this.ToImage();
            return this.ImagemBMP;
        }
        public int GetMedia()
        {
            int x, y, h = MatrizCor.Height, w = MatrizCor.Width;
            long m = 0;
            //if (this.imageType != ImageType.gray) { this.ToGray(); }
            for (x = 0; x < w; x++)
            {
                for (y = 0; y < h; y++)
                {
                    m += (long)MatrizCor.Matriz[x, y, 0];
                }
            }
            Console.WriteLine("A média é:" + m);
            return (int)(m / (w * h));
        }
        public Image ToAperiodicoDispersao(int quant)
        {
            int x, y, h = MatrizCor.Height - 1, w = MatrizCor.Width - 1, erro;

            if (this.imageType != ImageType.gray) { this.ToGray(); }

            if (quant == 2)
            {
                int media = GetMedia();

                for (y = 0; y < h; y++)
                {
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
                }
            }
            else
            {

                int media = GetMedia();
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
            }


            this.CorrecaoMinMax(Correcao.limiar);
            this.ToImage();
            return this.ImagemBMP;
        }
    }
}
