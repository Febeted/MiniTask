using MiniTask.Infrastructure;

namespace MiniTask.Tests
{
    public class AppDataPathsTests
    {
        [Fact]
        public void Constructor_ShouldCreateDirectory()
        {
            // Arrange
            var fileName = "tasks_test.json";

            // Act
            var appPath = new AppDataPaths(fileName);

            // Assert
            string? directoryPath = Path.GetDirectoryName(appPath.TasksFilePath);

            Assert.NotNull(directoryPath);
            Assert.True(Directory.Exists(directoryPath));
        }

        [Fact]
        public void TasksFilePath_ShouldContainFileName()
        {
            // Arrange
            var fileName = "tasks_test.json";

            // Act
            var appPath = new AppDataPaths(fileName);

            // Assert
            Assert.EndsWith(fileName, appPath.TasksFilePath);
        }

        [Fact]
        public void TasksFilePath_ShouldBeInsideLocalApplicationData()
        {
            // Arrange
            var fileName = "tasks_test.json";

            // Act
            var appPath = new AppDataPaths(fileName);

            // Assert
            string? directoryPath = Path.GetDirectoryName(appPath.TasksFilePath);
            string expectedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MiniTask");
            
            Assert.NotNull(directoryPath);
            Assert.Equal(expectedPath, directoryPath);
            Assert.Contains(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), directoryPath);
        }
    }
}
