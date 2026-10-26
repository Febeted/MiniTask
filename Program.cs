using MiniTask.Services;

namespace MiniTask;

internal class Program
{
    private static readonly TaskService Service = new();

    private static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== MiniTask v0.1 ===");
            Console.WriteLine("1. Dodaj zadanie");
            Console.WriteLine("2. Pokaż zadania");
            Console.WriteLine("3. Zakończ zadanie");
            Console.WriteLine("0. Wyjście");
            Console.Write("Wybierz opcję: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;

                case "2":
                    ShowTasks();
                    break;

                case "3":
                    CompleteTask();
                    break;

                case "0":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Nieznana opcja.");
                    break;
            }
        }
    }

    private static void AddTask()
    {
        Console.Write("Tytuł: ");
        string title = Console.ReadLine() ?? "";

        try
        {
            var task = Service.AddTask(title);
            Console.WriteLine(
                $"Dodano zadanie nr {task.Id}.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void ShowTasks()
    {
        var tasks = Service.GetTasks();
        
        Console.WriteLine();
        Console.WriteLine("=== Lista zadań ===");

        if (tasks.Count == 0)
        {
            Console.WriteLine("Brak zadań.");
            return;
        }

        foreach (var task in tasks)
        {
            string status = task.IsCompleted ? "[x]" : "[ ]";
            Console.WriteLine(
                $"{task.Id}. {status} {task.Title}");
        }
    }

    private static void CompleteTask()
    {
        Console.Write("Numer zadania: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Nieprawidłowy numer.");
            return;
        }

        bool success = Service.CompleteTask(id);

        Console.WriteLine(success
            ? "Zadanie zakończone."
            : "Nie znaleziono zadania.");
    }
}