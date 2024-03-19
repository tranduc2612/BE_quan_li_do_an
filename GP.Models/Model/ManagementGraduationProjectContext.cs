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

    public virtual DbSet<Classification> Classifications { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Council> Councils { get; set; }

    public virtual DbSet<DetailScheduleWeek> DetailScheduleWeeks { get; set; }

    public virtual DbSet<GroupReviewOutline> GroupReviewOutlines { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<ProjectOutline> ProjectOutlines { get; set; }

    public virtual DbSet<ScheduleSemester> ScheduleSemesters { get; set; }

    public virtual DbSet<ScheduleWeek> ScheduleWeeks { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    public virtual DbSet<Teaching> Teachings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=MSI\\SQLEXPRESS;Initial Catalog=ManagementGraduationProject;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Classification>(entity =>
        {
            entity.HasKey(e => e.ClassifiId).HasName("PK__Classifi__AE2013258D2223C7");

            entity.ToTable("Classification");

            entity.Property(e => e.ClassifiId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Role).HasMaxLength(50);
            entity.Property(e => e.TypeCode).HasMaxLength(50);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.CommentId).HasName("PK__Comment__C3B4DFCAE73131A1");

            entity.ToTable("Comment");

            entity.Property(e => e.CommentId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserName).HasMaxLength(50);

            entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserName)
                .HasConstraintName("FK_Comment_ProjectOutline");
        });

        modelBuilder.Entity<Council>(entity =>
        {
            entity.HasKey(e => e.CouncilId).HasName("PK__Council__1BBAA5C1C8F060B1");

            entity.ToTable("Council");

            entity.Property(e => e.CouncilId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.CouncilName).HasMaxLength(100);
            entity.Property(e => e.CouncilZoom).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<DetailScheduleWeek>(entity =>
        {
            entity.HasKey(e => new { e.ScheduleWeekId, e.UserNameProject }).HasName("PK__DetailSc__459D05541981ABA1");

            entity.ToTable("DetailScheduleWeek");

            entity.Property(e => e.ScheduleWeekId).HasMaxLength(50);
            entity.Property(e => e.UserNameProject).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);

            entity.HasOne(d => d.ScheduleWeek).WithMany(p => p.DetailScheduleWeeks)
                .HasForeignKey(d => d.ScheduleWeekId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetailScheduleWeek_ScheduleWeek");

            entity.HasOne(d => d.UserNameProjectNavigation).WithMany(p => p.DetailScheduleWeeks)
                .HasForeignKey(d => d.UserNameProject)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_DetailScheduleWeek_Project");
        });

        modelBuilder.Entity<GroupReviewOutline>(entity =>
        {
            entity.HasKey(e => e.GroupReviewOutlineId).HasName("PK__GroupRev__0C859B6FBBFC201F");

            entity.ToTable("GroupReviewOutline");

            entity.Property(e => e.GroupReviewOutlineId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            entity.Property(e => e.NameGroupReviewOutline).HasMaxLength(50);
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.HasKey(e => e.MajorId).HasName("PK__Major__D5B8BF91CE1F5EB0");

            entity.ToTable("Major");

            entity.Property(e => e.MajorId).HasMaxLength(10);
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__Project__C9F284578EC0671A");

            entity.ToTable("Project");

            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.CommentUv1)
                .HasMaxLength(500)
                .HasColumnName("CommentUV1");
            entity.Property(e => e.CommentUv2)
                .HasMaxLength(500)
                .HasColumnName("CommentUV2");
            entity.Property(e => e.CommentUv3)
                .HasMaxLength(500)
                .HasColumnName("CommentUV3");
            entity.Property(e => e.CouncilId).HasMaxLength(50);
            entity.Property(e => e.ScoreGvhd).HasColumnName("ScoreGVHD");
            entity.Property(e => e.ScoreGvpb).HasColumnName("ScoreGVPB");
            entity.Property(e => e.ScoreUv1).HasColumnName("ScoreUV1");
            entity.Property(e => e.ScoreUv2).HasColumnName("ScoreUV2");
            entity.Property(e => e.ScoreUv3).HasColumnName("ScoreUV3");
            entity.Property(e => e.SemesterId).HasMaxLength(50);
            entity.Property(e => e.StatusProject)
                .HasMaxLength(50)
                .HasDefaultValueSql("('DOING')");
            entity.Property(e => e.UserNameCommentator).HasMaxLength(50);
            entity.Property(e => e.UserNameMentor).HasMaxLength(50);

            entity.HasOne(d => d.Council).WithMany(p => p.Projects)
                .HasForeignKey(d => d.CouncilId)
                .HasConstraintName("FK_Project_Council");

            entity.HasOne(d => d.Semester).WithMany(p => p.Projects)
                .HasForeignKey(d => d.SemesterId)
                .HasConstraintName("FK_Project_Semester");

            entity.HasOne(d => d.UserNameNavigation).WithOne(p => p.Project)
                .HasForeignKey<Project>(d => d.UserName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Project_Student");

            entity.HasOne(d => d.UserNameCommentatorNavigation).WithMany(p => p.ProjectUserNameCommentatorNavigations)
                .HasForeignKey(d => d.UserNameCommentator)
                .HasConstraintName("FK_Project_Teacher_Commentator");

            entity.HasOne(d => d.UserNameMentorNavigation).WithMany(p => p.ProjectUserNameMentorNavigations)
                .HasForeignKey(d => d.UserNameMentor)
                .HasConstraintName("FK_Project_Teacher_Mentor");
        });

        modelBuilder.Entity<ProjectOutline>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__ProjectO__C9F28457C77115FA");

            entity.ToTable("ProjectOutline");

            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.GroupReviewOutlineId).HasMaxLength(50);

            entity.HasOne(d => d.GroupReviewOutline).WithMany(p => p.ProjectOutlines)
                .HasForeignKey(d => d.GroupReviewOutlineId)
                .HasConstraintName("FK_ProjectOutline_GroupReviewOutline");

            entity.HasOne(d => d.UserNameNavigation).WithOne(p => p.ProjectOutline)
                .HasForeignKey<ProjectOutline>(d => d.UserName)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProjectOutline_Project");
        });

        modelBuilder.Entity<ScheduleSemester>(entity =>
        {
            entity.HasKey(e => e.ScheduleSemesterId).HasName("PK__Schedule__81105223B17EFB46");

            entity.ToTable("ScheduleSemester");

            entity.Property(e => e.ScheduleSemesterId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.Implementer).HasMaxLength(200);
            entity.Property(e => e.SemesterId).HasMaxLength(50);
            entity.Property(e => e.ToDate).HasColumnType("datetime");
            entity.Property(e => e.TypeSchedule).HasMaxLength(50);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ScheduleSemesters)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_ScheduleSemester_Teacher");

            entity.HasOne(d => d.Semester).WithMany(p => p.ScheduleSemesters)
                .HasForeignKey(d => d.SemesterId)
                .HasConstraintName("FK_ScheduleSemester_Semester");
        });

        modelBuilder.Entity<ScheduleWeek>(entity =>
        {
            entity.HasKey(e => e.ScheduleWeekId).HasName("PK__Schedule__33DBB5B654EE4EF6");

            entity.ToTable("ScheduleWeek");

            entity.Property(e => e.ScheduleWeekId)
                .HasMaxLength(50)
                .HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.SemesterId).HasMaxLength(50);
            entity.Property(e => e.ToDate).HasColumnType("datetime");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ScheduleWeeks)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_ScheduleWeek_Teacher");

            entity.HasOne(d => d.Semester).WithMany(p => p.ScheduleWeeks)
                .HasForeignKey(d => d.SemesterId)
                .HasConstraintName("FK_ScheduleWeek_Semester");
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.HasKey(e => e.SemesterId).HasName("PK__Semester__043301DD5311E3CC");

            entity.ToTable("Semester");

            entity.Property(e => e.SemesterId).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.FromDate).HasColumnType("date");
            entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            entity.Property(e => e.NameSemester).HasMaxLength(50);
            entity.Property(e => e.ScheduleSemesterId).HasMaxLength(50);
            entity.Property(e => e.ToDate).HasColumnType("date");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Semesters)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("fk_Semester_Teacher");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__Student__C9F28457704A2DFE");

            entity.ToTable("Student");

            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.ClassName).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.Gpa).HasColumnName("GPA");
            entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            entity.Property(e => e.MajorId).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.RefreshToken).IsUnicode(false);
            entity.Property(e => e.SchoolYearName).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('AUTH')");
            entity.Property(e => e.StudentCode).HasMaxLength(30);
            entity.Property(e => e.TokenCreated).HasColumnType("datetime");
            entity.Property(e => e.TokenExpires)
                .HasColumnType("datetime")
                .HasColumnName("tokenExpires");

            entity.HasOne(d => d.Major).WithMany(p => p.Students)
                .HasForeignKey(d => d.MajorId)
                .HasConstraintName("FK_Student_Major");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__Teacher__C9F2845761E39A3C");

            entity.ToTable("Teacher");

            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Education).HasMaxLength(50);
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.IsDelete).HasDefaultValueSql("((0))");
            entity.Property(e => e.MajorId).HasMaxLength(10);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.RefreshToken).IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasDefaultValueSql("('AUTH')");
            entity.Property(e => e.TeacherCode).HasMaxLength(30);
            entity.Property(e => e.TokenCreated).HasColumnType("datetime");
            entity.Property(e => e.TokenExpires)
                .HasColumnType("datetime")
                .HasColumnName("tokenExpires");

            entity.HasOne(d => d.Major).WithMany(p => p.Teachers)
                .HasForeignKey(d => d.MajorId)
                .HasConstraintName("FK_Teacher_Major");
        });

        modelBuilder.Entity<Teaching>(entity =>
        {
            entity.HasKey(e => new { e.SemesterId, e.UserNameTeacher }).HasName("PK__Teaching__7EC252832CF81CB8");

            entity.ToTable("Teaching");

            entity.Property(e => e.SemesterId).HasMaxLength(50);
            entity.Property(e => e.UserNameTeacher).HasMaxLength(50);
            entity.Property(e => e.CouncilId).HasMaxLength(50);
            entity.Property(e => e.GroupReviewOutlineId).HasMaxLength(50);
            entity.Property(e => e.PostionInCouncil)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Council).WithMany(p => p.Teachings)
                .HasForeignKey(d => d.CouncilId)
                .HasConstraintName("FK_Teaching_Council");

            entity.HasOne(d => d.GroupReviewOutline).WithMany(p => p.Teachings)
                .HasForeignKey(d => d.GroupReviewOutlineId)
                .HasConstraintName("FK_Teaching_GroupReviewOutline");

            entity.HasOne(d => d.Semester).WithMany(p => p.Teachings)
                .HasForeignKey(d => d.SemesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teaching_Semester");

            entity.HasOne(d => d.UserNameTeacherNavigation).WithMany(p => p.Teachings)
                .HasForeignKey(d => d.UserNameTeacher)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teaching_Teacher");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
