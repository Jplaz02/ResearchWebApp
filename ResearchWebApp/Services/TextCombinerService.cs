using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class TextCombinerService : ITextCombinerService
{
    public async Task<string> CombineTextFilesAsync(List<string> filePaths, string outputFileName)
    {
        string combinedFilePath = Path.Combine("Output", outputFileName);

        // Ensure the output directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(combinedFilePath));

        using (StreamWriter writer = new StreamWriter(combinedFilePath, false))
        {
            foreach (string filePath in filePaths)
            {
                // Ensure the file exists before trying to read it
                if (File.Exists(filePath))
                {
                    // Read the content of the current file
                    string fileContent = await File.ReadAllTextAsync(filePath);

                    // Check if file content is not empty
                    if (!string.IsNullOrWhiteSpace(fileContent))
                    {
                        await writer.WriteLineAsync(fileContent);
                    }
                    else
                    {
                        // Debugging message for empty files
                        Console.WriteLine($"File '{filePath}' is empty.");
                    }
                }
                else
                {
                    // Debugging message for missing files
                    Console.WriteLine($"File '{filePath}' does not exist.");
                }
            }
        }

        return combinedFilePath; // Return the path of the combined file
    }
}
