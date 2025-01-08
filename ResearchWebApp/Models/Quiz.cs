using ResearchWebApp.Models;
namespace ResearchWebApp.Models
{
    public class Quiz
    {
        public int Id { get; set; }
        public int SubjectFileId { get; set; } // Explicit foreign key
        public SubjectFile SubjectFile { get; set; } // Navigation property to SubjectFile
        public string Title { get; set; }
        public int TotalQuestions { get; set; }
        public ICollection<QuizQuestion> Questions { get; set; }
        public ICollection<QuizAttempt> Attempts { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
