using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GP.Models.Model;

public partial class ManagementGraduationProjectContext : DbContext
{
    public ManagementGraduationProjectContext()
    {
    }

    public ManagementGraduationProjectContext(DbContextOptions<ManagementGraduationProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Classifi> Classifis { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectOutline> ProjectOutlines { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Teaching> Teachings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MSI\\SQLEXPRESS;Initial Catalog=ManagementGraduationProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__Account__C9F284579E50FE4D");

            entity.ToTable("Account");

            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.RefreshToken).IsUnicode(false);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('AUTH')");
            entity.Property(e => e.TokenCreated).HasColumnType("datetime");
            entity.Property(e => e.TokenExpires)
                .HasColumnType("datetime")
                .HasColumnName("tokenExpires");
        });

        modelBuilder.Entity<Classifi>(entity =>
        {
            entity.HasKey(e => e.ClassifiId).HasName("PK__Classifi__AE2013252BB920EC");

            entity.ToTable("Classifi");

            entity.Property(e => e.ClassifiId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.TypeCode).HasMaxLength(50);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__C3B4DFCAAA490FB6");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasMaxLength(50);
            entity.Property(e => e.ProjectOutlineId).HasMaxLength(50);

            entity.HasOne(d => d.ProjectOutline).WithMany(p => p.Comments)
                .HasForeignKey(d => d.ProjectOutlineId)
                .HasConstraintName("FK_Comment_ProjectOutline");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PK__Project__761ABEF002FE663B");

            entity.ToTable("Project");

            entity.HasIndex(e => e.UserName, "UQ__Project__C9F284569D6D52AB").IsUnique();

            entity.Property(e => e.ProjectId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.CommentGroupReviewOutline).HasMaxLength(500);
            entity.Property(e => e.CommentUv1)
                .HasMaxLength(500)
                .HasColumnName("CommentUV1");
            entity.Property(e => e.CommentUv2)
                .HasMaxLength(500)
                .HasColumnName("CommentUV2");
            entity.Property(e => e.CommentUv3)
                .HasMaxLength(500)
                .HasColumnName("CommentUV3");
            entity.Property(e => e.ProjectOutlineId).HasMaxLength(50);
            entity.Property(e => e.ScoreGvhd).HasColumnName("ScoreGVHD");
            entity.Property(e => e.ScoreGvpb).HasColumnName("ScoreGVPB");
            entity.Property(e => e.ScoreUv1).HasColumnName("ScoreUV1");
            entity.Property(e => e.ScoreUv2).HasColumnName("ScoreUV2");
            entity.Property(e => e.ScoreUv3).HasColumnName("ScoreUV3");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.UserNameNavigation).WithOne(p => p.Project)
                .HasForeignKey<Project>(d => d.UserName)
                .HasConstraintName("FK_Project_Student");
        });

        modelBuilder.Entity<ProjectOutline>(entity =>
        {
            entity.HasKey(e => e.ProjectOutlineId).HasName("PK__ProjectO__CB0CEA85C2C1FA46");

            entity.ToTable("ProjectOutline");

            entity.Property(e => e.ProjectOutlineId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.ProjectId).HasMaxLength(50);

            entity.HasOne(d => d.Project).WithMany(p => p.ProjectOutlines)
                .HasForeignKey(d => d.ProjectId)
                .HasConstraintName("FK_ProjectOutline_Project");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.SemesterId).HasName("PK__Semester__043301DD561FA74B");

            entity.ToTable("Semester");

            entity.Property(e => e.SemesterId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.FromDate).HasColumnType("date");
            entity.Property(e => e.NameSemester).HasMaxLength(50);
            entity.Property(e => e.ToDate).HasColumnType("date");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__Student__C9F2845746468E97");

            entity.ToTable("Student");

            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.Class).HasMaxLength(30);
            entity.Property(e => e.Major).HasMaxLength(10);
            entity.Property(e => e.SchoolYear).HasMaxLength(10);
            entity.Property(e => e.StudentCode).HasMaxLength(30);

            entity.HasOne(d => d.UserNameNavigation).WithOne(p => p.Student)
                .HasForeignKey<Student>(d => d.UserName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Account");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__Teacher__C9F284579CB2C096");

            entity.ToTable("Teacher");

            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.Education).HasMaxLength(50);
            entity.Property(e => e.Major).HasMaxLength(10);
            entity.Property(e => e.TeacherCode).HasMaxLength(30);

            entity.HasOne(d => d.UserNameNavigation).WithOne(p => p.Teacher)
                .HasForeignKey<Teacher>(d => d.UserName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teacher_Account");
        });

        modelBuilder.Entity<Teaching>(entity =>
        {
            entity.HasKey(e => e.TeachingId).HasName("PK__Teaching__69B6BA3E01AAA869");

            entity.ToTable("Teaching");

            entity.Property(e => e.TeachingId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.PostionInCouncil)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.SemesterId).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.Semester).WithMany(p => p.Teachings)
                .HasForeignKey(d => d.SemesterId)
                .HasConstraintName("FK_Teaching_Semester");

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.Teachings)
                .HasForeignKey(d => d.UserName)
                .HasConstraintName("FK_Teaching_Teacher");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
