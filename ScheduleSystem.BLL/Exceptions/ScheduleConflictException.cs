namespace ScheduleSystem.BLL.Exceptions;

public class ScheduleConflictException : Exception
{
    public ScheduleConflictException(string message) : base(message) { }
}
