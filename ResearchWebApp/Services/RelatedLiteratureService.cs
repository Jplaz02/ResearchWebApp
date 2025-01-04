using Microsoft.EntityFrameworkCore;
using ResearchWebApp.Data;
using ResearchWebApp.Models;

namespace ResearchWebApp.Services
{
    public class RelatedLiteratureService : IRelatedLiteratureService
    {
        private readonly DataContext _context;

        public RelatedLiteratureService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<RelatedLiterature>> GetRelatedLiteratureBySubjectFileId(int subjectFileId)
        {
            return await _context.RelatedLiterature
                .Where(rl => rl.SubjectFileId == subjectFileId)
                .ToListAsync();
        }
    }
}
