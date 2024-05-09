using System;
using System.IO;
using System.Threading;

// Ім'я файлу: ConsoleTask2.Program.cs
// Ремарка: Сортування та вивід чисел у форматі зірочок синхронізовані за допомогою SemaphoreSlim.
//          Реалізована обробка виняткових ситуацій, у випадку якщо один з потоків стикнеться з
//          винятковою ситуацією - зупиняться обидва
// Автор: Андрій Сахно

class Program
{
    static SemaphoreSlim semaphore = new SemaphoreSlim(2);
    static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    static string FilePath { get; set; } = String.Empty;

    static async Task Main(string[] args)
    {
        string solutionRoot = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.Parent!.FullName;
        FilePath = Path.Combine(solutionRoot, "data.dat");

        Task output = Output();
        Task sort = Sort();

        await Task.WhenAll(output, sort);
    }

    static public async Task Output()
    {
        try
        {
            await semaphore.WaitAsync(cancellationTokenSource.Token);

            do
            {
                Console.Clear();
                int[] numbers = File.ReadAllLines(FilePath).Select(int.Parse).ToArray();

                foreach (var item in numbers)
                {
                    for (int i = 0; i < item; i++)
                    {
                        Console.Write("*");
                    }
                    Console.WriteLine();
                }

                await Task.Delay(500);
            } while (!cancellationTokenSource.Token.IsCancellationRequested);
        }
        catch (Exception ex)
        {
            cancellationTokenSource.Cancel();
            Console.WriteLine($"Exception in Output: {ex.Message}");
        }
    }

    static public async Task Sort()
    {
        try
        {
            await semaphore.WaitAsync(cancellationTokenSource.Token);

            int[] numbers = File.ReadAllLines(FilePath).Select(int.Parse).ToArray();
            await SortAndWriteToFileWithDelay(numbers);

            semaphore.Release();
            if(!cancellationTokenSource.Token.IsCancellationRequested)
                Console.WriteLine($"\n\n~~~ Numbers in the file {FilePath} have been sorted. ~~~");
            
            cancellationTokenSource.Cancel();

            Console.WriteLine($"Stoping the semaphore...\n\n");

        }
        catch (Exception ex)
        {
            cancellationTokenSource.Cancel();
            Console.WriteLine($"Exception in Sort: {ex.Message}");
        }
    }

    static private async Task SortAndWriteToFileWithDelay(int[] array)
    {
        for (int i = 0; i < array.Length - 1; i++)
        {
            for (int j = 0; j < array.Length - i - 1; j++)
            {
                if (cancellationTokenSource.Token.IsCancellationRequested)
                    return;

                if (array[j] > array[j + 1])
                {
                    Swap(ref array[j], ref array[j + 1]);
                    File.WriteAllLines(FilePath, array.Select(n => n.ToString()));
                    await Task.Delay(1000);
                }
            }
            if (cancellationTokenSource.Token.IsCancellationRequested)
                return;
        }
    }

    static private void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }
}

// Кінець файлу