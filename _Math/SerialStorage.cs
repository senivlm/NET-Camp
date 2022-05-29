using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Math
{
    public class SerialStorage
    {
        #region fields
        //array
        private readonly int[]? array;
        private int indexArray;

        //file
        private readonly string nameFile = "";
        private readonly StreamWriter? writer;
        private bool writerIsOpened;
        #endregion

        #region constructors
        public SerialStorage(int n)
        {
            this.array = new int[n];
            this.indexArray = 0;

            this.nameFile = "";
            this.writer = null;
            this.writerIsOpened = true;
        }

        public SerialStorage(string nameFile)
        {
            this.nameFile = nameFile;
            this.writer = new StreamWriter(this.nameFile);
            this.writerIsOpened = true;

            this.array = null;
            this.indexArray = 0;
        }

        ~SerialStorage()
        {
            if (writer != null)
            {
                writer.Close();
                if (File.Exists(nameFile))
                {
                    File.Delete(nameFile);
                }
            }
        }
        #endregion

        public void Add(int nom)
        {
            if (array != null)
            {
                if (indexArray >= array.Length)
                {
                    throw new ArgumentException($"Storage is full. indexArray = {indexArray}");
                }
                array[indexArray++] = nom;
            }
            else if (writer != null)
            {
                if (!writerIsOpened)
                {
                    throw new ArgumentException($"Storage is closed");
                }
                writer.WriteLine(nom);
            }
            else
            {
                throw new Exception("Storage not initialized");
            }
        }

        public void ExportToArray(int[] extArray, int indexStart1)
        {
            if (array != null)
            {   
                for (int i = 0; i < array.Length; i++)
                {
                    extArray[indexStart1 + i] = array[i];
                }
            }
            else if (writer != null)
            {
                if (writerIsOpened)
                {
                    writer.Close();
                    writerIsOpened = false;
                }

                using (StreamReader reader = new(nameFile))
                {
                    int i = 0;
                    while (!reader.EndOfStream)
                    {
                        if (!Int32.TryParse(reader.ReadLine(), out extArray[indexStart1 + (i++)]))
                        {
                            throw new IOException("Error working with temporary file");
                        }
                    }
                }

            }
            else
            {
                throw new Exception("Storage not initialized");
            }

        }
    }
}
