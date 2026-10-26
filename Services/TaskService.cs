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
}