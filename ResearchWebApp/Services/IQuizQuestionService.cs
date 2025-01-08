using ResearchWebApp.Models;
namespace ResearchWebApp.Services
{
    public interface IQuizQuestionService
    {
        Task<List<QuizQuestion>> GetQuestionsByQuizIdAsync(int quizId);
        Task<QuizQuestion> AddQuestionAsync(QuizQuestion question);
        Task UpdateQuestionAsync(QuizQuestion question);
        Task DeleteQuestionAsync(int id);
    }

}
