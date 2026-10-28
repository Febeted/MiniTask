
using MiniTask.Models;
using MiniTask.Repositories;

namespace MiniTask.Tests;

public class JsonTaskRepositoryTests
{
    [Fact]
    public void SaveAndLoad_ShouldPreserveTasks()
    {
        string path = Path.Combine(
            Path.GetTempPath(),
            $"{Guid.NewGuid()}.json");

        try
        {
            var repository = new JsonTaskRepository(path);

            var tasks = new List<TaskItem>
            {
                new TaskItem
                {
                    Id = 1,
                    Title = "Zażółć gęślą jaźń",
                    IsCompleted = true
                }
            };

            repository.Save(tasks);

            var loaded = repository.Load();

            Assert.Single(loaded);
            Assert.Equal(1, loaded[0].Id);
            Assert.Equal(
                "Zażółć gęślą jaźń",
                loaded[0].Title);
            Assert.True(loaded[0].IsCompleted);
        }
        finally
        {
            File.Delete(path);
        }
    }

    [Fact]
    public void Load_WhenFileDoesNotExist_ReturnsEmptyList()
    {
        string path = Path.Combine(
            Path.GetTempPath(),
            $"{Guid.NewGuid()}.json");

        var repository = new JsonTaskRepository(path);

        var tasks = repository.Load();

        Assert.Empty(tasks);
    }

    [Fact]
    public void Save_WhenListIsEmpty_LoadsEmptyList()
    {
        string path = Path.Combine(
            Path.GetTempPath(),
            $"{Guid.NewGuid()}.json");

        try
        {
            var repository = new JsonTaskRepository(path);

            repository.Save(new List<TaskItem>());

            var tasks = repository.Load();

            Assert.Empty(tasks);
        }
        finally
        {
            File.Delete(path);
        }
    }
}
