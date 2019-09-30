using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace ImageProcessing
{
    public class ManipuladorImagem
    {
        private Bitmap _bitmapAtual;
        private Bitmap _bitmapPreAreaCrop;
        private ImageType imageType = ImageType.color;
        private int maxColorValue = 255;
        //public int Pos { get; set; }
        public ImagemBool MatrizBool;
        public ImagemInt MatrizInt;
        public ManipuladorImagem()
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

        public void ResetBitmap()
        {
            if (_bitmapAtual != null && BitmapPreProcess != null)
            {
                Bitmap temp = (Bitmap)_bitmapAtual.Clone();
                _bitmapAtual = (Bitmap)BitmapPreProcess.Clone();
                BitmapPreProcess = (Bitmap)temp.Clone();
            }
        }

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
        public ManipuladorImagem Inverter(ManipuladorImagem imagem)
        {
            int r, g, b, x, y;
            for (x = 0; x < imagem.BitmapAtual.Width; x++)
            {
                for (y = 0; y < imagem.BitmapAtual.Height; y++)
                {
                    r = (int)(255 - (int)imagem.BitmapAtual.GetPixel(x, y).R);
                    g = (int)(255 - (int)imagem.BitmapAtual.GetPixel(x, y).G);
                    b = (int)(255 - (int)imagem.BitmapAtual.GetPixel(x, y).B);

                    imagem.BitmapAtual.SetPixel(x, y, Color.FromArgb(r, g, b));
                }
            }
            return imagem;
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

        public void SetGamma(double red, double green, double blue)
        {
            Bitmap temp = (Bitmap)_bitmapAtual;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;
            byte[] redGamma = CreateGammaArray(red);
            byte[] greenGamma = CreateGammaArray(green);
            byte[] blueGamma = CreateGammaArray(blue);
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    bmap.SetPixel(i, j, Color.FromArgb(redGamma[c.R], greenGamma[c.G], blueGamma[c.B]));
                }
            }
            _bitmapAtual = (Bitmap)bmap.Clone();
        }

        private byte[] CreateGammaArray(double color)
        {
            byte[] gammaArray = new byte[256];
            for (int i = 0; i < 256; ++i)
            {
                gammaArray[i] = (byte)Math.Min(255, (int)((255.0 * Math.Pow(i / 255.0, 1.0 / color)) + 0.5));
            }
            return gammaArray;
        }

        public void SetBrightness(int brightness)
        {
            Bitmap temp = (Bitmap)_bitmapAtual;
            Bitmap bmap = (Bitmap)temp.Clone();
            if (brightness < -255) brightness = -255;
            if (brightness > 255) brightness = 255;
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    int cR = c.R + brightness;
                    int cG = c.G + brightness;
                    int cB = c.B + brightness;

                    if (cR < 0) cR = 1;
                    if (cR > 255) cR = 255;

                    if (cG < 0) cG = 1;
                    if (cG > 255) cG = 255;

                    if (cB < 0) cB = 1;
                    if (cB > 255) cB = 255;

                    bmap.SetPixel(i, j, Color.FromArgb((byte)cR, (byte)cG, (byte)cB));
                }
            }
            _bitmapAtual = (Bitmap)bmap.Clone();
        }

        public void SetContrast(double contrast)
        {
            Bitmap temp = (Bitmap)_bitmapAtual;
            Bitmap bmap = (Bitmap)temp.Clone();
            if (contrast < -100) contrast = -100;
            if (contrast > 100) contrast = 100;
            contrast = (100.0 + contrast) / 100.0;
            contrast *= contrast;
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    double pR = c.R / 255.0;
                    pR -= 0.5;
                    pR *= contrast;
                    pR += 0.5;
                    pR *= 255;
                    if (pR < 0) pR = 0;
                    if (pR > 255) pR = 255;

                    double pG = c.G / 255.0;
                    pG -= 0.5;
                    pG *= contrast;
                    pG += 0.5;
                    pG *= 255;
                    if (pG < 0) pG = 0;
                    if (pG > 255) pG = 255;

                    double pB = c.B / 255.0;
                    pB -= 0.5;
                    pB *= contrast;
                    pB += 0.5;
                    pB *= 255;
                    if (pB < 0) pB = 0;
                    if (pB > 255) pB = 255;

                    bmap.SetPixel(i, j, Color.FromArgb((byte)pR, (byte)pG, (byte)pB));
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

        public Image LogicOp(LogicOperationType operation, ManipuladorImagem imagemB)
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

        public Image MathOp(MathOperationType operation, ManipuladorImagem imagemB)
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
        public void SetInvert()
        {
            Bitmap temp = (Bitmap)_bitmapAtual;
            Bitmap bmap = (Bitmap)temp.Clone();
            Color c;
            for (int i = 0; i < bmap.Width; i++)
            {
                for (int j = 0; j < bmap.Height; j++)
                {
                    c = bmap.GetPixel(i, j);
                    bmap.SetPixel(i, j, Color.FromArgb(255 - c.R, 255 - c.G, 255 - c.B));
                }
            }
            _bitmapAtual = (Bitmap)bmap.Clone();
        }

        public void Resize(int newWidth, int newHeight)
        {
            if (newWidth != 0 && newHeight != 0)
            {
                Bitmap temp = (Bitmap)_bitmapAtual;
                Bitmap bmap = new Bitmap(newWidth, newHeight, temp.PixelFormat);

                double nWidthFactor = (double)temp.Width / (double)newWidth;
                double nHeightFactor = (double)temp.Height / (double)newHeight;

                double fx, fy, nx, ny;
                int cx, cy, fr_x, fr_y;
                Color color1 = new Color();
                Color color2 = new Color();
                Color color3 = new Color();
                Color color4 = new Color();
                byte nRed, nGreen, nBlue;

                byte bp1, bp2;

                for (int x = 0; x < bmap.Width; ++x)
                {
                    for (int y = 0; y < bmap.Height; ++y)
                    {

                        fr_x = (int)Math.Floor(x * nWidthFactor);
                        fr_y = (int)Math.Floor(y * nHeightFactor);
                        cx = fr_x + 1;
                        if (cx >= temp.Width) cx = fr_x;
                        cy = fr_y + 1;
                        if (cy >= temp.Height) cy = fr_y;
                        fx = x * nWidthFactor - fr_x;
                        fy = y * nHeightFactor - fr_y;
                        nx = 1.0 - fx;
                        ny = 1.0 - fy;

                        color1 = temp.GetPixel(fr_x, fr_y);
                        color2 = temp.GetPixel(cx, fr_y);
                        color3 = temp.GetPixel(fr_x, cy);
                        color4 = temp.GetPixel(cx, cy);

                        // Blue
                        bp1 = (byte)(nx * color1.B + fx * color2.B);

                        bp2 = (byte)(nx * color3.B + fx * color4.B);

                        nBlue = (byte)(ny * (double)(bp1) + fy * (double)(bp2));

                        // Green
                        bp1 = (byte)(nx * color1.G + fx * color2.G);

                        bp2 = (byte)(nx * color3.G + fx * color4.G);

                        nGreen = (byte)(ny * (double)(bp1) + fy * (double)(bp2));

                        // Red
                        bp1 = (byte)(nx * color1.R + fx * color2.R);

                        bp2 = (byte)(nx * color3.R + fx * color4.R);

                        nRed = (byte)(ny * (double)(bp1) + fy * (double)(bp2));

                        bmap.SetPixel(x, y, System.Drawing.Color.FromArgb(255, nRed, nGreen, nBlue));
                    }
                }
                _bitmapAtual = (Bitmap)bmap.Clone();
            }
        }

        public void RotateFlip(RotateFlipType rotateFlipType)
        {
            Bitmap temp = (Bitmap)_bitmapAtual;
            Bitmap bmap = (Bitmap)temp.Clone();
            bmap.RotateFlip(rotateFlipType);
            _bitmapAtual = (Bitmap)bmap.Clone();
        }

        public void Crop(int xPosition, int yPosition, int width, int height)
        {
            Bitmap temp = (Bitmap)_bitmapAtual;
            Bitmap bmap = (Bitmap)temp.Clone();
            if (xPosition + width > _bitmapAtual.Width)
                width = _bitmapAtual.Width - xPosition;
            if (yPosition + height > _bitmapAtual.Height)
                height = _bitmapAtual.Height - yPosition;
            Rectangle rect = new Rectangle(xPosition, yPosition, width, height);
            _bitmapAtual = (Bitmap)bmap.Clone(rect, bmap.PixelFormat);
        }

        public void DrawOutCropArea(int xPosition, int yPosition, int width, int height)
        {
            _bitmapPreAreaCrop = (Bitmap)_bitmapAtual;
            Bitmap bmap = (Bitmap)_bitmapPreAreaCrop.Clone();
            Graphics gr = Graphics.FromImage(bmap);
            Brush cBrush = new Pen(Color.FromArgb(150, Color.White)).Brush;
            Rectangle rect1 = new Rectangle(0, 0, _bitmapAtual.Width, yPosition);
            Rectangle rect2 = new Rectangle(0, yPosition, xPosition, height);
            Rectangle rect3 = new Rectangle(0, (yPosition + height), _bitmapAtual.Width, _bitmapAtual.Height);
            Rectangle rect4 = new Rectangle((xPosition + width), yPosition, (_bitmapAtual.Width - xPosition - width), height);
            gr.FillRectangle(cBrush, rect1);
            gr.FillRectangle(cBrush, rect2);
            gr.FillRectangle(cBrush, rect3);
            gr.FillRectangle(cBrush, rect4);
            _bitmapAtual = (Bitmap)bmap.Clone();
        }

        public void RemoveCropAreaDraw()
        {
            _bitmapAtual = (Bitmap)_bitmapPreAreaCrop.Clone();
        }

        public void InsertText(string text, int xPosition, int yPosition, string fontName, float fontSize, string fontStyle, string colorName1, string colorName2)
        {
            Bitmap temp = (Bitmap)_bitmapAtual;
            Bitmap bmap = (Bitmap)temp.Clone();
            Graphics gr = Graphics.FromImage(bmap);
            if (string.IsNullOrEmpty(fontName))
                fontName = "Times New Roman";
            if (fontSize.Equals(null))
                fontSize = 10.0F;
            Font font = new Font(fontName, fontSize);
            if (!string.IsNullOrEmpty(fontStyle))
            {
                FontStyle fStyle = FontStyle.Regular;
                switch (fontStyle.ToLower())
                {
                    case "bold":
                        fStyle = FontStyle.Bold;
                        break;
                    case "italic":
                        fStyle = FontStyle.Italic;
                        break;
                    case "underline":
                        fStyle = FontStyle.Underline;
                        break;
                    case "strikeout":
                        fStyle = FontStyle.Strikeout;
                        break;

                }
                font = new Font(fontName, fontSize, fStyle);
            }
            if (string.IsNullOrEmpty(colorName1))
                colorName1 = "Black";
            if (string.IsNullOrEmpty(colorName2))
                colorName2 = colorName1;
            Color color1 = Color.FromName(colorName1);
            Color color2 = Color.FromName(colorName2);
            int gW = (int)(text.Length * fontSize);
            gW = gW == 0 ? 10 : gW;
            LinearGradientBrush LGBrush = new LinearGradientBrush(new Rectangle(0, 0, gW, (int)fontSize), color1, color2, LinearGradientMode.Vertical);
            gr.DrawString(text, font, LGBrush, xPosition, yPosition);
            _bitmapAtual = (Bitmap)bmap.Clone();
        }

        public void InsertImage(string imagePath, int xPosition, int yPosition)
        {
            Bitmap temp = (Bitmap)_bitmapAtual;
            Bitmap bmap = (Bitmap)temp.Clone();
            Graphics gr = Graphics.FromImage(bmap);
            if (!string.IsNullOrEmpty(imagePath))
            {
                Bitmap i_bitmap = (Bitmap)Bitmap.FromFile(imagePath);
                Rectangle rect = new Rectangle(xPosition, yPosition, i_bitmap.Width, i_bitmap.Height);
                gr.DrawImage(Bitmap.FromFile(imagePath), rect);
            }
            _bitmapAtual = (Bitmap)bmap.Clone();
        }

        public void InsertShape(string shapeType, int xPosition, int yPosition, int width, int height, string colorName)
        {
            Bitmap temp = (Bitmap)_bitmapAtual;
            Bitmap bmap = (Bitmap)temp.Clone();
            Graphics gr = Graphics.FromImage(bmap);
            if (string.IsNullOrEmpty(colorName))
                colorName = "Black";
            Pen pen = new Pen(Color.FromName(colorName));
            switch (shapeType.ToLower())
            {
                case "filledellipse":
                    gr.FillEllipse(pen.Brush, xPosition, yPosition, width, height);
                    break;
                case "filledrectangle":
                    gr.FillRectangle(pen.Brush, xPosition, yPosition, width, height);
                    break;
                case "ellipse":
                    gr.DrawEllipse(pen, xPosition, yPosition, width, height);
                    break;
                case "rectangle":
                default:
                    gr.DrawRectangle(pen, xPosition, yPosition, width, height);
                    break;

            }
            _bitmapAtual = (Bitmap)bmap.Clone();
        }
    }
}
