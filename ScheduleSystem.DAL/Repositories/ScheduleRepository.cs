
using Microsoft.EntityFrameworkCore;
using ScheduleSystem.DAL.Entities;
using ScheduleSystem.DAL.Enums;
using ScheduleSystem.DAL.Interfaces;

namespace ScheduleSystem.DAL.Repositories
{
    public class ScheduleRepository : Repository<ScheduleEntry>, IScheduleRepository
    {
        public ScheduleRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ScheduleEntry>> GetByGroupAsync(int groupId)
        {
            return await _context.ScheduleEntries
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                .Include(s => s.Group)
                .Include(s => s.Classroom)
                .Where(s => s.GroupId == groupId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ScheduleEntry>> GetByTeacherAsync(int teacherId)
        {
            return await _context.ScheduleEntries
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                .Include(s => s.Group)
                .Include(s => s.Classroom)
                .Where(s => s.TeacherId == teacherId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ScheduleEntry>> GetByClassroomAsync(int classroomId)
        {
            return await _context.ScheduleEntries
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                .Include(s => s.Group)
                .Include(s => s.Classroom)
                .Where(s => s.ClassroomId == classroomId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ScheduleEntry>> GetBySubjectAsync(int subjectId)
        {
            return await _context.ScheduleEntries
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                .Include(s => s.Group)
                .Include(s => s.Classroom)
                .Where(s => s.SubjectId == subjectId)
                .ToListAsync();
        }

        public async Task<IEnumerable<ScheduleEntry>> GetFilteredAsync(
            int? groupId, 
            int? teacherId, 
            int? classroomId, 
            int? subjectId, 
            SchoolDayOfWeek? dayOfWeek, 
            WeekType? weekType)
        {
            IQueryable<ScheduleEntry> query = _context.ScheduleEntries
                .Include(s => s.Subject)
                .Include(s => s.Teacher)
                .Include(s => s.Group)
                .Include(s => s.Classroom);

            if (groupId.HasValue)
                query = query.Where(s => s.GroupId == groupId.Value);

            if (teacherId.HasValue)
                query = query.Where(s => s.TeacherId == teacherId.Value);

            if (classroomId.HasValue)
                query = query.Where(s => s.ClassroomId == classroomId.Value);

            if (subjectId.HasValue)
                query = query.Where(s => s.SubjectId == subjectId.Value);

            if (dayOfWeek.HasValue)
                query = query.Where(s => s.DayOfWeek == dayOfWeek.Value);

            if (weekType.HasValue)
                query = query.Where(s => s.WeekType == weekType.Value);

            return await query.OrderBy(s => s.DayOfWeek).ThenBy(s => s.LessonNumber).ToListAsync();
        }

       public async Task<bool> HasConflictAsync(ScheduleEntry entry)
{
    return await _context.ScheduleEntries.AnyAsync(s =>
        s.Id != entry.Id && 
        s.DayOfWeek == entry.DayOfWeek &&
        s.LessonNumber == entry.LessonNumber &&
        s.Semester == entry.Semester &&
        s.Year == entry.Year &&
        (s.WeekType == entry.WeekType || s.WeekType == WeekType.Both || entry.WeekType == WeekType.Both) &&
        (
            s.ClassroomId == entry.ClassroomId || 
            s.TeacherId == entry.TeacherId ||     
            s.GroupId == entry.GroupId            
        ));
}
    }
}