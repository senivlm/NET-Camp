using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_05
{
    public class SerialStorageFile : ISerialStorage
    {
        #region fields
        private readonly string nameFile = "";
        private readonly StreamWriter writer;
        private bool writerIsOpened;
        #endregion

        #region constructors
        public SerialStorageFile(string nameFile)
        {
            this.nameFile = nameFile;
            this.writer = new StreamWriter(this.nameFile);
            this.writerIsOpened = true;

        }

        ~SerialStorageFile()
        {
            if (writer != null)
            {
                if (writerIsOpened)
                {
                    writer.Close();
                }
                if (File.Exists(nameFile))
                {
                    File.Delete(nameFile);
                }
            }
        }
        #endregion

        #region methods
        public void Add(int nom)
        {
            if (!writerIsOpened)
            {
                throw new ArgumentException($"Storage is closed");
            }
            writer.WriteLine(nom);
        }
        public void ExportToArray(int[] extArray, int indexStart1)
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
        #endregion

    }
}
