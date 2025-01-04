using Syncfusion.PdfToImageConverter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

public class PdfConversionService : IPdfConversionService
{
    public async Task<List<byte[]>> ConvertPdfToImageAsync(Stream pdfStream, string outputDir, string originalFileName)
    {
        try
        {
            List<byte[]> imageBytesList = new List<byte[]>();

            using (var imageConverter = new PdfToImageConverter())
            {
                imageConverter.Load(pdfStream);

                // Get the total number of pages
                int pageCount = imageConverter.PageCount;

                // Loop through all the pages
                for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
                {
                    using (var outputStream = imageConverter.Convert(pageIndex, false, false) as MemoryStream)
                    {
                        if (outputStream != null)
                        {
                            // Create image name based on the original PDF file name
                            string imageName = Path.GetFileNameWithoutExtension(originalFileName) + $"_{pageIndex + 1}.png";
                            string imagePath = Path.Combine(outputDir, imageName);

                            await File.WriteAllBytesAsync(imagePath, outputStream.ToArray());

                            // Add the image bytes to the list for OCR processing
                            imageBytesList.Add(outputStream.ToArray());
                        }
                    }
                }

                return imageBytesList; // Return the list of images as byte arrays
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            throw; // Rethrow the exception to be handled by the caller
        }
    }
}
