using backend.models;
using Microsoft.EntityFrameworkCore;

namespace backend;

public class TeacherDbContext : DbContext
{
    public DbSet<Teacher> Teachers { get; set; }
    public DbSet<TeacherClass> TeacherClasses { get; set; }
    public DbSet<QuestionBank> QuestionBanks { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<QuestionCriteria> QuestionCriterias { get; set; }
    public DbSet<QuestionTestWritten> QuestionTestWrittens { get; set; }
    public DbSet<TestWritten> TestWrittens { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<Correction> Corrections { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Comment> Comments { get; set; }
    
    public TeacherDbContext(DbContextOptions<TeacherDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        IdsCreating(modelBuilder);
        TeacherCreating(modelBuilder);
        QuestionBankCreating(modelBuilder);
        TeacherClassCreating(modelBuilder);
        TestWrittenCreating(modelBuilder);
        QuestionTestWrittenCreating(modelBuilder);
        QuestionCreating(modelBuilder);
        QuestionCriteriaCreating(modelBuilder);
        AnswerCreating(modelBuilder);
        StudentCreating(modelBuilder);
        CorrectionCreating(modelBuilder);
        CommentCreating(modelBuilder);
        
        base.OnModelCreating(modelBuilder);
    }

    private void IdsCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Teacher>()
            .HasIndex(t => t.Id)
            .IsUnique();

        modelBuilder.Entity<Teacher>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<TeacherClass>()
            .HasIndex(t => t.Id)
            .IsUnique();

        modelBuilder.Entity<TeacherClass>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<TestWritten>()
            .HasIndex(tw => tw.Id)
            .IsUnique();

        modelBuilder.Entity<TestWritten>()
            .Property(tw => tw.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Answer>()
            .HasIndex(t => t.Id)
            .IsUnique();
        
        modelBuilder.Entity<Answer>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Question>()
            .HasIndex(q => q.Id)
            .IsUnique();
        
        modelBuilder.Entity<Question>()
            .Property(q => q.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<QuestionBank>()
            .HasIndex(qb => qb.Id)
            .IsUnique();
        
        modelBuilder.Entity<QuestionBank>()
            .Property(qb => qb.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<QuestionCriteria>()
            .HasIndex(qc => qc.Id)
            .IsUnique();
        
        modelBuilder.Entity<QuestionCriteria>()
            .Property(qc => qc.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<QuestionTestWritten>()
            .HasIndex(qtw => qtw.Id)
            .IsUnique();

        modelBuilder.Entity<QuestionTestWritten>()
            .Property(qtw => qtw.Id)
            .ValueGeneratedOnAdd();

        modelBuilder.Entity<Student>()
            .HasIndex(s => s.Id)
            .IsUnique();

        modelBuilder.Entity<Student>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Correction>()
            .HasIndex(t => t.Id)
            .IsUnique();

        modelBuilder.Entity<Correction>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Comment>()
            .HasIndex(t => t.Id)
            .IsUnique();

        modelBuilder.Entity<Comment>()
            .Property(t => t.Id)
            .ValueGeneratedOnAdd();

    }
    
    private void TeacherCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Teacher>()
            .HasMany(t => t.TeacherClasses)
            .WithOne(t => t.Teacher)
            .HasForeignKey(t => t.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Teacher>()
            .HasMany(t => t.QuestionBanks)
            .WithOne(q => q.Teacher)
            .HasForeignKey(q => q.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    private void TeacherClassCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeacherClass>()
            .HasMany(tc => tc.TestWrittens)
            .WithOne(tw => tw.TeacherClass)
            .HasForeignKey(tc => tc.TeacherClassId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<TeacherClass>()
            .HasMany(tc => tc.Students)
            .WithOne(st => st.TeacherClass)
            .HasForeignKey(st => st.TeacherClassId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    private void TestWrittenCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TestWritten>()
            .HasOne(tw => tw.TeacherClass)
            .WithMany(tc => tc.TestWrittens)
            .HasForeignKey(tw => tw.TeacherClassId);

        modelBuilder.Entity<TestWritten>()
            .HasMany(tw => tw.Corrections)
            .WithOne(c => c.TestWritten)
            .HasForeignKey(c => c.TestWrittenId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<TestWritten>()
            .HasMany(tw => tw.QuestionTestWrittens)
            .WithOne(qtw => qtw.TestWritten)
            .HasForeignKey(qtw => qtw.TestWrittenId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    private void AnswerCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Answer>()
            .HasOne(a => a.Correction)
            .WithMany(c => c.Answers)
            .HasForeignKey(a => a.CorrectionId);
        
        modelBuilder.Entity<Answer>()
            .HasOne(a => a.QuestionTestWritten)
            .WithMany(q => q.Answers)
            .HasForeignKey(a => a.QuestionTestWrittenId);
        
        modelBuilder.Entity<Answer>()
            .HasMany(a => a.AiComments)
            .WithOne(c => c.AnswerAi)
            .HasForeignKey(c => c.AnswerAiId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Answer>()
            .HasMany(a => a.TeacherComments)
            .WithOne(c => c.AnswerTeacher)
            .HasForeignKey(a => a.AnswerTeacherId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    private void QuestionCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Question>()
            .HasOne(q => q.QuestionBank)
            .WithMany(qb => qb.Questions)
            .HasForeignKey(q => q.QuestionBankId);
        
        modelBuilder.Entity<Question>()
            .HasMany(q => q.QuestionCriterias)
            .WithOne(qc => qc.Question)
            .HasForeignKey(qc => qc.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Question>()
            .HasMany(q => q.QuestionTestWrittens)
            .WithOne(qtw => qtw.Question)
            .HasForeignKey(qtw => qtw.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void QuestionBankCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuestionBank>()
            .HasOne(qb => qb.Teacher)
            .WithMany(t => t.QuestionBanks)
            .HasForeignKey(qb => qb.TeacherId);
        
        modelBuilder.Entity<QuestionBank>()
            .HasMany(qb => qb.Questions)
            .WithOne(q => q.QuestionBank)
            .HasForeignKey(q => q.QuestionBankId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    private void QuestionCriteriaCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuestionCriteria>()
            .HasOne(qc => qc.Question)
            .WithMany(q => q.QuestionCriterias)
            .HasForeignKey(qc => qc.QuestionId);
    }
    
    private void QuestionTestWrittenCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<QuestionTestWritten>()
            .HasOne(qtw => qtw.TestWritten)
            .WithMany(tw => tw.QuestionTestWrittens)
            .HasForeignKey(qtw => qtw.TestWrittenId);
        
        modelBuilder.Entity<QuestionTestWritten>()
            .HasMany(qtw => qtw.Answers)
            .WithOne(a => a.QuestionTestWritten)
            .HasForeignKey(a => a.QuestionTestWrittenId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<QuestionTestWritten>()
            .HasOne(qtw => qtw.Question)
            .WithMany(q => q.QuestionTestWrittens)
            .HasForeignKey(qtw => qtw.QuestionId);
    }
    
    private void StudentCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>()
            .HasOne(s => s.TeacherClass)
            .WithMany(t => t.Students)
            .HasForeignKey(t => t.TeacherClassId);
        
        modelBuilder.Entity<Student>()
            .HasMany(s => s.Corrections)
            .WithOne(c => c.Student)
            .HasForeignKey(t => t.StudentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    private void CorrectionCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Correction>()
            .HasOne(c => c.TestWritten)
            .WithMany(t => t.Corrections)
            .HasForeignKey(c => c.TestWrittenId);
        
        modelBuilder.Entity<Correction>()
            .HasOne(c => c.Student)
            .WithMany(s => s.Corrections)
            .HasForeignKey(t => t.StudentId);
        
        modelBuilder.Entity<Correction>()
            .HasMany(c => c.Answers)
            .WithOne(a => a.Correction)
            .HasForeignKey(a => a.CorrectionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
    
    private void CommentCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.AnswerAi)
            .WithMany(a => a.AiComments)
            .HasForeignKey(c => c.AnswerAiId);
        
        modelBuilder.Entity<Comment>()
            .HasOne(c => c.AnswerTeacher)
            .WithMany(a => a.TeacherComments)
            .HasForeignKey(c => c.AnswerTeacherId);
    }
    
    
    
}