namespace ScheduleSystem.DAL.Entities;

public class Subject
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public ICollection<ScheduleEntry> ScheduleEntries { get; set; } = new List<ScheduleEntry>();
}
