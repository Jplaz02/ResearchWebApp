using ResearchWebApp.Models;
using Microsoft.EntityFrameworkCore;
using ResearchWebApp.Data;


namespace ResearchWebApp.Services
{
    public class SubjectFileService : ISubjectFileService
    {
        private readonly DataContext _context;

        public SubjectFileService(DataContext context)
        {
            _context = context;
        }

        public async Task AddSubjectFileAsync(SubjectFile subjectFile)
        {
            // Add the new SubjectFile to the database
            await _context.SubjectFiles.AddAsync(subjectFile);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SubjectFile>> GetFilesBySubjectIdAsync(int subjectId)
        {
            return await _context.SubjectFiles
                .Where(sf => sf.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task DeleteSubjectFileAsync(int fileId)
        {
            // Load the SubjectFile along with its related literature if necessary (not needed for cascade delete, but can help debug)
            var fileToDelete = await _context.SubjectFiles
                .Include(sf => sf.RelatedLiteratures)  // Explicitly include related literature (if needed for debugging)
                .FirstOrDefaultAsync(sf => sf.Id == fileId);

            if (fileToDelete != null)
            {
                // Remove the SubjectFile (this will also trigger cascading delete for RelatedLiterature if configured)
                _context.SubjectFiles.Remove(fileToDelete);

                // Save the changes to delete both the SubjectFile and RelatedLiterature
                await _context.SaveChangesAsync();
            }
        }

    }

}
