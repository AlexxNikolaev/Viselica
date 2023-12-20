using System;
using System.Threading;

struct Game
{
    public string word;
    public char[] stars;
    public int count;
}

internal class Program
{
    private static Game[] games;
    private static void Main(string[] args)
    {
        games = new Game[3]; 

        string[] wordsToGuess = { "шина", "шишка", "монітор" };

        for (int i = 0; i < 3; i++)
        {
            games[i].word = wordsToGuess[i];
            games[i].stars = new string('*', games[i].word.Length).ToCharArray();
        }

        const int maxCount = 15;

        char symbol = ' ';
        Thread th = new Thread(() =>
        {
            while (true)
            {
                Console.WriteLine(new string('-', 30));
                for (int i = 0; i < 3; i++)
                {
                    NewWord(symbol, i);
                    Console.WriteLine($"Загаданное слово {i + 1}: {string.Join("", games[i].stars)}");
                }
                Console.WriteLine(new string('-', 30));
                Console.WriteLine($"Количество попыток {games[0].count}, У Вас еще осталось {maxCount - games[0].count}");

                bool allWordsGuessed = true;
                for (int i = 0; i < 3; i++)
                {
                    if (!games[i].word.Equals(string.Join("", games[i].stars)))
                    {
                        allWordsGuessed = false;
                        break;
                    }
                }

                if (allWordsGuessed)
                {
                    DisplayVictoryMessage();
                    return;
                }

                if (games[0].count == maxCount)
                {
                    Console.WriteLine("Вы не угадали все слова!");
                    return;
                }

                Thread.Sleep(200);
                Console.Clear();
            }
        });

        th.Start();
        Thread th2 = new Thread(() =>
        {
            while (true)
            {
                symbol = (char.ToLower(Console.ReadKey().KeyChar));
                games[0].count++;
                Thread.Sleep(300);
            }
        });
        th2.IsBackground = true;
        th2.Start();
        Console.ReadKey(true);
    }

    static void NewWord(char s, int index)
    {
        for (int i = 0; i < games[index].word.Length; i++)
        {
            if (games[index].word[i] == s)
            {
                games[index].stars[i] = s;
            }
        }
    }

    static void DisplayVictoryMessage()
    {
        Console.WriteLine("ПЕРЕМОГА!");
    }
}



