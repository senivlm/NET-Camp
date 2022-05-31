using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_05
{
    public class Matrix
    {
        #region fields
        private int[,] matrix;
        #endregion

        #region properties
        public int LenghtX => matrix.GetLength(0);
        public int LenghtY => matrix.GetLength(1);
        public int Lenght => LenghtX * LenghtY;
        #endregion

        #region events
        public event Action<string>? NotifyStep;
        #endregion

        #region constructors
        public Matrix(uint x, uint y) => this.matrix = new int[x, y];
        public Matrix(uint x) : this(x, x) { }
        #endregion

        #region indexers
        public int this[uint x, uint y] => matrix[x, y];
        #endregion

        #region overrided_methods
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
        #endregion

        #region init_methods
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
                        (i%2!=0 && direction == Direction.RIGHT)
                        || (i%2==0 && direction == Direction.DOWN)
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
        public void InitFronStream(StreamReader stream)
        {
            string? line = stream.ReadLine();
            if (line == null)
            {
                throw new IOException("Incorrect format file (empry)");
            }
            char[] separators = {' ', "\t"[0]};
            string[] size = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            if (size.Length != 2)
            {
                throw new IOException("Incorrect format file (error in size)");
            }

            int rows = int.Parse(size[0]);
            int coloms = int.Parse(size[1]);

            this.matrix = new int[rows, coloms];
            for (int i = 0; i < rows; i++)
            {
                line = stream.ReadLine();
                if (line == null)
                {
                    throw new IOException("Incorrect format file (few lines)");
                }
                string[] value = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (value.Length != coloms)
                {
                    throw new IOException("Incorrect format file (few or many coloms)");
                }

                for (int j = 0; j < coloms; j++)
                {
                    this.matrix[i,j] = int.Parse(value[j]);
                }
            }

            if (!stream.EndOfStream)
            {
                throw new IOException("Incorrect format file (many lines)");
            }
        }
        #endregion

        #region other_methods
        public void SaveToStream(StreamWriter stream)
        {
            stream.WriteLine($"{this.LenghtX} {this.LenghtY}");
            stream.WriteLine(this.ToString()); 

        }
        #endregion

    }
}
