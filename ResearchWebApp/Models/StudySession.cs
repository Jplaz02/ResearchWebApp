using ResearchWebApp.Models;

public class StudySession
{
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public int ExamScheduleId { get; set; }
    public DateTime SessionDate { get; set; }
    public string StartTime { get; set; }
    public string EndTime { get; set; }
    public Subject Subject { get; set; }
    public ExamSchedule ExamSchedule { get; set; }
}
