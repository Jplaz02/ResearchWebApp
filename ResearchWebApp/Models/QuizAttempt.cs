namespace ResearchWebApp.Models
{
    public class QuizAttempt
    {
        public int Id { get; set; }
        public int QuizId { get; set; } // Link to the quiz attempted
        public Quiz Quiz { get; set; }
        public DateTime DateAttempted { get; set; }
        public string Status { get; set; } // e.g., "In Progress", "Completed"
        public ICollection<QuizAnswer> Answers { get; set; }
        public int Score { get; set; }
    }

}
