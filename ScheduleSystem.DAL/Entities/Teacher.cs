namespace ScheduleSystem.DAL.Entities;

public class Teacher
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int DepartmentId { get; set; }

    public Department Department { get; set; } = null!;
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<ScheduleEntry> ScheduleEntries { get; set; } = new List<ScheduleEntry>();
}
