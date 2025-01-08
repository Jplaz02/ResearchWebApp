using ResearchWebApp.Data;
using ResearchWebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ResearchWebApp.Services
{
    public class QuizAnswerService : IQuizAnswerService
    {
        private readonly DataContext _context;

        public QuizAnswerService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<QuizAnswer>> GetAnswersByAttemptIdAsync(int attemptId)
        {
            return await _context.QuizAnswers
                .Where(a => a.QuizQuestion.QuizId == attemptId)
                .ToListAsync();
        }

        public async Task<QuizAnswer> AddAnswerAsync(QuizAnswer answer)
        {
            _context.QuizAnswers.Add(answer);
            await _context.SaveChangesAsync();
            return answer;
        }

        public async Task UpdateAnswerAsync(QuizAnswer answer)
        {
            _context.QuizAnswers.Update(answer);
            await _context.SaveChangesAsync();
        }
    }

}
