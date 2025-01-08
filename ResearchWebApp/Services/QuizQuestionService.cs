using ResearchWebApp.Data;
using ResearchWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResearchWebApp.Services
{
    public class QuizQuestionService : IQuizQuestionService
    {
        private readonly DataContext _context;

        public QuizQuestionService(DataContext context)
        {
            _context = context;
        }

        // Add Question with validation for CorrectAnswer
        public async Task<QuizQuestion> AddQuestionAsync(QuizQuestion question)
        {
            // Validate CorrectAnswer before saving
            if (string.IsNullOrEmpty(question.CorrectAnswer))
            {
                throw new InvalidOperationException("CorrectAnswer must not be null or empty.");
            }

            _context.QuizQuestions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        // Update Question with validation for CorrectAnswer
        public async Task UpdateQuestionAsync(QuizQuestion question)
        {
            // Validate CorrectAnswer before saving
            if (string.IsNullOrEmpty(question.CorrectAnswer))
            {
                throw new InvalidOperationException("CorrectAnswer must not be null or empty.");
            }

            _context.QuizQuestions.Update(question);
            await _context.SaveChangesAsync();
        }

        // Delete Question
        public async Task DeleteQuestionAsync(int id)
        {
            var question = await _context.QuizQuestions.FindAsync(id);
            if (question != null)
            {
                _context.QuizQuestions.Remove(question);
                await _context.SaveChangesAsync();
            }
        }

        // Get Questions by QuizId (not implemented yet)
        public Task<List<QuizQuestion>> GetQuestionsByQuizIdAsync(int quizId)
        {
            throw new NotImplementedException();
        }
    }
}
