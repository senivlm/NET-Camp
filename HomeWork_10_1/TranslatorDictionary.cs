using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork_10_1
{
    public class TranslatorDictionary 
    {
        public Dictionary<string, string> Dictionary { get; }
        public TranslatorDictionary()
        {
            Dictionary = new();
        }
        
        public void Add(string key, string value) //always ToLower !!!!
        {
            Dictionary[key.ToLower()] = value.ToLower();
        }
        public void Add(params KeyValuePair<string, string>[] pairs)
        {
            foreach(KeyValuePair<string, string> pair in pairs)
            {
                Add(pair.Key, pair.Value);
            }
        }
        public void Add(Dictionary<string, string> dict)
        {
            foreach (KeyValuePair<string, string> pair in dict)
            {
                Add(pair.Key, pair.Value);
            }
        }

        public bool TryTranslateWord(string word, out string newWord)
        {
            newWord = "";
            string key = word.ToLower();

            if (
                String.IsNullOrEmpty(word)
                || !word.All(Char.IsLetter)
                )
            {
                newWord = word;
                return true;
            }

            if (!Dictionary.ContainsKey(key))
            {
                return false;
            }

            string wordUp = word.ToUpper();

            if (word == wordUp && word.Length > 1)
            {
                //if everyone is Upper
                newWord = Dictionary[key].ToUpper();
            }
            else if (word[0] == wordUp[0])
            {
                //if first is Upper
                string value = Dictionary[key];
                newWord = value.ToUpper()[0] + value[1..];
            }
            else
            {
                newWord = Dictionary[key];
            }

            return true;
        }
    }
}
