using ScheduleSystem.DAL.Enums;

namespace ScheduleSystem.WebAPI.Models;

public record LoginRequest(string Login, string Password);

public record LoginResponse(string Token);

public record RegisterRequest(
    string Login,
    string Password,
    UserRole Role,
    int? TeacherId,
    int? GroupId);

public class ScheduleEntryRequest
{
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
    public int GroupId { get; set; }
    public int ClassroomId { get; set; }
    public SchoolDayOfWeek DayOfWeek { get; set; }
    public LessonNumber LessonNumber { get; set; }
    public WeekType WeekType { get; set; }
    public int Semester { get; set; }
    public int Year { get; set; }
}

public class ScheduleEntryResponse
{
    public int Id { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public string TeacherFullName { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public string ClassroomName { get; set; } = string.Empty;
    public string DayOfWeek { get; set; } = string.Empty;
    public int LessonNumber { get; set; }
    public string WeekType { get; set; } = string.Empty;
    public int Semester { get; set; }
    public int Year { get; set; }
}

public class FilterRequest
{
    public int? GroupId { get; set; }
    public int? TeacherId { get; set; }
    public int? ClassroomId { get; set; }
    public int? SubjectId { get; set; }
    public SchoolDayOfWeek? DayOfWeek { get; set; }
    public WeekType? WeekType { get; set; }
}
