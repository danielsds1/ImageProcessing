using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageProcessing
{
    public class Imagem
    {
        private Bitmap _bitmapAtual;
        private ImageType imageType = ImageType.color;
        private int maxColorValue = 255;
        //public int Pos { get; set; }
        public ImagemBool MatrizBool;
        public ImagemInt MatrizInt;
        public Imagem()
        {

        }

        public Bitmap BitmapAtual
        {
            get
            {
                if (_bitmapAtual == null)
                    _bitmapAtual = new Bitmap(1, 1);
                return _bitmapAtual;
            }
            set
            {
                _bitmapAtual = value;
                imageType = ImageType.color;
            }
        }

        public Bitmap BitmapPreProcess { get; set; }

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
            _bitmapAtual.Save(saveFilePath);
        }

        public void LimparImagem()
        {
            _bitmapAtual = new Bitmap(1, 1);
        }

        public void RestorePrevious()
        {
            BitmapPreProcess = _bitmapAtual;
        }
        public void InverterCores()
        {
            int x, y;

            Color color;
            for (x = 0; x < this.BitmapAtual.Width; x++)
            {
                for (y = 0; y < this.BitmapAtual.Height; y++)
                {
                    color = this.BitmapAtual.GetPixel(x, y);
                    this.BitmapAtual.SetPixel(x, y, Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B));
                }
            }
        }
        public void SetColorFilter(ColorFilterTypes colorFilterType)
        {
            Bitmap temp = (Bitmap)_bitmapAtual;
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
            _bitmapAtual = (Bitmap)bmap.Clone();
        }

        public void ToBool()
        {
            int r, x, y;

            MatrizBool.Matriz = new bool[BitmapAtual.Width, BitmapAtual.Height];
            MatrizBool.Width = BitmapAtual.Width;
            MatrizBool.Height = BitmapAtual.Height;

            for (x = 0; x < BitmapAtual.Width; x++)
            {
                for (y = 0; y < BitmapAtual.Height; y++)
                {
                    r = ((int)BitmapAtual.GetPixel(x, y).R);
                    MatrizBool.Matriz[x, y] = (r > 127) ? true : false;
                }
            }
            imageType = ImageType.binary;
        }
        public void ToInt()
        {
            int x, y;

            MatrizInt.Matriz = new int[BitmapAtual.Width, BitmapAtual.Height, 3];
            MatrizInt.Width = BitmapAtual.Width;
            MatrizInt.Height = BitmapAtual.Height;
            Color color;
            for (x = 0; x < BitmapAtual.Width; x++)
            {
                for (y = 0; y < BitmapAtual.Height; y++)
                {
                    color = BitmapAtual.GetPixel(x, y);
                    MatrizInt.Matriz[x, y, 0] = (int)color.R;
                    MatrizInt.Matriz[x, y, 1] = (int)color.G;
                    MatrizInt.Matriz[x, y, 2] = (int)color.B;
                }
            }
            imageType = ImageType.integer;
        }
        public void ToImage()
        {
            int r, x, y;
            switch (imageType)
            {
                case ImageType.binary:
                    for (x = 0; x < MatrizBool.Width; x++)
                    {
                        for (y = 0; y < MatrizBool.Height; y++)
                        {
                            r = MatrizBool.Matriz[x, y] ? 255 : 0;
                            BitmapAtual.SetPixel(x, y, Color.FromArgb(r, r, r));
                        }
                    }
                    break;
                case ImageType.integer:
                    for (x = 0; x < MatrizInt.Width; x++)
                    {
                        for (y = 0; y < MatrizInt.Height; y++)
                        {
                            BitmapAtual.SetPixel(x, y, Color.FromArgb(MatrizInt.Matriz[x, y, 0], MatrizInt.Matriz[x, y, 1], MatrizInt.Matriz[x, y, 2]));
                        }
                    }
                    break;
                case ImageType.gray:
                    for (x = 0; x < MatrizInt.Width; x++)
                    {
                        for (y = 0; y < MatrizInt.Height; y++)
                        {
                            BitmapAtual.SetPixel(x, y, Color.FromArgb(MatrizInt.Matriz[x, y, 0], MatrizInt.Matriz[x, y, 0], MatrizInt.Matriz[x, y, 0]));
                        }
                    }
                    break;
            }

            imageType = ImageType.color;

        }
        public void ToGray()
        {
            this.ToInt();
            //Bitmap temp = (Bitmap)_bitmapAtual;
            //Bitmap bmap = (Bitmap)temp.Clone();
            //Color c;
            int gray = 0;
            int i, j;
            for (i = 0; i < this.BitmapAtual.Width; i++)
            {
                for (j = 0; j < this.BitmapAtual.Height; j++)
                {
                    //c = bmap.GetPixel(i, j);
                    gray += (int)(.299 * (double)MatrizInt.Matriz[i, j, 0]);
                    gray += (int)(.587 * (double)MatrizInt.Matriz[i, j, 1]);
                    gray += (int)(.114 * (double)MatrizInt.Matriz[i, j, 2]);
                    MatrizInt.Matriz[i, j, 0] = gray;
                    MatrizInt.Matriz[i, j, 1] = gray;
                    MatrizInt.Matriz[i, j, 2] = gray;
                    gray = 0;
                }
            }
            this.ToImage();
            imageType = ImageType.gray;
        }
        public Image FiltroMediana()
        {
            this.ToInt();
            int[] r = new int[9];
            int[] g = new int[9];
            int[] b = new int[9];
            int x, y, i, j, pos = 0;
            int w = BitmapAtual.Width - 1;
            int h = BitmapAtual.Height - 1;
            //Color color;
            for (x = 1; x < w; x++)
            {
                for (y = 1; y < h; y++)
                {
                    for (i = x - 1; i < x + 2; i++)
                    {
                        for (j = y - 1; j < y + 2; j++)
                        {
                            r[pos] = MatrizInt.Matriz[i, j, 0];
                            g[pos] = MatrizInt.Matriz[i, j, 1];
                            b[pos] = MatrizInt.Matriz[i, j, 2];
                            pos++;
                        }
                    }
                    Array.Sort(r);
                    Array.Sort(g);
                    Array.Sort(b);
                    MatrizInt.Matriz[x, y, 0] = r[4];
                    MatrizInt.Matriz[x, y, 1] = g[4];
                    MatrizInt.Matriz[x, y, 2] = b[4];
                    //imagemA.BitmapAtual.SetPixel(x, y, Color.FromArgb(r[4], g[4], b[4]));
                    pos = 0;
                }
            }
            this.ToImage();
            return (Image)this.BitmapAtual;
        }
        public Image FiltroMedia()
        {
            this.ToInt();
            int r = 0, g = 0, b = 0, x, y, i, j;
            int width = BitmapAtual.Width;
            int height = BitmapAtual.Height;

            //int[] aux = new int[9];
            for (x = 1; x < width - 1; x++)
            {
                for (y = 1; y < height - 1; y++)
                {
                    for (i = x - 1; i < x + 2; i++)
                    {
                        for (j = y - 1; j < y + 2; j++)
                        {
                            r += MatrizInt.Matriz[i, j, 0];
                            g += MatrizInt.Matriz[i, j, 1];
                            b += MatrizInt.Matriz[i, j, 2];
                        }
                    }
                    r /= 9;
                    g /= 9;
                    b /= 9;

                    MatrizInt.Matriz[x, y, 0] = r;
                    MatrizInt.Matriz[x, y, 1] = g;
                    MatrizInt.Matriz[x, y, 2] = b;

                    r = 0; g = 0; b = 0;
                }
            }
            this.ToImage();
            return (Image)BitmapAtual;
        }

        public Image FiltroPassaAlta()
        {

            //this.ToGray();
            this.ToInt();
            int[,] maskX = new int[3, 3] { { -1, 00, 01 },
                                           { -2, 00, 02 },
                                           { -1, 00, 01 } };
            int[,] maskY = new int[3, 3] { { -1, -2, -1 },
                                           { 00, 00, 00 },
                                           { 01, 02, 01 } };

            int rx = 0, gx = 0, bx = 0, ry = 0, gy = 0, by = 0, x, y, i, j, k = 0, l = 0;

            int width = BitmapAtual.Width;
            int height = BitmapAtual.Height;
            int[,,] G = new int[width, height, 3];
            //int[,] Gy = new int[width, height];


            //int[] aux = new int[9];
            for (x = 1; x < width - 1; x++)
            {
                for (y = 1; y < height - 1; y++)
                {
                    for (i = x - 1; i < x + 2; i++)
                    {
                        for (j = y - 1; j < y + 2; j++)
                        {
                            rx += (MatrizInt.Matriz[i, j, 0] * maskX[k, l]);
                            gx += MatrizInt.Matriz[i, j, 1] * maskX[k, l];
                            bx += MatrizInt.Matriz[i, j, 2] * maskX[k, l];
                            ry += (MatrizInt.Matriz[i, j, 0] * maskY[k, l]);
                            gy += MatrizInt.Matriz[i, j, 1] * maskY[k, l];
                            by += MatrizInt.Matriz[i, j, 2] * maskY[k, l];
                            k++;
                        }
                        k = 0;
                        l++;
                    }

                    //aux= (int)Math.Sqrt(Math.Pow((double)rx,2.0) + Math.Pow((double)ry,2.0))/9;

                    G[x, y, 0] = (int)Math.Sqrt(Math.Pow((double)rx, 2.0) + Math.Pow((double)ry, 2.0)) / 9;
                    G[x, y, 1] = (int)Math.Sqrt(Math.Pow((double)gx, 2.0) + Math.Pow((double)gy, 2.0)) / 9;
                    G[x, y, 2] = (int)Math.Sqrt(Math.Pow((double)bx, 2.0) + Math.Pow((double)by, 2.0)) / 9;


                    rx = 0; gx = 0; bx = 0;
                    ry = 0; gy = 0; by = 0;
                    l = 0;
                }
            }
            MatrizInt.Matriz = G;
            this.ToImage();
            return (Image)BitmapAtual;
        }

        public Image BordasSobel()
        {
            this.ToInt();
            double[,] maskX = new double[3, 3] { { -1, 00, 01 },
                                           { -2, 00, 02 },
                                           { -1, 00, 01 } };
            double[,] maskY = new double[3, 3] { { -1, -2, -1 },
                                           { 00, 00, 00 },
                                           { 01, 02, 01 } };

            double rx = 0, gx = 0, bx = 0, ry = 0, gy = 0, by = 0;
            int x, y, i, j, k = 0, l = 0;

            int width = BitmapAtual.Width;
            int height = BitmapAtual.Height;
            int[,,] G = new int[width, height, 3];

            for (x = 1; x < width - 1; x++)
            {
                for (y = 1; y < height - 1; y++)
                {
                    for (i = x - 1; i < x + 2; i++)
                    {
                        for (j = y - 1; j < y + 2; j++)
                        {
                            rx += (MatrizInt.Matriz[i, j, 0] * maskX[k, l]);
                            gx += (MatrizInt.Matriz[i, j, 1] * maskX[k, l]);
                            bx += (MatrizInt.Matriz[i, j, 2] * maskX[k, l]);
                            ry += (MatrizInt.Matriz[i, j, 0] * maskY[k, l]);
                            gy += (MatrizInt.Matriz[i, j, 1] * maskY[k, l]);
                            by += (MatrizInt.Matriz[i, j, 2] * maskY[k, l]);
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
            MatrizInt.Matriz = G;
            this.ToImage();
            return (Image)BitmapAtual;
        }
        public Image BordasPrewitt()
        {
            this.ToInt();
            double[,] maskX = new double[3, 3] { { -1, -1, -1 },
                                                 { 00, 00, 00 },
                                                 { 01, 01, 01 } };
            double[,] maskY = new double[3, 3] { { -1, 00, 01 },
                                                 { -1, 00, 01 },
                                                 { -1, 00, 01 } };

            double rx = 0, gx = 0, bx = 0, ry = 0, gy = 0, by = 0;
            int x, y, i, j, k = 0, l = 0;

            int width = BitmapAtual.Width;
            int height = BitmapAtual.Height;
            int[,,] G = new int[width, height, 3];

            for (x = 1; x < width - 1; x++)
            {
                for (y = 1; y < height - 1; y++)
                {
                    for (i = x - 1; i < x + 2; i++)
                    {
                        for (j = y - 1; j < y + 2; j++)
                        {
                            rx += (MatrizInt.Matriz[i, j, 0] * maskX[k, l]);
                            gx += (MatrizInt.Matriz[i, j, 1] * maskX[k, l]);
                            bx += (MatrizInt.Matriz[i, j, 2] * maskX[k, l]);
                            ry += (MatrizInt.Matriz[i, j, 0] * maskY[k, l]);
                            gy += (MatrizInt.Matriz[i, j, 1] * maskY[k, l]);
                            by += (MatrizInt.Matriz[i, j, 2] * maskY[k, l]);
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
            MatrizInt.Matriz = G;
            this.ToImage();
            return (Image)BitmapAtual;
        }
        public Image BordasRoberts()
        {
            this.ToInt();
            double[,] maskX = new double[2, 2] { { 1, 0},
                                                 { 0, -1}};
            double[,] maskY = new double[2, 2] { { 0, 1},
                                                 { -1, 0}};

            double rx = 0, gx = 0, bx = 0, ry = 0, gy = 0, by = 0;
            int x, y, i, j, k = 0, l = 0;

            int width = BitmapAtual.Width;
            int height = BitmapAtual.Height;
            int[,,] G = new int[width, height, 3];

            for (x = 0; x < width - 1; x++)
            {
                for (y = 0; y < height - 1; y++)
                {
                    for (i = x ; i < x + 2; i++)
                    {
                        for (j = y ; j < y + 2; j++)
                        {
                            rx += (MatrizInt.Matriz[i, j, 0] * maskX[k, l]);
                            gx += (MatrizInt.Matriz[i, j, 1] * maskX[k, l]);
                            bx += (MatrizInt.Matriz[i, j, 2] * maskX[k, l]);
                            ry += (MatrizInt.Matriz[i, j, 0] * maskY[k, l]);
                            gy += (MatrizInt.Matriz[i, j, 1] * maskY[k, l]);
                            by += (MatrizInt.Matriz[i, j, 2] * maskY[k, l]);
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
            MatrizInt.Matriz = G;
            this.ToImage();
            return (Image)BitmapAtual;
        }
        public Image BordasIsotropico()
        {
            this.ToInt();
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

            int width = BitmapAtual.Width;
            int height = BitmapAtual.Height;
            int[,,] G = new int[width, height, 3];

            for (x = 1; x < width - 1; x++)
            {
                for (y = 1; y < height - 1; y++)
                {
                    for (i = x - 1; i < x + 2; i++)
                    {
                        for (j = y - 1; j < y + 2; j++)
                        {
                            rx += (MatrizInt.Matriz[i, j, 0] * maskX[k, l]);
                            gx += (MatrizInt.Matriz[i, j, 1] * maskX[k, l]);
                            bx += (MatrizInt.Matriz[i, j, 2] * maskX[k, l]);
                            ry += (MatrizInt.Matriz[i, j, 0] * maskY[k, l]);
                            gy += (MatrizInt.Matriz[i, j, 1] * maskY[k, l]);
                            by += (MatrizInt.Matriz[i, j, 2] * maskY[k, l]);
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
            MatrizInt.Matriz = G;
            this.ToImage();
            return (Image)BitmapAtual;
        }
        public Image BordasLaplace()
        {
            this.ToInt();
            double[,] maskX = new double[3, 3] {{  0, -1,  0 },
                                                { -1,  4, -1 },
                                                {  0, -1,  0 }};

            double[,] maskY = new double[3, 3] {{ -1, -1, -1 },
                                                { -1,  8, -1 },
                                                { -1, -1, -1 }};

            double rx = 0, gx = 0, bx = 0, ry = 0, gy = 0, by = 0;
            int x, y, i, j, k = 0, l = 0;

            int width = BitmapAtual.Width;
            int height = BitmapAtual.Height;
            int[,,] G = new int[width, height, 3];

            for (x = 1; x < width - 1; x++)
            {
                for (y = 1; y < height - 1; y++)
                {
                    for (i = x - 1; i < x + 2; i++)
                    {
                        for (j = y - 1; j < y + 2; j++)
                        {
                            rx += (MatrizInt.Matriz[i, j, 0] * maskX[k, l]);
                            gx += (MatrizInt.Matriz[i, j, 1] * maskX[k, l]);
                            bx += (MatrizInt.Matriz[i, j, 2] * maskX[k, l]);
                            ry += (MatrizInt.Matriz[i, j, 0] * maskY[k, l]);
                            gy += (MatrizInt.Matriz[i, j, 1] * maskY[k, l]);
                            by += (MatrizInt.Matriz[i, j, 2] * maskY[k, l]);
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
            MatrizInt.Matriz = G;
            this.ToImage();
            return (Image)BitmapAtual;
        }
        public Image LogicOp(LogicOperationType operation, Imagem imagemB)
        {
            this.ToBool();
            if (imagemB == null)
                imagemB = this;

            imagemB.ToBool();
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

            return this.BitmapAtual;
        }

        public Image MathOp(MathOperationType operation, Imagem imagemB)
        {
            this.ToInt();
            if (imagemB != null)
            {
                imagemB.ToInt();
                int width = imagemB.BitmapAtual.Width;
                int height = imagemB.BitmapAtual.Height;
                int x, y, ra, ga, ba, rb, gb, bb;
                for (x = 0; x < width; x++)
                {
                    for (y = 0; y < height; y++)
                    {
                        ra = this.MatrizInt.Matriz[x, y, 0];
                        rb = imagemB.MatrizInt.Matriz[x, y, 0];
                        ga = this.MatrizInt.Matriz[x, y, 1];
                        gb = imagemB.MatrizInt.Matriz[x, y, 1];
                        ba = this.MatrizInt.Matriz[x, y, 2];
                        bb = imagemB.MatrizInt.Matriz[x, y, 2];
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
                                rb = (rb < 1) ? 1 : rb;
                                gb = (gb < 1) ? 1 : gb;
                                bb = (bb < 1) ? 1 : bb;
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
                        this.MatrizInt.Matriz[x, y, 0] = ra;
                        this.MatrizInt.Matriz[x, y, 1] = ga;
                        this.MatrizInt.Matriz[x, y, 2] = ba;
                    }
                }
            }
            this.ToImage();
            return this.BitmapAtual;
        }

        public Image CorrecaoHistograma()
        {
            this.ToInt();
            int[] histograma = new int[256];
            int[] histogramaAc = new int[256];
            int[] histogramaCalc = new int[256];
            int x, y, width = this.BitmapAtual.Width, height = this.BitmapAtual.Height;
            for (x = 0; x < width; x++)
            {
                for (y = 0; y < height; y++)
                {
                    histograma[MatrizInt.Matriz[x, y, 0]]++;
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
                    aux = histogramaCalc[MatrizInt.Matriz[x, y, 0]];
                    MatrizInt.Matriz[x, y, 0] = aux;
                    MatrizInt.Matriz[x, y, 1] = aux;
                    MatrizInt.Matriz[x, y, 2] = aux;
                }
            }
            this.ToImage();

            return (Image)BitmapAtual;
        }








    }
}
