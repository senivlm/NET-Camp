using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_03
{
    public class Matrix
    {
        private readonly int[,] matrix;

        public int LenghtX => matrix.GetLength(0);
        public int LenghtY => matrix.GetLength(1);
        public int Lenght => LenghtX * LenghtY;
        
        public Matrix(uint x, uint y) => this.matrix = new int[x, y];
        public Matrix(uint x) : this(x, x) { }

        public int this[uint x, uint y] => matrix[x, y];


        public void InitRandom(int a, int b)
        {
            Random ran = new();
            for (int x = 0; x < LenghtX; x++)
            {
                for (int y = 0; y < LenghtY; y++)
                {
                    matrix[x, y] = ran.Next(a, b);
                }
            }
        }

        public void InitDiagonalSnake(Direction direction)
        {
            if (LenghtX != LenghtY)
            {
                throw new ArgumentException("Matrix isn't square");
            }

            int value = 1;
            int mirrorValue = Lenght; // equal (Lenght - value + 1) 

            for (int i = 0; i < LenghtX; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    int x = i - j; 
                    int y = j;

                    if (
                        (i%2!=0 && direction == Direction.right)
                        || (i%2==0 && direction == Direction.down)
                        )
                    {
                        (x, y) = (y, x);
                    }

                    int mirrorX = LenghtX - x - 1;
                    int mirrorY = LenghtY - y - 1;

                    this.matrix[x, y] = value++;
                    this.matrix[mirrorX, mirrorY] = mirrorValue--;

                    if (value > mirrorValue)
                    {
                        break;
                    }
                }

                if (value > mirrorValue)
                {
                    break;
                }

            }

        }

        public override string? ToString()
        {
            string result = "";

            for (int x = 0; x < LenghtX; x++)
            {
                for (int y = 0; y < LenghtY; y++)
                {
                    result = result + matrix[x, y] + "\t";
                }
                result = result + '\n';
            }

            return result;
        }
    }
}
