namespace ResearchWebApp.Models
{
    public class ScheduleEvent
    {
        public int Id { get; set; }
        public string Subject { get; set; } = "Unknown Subject";
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string CategoryColor { get; set; } = "#FFFFFF";
        public string RecurrenceRule { get; set; } = "";
    }
}
