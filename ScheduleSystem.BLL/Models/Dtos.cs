using ScheduleSystem.DAL.Enums;

namespace ScheduleSystem.BLL.Models;

public class ScheduleEntryDto
{
    public int Id { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public string TeacherFullName { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public string ClassroomName { get; set; } = string.Empty;
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

public class ScheduleFilterDto
{
    public int? GroupId { get; set; }
    public int? TeacherId { get; set; }
    public int? ClassroomId { get; set; }
    public int? SubjectId { get; set; }
    public SchoolDayOfWeek? DayOfWeek { get; set; }
    public WeekType? WeekType { get; set; }
}

public class ClassroomDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Building { get; set; } = string.Empty;
}

public class GroupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
}

public class TeacherDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
}

public class SubjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class DepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class UserDto
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public int? TeacherId { get; set; }
    public int? GroupId { get; set; }
}
