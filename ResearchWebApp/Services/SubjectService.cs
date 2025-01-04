using Microsoft.EntityFrameworkCore;
using ResearchWebApp.Data;
using ResearchWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResearchWebApp.Services
{
    public class SubjectService : ISubjectService
    {
        private readonly DataContext _context;

        public SubjectService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Subject>> GetAllSubjectsAsync()
        {
            return await _context.Subjects
                .Include(s => s.Schedules)
                .Include(s => s.SubjectFiles)
                .Include(s => s.ExamSchedules)
                .ToListAsync();
        }

        public async Task<Subject> GetSubjectsByIdAsync(int id)
        {
            return await _context.Subjects
                .Include(s => s.Schedules)
                .Include(s => s.SubjectFiles)
                .Include(s => s.ExamSchedules)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<List<Subject>> GetAllSubjectsWithDetailsAsync()
        {
            var subjects = await _context.Subjects
                .Include(s => s.Schedules)
                .Include(s => s.SubjectFiles)
                .Include(s => s.ExamSchedules)
                .AsSplitQuery() // or use .AsSingleQuery() to avoid splitting
                .ToListAsync();

            return subjects;
        }

        public async Task AddSubjectAsync(Subject subject)
        {
            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSubjectAsync(Subject subject, int id)
        {
            var dbSubject = await _context.Subjects.FindAsync(id);
            if (dbSubject != null)
            {
                dbSubject.SubjectName = subject.SubjectName;
                dbSubject.SubjectDescription = subject.SubjectDescription;
                dbSubject.Teacher = subject.Teacher;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteSubjectAsync(int id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject != null)
            {
                _context.Subjects.Remove(subject);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddScheduleAsync(Schedule schedule)
        {
            _context.Schedules.Add(schedule);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateScheduleAsync(Schedule schedule)
        {
            var dbSchedule = await _context.Schedules.FindAsync(schedule.Id);
            if (dbSchedule != null)
            {
                dbSchedule.SubjectId = schedule.SubjectId;
                dbSchedule.Day = schedule.Day;
                dbSchedule.StartTime = schedule.StartTime;
                dbSchedule.EndTime = schedule.EndTime;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteScheduleAsync(int id)
        {
            var schedule = await _context.Schedules.FindAsync(id);
            if (schedule != null)
            {
                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();
            }
        }
    }
}
