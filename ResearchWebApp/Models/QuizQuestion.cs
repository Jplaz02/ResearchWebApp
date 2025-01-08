namespace ResearchWebApp.Models
{
    public class QuizQuestion
    {
        public int Id { get; set; }

        public int QuizId { get; set; } // Foreign key to Quiz
        public Quiz Quiz { get; set; } // Navigation property to Quiz

        public string QuestionText { get; set; }
        public string CorrectAnswer { get; set; }
        public string? AnswerOptions { get; set; } // JSON of answer options for MCQ

        public ICollection<QuizAnswer> Answers { get; set; }
        public QuestionType QuestionType { get; set; }
    }
}
