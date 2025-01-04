using ResearchWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResearchWebApp.Services
{
    public interface IExamScheduleService
    {
        Task<List<ExamSchedule>> GetAllExamSchedulesAsync();
        Task<ExamSchedule> GetExamScheduleByIdAsync(int id);
        Task AddExamScheduleAsync(ExamSchedule examSchedule);
        Task UpdateExamScheduleAsync(ExamSchedule examSchedule);
        Task DeleteExamScheduleAsync(int id);
        Task<List<ExamSchedule>> GetExamSchedulesBySubjectIdAsync(int subjectId);  // Added method
    }
}
