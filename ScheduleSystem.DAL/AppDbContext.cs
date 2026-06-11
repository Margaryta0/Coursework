using Microsoft.EntityFrameworkCore;
using ScheduleSystem.DAL.Entities;

namespace ScheduleSystem.DAL;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Department> Departments => Set<Department>();
    public DbSet<Classroom> Classrooms => Set<Classroom>();
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<Teacher> Teachers => Set<Teacher>();
    public DbSet<Subject> Subjects => Set<Subject>();
    public DbSet<ScheduleEntry> ScheduleEntries => Set<ScheduleEntry>();
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Department ? Groups (???? ?? ????????)
        modelBuilder.Entity<Group>()
            .HasOne(g => g.Department)
            .WithMany(d => d.Groups)
            .HasForeignKey(g => g.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // Department ? Teachers
        modelBuilder.Entity<Teacher>()
            .HasOne(t => t.Department)
            .WithMany(d => d.Teachers)
            .HasForeignKey(t => t.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        // User ? Teacher (nullable)
        modelBuilder.Entity<User>()
            .HasOne(u => u.Teacher)
            .WithMany(t => t.Users)
            .HasForeignKey(u => u.TeacherId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        // User ? Group (nullable)
        modelBuilder.Entity<User>()
            .HasOne(u => u.Group)
            .WithMany(g => g.Users)
            .HasForeignKey(u => u.GroupId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        // ScheduleEntry ??'????
        modelBuilder.Entity<ScheduleEntry>()
            .HasOne(s => s.Subject)
            .WithMany(sub => sub.ScheduleEntries)
            .HasForeignKey(s => s.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ScheduleEntry>()
            .HasOne(s => s.Teacher)
            .WithMany(t => t.ScheduleEntries)
            .HasForeignKey(s => s.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ScheduleEntry>()
            .HasOne(s => s.Group)
            .WithMany(g => g.ScheduleEntries)
            .HasForeignKey(s => s.GroupId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ScheduleEntry>()
            .HasOne(s => s.Classroom)
            .WithMany(c => c.ScheduleEntries)
            .HasForeignKey(s => s.ClassroomId)
            .OnDelete(DeleteBehavior.Restrict);

        // ?????????? ?????? — ?????? ??? ?????????? ? ????????
        modelBuilder.Entity<ScheduleEntry>()
            .HasIndex(s => new
            {
                s.GroupId,
                s.TeacherId,
                s.ClassroomId,
                s.DayOfWeek,
                s.LessonNumber,
                s.WeekType,
                s.Semester,
                s.Year
            })
            .IsUnique();

        // Login ??? ???? ??????????
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Login)
            .IsUnique();
    }
}
