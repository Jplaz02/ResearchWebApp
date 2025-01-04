using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

public interface IPdfConversionService
{
    Task<List<byte[]>> ConvertPdfToImageAsync(Stream pdfStream, string outputDir, string originalFileName);
}