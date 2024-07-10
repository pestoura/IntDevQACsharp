# Internal Development in QA (SDET) C#

This application synchronizes files between two directories at specified intervals using SHA-256 hashing for comparison. It logs synchronization activities to a specified log file.

## Project Structure and Conventions

IntDevQACsharp/  
│  
├── src/  
│ ├── Program.cs   
│    
├── README.md  
└── IntDevQACsharp.csproj  

## Features

- One-way Synchronization: Automatically updates the replica folder to match the source folder's content.
- Efficient file synchronization using SHA-256 hashing.
- Logging of synchronization activities to a specified file.
- Supports recursive synchronization of subdirectories.

## Implementation Details

- Main File: Program.cs contains the main entry point and synchronization logic.
- File Hashing: Uses SHA256 from System.Security.Cryptography for file integrity checks.
- Logging: Writes synchronization activities and errors to a specified log file using StreamWriter.

## Usage

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)

### Running the Application

1. **Clone the repository:**
   ```bash
   git clone https://github.com/pestoura/IntDevQACsharp.git

2. **Navigate to the project directory:**
   ```bash
   cd IntDevQACsharp

3. **Compile and run the application:**
   ```bash
   dotnet build  
   dotnet run --project IntDevQACsharp.csproj -- <source_dir> <replica_dir> <interval_seconds> <log_file>
   ```
   Replace <source_dir>, <replica_dir>, <interval_seconds>, and <log_file> with your specific directories and settings.
   
4. **Run the Project:**
   ```bash
   dotnet run --project src <source> <replica> <interval> <logFile>
   ```
   Replace **source**, **replica**, **interval**, and **logFile** with actual paths and interval in seconds.

   ### Example:
   
   To synchronize files from C:\Source to D:\Replica every 60 seconds and log activities to sync.log:
   ```bash
   dotnet run -- "C:\Source" "D:\Replica" 60 sync.log
   
## Contributing

Contributions are welcome! If you find any issues or have improvements in mind, feel free to open an issue or submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](./LICENSE) file for more details.
   
