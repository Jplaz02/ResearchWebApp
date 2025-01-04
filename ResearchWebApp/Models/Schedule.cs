using System;

namespace ResearchWebApp.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public int SubjectId { get; set; }
        public DayOfWeek Day { get; set; }
        public string? StartTime { get; set; } 
        public string? EndTime { get; set; } 
        public Subject? Subject { get; set; }

        // Optional: Format properties for display
        public string? StartTimeString => StartTime; // Display format
        public string? EndTimeString => EndTime; // Display format
    }
}
