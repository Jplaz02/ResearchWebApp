using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITextCombinerService
{
    Task<string> CombineTextFilesAsync(List<string> filePaths, string outputFileName);
}
