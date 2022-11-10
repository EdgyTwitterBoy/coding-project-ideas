// See https://aka.ms/new-console-template for more information
using System.IO;

string[] lines = File.ReadAllLines("words.txt");
List<string> words = new List<string>();

foreach (string word in lines) 
{
    if(word.Length == 5) words.Add(word);
}

File.Create("FiveLetterWords.txt").Close();
File.WriteAllLines("FiveLetterWords.txt", words);