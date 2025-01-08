using ResearchWebApp.Models;

namespace ResearchWebApp.Services
{
    public interface IQuizAttemptService
    {
        Task<List<QuizAttempt>> GetAttemptsByQuizIdAsync(int quizId);
        Task<QuizAttempt> StartAttemptAsync(QuizAttempt attempt);
        Task UpdateAttemptAsync(QuizAttempt attempt);
    }

}
