using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_10_1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                //налаштування дублючання єкрану у файл
                Logger Dispay = new("..\\..\\..\\result.txt");
                Dispay.ExtDisplayAction = Console.WriteLine;
                Dispay.WithTime = false;
                
                //Завантажуємо текст і словник
                List<string> text = TranslatorReader.ReadText(@"../../../Text.txt");
                TranslatorDictionary dictionary = TranslatorReader.ReadDictionary(@"../../../Dictionary.txt");

                //Налаштовуємо перекладач
                Translator translator = new(dictionary);
                translator.ExtDisplayAction = Console.WriteLine;
                //Подія тут є вдалою.
                translator.ExtDisplayAction += Dispay.Add;
                translator.ExtInputAction = Console.ReadLine;

                //Перекладаємо
                List<string> textTranslated = translator.Translate(text);

                //Виводимо результат
                translator.ExtDisplayAction?.Invoke("\nOriginal text:");
                text.ForEach(translator.ExtDisplayAction ?? Console.WriteLine);
         
                translator.ExtDisplayAction?.Invoke("\nTranslated text:");
                textTranslated.ForEach(translator.ExtDisplayAction ?? Console.WriteLine);

            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("FileNotFoundException");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
