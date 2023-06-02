using AspnetApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspnetApp.Data.DbContexts
{
    public class AspnetAppDbContext : DbContext
    {
        public AspnetAppDbContext(DbContextOptions<AspnetAppDbContext> options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<QuizRezult> QuizResults { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<SolvedQuestion> SolvedQuestions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique(true);

            modelBuilder.Entity<Course>()
                .HasIndex(c => c.Name)
                .IsUnique(true);
        }

    }
}
