using MiniTask.Services;
using MiniTask.Models;
using MiniTask.Repositories;
using System.Text.Json;

namespace MiniTask;

internal class Program
{
    private static readonly TaskService Service = new();

    private static readonly JsonTaskRepository Repository = new(
        Path.Combine(AppContext.BaseDirectory, "tasks.json")
    );


    private static void Main(string[] args)
    {
        // UTF-16 rozwiązuje problem odczytu
        // polskich znaków w używanym CMD.
        Console.InputEncoding = System.Text.Encoding.Unicode;
        Console.OutputEncoding = System.Text.Encoding.Unicode;

        try
        {
            Service.LoadTasks(Repository.Load());

            Console.WriteLine(
                $"Wczytano {Service.GetTasks().Count} zadań.");
        }
        catch (JsonException)
        {
            Console.WriteLine();
            Console.WriteLine("BŁĄD ODCZYTU DANYCH");
            Console.WriteLine("Plik JSON jest uszkodzony lub ma nieprawidłowy format.");
            Console.WriteLine("Program zakończy działanie, aby nie nadpisać danych.");

            return;
        }
        catch (IOException ex)
        {
            Console.WriteLine();
            Console.WriteLine("BŁĄD ODCZYTU PLIKU");
            Console.WriteLine($"Szczegóły: {ex.Message}");

            return;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine();
            Console.WriteLine("BRAK DOSTĘPU DO PLIKU");
            Console.WriteLine($"Szczegóły: {ex.Message}");

            return;
        }

        bool running = true;

        while (running)
        {
            Console.WriteLine("\n=== MiniTask v0.1 ===");
            Console.WriteLine("1. Dodaj zadanie");
            Console.WriteLine("2. Pokaż zadania");
            Console.WriteLine("3. Zakończ zadanie");
            Console.WriteLine("4. Usuń zadanie");
            Console.WriteLine("5. Edytuj zadanie");
            Console.WriteLine("6. Pokaż aktywne zadania");
            Console.WriteLine("7. Pokaż zakończone zadania");
            Console.WriteLine("0. Wyjście");
            Console.Write("Wybierz opcję: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddTask();
                    break;

                case "2":
                    ShowTasks(
                        Service.GetTasks(),
                        "Wszystkie zadania");
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

                case "6":
                    ShowTasks(
                        Service.GetActiveTasks(),
                        "Aktywne zadania");
                    break;

                case "7":
                    ShowTasks(
                        Service.GetCompletedTasks(),
                        "Zakończone zadania");
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

            if (SaveTasks())
            {
                Console.WriteLine(
                    $"Dodano zadanie nr {task.Id}.");
            }
            else
            {
                Console.WriteLine(
                    "Zadanie dodano do pamięci, ale NIE zostało zapisane na dysku.");
            }
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    private static void ShowTasks(IReadOnlyList<TaskItem> tasks, string heading)
    {
        Console.WriteLine();
        Console.WriteLine($"=== {heading} ===");

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

        if (!success)
        {
            Console.WriteLine("Nie znaleziono zadania.");
            return;
        }

        if (SaveTasks())
            Console.WriteLine("Zadanie zakończone i zapisane.");
        else
            Console.WriteLine("Zadanie zakończono tylko w pamięci.");
    }


    private static void RemoveTask()
    {
        int? id = ReadTaskId("Podaj numer zadania do usunięcia: ");

        if (id is null)
            return;

        bool removed = Service.RemoveTask(id.Value);

        if (!removed)
        {
            Console.WriteLine("Nie znaleziono zadania.");
            return;
        }

        if (SaveTasks())
            Console.WriteLine("Zadanie usunięto i zapisano.");
        else
            Console.WriteLine("Zadanie usunięto tylko w pamięci.");
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

            if (!edited)
            {
                Console.WriteLine("Nie znaleziono zadania.");
                return;
            }

            if (SaveTasks())
                Console.WriteLine("Tytuł zadania zmieniono i zapisano.");
            else
                Console.WriteLine("Tytuł zmieniono tylko w pamięci.");
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

    private static bool SaveTasks()
    {
        try
        {
            Repository.Save(Service.GetTasks());
            return true;
        }
        catch (IOException ex)
        {
            Console.WriteLine();
            Console.WriteLine("BŁĄD ZAPISU DANYCH");
            Console.WriteLine($"Szczegóły: {ex.Message}");
            return false;
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine();
            Console.WriteLine("BRAK UPRAWNIEŃ DO ZAPISU");
            Console.WriteLine($"Szczegóły: {ex.Message}");
            return false;
        }
    }

}