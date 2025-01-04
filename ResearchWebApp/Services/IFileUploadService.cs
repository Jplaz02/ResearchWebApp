using ResearchWebApp.DTOs;
using System.Threading.Tasks;

namespace PdftoImageApp.Service
{
    public interface IFileUploadService
    {
        Task UploadFileAsync(FileUploadDto fileUploadDto);
    }
}
