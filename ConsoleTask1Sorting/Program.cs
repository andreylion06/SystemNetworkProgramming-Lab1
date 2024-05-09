using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// Ім'я файлу: ConsoleTask1Sorting.Program.cs
// Ремарка: Виконує сортування чисел з файлу "data.dat" з затримкою
// Автор: Андрій Сахно

class Program
{
    static async Task Main(string[] args)
    {
        try
        {
            string solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent!.FullName;
            string filePath = Path.Combine(solutionRoot, "data.dat");

            // Read numbers from the file
            int[] numbers = File.ReadAllLines(filePath).Select(int.Parse).ToArray();

            // Loop until 'q' is pressed
            while (true)
            {
                Console.Write("Press Space to start sorting ==> ");
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                Console.WriteLine();

                if (keyInfo.Key == ConsoleKey.Spacebar)
                {
                    Console.Clear();
                    break;
                }
            }

            await SortAndWriteToFileWithDelay(numbers, filePath);

            Console.WriteLine($"\n\n*** Numbers in the file {filePath} have been sorted. ***");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static async Task SortAndWriteToFileWithDelay(int[] array, string filePath)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - i - 1; j++)
            {
                if (array[j] > array[j + 1])
                {
                    Console.WriteLine($"'{array[j]}' and '{array[j + 1]}' have been swapped");

                    Swap(ref array[j], ref array[j + 1]);

                    File.WriteAllLines(filePath, array.Select(n => n.ToString()));

                    await Task.Delay(1000);
                }
            }
        }
    }

    static void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }
}

// Кінець файлу