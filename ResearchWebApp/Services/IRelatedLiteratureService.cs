using ResearchWebApp.Models;

namespace ResearchWebApp.Services
{
    public interface IRelatedLiteratureService
    {
        Task<List<RelatedLiterature>> GetRelatedLiteratureBySubjectFileId(int subjectFileId);
    }
}
