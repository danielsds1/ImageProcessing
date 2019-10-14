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
        adicaoLimiar = 1,
        adicaoMedia = 2,
        subtracaoLimiar = 3,
        subtracaoMedia = 4,
        multiplicacao = 5,
        divisao = 6
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
        limiar=0,
        proporcao=1
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
