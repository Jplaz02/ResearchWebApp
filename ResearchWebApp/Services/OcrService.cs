using Syncfusion.OCRProcessor;
using System.IO;
using System.Threading.Tasks;

public class OcrService : IOcrService
{
    public async Task<string> PerformOcrAsync(byte[] imageBytes, string outputFileName)
    {
        string tessDataPath = @"D:\Research sample\PdftoImageApp\PdftoImageApp\Tessdata\";

        // Set the TESSDATA_PREFIX environment variable
        Environment.SetEnvironmentVariable("TESSDATA_PREFIX", tessDataPath);

        using (OCRProcessor processor = new OCRProcessor())
        {
            processor.Settings.Language = Languages.English;

            // Load the image from byte array
            using (var stream = new MemoryStream(imageBytes))
            {
                // Perform the OCR process for the image stream
                string ocrText = processor.PerformOCR(stream, tessDataPath);

                // Define output path
                string outputPath = Path.Combine("Output", outputFileName);

                // Write the OCR'ed text to a text file
                using (StreamWriter writer = new StreamWriter(outputPath, false))
                {
                    writer.WriteLine(ocrText);
                }

                return outputPath; // Return the path to the output file
            }
        }
    }
}
