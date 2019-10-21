using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{
    public struct ImagemBool
    {

        public int Width, Height;
        public bool[,] Matriz;

    };
    public struct ImagemInt
    {

        public int Width, Height;
        public int[,,] Matriz;
    };
    public struct ImagemGray
    {

        public int Width, Height;
        public int[,] Matriz;
    };
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
        adicao = 1,
        subtracao = 2,
        multiplicacao = 3,
        divisao = 4
    }
    public enum ImageType
    {
        binary = 2,
        integer = 1,
        gray = 256,
        color = 0,
    }
    public enum Correcao
    {
        limiar = 0,
        proporcao = 1
    }
    public enum StretchingType
    {
        linear = 0,
        quad = 1,
        srqt = 2,
        log = 3,
        neg = 4
    }
    public enum EdgeDetection
    {
        Sobel = 0,
        Prewitt = 1,
        Isotropico = 2,
        Laplace = 3,
        Roberts = 4,

    }
    //public enum MaskType
    //{
    //    media = 0,
    //    mediana = 1,
    //    passaAlta = 2,
    //    sobel = 3,
    //    prewitt = 4,
    //    roberts = 5,
    //    Isotropico =6,
    //    laplace=7
    //}
    //public struct MaskMatrix
    //{
    //    public double[,] passaAlta = new double[,] { { -1.0, -1.0, -1.0 }, { -1.0, -1.0, -1.0 }, { -1.0, -1.0, -1.0 } };

    //}
}
