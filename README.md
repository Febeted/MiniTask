# MiniTask

MiniTask is a simple task management application written in C# and .NET 10.

The project is developed as a practical programming project, focusing on clean code, data persistence, error handling and automated testing.

## Current Features

- Add, list, edit and delete tasks
- Mark tasks as completed
- Display active and completed tasks
- Input validation and error handling
- JSON data persistence
- Automatic creation of the user data directory
- Unit tests with xUnit

## Technologies

- C#
- .NET 10
- JSON
- xUnit
- Git

## Data Storage

Task data is stored locally in:

```text
%LOCALAPPDATA%\MiniTask\tasks.json
```

The application automatically creates the `MiniTask` directory when needed.

## Requirements

- .NET 10 SDK

## Run

Clone the repository:

```bash
git clone https://github.com/Febeted/MiniTask.git
cd MiniTask
```

Run the application:

```bash
dotnet run
```

## Tests

Run the automated test suite:

```bash
dotnet test
```

## Roadmap

- [ ] Extract shared application logic
- [ ] Separate business logic from the console interface
- [ ] Add an alternative user interface (planned: Windows Forms)
- [ ] Expand automated test coverage

## Status

Version 0.1 — work in progress.

## License

This project is licensed under the MIT License.

See the [LICENSE](LICENSE) file for details.