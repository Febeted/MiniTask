
using System.Text.Json;
using MiniTask.Models;

namespace MiniTask.Repositories;

public class JsonTaskRepository
{
    private readonly string _filePath;

    public JsonTaskRepository(string filePath)
    {
        _filePath = filePath;
    }

    public void Save(IEnumerable<TaskItem> tasks)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        string json = JsonSerializer.Serialize(tasks, options);

        string tempPath = _filePath + ".tmp";

        try
        {
            // 1. Zapis do pliku tymczasowego
            File.WriteAllText(tempPath, json);

            // 2. Podmiana pliku docelowego
            File.Move(tempPath, _filePath, overwrite: true);
        }
        finally
        {
            // 3. Usunięcie pozostałości po nieudanym zapisie
            if (File.Exists(tempPath))
                File.Delete(tempPath);
        }
    }

    public List<TaskItem> Load()
    {
        if (!File.Exists(_filePath))
            return new List<TaskItem>();

        string json = File.ReadAllText(_filePath);

        return JsonSerializer.Deserialize<List<TaskItem>>(json)
               ?? new List<TaskItem>();
    }
}
