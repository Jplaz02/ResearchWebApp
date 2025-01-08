namespace ResearchWebApp.Models
{
    public class QuizQuestionDto // DTO for OpenAI
    {
        public string Question { get; set; }
        public List<string> Options { get; set; } = new();
        public string CorrectAnswer { get; set; }
    }

}
