using ScheduleSystem.DAL.Entities;
using ScheduleSystem.DAL.Enums;

namespace ScheduleSystem.DAL.Interfaces;

public interface IScheduleRepository : IRepository<ScheduleEntry>
{
    Task<IEnumerable<ScheduleEntry>> GetByGroupAsync(int groupId);
    Task<IEnumerable<ScheduleEntry>> GetByTeacherAsync(int teacherId);
    Task<IEnumerable<ScheduleEntry>> GetByClassroomAsync(int classroomId);
    Task<IEnumerable<ScheduleEntry>> GetBySubjectAsync(int subjectId);
    Task<IEnumerable<ScheduleEntry>> GetFilteredAsync(
        int? groupId,
        int? teacherId,
        int? classroomId,
        int? subjectId,
        SchoolDayOfWeek? dayOfWeek,
        WeekType? weekType);
    Task<bool> HasConflictAsync(ScheduleEntry entry);
}
