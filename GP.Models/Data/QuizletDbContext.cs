using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GP.Models.Data;

public partial class QuizletDbContext : DbContext
{
    public QuizletDbContext()
    {
    }

    public QuizletDbContext(DbContextOptions<QuizletDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AccountJoinClass> AccountJoinClasses { get; set; }

    public virtual DbSet<AccountLearnCredit> AccountLearnCredits { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Classification> Classifications { get; set; }

    public virtual DbSet<Credit> Credits { get; set; }

    public virtual DbSet<Flashcard> Flashcards { get; set; }

    public virtual DbSet<Folder> Folders { get; set; }

    public virtual DbSet<Learn> Learns { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Streak> Streaks { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-DCA8K2S\\MSSQLSERVER01;Initial Catalog=quizlet_db;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Account__F3DBC5730C0751A9");

            entity.ToTable("Account");

            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.Avatar).HasColumnName("avatar");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.HasWarning)
                .HasDefaultValueSql("((0))")
                .HasColumnName("has_warning");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.PasswordSalt).HasColumnName("password_salt");
            entity.Property(e => e.RefreshToken).HasColumnName("refresh_token");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('USER')")
                .HasColumnName("role");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('AUTH')")
                .HasColumnName("status");
            entity.Property(e => e.TokenCreated)
                .HasColumnType("datetime")
                .HasColumnName("token_created");
            entity.Property(e => e.TokenExpires)
                .HasColumnType("datetime")
                .HasColumnName("token_expires");
        });

        modelBuilder.Entity<AccountJoinClass>(entity =>
        {
            entity.HasKey(e => new { e.ClassId, e.Username });

            entity.ToTable("AccountJoinClass");

            entity.Property(e => e.ClassId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("class_id");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Class).WithMany(p => p.AccountJoinClasses)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Join_Class");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.AccountJoinClasses)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Join_Account");
        });

        modelBuilder.Entity<AccountLearnCredit>(entity =>
        {
            entity.HasKey(e => new { e.CreditId, e.Username });

            entity.ToTable("AccountLearnCredit");

            entity.Property(e => e.CreditId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("credit_id");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");

            entity.HasOne(d => d.Credit).WithMany(p => p.AccountLearnCredits)
                .HasForeignKey(d => d.CreditId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountLearn_Credit");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.AccountLearnCredits)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AccountLearn_Account");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__D54EE9B42A6D56A9");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("category_id");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Class__FDF4798661E85E78");

            entity.ToTable("Class");

            entity.Property(e => e.ClassId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("class_id");
            entity.Property(e => e.AcceptEdit)
                .HasDefaultValueSql("((0))")
                .HasColumnName("accept_edit");
            entity.Property(e => e.CountJoin)
                .HasDefaultValueSql("((0))")
                .HasColumnName("count_join");
            entity.Property(e => e.CountReport)
                .HasDefaultValueSql("((0))")
                .HasColumnName("count_report");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('AUTH')")
                .HasColumnName("status");

            entity.HasMany(d => d.Folders).WithMany(p => p.Classes)
                .UsingEntity<Dictionary<string, object>>(
                    "ClassHaveFolder",
                    r => r.HasOne<Folder>().WithMany()
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_HaveFolder_Folder"),
                    l => l.HasOne<Class>().WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_HaveFolder_Class"),
                    j =>
                    {
                        j.HasKey("ClassId", "FolderId");
                        j.ToTable("ClassHaveFolder");
                        j.IndexerProperty<string>("ClassId")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("class_id");
                        j.IndexerProperty<string>("FolderId")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("folder_id");
                    });
        });

        modelBuilder.Entity<Classification>(entity =>
        {
            entity.HasKey(e => e.ClassifyId).HasName("PK__Classifi__6291E482A6052B31");

            entity.ToTable("Classification");

            entity.Property(e => e.ClassifyId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("classify_id");
            entity.Property(e => e.Code)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Info)
                .HasMaxLength(4000)
                .HasColumnName("info");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");
            entity.Property(e => e.Value)
                .HasMaxLength(4000)
                .HasColumnName("value");
        });

        modelBuilder.Entity<Credit>(entity =>
        {
            entity.HasKey(e => e.CreditId).HasName("PK__Credit__C15A9C3676651D5E");

            entity.ToTable("Credit");

            entity.Property(e => e.CreditId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("credit_id");
            entity.Property(e => e.CountLearn)
                .HasDefaultValueSql("((0))")
                .HasColumnName("count_learn");
            entity.Property(e => e.CountReport)
                .HasDefaultValueSql("((0))")
                .HasColumnName("count_report");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");

            entity.HasMany(d => d.Categories).WithMany(p => p.Credits)
                .UsingEntity<Dictionary<string, object>>(
                    "CreditInCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CreditInCate_Cate"),
                    l => l.HasOne<Credit>().WithMany()
                        .HasForeignKey("CreditId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CreditInCate_Credit"),
                    j =>
                    {
                        j.HasKey("CreditId", "CategoryId");
                        j.ToTable("CreditInCategory");
                        j.IndexerProperty<string>("CreditId")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("credit_id");
                        j.IndexerProperty<string>("CategoryId")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("category_id");
                    });

            entity.HasMany(d => d.Classes).WithMany(p => p.Credits)
                .UsingEntity<Dictionary<string, object>>(
                    "ClassHaveCredit",
                    r => r.HasOne<Class>().WithMany()
                        .HasForeignKey("ClassId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ClHaveCredit_Class"),
                    l => l.HasOne<Credit>().WithMany()
                        .HasForeignKey("CreditId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ClHaveCredit_Credit"),
                    j =>
                    {
                        j.HasKey("CreditId", "ClassId");
                        j.ToTable("ClassHaveCredit");
                        j.IndexerProperty<string>("CreditId")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("credit_id");
                        j.IndexerProperty<string>("ClassId")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("class_id");
                    });

            entity.HasMany(d => d.Folders).WithMany(p => p.Credits)
                .UsingEntity<Dictionary<string, object>>(
                    "FolderHaveCredit",
                    r => r.HasOne<Folder>().WithMany()
                        .HasForeignKey("FolderId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_HaveCredit_Folder"),
                    l => l.HasOne<Credit>().WithMany()
                        .HasForeignKey("CreditId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_HaveCredit_Credit"),
                    j =>
                    {
                        j.HasKey("CreditId", "FolderId");
                        j.ToTable("FolderHaveCredit");
                        j.IndexerProperty<string>("CreditId")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("credit_id");
                        j.IndexerProperty<string>("FolderId")
                            .HasMaxLength(50)
                            .IsUnicode(false)
                            .HasColumnName("folder_id");
                    });
        });

        modelBuilder.Entity<Flashcard>(entity =>
        {
            entity.HasKey(e => e.FlashcardId).HasName("PK__Flashcar__467571D26173C3F2");

            entity.ToTable("Flashcard");

            entity.Property(e => e.FlashcardId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("flashcard_id");
            entity.Property(e => e.Answer).HasColumnName("answer");
            entity.Property(e => e.AnswerLang)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("answer_lang");
            entity.Property(e => e.CreditId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("credit_id");
            entity.Property(e => e.Image).HasColumnName("image");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Question).HasColumnName("question");
            entity.Property(e => e.QuestionLang)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("question_lang");

            entity.HasOne(d => d.Credit).WithMany(p => p.Flashcards)
                .HasForeignKey(d => d.CreditId)
                .HasConstraintName("FK_Flashcard_Credit");
        });

        modelBuilder.Entity<Folder>(entity =>
        {
            entity.HasKey(e => e.FolderId).HasName("PK__Folder__0045071B46DB83CC");

            entity.ToTable("Folder");

            entity.Property(e => e.FolderId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("folder_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.Description)
                .HasMaxLength(4000)
                .HasColumnName("description");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Learn>(entity =>
        {
            entity.HasKey(e => new { e.Username, e.FlashcardId });

            entity.ToTable("Learn");

            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.FlashcardId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("flashcard_id");
            entity.Property(e => e.LastLearnedDate)
                .HasColumnType("datetime")
                .HasColumnName("last_learned_date");
            entity.Property(e => e.Process)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('0')")
                .HasColumnName("process");
            entity.Property(e => e.RecentWrongExam)
                .HasDefaultValueSql("((0))")
                .HasColumnName("recent_wrong_exam");
            entity.Property(e => e.RecentWrongLearn)
                .HasDefaultValueSql("((0))")
                .HasColumnName("recent_wrong_learn");

            entity.HasOne(d => d.Flashcard).WithMany(p => p.Learns)
                .HasForeignKey(d => d.FlashcardId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Learn_Flashcard");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Learns)
                .HasForeignKey(d => d.Username)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Learn_Account");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotiId).HasName("PK__Notifica__FDA4F30A7930AB3C");

            entity.ToTable("Notification");

            entity.Property(e => e.NotiId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("noti_id");
            entity.Property(e => e.Content)
                .HasMaxLength(4000)
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.IsDeleted)
                .HasDefaultValueSql("((0))")
                .HasColumnName("is_deleted");
            entity.Property(e => e.Link).HasColumnName("link");
            entity.Property(e => e.NotiType)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("noti_type");
            entity.Property(e => e.Seen)
                .HasDefaultValueSql("((0))")
                .HasColumnName("seen");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK_Notification_Account");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__Report__779B7C582781DB09");

            entity.ToTable("Report");

            entity.Property(e => e.ReportId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("report_id");
            entity.Property(e => e.Message)
                .HasMaxLength(4000)
                .HasColumnName("message");
            entity.Property(e => e.ObjReportedId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("obj_reported_id");
            entity.Property(e => e.Reason)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("reason");
            entity.Property(e => e.TypeObj)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type_obj");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK_Report_Account");
        });

        modelBuilder.Entity<Streak>(entity =>
        {
            entity.HasKey(e => e.StreakId).HasName("PK__Streak__64A11D3FFF091104");

            entity.ToTable("Streak");

            entity.Property(e => e.StreakId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("streak_id");
            entity.Property(e => e.LearnDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("learn_date");
            entity.Property(e => e.Username)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("username");

            entity.HasOne(d => d.UsernameNavigation).WithMany(p => p.Streaks)
                .HasForeignKey(d => d.Username)
                .HasConstraintName("FK_Streak_Account");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
