namespace ResearchWebApp.Models
{
    public enum PriorityLevel
    {
        High,
        Medium,
        Low
    }
    public class ExamSchedule
    {
        public int Id { get; set; }
        public DateTime ExamDate { get; set; }
        public string? StartTime { get; set; } 
        public string? EndTime { get; set; }
        public PriorityLevel Priority { get; set; }
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }
        public List<StudySession> StudySessions { get; set; } = new List<StudySession>();
    }
}
