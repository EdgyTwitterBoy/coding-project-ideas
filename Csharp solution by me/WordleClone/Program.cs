// See https://aka.ms/new-console-template for more information

using System.Drawing;

public class Program
{
    private static List<string> guessedWords = new();
    private static bool correctGuess = false;
    private static string word;
    private static ConsoleColor defaultConsoleColor;
    private static string[] words = new[]{""};

    public static void Main()
    {
        words = File.ReadAllLines("FiveLetterWords.txt");
        defaultConsoleColor = Console.ForegroundColor;
        
        word = SelectWord();
        
        while (guessedWords.Count < 6 && !correctGuess)
        {
            Console.Clear();
            WriteGame();

            while (!GetUserInput())
            {
                WriteGame();
            }
        }
        
        Console.Clear();
        WriteGame();

        if (!correctGuess)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You lost!");
            Console.WriteLine("The word was: " + word);
        }
        
        Console.ForegroundColor = defaultConsoleColor;
        Console.WriteLine("Would you like to play again? (y/n)");
        if (Console.ReadLine().ToLower() == "y")
        {
            guessedWords.Clear();
            correctGuess = false;
            Main();
        }
    }

    private static bool GetUserInput()
    {
        string? userInput = Console.ReadLine();
        if (userInput.Length != 5)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Word must be 5 letters long!");
            Console.ForegroundColor = defaultConsoleColor;
            return false;
        }
        
        if (!words.Contains(userInput))
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("This word doesn't exist!");
            Console.ForegroundColor = defaultConsoleColor;
            return false;
        }

        if (userInput.ToLower() == word.ToLower()) correctGuess = true;
        guessedWords.Add(userInput);
        return true;
    }

    private static void WriteGame()
    {
        Console.WriteLine("Guess a five letter word: " + word);

        foreach (var w in guessedWords)
        {
            WriteWordToGrid(w);
        }

        for (int i = 0; i < 6 - guessedWords.Count; i++)
        {
            WriteEmptyGird();
        }

        if (correctGuess)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You won!");
        }
        else
        {
            Console.WriteLine("Type your guess: ");   
        }
    }

    private static string SelectWord()
    {
        Random random = new Random();
        int index = random.Next(words.Length);
        return words[index];
    }

    private static void WriteEmptyGird()
    {
        Console.WriteLine("|_|_|_|_|_|");
    }

    private static void WriteWordToGrid(string guessedWord)
    {
        int index = 0;
        Console.Write("|");
        foreach (var ch in guessedWord)
        {
            if (word.Contains(ch)) Console.ForegroundColor = ConsoleColor.Yellow;
            if (word[index] == ch) Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(ch);
            Console.ForegroundColor = defaultConsoleColor;
            Console.Write("|");
            index++;
        }
        Console.Write("\n");
    }
}