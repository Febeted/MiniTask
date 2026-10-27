
using MiniTask.Services;

namespace MiniTask.Tests;

public class TaskServiceTests
{
    [Fact]
    public void AddTask_ValidTitle_AddsTask()
    {
        // Arrange
        var service = new TaskService();

        // Act
        var task = service.AddTask("Learn C#");

        // Assert
        Assert.Equal(1, task.Id);
        Assert.Equal("Learn C#", task.Title);
        Assert.False(task.IsCompleted);
        Assert.Single(service.GetTasks());
    }


    [Fact]
    public void RemoveTask_ExistingTask_RemovesTask()
    {
        // Arrange
        var service = new TaskService();
        var task1 = service.AddTask("Learn C#");
        var task2 = service.AddTask("Learn testing");

        // Act
        bool removed = service.RemoveTask(task1.Id);

        // Assert
        Assert.True(removed);
        Assert.Single(service.GetTasks());
        Assert.Equal(task2.Id, service.GetTasks()[0].Id);
    }

    [Fact]
    public void RemoveTask_NonExistingTask_ReturnsFalse()
    {
        // Arrange
        var service = new TaskService();

        // Act
        bool removed = service.RemoveTask(999);

        // Assert
        Assert.False(removed);
        Assert.Empty(service.GetTasks());
    }

}
