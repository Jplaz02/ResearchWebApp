using ResearchWebApp.Models;

namespace ResearchWebApp.Services
{
    public interface ISubjectService
    {
        Task<List<Subject>> GetAllSubjectsAsync();
        Task<Subject> GetSubjectsByIdAsync(int id);
        Task<List<Subject>> GetAllSubjectsWithDetailsAsync();
        Task AddSubjectAsync(Subject subject);
        Task UpdateSubjectAsync(Subject subject, int id);
        Task DeleteSubjectAsync(int id);


        // Schedule methods
        Task AddScheduleAsync(Schedule schedule);
        Task UpdateScheduleAsync(Schedule schedule);
        Task DeleteScheduleAsync(int id);
    }
}
