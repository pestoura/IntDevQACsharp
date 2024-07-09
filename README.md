# Folder Sync C# Project

This C# project facilitates one-way synchronization of two folders: a source folder and a replica folder. The synchronization ensures that the replica folder maintains an identical copy of the source folder's contents, handling file creations, updates, and deletions as necessary.

## Project Structure

IntDevQACsharp/  
│  
├── src/  
│ ├── Program.cs  
│ ├── Logger.cs  
│ ├── FileHelper.cs  
│ └── SyncManager.cs  
│  
├── tests/  
│ └── TestFolderSync.cs  
│  
├── README.md  
└── setup.csproj  

### Files

- **Program.cs**: Entry point that initializes logging, sets up synchronization, and runs it periodically.
- **Logger.cs**: Handles logging both to the console and a log file, updated for thread-safe logging.
- **FileHelper.cs**: Provides utility methods for file and directory operations.
- **SyncManager.cs**: Manages the synchronization process between the source and replica folders.
- **tests/TestFolderSync.cs**: Unit tests using NUnit framework to verify synchronization functionality.

## Features

- **One-way Synchronization**: Automatically updates the replica folder to match the source folder's content.
- **Periodic Execution**: Synchronization process runs at intervals specified by the user.
- **Logging**: Detailed logs of file operations (create, copy, delete) are generated to a specified log file and console output.
- **Command Line Arguments**: Configure source folder path, replica folder path, synchronization interval, and log file path via command line arguments.

## Dependencies

- [.NET Core SDK](https://dotnet.microsoft.com/download) (for building and running the project)
- [NUnit](https://nunit.org/) (for running unit tests)

## How to Use

1. **Clone Repository**:
   ```bash
   git clone https://github.com/pestoura/IntDevQACsharp.git
   cd IntDevQACsharp
   cd src

2. **Build the Project:**
   ```bash
   dotnet build
   
3. **Run the Project:**
   ```bash
   dotnet run --project src <source> <replica> <interval> <logFile>
   ```
   Replace **source**, **replica**, **interval**, and **logFile** with actual paths and interval in seconds.

   Example:
   ```bash
   dotnet run --project src /path/to/source /path/to/replica 60 /path/to/logfile.log

4. **Run Unit Tests:**
   ```bash
   cd tests
   dotnet test
   ```
   This command runs all the unit tests located in the tests directory.

##Unit Tests

Unit tests are implemented using NUnit framework to verify the functionality of synchronization:

TestFolderSync.cs: Tests various scenarios of file creation, deletion, and update to ensure synchronization works correctly.

##Contributing

Contributions are welcome! If you find any issues or have improvements in mind, feel free to open an issue or submit a pull request.

##License

This project is licensed under the MIT License. See the LICENSE file for details.
   
