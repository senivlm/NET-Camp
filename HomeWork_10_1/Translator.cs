using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_10_1
{
    public class Translator
    {// чому обрано клас String, а не StringBuilder
        private const int COUNT_TRY = 3;
        private TranslatorDictionary vocabluary;

        #region delegates
        public Action<string>? ExtDisplayAction;
        public Func<string?>? ExtInputAction;
        #endregion

        public Translator() : this(new TranslatorDictionary()) { }
        public Translator(TranslatorDictionary dictionary)
        {// проблема неглибоких копій
            this.vocabluary = dictionary;
        }

        public void SetDictionary(TranslatorDictionary dictionary)
        {
            this.vocabluary = dictionary;
        }

        public string Translate(string text)
        {
            List<string> words = TextSplit(text);
            StringBuilder sbResult = new();
            for (int i = 0; i < words.Count; i++)
            {
                string word = words[i];
                bool translationDone = false;
                for (int j = 0; j < COUNT_TRY; j++)
                {
                    //Після додавання у словник шнукаю через TryTranslateWord щоб врахувати розмір літер реалізованому у TryTranslateWord  
                    if (vocabluary.TryTranslateWord(word, out string newWord))
                    {
                        sbResult.Append(newWord);
                        translationDone = true;
                        break;
                    }

                    ExtDisplayAction?.Invoke($"Enter the translation of the word: {word}");
                    string? wordInput = ExtInputAction?.Invoke();
                    ExtDisplayAction?.Invoke($"You entered: {wordInput}");
                    if (!String.IsNullOrEmpty(wordInput) && wordInput.All(Char.IsLetter))
                    {
                        vocabluary.Add(word, wordInput);
                    }
                    else
                    {
                        ExtDisplayAction?.Invoke($"The entered word is incorrect");
                    }
                }
                if (!translationDone)
                {
                    sbResult.Append(word);
                }
            }
            return sbResult.ToString();
        }
        public List<string> Translate(List<string> text)
        {
            List<string> textTranslated = new();
            foreach (string strLine in text)
            {
                textTranslated.Add(Translate(strLine));
            }
            return textTranslated;
        }

        private List<string> TextSplit(string text)
        {
            // Навіщо це робити вручну?
            List<string> strings = new();

            bool isWorld = false;
            int indexStart = 0;
            for (int i = 0; i < text.Length; i++)
            {
                string currentChar = text.Substring(i, 1);
                bool isLetter = currentChar.All(Char.IsLetter);
                if (isWorld != isLetter)
                {
                    int lenghtСycle = i - indexStart;
                    if (lenghtСycle > 0)
                    {
                        strings.Add(text.Substring(indexStart, lenghtСycle));
                    }
                    isWorld = isLetter;
                    indexStart = i;
                }
            }
            int lenght = text.Length - indexStart;
            if (lenght > 0)
            {
                strings.Add(text.Substring(indexStart, lenght));
            }
            return strings;

        }
    }
}
 
