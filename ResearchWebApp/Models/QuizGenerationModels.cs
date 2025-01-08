using Newtonsoft.Json;

namespace ResearchWebApp.Models
{
    public class QuizGenerationResponse
    {
        [JsonProperty("questions")]
        public List<ApiQuizQuestion> Questions { get; set; }
    }

    public class ApiQuizQuestion
    {
        [JsonProperty("question_text")]
        public string QuestionText { get; set; }

        [JsonProperty("correct_answer")]
        public string CorrectAnswer { get; set; }

        [JsonProperty("answer_options")]
        public List<string> AnswerOptions { get; set; }

        [JsonProperty("question_type")]
        public string QuestionType { get; set; }
    }
}
