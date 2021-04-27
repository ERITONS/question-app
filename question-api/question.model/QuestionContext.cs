using Microsoft.EntityFrameworkCore;
using question.domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace question.model
{
    public class QuestionContext : DbContext
    {
        public QuestionContext(DbContextOptions options)
        : base(options)
        {
            
        }

        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }

        

        private void ConfigureQuestion(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("tbQuestion");
                entity.HasKey(q => q.QuestionId).HasName("questionID");
                entity.Property(q => q.QuestionId).HasColumnName("questionID").ValueGeneratedOnAdd();
                entity.Property(q => q.QuestionName).HasColumnName("questionName");
                entity.Property(q => q.ImageUrl).HasColumnName("imageUrl");
                entity.Property(q => q.ThumbUrl).HasColumnName("thumbUrl");
                entity.Property(q => q.PublishedAt).HasColumnName("publishedAt");

            });
        }

        private void ConfigureChoise(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Choice>(entity =>
            {
                entity.ToTable("tbChoise");
                entity.HasKey(q => q.ChoiceId).HasName("choiceId");
                entity.Property(q => q.ChoiceId).HasColumnName("choiceId").ValueGeneratedOnAdd();
                entity.Property(c => c.ChoiceName).HasColumnName("choiceName");
                entity.Property(c => c.Votes).HasColumnName("votes");
                entity.HasOne(q => q.Question).WithMany(p => p.Choices);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ForSqlServerUseIdentityColumns();
            modelBuilder.HasDefaultSchema("questionDb");

            ConfigureQuestion(modelBuilder);
            ConfigureChoise(modelBuilder);
        }
    }
}

