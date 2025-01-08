using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using ResearchWebApp.Models;

namespace ResearchWebApp.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<ExamSchedule> ExamSchedules { get; set; }
        public DbSet<StudySession> StudySessions { get; set; }
        public DbSet<SubjectFile> SubjectFiles { get; set; }
        public DbSet<RelatedLiterature> RelatedLiterature { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<QuizQuestion> QuizQuestions { get; set; }
        public DbSet<QuizAttempt> QuizAttempts { get; set; }
        public DbSet<QuizAnswer> QuizAnswers { get; set; }




        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("YourConnectionStringHere")
                              .ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.MultipleCollectionIncludeWarning));
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Subject>()
                .HasMany(s => s.Schedules)
                .WithOne(s => s.Subject)
                .HasForeignKey(s => s.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subject>()
                .HasMany(s => s.ExamSchedules)
                .WithOne(es => es.Subject)
                .HasForeignKey(es => es.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subject>()
                .HasMany(s => s.StudySessions)
                .WithOne(ss => ss.Subject)
                .HasForeignKey(ss => ss.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Subject>()
                .HasMany(s => s.SubjectFiles)
                .WithOne(sf => sf.Subject)
                .HasForeignKey(sf => sf.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExamSchedule>()
                .HasMany(es => es.StudySessions)
                .WithOne(ss => ss.ExamSchedule)
                .HasForeignKey(ss => ss.ExamScheduleId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RelatedLiterature>()
                .HasOne(rl => rl.SubjectFile)
                .WithMany(sf => sf.RelatedLiteratures)
                .HasForeignKey(rl => rl.SubjectFileId)
                .OnDelete(DeleteBehavior.Cascade);

            // Cascade delete Quiz when SubjectFile is deleted
            // SubjectFile -> Quiz
            modelBuilder.Entity<Quiz>()
                .HasOne(q => q.SubjectFile)  // Quiz has one SubjectFile
                .WithMany(sf => sf.Quizzes)  // SubjectFile can have many Quizzes
                .HasForeignKey(q => q.SubjectFileId)  // Use SubjectFileId as the foreign key
                .OnDelete(DeleteBehavior.Cascade);

            // Quiz -> QuizQuestion
            modelBuilder.Entity<QuizQuestion>(entity =>
            {
                entity.HasKey(q => q.Id); // Explicitly setting the primary key
                entity.HasOne(q => q.Quiz)
                      .WithMany(q => q.Questions)
                      .HasForeignKey(q => q.QuizId)
                      .OnDelete(DeleteBehavior.Cascade); // Optional: Delete questions when quiz is deleted
            });


            // Quiz -> QuizAttempt
            modelBuilder.Entity<QuizAttempt>()
                .HasOne(qa => qa.Quiz)
                .WithMany(q => q.Attempts)
                .HasForeignKey(qa => qa.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            // QuizQuestion -> QuizAnswer
            modelBuilder.Entity<QuizAnswer>()
                .HasOne(qa => qa.QuizQuestion)
                .WithMany(q => q.Answers)
                .HasForeignKey(qa => qa.QuizQuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Seed data for subjects, schedules, and exam schedules
            modelBuilder.Entity<Subject>().HasData(
                new Subject { Id = 1, SubjectName = "Mathematics", SubjectDescription = "Basic Math", Teacher = "John Doe" },
                new Subject { Id = 2, SubjectName = "Science", SubjectDescription = "Basic Science", Teacher = "Jane Smith" }
            );

            modelBuilder.Entity<Schedule>().HasData(
                new Schedule { Id = 1, SubjectId = 1, Day = DayOfWeek.Monday, StartTime = "09:00", EndTime = "10:00" },
                new Schedule { Id = 2, SubjectId = 2, Day = DayOfWeek.Tuesday, StartTime = "11:00", EndTime = "12:00" }
            );

            modelBuilder.Entity<ExamSchedule>().HasData(
                new ExamSchedule { Id = 1, SubjectId = 1, ExamDate = new DateTime(2024, 7, 15), StartTime = "09:00", EndTime = "11:00", Priority = PriorityLevel.High },
                new ExamSchedule { Id = 2, SubjectId = 2, ExamDate = new DateTime(2024, 7, 20), StartTime = "13:00", EndTime = "15:00", Priority = PriorityLevel.Medium }
            );


        }
    }
}
