using ScheduleSystem.DAL.Enums;

namespace ScheduleSystem.DAL.Entities;

public class ScheduleEntry
{
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
    public int GroupId { get; set; }
    public int ClassroomId { get; set; }
    public SchoolDayOfWeek DayOfWeek { get; set; }
    public LessonNumber LessonNumber { get; set; }
    public WeekType WeekType { get; set; }
    public int Semester { get; set; }
    public int Year { get; set; }

    public Subject Subject { get; set; } = null!;
    public Teacher Teacher { get; set; } = null!;
    public Group Group { get; set; } = null!;
    public Classroom Classroom { get; set; } = null!;
}
