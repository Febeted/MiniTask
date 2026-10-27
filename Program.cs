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
            Console.WriteLine("4. Usuń zadanie");
            Console.WriteLine("5. Edytuj zadanie");
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

                case "4":
                    RemoveTask();
                    break;

                case "5":
                    EditTask();
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
        int? id = ReadTaskId("Podaj numer zadania do zakończenia: ");

        if (id is null)
            return;

        bool success = Service.CompleteTask(id.Value);

        Console.WriteLine(success
            ? "Zadanie zakończone."
            : "Nie znaleziono zadania.");
    }


    private static void RemoveTask()
    {
        int? id = ReadTaskId("Podaj numer zadania do usunięcia: ");

        if (id is null)
            return;

        bool removed = Service.RemoveTask(id.Value);

        Console.WriteLine(removed
            ? "Zadanie zostało usunięte."
            : "Nie znaleziono zadania.");
    }

    private static void EditTask()
    {
        int? id = ReadTaskId("Podaj numer zadania do zmiany: ");

        if (id is null)
            return;

        Console.Write("Podaj nowy tytuł: ");
        string title = Console.ReadLine() ?? "";

        try
        {
            bool edited = Service.UpdateTask(id.Value, title);

            Console.WriteLine(edited
                ? "Tytuł zadania został zmieniony."
                : "Nie znaleziono zadania.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }        
    }

    private static int? ReadTaskId(string message)
    {
        Console.Write(message);

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Nieprawidłowy numer.");
            return null;
        }

        return id;
    }


}