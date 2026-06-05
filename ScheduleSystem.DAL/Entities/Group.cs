namespace ScheduleSystem.DAL.Entities;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DepartmentId { get; set; }

    public Department Department { get; set; } = null!;
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<ScheduleEntry> ScheduleEntries { get; set; } = new List<ScheduleEntry>();
}
