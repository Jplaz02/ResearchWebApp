namespace ResearchWebApp.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string? SubjectName { get; set; }
        public string? SubjectDescription { get; set; }
        public string? Teacher { get; set; }
        
        public List<Schedule> Schedules { get; set; } = new List<Schedule>();
        public List<ExamSchedule> ExamSchedules { get; set; } = new List<ExamSchedule>();
        public List<StudySession> StudySessions { get; set; } = new List<StudySession>();
        public List<SubjectFile> SubjectFiles { get; set; } = new List<SubjectFile>();
    }
}
