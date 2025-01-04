using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ResearchWebApp.Data;
using ResearchWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResearchWebApp.Services
{
    public class StudySessionService : IStudySessionService
    {
        private readonly DataContext _context;
        private readonly ILogger<StudySessionService> _logger;

        public StudySessionService(DataContext context, ILogger<StudySessionService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<StudySession>> GenerateStudySessionsAsync(int preferredTime)
        {
            _logger.LogInformation("Starting study session generation with preferred time: {PreferredTime}", preferredTime);

            var subjects = await _context.Subjects
                .Include(s => s.ExamSchedules)
                .Include(s => s.Schedules)
                .ToListAsync();

            var studySessions = new List<StudySession>();

            var sortedSubjects = subjects
                .Where(s => s.ExamSchedules.Any())
                .OrderBy(s => s.ExamSchedules.First().ExamDate)
                .ThenBy(s => s.ExamSchedules.First().StartTime)
                .ToList();

            foreach (var subject in sortedSubjects)
            {
                var examSchedule = subject.ExamSchedules.FirstOrDefault();
                if (examSchedule != null)
                {
                    var dailyStudyTime = GetDailyStudyTime(examSchedule.Priority);
                    var totalStudyDays = GetDaysToStartStudying(examSchedule.Priority);

                    var currentDate = DateTime.Today;
                    var daysUntilExam = (examSchedule.ExamDate - currentDate).Days;
                    var startDate = currentDate;

                    if (daysUntilExam >= totalStudyDays)
                    {
                        startDate = examSchedule.ExamDate.AddDays(-totalStudyDays);
                    }

                    _logger.LogInformation("Deleting existing sessions for subject {SubjectName} from {StartDate} to {EndDate}", subject.SubjectName, startDate, examSchedule.ExamDate);
                    await DeleteExistingStudySessions(subject.Id, startDate, examSchedule.ExamDate); // Delete existing sessions

                    _logger.LogInformation("Generating sessions for subject {SubjectName} from {StartDate} to {EndDate}", subject.SubjectName, startDate, examSchedule.ExamDate);

                    for (var date = startDate; date < examSchedule.ExamDate; date = date.AddDays(1))
                    {
                        if (date.DayOfWeek == DayOfWeek.Sunday)
                        {
                            _logger.LogInformation("Skipping date {Date} for subject {SubjectName}", date, subject.SubjectName);
                            continue;
                        }

                        var session = CalculateDailySessions(date, dailyStudyTime, subjects, examSchedule.Priority, preferredTime, studySessions);
                        if (session != null)
                        {
                            session.SubjectId = subject.Id;
                            session.ExamScheduleId = examSchedule.Id; // Ensure ExamScheduleId is set
                            studySessions.Add(session);
                            await AddStudySessionAsync(session);

                            _logger.LogInformation("Added session for subject {SubjectName} on {Date}", subject.SubjectName, date);
                        }
                    }
                }
            }

            _logger.LogInformation("Completed study session generation");
            return studySessions;
        }

        public async Task AddStudySessionAsync(StudySession studySession)
        {
            _context.StudySessions.Add(studySession);
            await _context.SaveChangesAsync();
        }

        public async Task<List<StudySession>> GetAllStudySessionsAsync()
        {
            return await _context.StudySessions
                .Include(ss => ss.Subject)
                .Include(ss => ss.ExamSchedule)
                .ToListAsync();
        }

        public async Task<List<StudySession>> GetStudySessionsBySubjectIdAsync(int subjectId)
        {
            return await _context.StudySessions
                .Where(ss => ss.SubjectId == subjectId)
                .Include(ss => ss.Subject)
                .Include(ss => ss.ExamSchedule)
                .ToListAsync();
        }

        private async Task DeleteExistingStudySessions(int subjectId, DateTime startDate, DateTime endDate)
        {
            var existingSessions = await _context.StudySessions
                .Where(ss => ss.SubjectId == subjectId && ss.SessionDate >= startDate && ss.SessionDate < endDate)
                .ToListAsync();

            if (existingSessions.Any())
            {
                _context.StudySessions.RemoveRange(existingSessions);
                await _context.SaveChangesAsync();
                _logger.LogInformation("Deleted existing sessions for subject {SubjectId} between {StartDate} and {EndDate}", subjectId, startDate, endDate);
            }
        }

        private StudySession CalculateDailySessions(DateTime date, double dailyStudyTime, List<Subject> subjects, PriorityLevel priority, int preferredTime, List<StudySession> existingSessions)
        {
            var studyStartTime = GetPreferredStartTime(preferredTime);

            // Check if there are any subject schedules on this day
            var latestEndTime = GetLatestEndTimeForDay(date, subjects);
            if (latestEndTime.HasValue)
            {
                studyStartTime = latestEndTime.Value.Add(new TimeSpan(0, 30, 0)); // Adding 30 minutes gap
            }

            // Adjust the start time to avoid overlaps with existing sessions
            foreach (var session in existingSessions.Where(s => s.SessionDate == date))
            {
                var existingStartTime = DateTime.Parse(session.StartTime);
                var existingEndTime = DateTime.Parse(session.EndTime);
                var newStartTime = DateTime.Today.Add(studyStartTime);
                var newEndTime = newStartTime.Add(TimeSpan.FromHours(dailyStudyTime));

                if ((newStartTime >= existingStartTime && newStartTime < existingEndTime) ||
                    (newEndTime > existingStartTime && newEndTime <= existingEndTime) ||
                    (newStartTime <= existingStartTime && newEndTime >= existingEndTime))
                {
                    // Adjust the start time to avoid overlap
                    studyStartTime = existingEndTime.TimeOfDay.Add(new TimeSpan(0, 30, 0)); // Adding 30 minutes gap
                }
            }

            return new StudySession
            {
                SessionDate = date,
                StartTime = DateTime.Today.Add(studyStartTime).ToString("hh:mm tt"),
                EndTime = DateTime.Today.Add(studyStartTime).Add(TimeSpan.FromHours(dailyStudyTime)).ToString("hh:mm tt")
            };
        }

        private TimeSpan? GetLatestEndTimeForDay(DateTime date, List<Subject> subjects)
        {
            var endTimes = new List<TimeSpan>();

            foreach (var subject in subjects)
            {
                foreach (var schedule in subject.Schedules)
                {
                    if (schedule.Day == date.DayOfWeek)
                    {
                        endTimes.Add(TimeSpan.Parse(schedule.EndTime));
                    }
                }
            }

            return endTimes.Any() ? endTimes.Max() : (TimeSpan?)null;
        }

        private TimeSpan GetPreferredStartTime(int preferredTime)
        {
            return preferredTime switch
            {
                1 => new TimeSpan(8, 0, 0), // Morning 8:00 AM
                2 => new TimeSpan(14, 0, 0), // Afternoon 2:00 PM
                _ => new TimeSpan(18, 0, 0), // Evening 6:00 PM
            };
        }

        private int GetDaysToStartStudying(PriorityLevel priority)
        {
            return priority switch
            {
                PriorityLevel.High => 21, // 3 weeks
                PriorityLevel.Medium => 14, // 2 weeks
                PriorityLevel.Low => 7, // 1 week
                _ => 7
            };
        }

        private double GetDailyStudyTime(PriorityLevel priority)
        {
            return priority switch
            {
                PriorityLevel.High => 2.5,
                PriorityLevel.Medium => 1.5,
                PriorityLevel.Low => 1,
                _ => 1
            };
        }

        private bool StudySessionExists(int subjectId, DateTime date)
        {
            return _context.StudySessions.Any(ss => ss.SubjectId == subjectId && ss.SessionDate == date);
        }
    }
}
