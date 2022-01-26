using System;
using System.IO;
using System.Collections.Generic;

namespace ConsoleApp1
{


    class Program
    {

        static void Main(string[] args)
        {
            // vrátí index všech písmen ve slově
            List<int> GetIndex(string word, char toSearch)
            {
                List<int> indexy = new List<int>();
                for (int x = 0; x < word.Length; x++)
                {
                    if (word[x] == toSearch)
                    {
                        indexy.Add(x);
                    }
                }
                return indexy;
            }

            //
            string GetActualWordInString(char[] wordToJoin)
            {
                string final = "";
                foreach (char item in wordToJoin)
                {
                    final += item.ToString();
                    final += " ";
                }
                return final;
            }

            // náhodné slovo ze složky
            string GetRandomWord()
            {
                Random rnd = new Random();

                string file = File.ReadAllText("WordList.txt");
                string[] words = file.Split(',');
                int rand = rnd.Next(words.Length);
                return words[rand];
            }

            // hlavní nastavení (word, actualWord)
            string word = GetRandomWord();
            char[] actualWord = new char[word.Length];
            for (int i = 0; i < actualWord.Length; i++)
            {
                actualWord[i] = ' ';
            }
            List<char> guessedChars = new List<char>();
            int pokusy = 6;

            Tah();

            void WriteGame(char[] actualWord, int pokus)
            {
                int najitychPismen = 0;
                foreach (var item in actualWord)
                {
                    if (item != ' ')
                    {
                        najitychPismen++;
                    }
                }
                if (najitychPismen == word.Length)
                {
                    // vyhral si
                    Console.WriteLine("");
                    Console.WriteLine("Dobrá práce, uhádnul jsi hledané slovo, hledané slovo bylo: ");
                    Console.WriteLine("=====  " + word + "  =====");
                }
                else if (pokusy == 0)
                {
                    Console.WriteLine("");
                    Console.WriteLine("Bohužel se ti nepodařilo najít správné slovo ( " + word + " ) zkus to příště");
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Dobrá práce, uhádnul jsi " + najitychPismen + " z " + word.Length + " písmen.Na zbytek ti zbývá ještě " + pokusy + " pokusů. ");
                    Console.WriteLine("=====  " + GetActualWordInString(actualWord) + "  =====");
                    Tah();
                }
            }

            // hra
            void Tah()
            {

                Console.WriteLine("Zadej hádané písmeno ve slově: ");
                char hadanyChar;
                try
                {
                    hadanyChar = Char.Parse(Console.ReadLine());
                    List<int> indexy = GetIndex(word, hadanyChar);

                    // zkontroluje pokud se už nehádalo stejné písmeno
                    if (guessedChars.Contains(hadanyChar))
                    {
                        pokusy -= 1;
                        WriteGame(actualWord, pokusy);
                    }

                    // zkontroluje pokud slovo obsahuje hledané písmeno

                    else if (indexy.Count == 0)
                    {
                        pokusy = pokusy - 1;
                        WriteGame(actualWord, pokusy);
                    }
                    else
                    {
                        // slovo obsahuje písmeno
                        guessedChars.Add(hadanyChar);
                        foreach (int item in indexy)
                        {
                            actualWord[item] = word[item];
                        }
                        WriteGame(actualWord, pokusy);
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Špatně zadané písmeno... zkus to znova :)");
                    Tah();
                }
            }
           
        }
    }
}
