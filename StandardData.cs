using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageProcessing
{

    public struct ImagemInt
    {
        public int Width, Height;
        public int[,,] Matriz;
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
 
    public enum ElEst
    {
        quadrado =0,
        cruz=1,
        ponto=2,
        circulo=3,
        mask=4
    }
}
