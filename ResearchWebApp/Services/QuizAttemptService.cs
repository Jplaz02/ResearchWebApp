using ResearchWebApp.Data;
using ResearchWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ResearchWebApp.Services
{
    public class QuizAttemptService : IQuizAttemptService
    {
        private readonly DataContext _context;

        public QuizAttemptService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<QuizAttempt>> GetAttemptsByQuizIdAsync(int quizId)
        {
            return await _context.QuizAttempts
                .Where(a => a.QuizId == quizId)
                .Include(a => a.Answers)
                .ToListAsync();
        }

        public async Task<QuizAttempt> StartAttemptAsync(QuizAttempt attempt)
        {
            _context.QuizAttempts.Add(attempt);
            await _context.SaveChangesAsync();
            return attempt;
        }

        public async Task UpdateAttemptAsync(QuizAttempt attempt)
        {
            _context.QuizAttempts.Update(attempt);
            await _context.SaveChangesAsync();
        }
    }

}
