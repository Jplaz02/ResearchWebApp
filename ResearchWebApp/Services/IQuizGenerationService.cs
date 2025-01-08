using ResearchWebApp.Models;

namespace ResearchWebApp.Services
{
    public interface IQuizGenerationService
{
        Task<List<QuizQuestion>> GenerateQuizAsync(string fileContent, int numberOfQuestions, string quizType);
    }
}
