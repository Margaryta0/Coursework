using ScheduleSystem.DAL.Entities;
using ScheduleSystem.DAL.Enums;
using ScheduleSystem.DAL.Interfaces;

namespace ScheduleSystem.DAL.Repositories;

public class ScheduleRepository : Repository<ScheduleEntry>, IScheduleRepository
{
    public ScheduleRepository(AppDbContext context) : base(context) { }

    public Task<IEnumerable<ScheduleEntry>> GetByGroupAsync(int groupId)
    {
        // TODO: implement
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ScheduleEntry>> GetByTeacherAsync(int teacherId)
    {
        // TODO: implement
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ScheduleEntry>> GetByClassroomAsync(int classroomId)
    {
        // TODO: implement
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ScheduleEntry>> GetBySubjectAsync(int subjectId)
    {
        // TODO: implement
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ScheduleEntry>> GetFilteredAsync(
        int? groupId, int? teacherId, int? classroomId,
        int? subjectId, SchoolDayOfWeek? dayOfWeek, WeekType? weekType)
    {
        // TODO: implement
        throw new NotImplementedException();
    }

    public Task<bool> HasConflictAsync(ScheduleEntry entry)
    {
        // TODO: check classroom/teacher/group conflicts at same time slot
        throw new NotImplementedException();
    }
}
