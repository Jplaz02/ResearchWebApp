using ResearchWebApp.Models;

namespace ResearchWebApp.Services
{
    public interface IQuizService
    {
        Task<List<Quiz>> GetAllQuizzesAsync();
        Task<Quiz> GetQuizByIdAsync(int id);
        Task<Quiz> CreateQuizAsync(Quiz quiz);
        Task UpdateQuizAsync(Quiz quiz);
        Task DeleteQuizAsync(int id);
    }

}
