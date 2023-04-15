using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Dictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> dictionary = LoadDictionaryFromJsonFile("Dictionary.Resources.dictionary.json");

            Console.WriteLine("Выберите язык перевода: ");
            Console.WriteLine("1 - Английский -> Русский");
            Console.WriteLine("2 - Русский -> Английский");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 2)
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, выберите язык перевода еще раз:");
            }

            Console.Write("Введите слово: ");
            string word = Console.ReadLine().ToLower();

            if (choice == 2)
            {
                dictionary = ReverseDictionary(dictionary);
            }

            if (dictionary.TryGetValue(word, out string translation))
            {
                Console.WriteLine($"Перевод: {translation}");
            }
            else
            {
                Console.WriteLine("Перевод не найден");
            }

            Console.ReadLine();
        }

        static Dictionary<string, string> LoadDictionaryFromJsonFile(string fileName)
        {
            Dictionary<string, string> dictionary;
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(fileName))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string json = reader.ReadToEnd();
                    dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                }
            }

            return dictionary;
        }

        static Dictionary<string, string> ReverseDictionary(Dictionary<string, string> dictionary)
        {
            return dictionary.ToDictionary(x => x.Value.ToLower(), x => x.Key.ToLower());
        }
    }
}
