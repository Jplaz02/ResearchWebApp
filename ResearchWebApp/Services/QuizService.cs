using ResearchWebApp.Data;
using ResearchWebApp.Models;
using Microsoft.EntityFrameworkCore;


namespace ResearchWebApp.Services
{
    public class QuizService : IQuizService
    {
        private readonly DataContext _context;

        public QuizService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Quiz>> GetAllQuizzesAsync()
        {
            return await _context.Quizzes
                .Include(q => q.SubjectFile)
                .Include(q => q.Questions)
                .Include(q => q.Attempts)
                .ToListAsync();
        }

        public async Task<Quiz> GetQuizByIdAsync(int id)
        {
            return await _context.Quizzes
                .Include(q => q.SubjectFile)
                .Include(q => q.Questions)
                .Include(q => q.Attempts)
                .FirstOrDefaultAsync(q => q.Id == id);
        }

        public async Task<Quiz> CreateQuizAsync(Quiz quiz)
        {
            _context.Quizzes.Add(quiz);
            await _context.SaveChangesAsync();
            return quiz;
        }

        public async Task UpdateQuizAsync(Quiz quiz)
        {
            _context.Quizzes.Update(quiz);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteQuizAsync(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz != null)
            {
                _context.Quizzes.Remove(quiz);
                await _context.SaveChangesAsync();
            }
        }
    }

}
