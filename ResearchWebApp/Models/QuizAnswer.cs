namespace ResearchWebApp.Models
{
    public class QuizAnswer
    {
        public int Id { get; set; }
        public int QuizQuestionId { get; set; } // Link to the question
        public QuizQuestion QuizQuestion { get; set; }
        public string UserAnswer { get; set; }
        public bool IsCorrect { get; set; } // Whether the answer was correct
    }

}
