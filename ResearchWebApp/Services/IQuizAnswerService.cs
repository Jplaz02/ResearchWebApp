using ResearchWebApp.Models;

namespace ResearchWebApp.Services
{
    public interface IQuizAnswerService
    {
        Task<List<QuizAnswer>> GetAnswersByAttemptIdAsync(int attemptId);
        Task<QuizAnswer> AddAnswerAsync(QuizAnswer answer);
        Task UpdateAnswerAsync(QuizAnswer answer);
    }

}
