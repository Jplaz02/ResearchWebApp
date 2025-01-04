using ResearchWebApp.DTOs;

namespace PdftoImageApp.Service
{
    public class FileUploadService : IFileUploadService
    {
        private readonly string _uploadDirectory;

        public FileUploadService(IConfiguration configuration)
        {
            _uploadDirectory = Path.Combine(Directory.GetCurrentDirectory(), "uploads");
        }

        public async Task UploadFileAsync(FileUploadDto fileUploadDto)
        {
            try
            {
                // Create the upload directory if it doesn't exist
                if (!Directory.Exists(_uploadDirectory))
                {
                    Directory.CreateDirectory(_uploadDirectory);
                }

                // Define the file path
                var filePath = Path.Combine(_uploadDirectory, fileUploadDto.FileName);

                // Write the file content to the specified path
                await File.WriteAllBytesAsync(filePath, fileUploadDto.FileContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while uploading the file: {ex.Message}");
            }
        }
    }
}
