using AutoMapper;
using ScheduleSystem.BLL.Interfaces;
using ScheduleSystem.BLL.Models;
using ScheduleSystem.DAL.Interfaces;

namespace ScheduleSystem.BLL.Services;

public class ClassroomService : IClassroomService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public ClassroomService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public Task<IEnumerable<ClassroomDto>> GetAllAsync() => throw new NotImplementedException();
    public Task<ClassroomDto> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task<ClassroomDto> CreateAsync(ClassroomDto dto) => throw new NotImplementedException();
    public Task UpdateAsync(ClassroomDto dto) => throw new NotImplementedException();
    public Task DeleteAsync(int id) => throw new NotImplementedException();
}

public class GroupService : IGroupService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public GroupService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public Task<IEnumerable<GroupDto>> GetAllAsync() => throw new NotImplementedException();
    public Task<GroupDto> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task<GroupDto> CreateAsync(GroupDto dto) => throw new NotImplementedException();
    public Task UpdateAsync(GroupDto dto) => throw new NotImplementedException();
    public Task DeleteAsync(int id) => throw new NotImplementedException();
}

public class TeacherService : ITeacherService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public TeacherService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public Task<IEnumerable<TeacherDto>> GetAllAsync() => throw new NotImplementedException();
    public Task<TeacherDto> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task<TeacherDto> CreateAsync(TeacherDto dto) => throw new NotImplementedException();
    public Task UpdateAsync(TeacherDto dto) => throw new NotImplementedException();
    public Task DeleteAsync(int id) => throw new NotImplementedException();
}

public class SubjectService : ISubjectService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public SubjectService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public Task<IEnumerable<SubjectDto>> GetAllAsync() => throw new NotImplementedException();
    public Task<SubjectDto> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task<SubjectDto> CreateAsync(SubjectDto dto) => throw new NotImplementedException();
    public Task UpdateAsync(SubjectDto dto) => throw new NotImplementedException();
    public Task DeleteAsync(int id) => throw new NotImplementedException();
}

public class DepartmentService : IDepartmentService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public DepartmentService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public Task<IEnumerable<DepartmentDto>> GetAllAsync() => throw new NotImplementedException();
    public Task<DepartmentDto> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task<DepartmentDto> CreateAsync(DepartmentDto dto) => throw new NotImplementedException();
    public Task UpdateAsync(DepartmentDto dto) => throw new NotImplementedException();
    public Task DeleteAsync(int id) => throw new NotImplementedException();
}
