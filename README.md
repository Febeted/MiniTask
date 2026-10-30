# MiniTask

MiniTask is a simple task manager written in C# and .NET 10.

## Current Features

- Add, list, edit and delete tasks
- Mark tasks as completed
- Display active and completed tasks
- Basic input validation and error handling
- JSON data persistence
- Automatic creation of the user data directory

## Data Storage

Task data is stored locally in:

```text
%LOCALAPPDATA%\MiniTask\tasks.json
```

The application creates the `MiniTask` directory automatically when it starts.

## Run

```bash
dotnet run
```

## Roadmap

- [x] Console application
- [x] Task management
- [x] JSON persistence
- [x] Unit tests
- [x] Error handling
- [ ] Windows Forms interface
- [ ] Shared application logic for multiple interfaces

## Status

Version 0.1 — work in progress.

## License

This project is licensed under the MIT License.

See the [LICENSE](LICENSE) file for details.