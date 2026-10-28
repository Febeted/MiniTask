using MiniTask.Models;

namespace MiniTask.Services;

public class TaskService
{
    private readonly List<TaskItem> _tasks = new();
    private int _nextId = 1;

    public TaskItem AddTask(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(
                "Tytuł zadania nie może być pusty.");

        var task = new TaskItem
        {
            Id = _nextId++,
            Title = title.Trim()
        };

        _tasks.Add(task);
        return task;
    }

    public IReadOnlyList<TaskItem> GetTasks()
    {
        return _tasks.AsReadOnly();
    }

    public bool CompleteTask(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);

        if (task == null)
            return false;

        task.IsCompleted = true;
        return true;
    }


    public bool RemoveTask(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);

        if (task is null)
            return false;

        _tasks.Remove(task);
        return true;
    }


    public bool UpdateTask(int id, string newTitle)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);

        if (task is null)
            return false;

        if (string.IsNullOrWhiteSpace(newTitle))
            throw new ArgumentException(
                "Tytuł zadania nie może być pusty.");

        task.Title = newTitle.Trim();
        return true;
    }

    public IReadOnlyList<TaskItem> GetActiveTasks()
    {
        return _tasks
            .Where(task => !task.IsCompleted)
            .ToList();
    }

    public IReadOnlyList<TaskItem> GetCompletedTasks()
    {
        return _tasks
            .Where(task => task.IsCompleted)
            .ToList();
    }

    public void LoadTasks(IEnumerable<TaskItem> tasks)
    {
        _tasks.Clear();
        _tasks.AddRange(tasks);

        _nextId = _tasks.Count > 0
            ? _tasks.Max(t => t.Id) + 1
            : 1;
    }

}