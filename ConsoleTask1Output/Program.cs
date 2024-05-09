using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

// Ім'я файлу: ConsoleTask1Output.Program.cs
// Ремарка: Виводить числа з файлу "data.dat" у форматі зірочок
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
            while (true)
            {
                int[] numbers = File.ReadAllLines(filePath).Select(int.Parse).ToArray();

                foreach (var item in numbers)
                {
                    for (int i = 0; i < item; i++)
                    {
                        Console.Write("*");
                    }
                    Console.WriteLine();
                }

                await Task.Delay(500);
                Console.Clear();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

// Кінець файлу