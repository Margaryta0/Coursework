using ScheduleSystem.BLL.Models;

namespace ScheduleSystem.BLL.Interfaces;

public interface IScheduleService
{
    Task<IEnumerable<ScheduleEntryDto>> GetByGroupAsync(int groupId);
    Task<IEnumerable<ScheduleEntryDto>> GetByTeacherAsync(int teacherId);
    Task<IEnumerable<ScheduleEntryDto>> GetByClassroomAsync(int classroomId);
    Task<IEnumerable<ScheduleEntryDto>> GetBySubjectAsync(int subjectId);
    Task<IEnumerable<ScheduleEntryDto>> GetFilteredAsync(ScheduleFilterDto filter);
    Task<ScheduleEntryDto> GetByIdAsync(int id);
    Task<ScheduleEntryDto> CreateAsync(ScheduleEntryDto dto);
    Task UpdateAsync(ScheduleEntryDto dto);
    Task DeleteAsync(int id);
}

public interface IUserService
{
    Task<string> LoginAsync(string login, string password);
    Task<UserDto> GetByIdAsync(int id);
    Task<UserDto> CreateAsync(UserDto dto, string password);
    Task DeleteAsync(int id);
}

public interface IClassroomService
{
    Task<IEnumerable<ClassroomDto>> GetAllAsync();
    Task<ClassroomDto> GetByIdAsync(int id);
    Task<ClassroomDto> CreateAsync(ClassroomDto dto);
    Task UpdateAsync(ClassroomDto dto);
    Task DeleteAsync(int id);
}

public interface IGroupService
{
    Task<IEnumerable<GroupDto>> GetAllAsync();
    Task<GroupDto> GetByIdAsync(int id);
    Task<GroupDto> CreateAsync(GroupDto dto);
    Task UpdateAsync(GroupDto dto);
    Task DeleteAsync(int id);
}

public interface ITeacherService
{
    Task<IEnumerable<TeacherDto>> GetAllAsync();
    Task<TeacherDto> GetByIdAsync(int id);
    Task<TeacherDto> CreateAsync(TeacherDto dto);
    Task UpdateAsync(TeacherDto dto);
    Task DeleteAsync(int id);
}

public interface ISubjectService
{
    Task<IEnumerable<SubjectDto>> GetAllAsync();
    Task<SubjectDto> GetByIdAsync(int id);
    Task<SubjectDto> CreateAsync(SubjectDto dto);
    Task UpdateAsync(SubjectDto dto);
    Task DeleteAsync(int id);
}

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetAllAsync();
    Task<DepartmentDto> GetByIdAsync(int id);
    Task<DepartmentDto> CreateAsync(DepartmentDto dto);
    Task UpdateAsync(DepartmentDto dto);
    Task DeleteAsync(int id);
}
