public interface IOcrService
{
    Task<string> PerformOcrAsync(byte[] imageBytes, string outputFileName);
}
