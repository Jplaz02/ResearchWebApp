using ResearchWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResearchWebApp.Services
{
    public interface IStudySessionService
    {
        Task<List<StudySession>> GenerateStudySessionsAsync(int preferredTime);
        Task AddStudySessionAsync(StudySession studySession);
        Task<List<StudySession>> GetAllStudySessionsAsync();
        Task<List<StudySession>> GetStudySessionsBySubjectIdAsync(int subjectId);
    }
}
