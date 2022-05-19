using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_01
{
    internal class Program
    {

        static void Main(string[] args)
        {

            Console.WriteLine("Input Size");
            string? str = Console.ReadLine();
            int size;
            if (!Int32.TryParse(str, out size))
            {
                Console.WriteLine("Invalid Size");
                Console.ReadLine();
                return;
            }

            if (size % 2 == 0)
            {
                Console.WriteLine("Invalid Size");
                Console.ReadLine();
                return;
            }

            int[,] tabl = new int[size, size];

            int sizeHaf = size / 2;
            for (int x = 0; x < size; x++)
            {

                for (int y = 0; y < size; y++)
                {
                    if (x + y < sizeHaf) tabl[x, y] = 1;
                    else if (x + (size - 1 - y) < sizeHaf) tabl[x, y] = 2;
                    else if ((size - 1 - x) + y < sizeHaf) tabl[x, y] = 3;
                    else if ((size - 1 - x) + (size - 1 - y) < sizeHaf) tabl[x, y] = 4;
                    else tabl[x, y] = 0;
                }
            }

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    Console.Write(tabl[x, y]);

                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }

    }
}
