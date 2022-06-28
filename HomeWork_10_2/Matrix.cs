using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_10_2
{
    public class Matrix : IEnumerable<int>
    {// дуже добре.
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
        public void InitHorizontalSnake()
        {
            List<(int, int)> indexes = GetIndexesHorizontalSnake();
            InitByIndexes(indexes);
        }
        public void InitDiagonalSnake(Direction direction)
        {
            if (LenghtX != LenghtY)
            {
                throw new ArgumentException("Matrix isn't square");
            }
            List<(int, int)> indexes = GetIndexesDiagonalSnake(direction);
            InitByIndexes(indexes);
        }
        public void InitRandom(int a, int b)
        {
            List<(int, int)> indexes = GetIndexesLine();
            Random ran = new();
            foreach ((int x, int y) in indexes)
            {
                matrix[x, y] = ran.Next(a, b);
            }
        }
        public void InitFromStream(StreamReader stream)
        {
            string? line = stream.ReadLine();
            if (line == null)
            {
                throw new IOException("Incorrect format file (empry)");
            }
            char[] separators = { ' ', "\t"[0] };
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
                    this.matrix[i, j] = int.Parse(value[j]);
                }
            }

            if (!stream.EndOfStream)
            {
                throw new IOException("Incorrect format file (many lines)");
            }
        }

        private List<(int, int)> GetIndexesLine()
        {
            List<(int, int)> result = new();

            for (int x = 0; x < LenghtX; x++)
            {
                for (int y = 0; y < LenghtY; y++)
                {
                    result.Add((x, y));
                }
            }
            return result;
        }
        private List<(int, int)> GetIndexesHorizontalSnake()
        {
            List<(int, int)> result = new();

            for (int x = 0; x < LenghtX; x++)
            {
                for (int yTmp = 0; yTmp < LenghtY; yTmp++)
                {
                    int y = (x % 2 == 0) ? yTmp : (LenghtY - yTmp - 1);
                    result.Add((x, y));
                }
            }
            return result;
        }
        private List<(int, int)> GetIndexesDiagonalSnake(Direction direction)
        {
            if (LenghtX != LenghtY)
            {
                throw new ArgumentException("Matrix isn't square");
            }

            List<(int, int)> result = new();
            for (int i = 0; i <= (LenghtX - 1) * 2; i++)
            {
                int jStart;
                int jFinish;

                if (i < LenghtX)
                {
                    //Перша половина
                    jStart = 0;
                    jFinish = i;
                }
                else
                {
                    //Друга половина
                    jStart = i - LenghtX + 1;
                    jFinish = (LenghtX - 1);
                }

                for (int j = jStart; j <= jFinish; j++)
                {
                    int x = i - j;
                    int y = j;

                    if (
                        (i % 2 != 0 && direction == Direction.RIGHT)
                        || (i % 2 == 0 && direction == Direction.DOWN)
                        )
                    {
                        (x, y) = (y, x);
                    }
                    result.Add((x,y));
                }
            }
            return result;
        }
        private void InitByIndexes(List<(int, int)> indexes)
        {
            int value = 1;
            foreach ((int x, int y) in indexes)
            {
                matrix[x, y] = value++;
            }
        }
        #endregion

        #region Enumerators
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        public IEnumerator<int> GetEnumerator()
        {
            return GetEnumeratorDiagonalSnake(Direction.RIGHT);
        }
        public IEnumerator<int> GetEnumeratorHorizontalSnake()
        {
            List<(int, int)> indexes = GetIndexesHorizontalSnake();
            foreach ((int x, int y) in indexes)
            {
                yield return matrix[x, y];
            }
        }
        public IEnumerator<int> GetEnumeratorDiagonalSnake(Direction direction)
        {
            if (LenghtX != LenghtY)
            {
                throw new ArgumentException("Matrix isn't square");
            }

            List<(int, int)> indexes = GetIndexesDiagonalSnake(direction);
            foreach ((int x, int y) in indexes)
            {
                yield return matrix[x, y];
            }
         }
        public IEnumerable<int> GetEnumerableHorizontalSnake()
        {
            List<(int, int)> indexes = GetIndexesHorizontalSnake();
            foreach ((int x, int y) in indexes)
            {
                yield return matrix[x, y];
            }
        }
        public IEnumerable<int> GetEnumerableDiagonalSnake(Direction direction)
        {
            if (LenghtX != LenghtY)
            {
                throw new ArgumentException("Matrix isn't square");
            }

            List<(int, int)> indexes = GetIndexesDiagonalSnake(direction);
            foreach ((int x, int y) in indexes)
            {
                yield return matrix[x, y];
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

