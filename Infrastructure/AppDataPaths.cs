
namespace MiniTask.Infrastructure
{
    public class AppDataPaths
    {
        private readonly string _tasksFilePath;

        public string TasksFilePath => _tasksFilePath;

        public AppDataPaths(string fileName)
        {
            string folderPath = Path.Combine(
                Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData),
                "MiniTask"
            );

            Directory.CreateDirectory(folderPath);

            _tasksFilePath = Path.Combine(folderPath, fileName);
        }
    }
}
