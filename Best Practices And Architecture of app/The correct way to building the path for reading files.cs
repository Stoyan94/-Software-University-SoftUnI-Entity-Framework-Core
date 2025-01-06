string path = Path.Combine("Data", "Datasets", "cinemas.json");
string data = File.ReadAllText(path);


Explanation:
Path.Combine("Data", "Datasets", "cinemas.json"):

Path.Combine is a method used to safely combine multiple strings into a single file or directory path.
It ensures that the proper directory separator (e.g., / on Unix-based systems, \ on Windows) is inserted between the different parts of the path.
This is especially useful because file paths might differ based on the operating system. Using Path.Combine helps avoid manual errors when concatenating the path components.
In this example, the combined path is "Data\Datasets\cinemas.json" or "Data/Datasets/cinemas.json", depending on the platform.

File.ReadAllText(path):

File.ReadAllText is a method that reads all the content of the file specified by the path as a string.
It loads the entire file content into memory, which is useful when dealing with relatively small to medium-sized files.

Why is this a good way to read files?

Platform-Independent Path Construction:
Using Path.Combine ensures that the path is correctly constructed regardless of the operating system (Windows, Linux, macOS), as it abstracts away the need for manual management of file separators (\ vs /).

Simplicity and Readability:
File.ReadAllText(path) is easy to understand and use when reading the entire content of a file into a string. This makes your code more readable and concise.

Avoids Hardcoding Path Separators:
Hardcoding file paths manually can lead to errors, especially when your code is running on different environments or platforms. Path.Combine prevents this issue.

Safe Path Handling:
It prevents errors that might arise from forgotten or incorrect path separators when manually constructing paths, which could lead to incorrect file paths or issues when trying to access the file.

Considerations:
File Size: If you are working with large files, ReadAllText might not be the most efficient choice, as it reads the entire file into memory at once. In such cases, you might consider using StreamReader to read the file in chunks or line-by-line.

Exception Handling: When working with file operations, it’s a good idea to handle exceptions (e.g., FileNotFoundException, UnauthorizedAccessException) to ensure your program doesn't crash unexpectedly if the file isn't available or accessible.

Example with basic exception handling:

try
{
    string path = Path.Combine("Data", "Datasets", "cinemas.json");
    string data = File.ReadAllText(path);
}
catch (FileNotFoundException ex)
{
    Console.WriteLine($"The file was not found: {ex.Message}");
}
catch (UnauthorizedAccessException ex)
{
    Console.WriteLine($"You do not have permission to access the file: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
}