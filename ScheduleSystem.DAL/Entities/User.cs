using ScheduleSystem.DAL.Enums;

namespace ScheduleSystem.DAL.Entities;

public class User
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public int? TeacherId { get; set; }
    public int? GroupId { get; set; }

    public Teacher? Teacher { get; set; }
    public Group? Group { get; set; }
}
