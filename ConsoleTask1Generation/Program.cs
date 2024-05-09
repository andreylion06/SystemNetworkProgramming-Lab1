using System;
using System.IO;

// Ім'я файлу: ConsoleTask1Generation.Program.cs
// Ремарка: Виконує генерацію {numberOfRandomNumbers} чисел у файл "data.dat"
// Автор: Андрій Сахно

class Program
{
    static void Main(string[] args)
    {
        try
        {
            string solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent!.FullName;
            string filePath = Path.Combine(solutionRoot, "data.dat");

            int numberOfRandomNumbers = 25; // Number of random numbers in the file
            Random random = new Random(); // Create a single instance of Random

            // Loop until 'q' is pressed
            while (true)
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    for (int i = 0; i < numberOfRandomNumbers; i++)
                    {
                        int randomNumber = random.Next(10, 101); // Random number between 10 and 100
                        writer.WriteLine(randomNumber);
                    }
                }

                Console.WriteLine($"Written {numberOfRandomNumbers} random numbers to the file {filePath}.");

                Console.WriteLine("Press Enter for the next iteration or 'q' to exit:");
                string? input = Console.ReadLine();
                if (!string.IsNullOrEmpty(input) && input.ToLower() == "q")
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}

// Кінець файлу