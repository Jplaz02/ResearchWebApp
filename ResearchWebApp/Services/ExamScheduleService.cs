using Microsoft.EntityFrameworkCore;
using ResearchWebApp.Data;
using ResearchWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchWebApp.Services
{
    public class ExamScheduleService : IExamScheduleService
    {
        private readonly DataContext _context;

        public ExamScheduleService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ExamSchedule>> GetAllExamSchedulesAsync()
        {
            return await _context.ExamSchedules
                .Include(es => es.StudySessions) // Include related study sessions
                .ToListAsync();
        }

        public async Task<ExamSchedule> GetExamScheduleByIdAsync(int id)
        {
            return await _context.ExamSchedules
                .Include(es => es.StudySessions) // Include related study sessions
                .FirstOrDefaultAsync(es => es.Id == id);
        }

        public async Task AddExamScheduleAsync(ExamSchedule examSchedule)
        {
            _context.ExamSchedules.Add(examSchedule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExamScheduleAsync(ExamSchedule examSchedule)
        {
            var dbExamSchedule = await _context.ExamSchedules.FindAsync(examSchedule.Id);
            if (dbExamSchedule != null)
            {
                dbExamSchedule.SubjectId = examSchedule.SubjectId;
                dbExamSchedule.ExamDate = examSchedule.ExamDate;
                dbExamSchedule.StartTime = examSchedule.StartTime;
                dbExamSchedule.EndTime = examSchedule.EndTime;
                dbExamSchedule.Priority = examSchedule.Priority;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteExamScheduleAsync(int id)
        {
            var examSchedule = await _context.ExamSchedules
                .Include(es => es.StudySessions) // Include related study sessions
                .FirstOrDefaultAsync(es => es.Id == id);
            if (examSchedule != null)
            {
                // Delete related study sessions
                _context.StudySessions.RemoveRange(examSchedule.StudySessions);
                // Delete the exam schedule
                _context.ExamSchedules.Remove(examSchedule);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ExamSchedule>> GetExamSchedulesBySubjectIdAsync(int subjectId)
        {
            return await _context.ExamSchedules
                .Where(e => e.SubjectId == subjectId)
                .Include(es => es.StudySessions) // Include related study sessions
                .ToListAsync();
        }
    }
}
