using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CinemaApp.Infrastructure.Services
{
    public static class FileValidationService
    {
        // Validate the file existence, non-emptiness, and valid JSON content
        public static string ValidateAndReadFile(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("The file path cannot be null or empty.", nameof(filePath));
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"The file at {filePath} does not exist.");
            }

            string data = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentException($"The file at {filePath} is empty.");
            }

            // Optionally, validate JSON format here
            try
            {
                JsonDocument.Parse(data); // Check if it's valid JSON
            }
            catch (JsonException)
            {
                throw new ArgumentException($"The file at {filePath} contains invalid JSON.");
            }

            return data;
        }
    }
}
