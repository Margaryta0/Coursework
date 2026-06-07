using AutoMapper;
using ScheduleSystem.BLL.Exceptions;
using ScheduleSystem.BLL.Interfaces;
using ScheduleSystem.BLL.Models;
using ScheduleSystem.DAL.Entities;
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

    public async Task<IEnumerable<ClassroomDto>> GetAllAsync()
    {
        var items = await _uow.Classrooms.GetAllAsync();
        return _mapper.Map<IEnumerable<ClassroomDto>>(items);
    }

    public async Task<ClassroomDto> GetByIdAsync(int id)
    {
        var item = await _uow.Classrooms.GetByIdAsync(id);
        if (item == null) throw new NotFoundException($"Аудиторію з ID {id} не знайдено.");
        return _mapper.Map<ClassroomDto>(item);
    }

    public async Task<ClassroomDto> CreateAsync(ClassroomDto dto)
    {
        var entity = _mapper.Map<Classroom>(dto);
        await _uow.Classrooms.AddAsync(entity);
        await _uow.SaveAsync();
        dto.Id = entity.Id;
        return dto;
    }

    public async Task UpdateAsync(ClassroomDto dto)
    {
        var entity = await _uow.Classrooms.GetByIdAsync(dto.Id);
        if (entity == null) throw new NotFoundException($"Аудиторію з ID {dto.Id} не знайдено для оновлення.");

        _mapper.Map(dto, entity);
        _uow.Classrooms.Update(entity);
        await _uow.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _uow.Classrooms.GetByIdAsync(id);
        if (entity == null) throw new NotFoundException($"Аудиторію з ID {id} не знайдено для видалення.");

        _uow.Classrooms.Delete(entity);
        await _uow.SaveAsync();
    }
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

    public async Task<IEnumerable<GroupDto>> GetAllAsync()
    {
        var items = await _uow.Groups.GetAllAsync();
        return _mapper.Map<IEnumerable<GroupDto>>(items);
    }

    public async Task<GroupDto> GetByIdAsync(int id)
    {
        var item = await _uow.Groups.GetByIdAsync(id);
        if (item == null) throw new NotFoundException($"Групу з ID {id} не знайдено.");
        return _mapper.Map<GroupDto>(item);
    }

    public async Task<GroupDto> CreateAsync(GroupDto dto)
    {
        var entity = _mapper.Map<Group>(dto);
        await _uow.Groups.AddAsync(entity);
        await _uow.SaveAsync();
        dto.Id = entity.Id;
        return dto;
    }

    public async Task UpdateAsync(GroupDto dto)
    {
        var entity = await _uow.Groups.GetByIdAsync(dto.Id);
        if (entity == null) throw new NotFoundException($"Групу з ID {dto.Id} не знайдено для оновлення.");

        _mapper.Map(dto, entity);
        _uow.Groups.Update(entity);
        await _uow.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _uow.Groups.GetByIdAsync(id);
        if (entity == null) throw new NotFoundException($"Групу з ID {id} не знайдено для видалення.");

        _uow.Groups.Delete(entity);
        await _uow.SaveAsync();
    }
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

    public async Task<IEnumerable<TeacherDto>> GetAllAsync()
    {
        var items = await _uow.Teachers.GetAllAsync();
        return _mapper.Map<IEnumerable<TeacherDto>>(items);
    }

    public async Task<TeacherDto> GetByIdAsync(int id)
    {
        var item = await _uow.Teachers.GetByIdAsync(id);
        if (item == null) throw new NotFoundException($"Викладача з ID {id} не знайдено.");
        return _mapper.Map<TeacherDto>(item);
    }

    public async Task<TeacherDto> CreateAsync(TeacherDto dto)
    {
        var entity = _mapper.Map<Teacher>(dto);
        await _uow.Teachers.AddAsync(entity);
        await _uow.SaveAsync();
        dto.Id = entity.Id;
        return dto;
    }

    public async Task UpdateAsync(TeacherDto dto)
    {
        var entity = await _uow.Teachers.GetByIdAsync(dto.Id);
        if (entity == null) throw new NotFoundException($"Викладача з ID {dto.Id} не знайдено для оновлення.");

        _mapper.Map(dto, entity);
        _uow.Teachers.Update(entity);
        await _uow.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _uow.Teachers.GetByIdAsync(id);
        if (entity == null) throw new NotFoundException($"Викладача з ID {id} не знайдено для видалення.");

        _uow.Teachers.Delete(entity);
        await _uow.SaveAsync();
    }
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

    public async Task<IEnumerable<SubjectDto>> GetAllAsync()
    {
        var items = await _uow.Subjects.GetAllAsync();
        return _mapper.Map<IEnumerable<SubjectDto>>(items);
    }

    public async Task<SubjectDto> GetByIdAsync(int id)
    {
        var item = await _uow.Subjects.GetByIdAsync(id);
        if (item == null) throw new NotFoundException($"Предмет з ID {id} не знайдено.");
        return _mapper.Map<SubjectDto>(item);
    }

    public async Task<SubjectDto> CreateAsync(SubjectDto dto)
    {
        var entity = _mapper.Map<Subject>(dto);
        await _uow.Subjects.AddAsync(entity);
        await _uow.SaveAsync();
        dto.Id = entity.Id;
        return dto;
    }

    public async Task UpdateAsync(SubjectDto dto)
    {
        var entity = await _uow.Subjects.GetByIdAsync(dto.Id);
        if (entity == null) throw new NotFoundException($"Предмет з ID {dto.Id} не знайдено для оновлення.");

        _mapper.Map(dto, entity);
        _uow.Subjects.Update(entity);
        await _uow.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _uow.Subjects.GetByIdAsync(id);
        if (entity == null) throw new NotFoundException($"Предмет з ID {id} не знайдено для видалення.");

        _uow.Subjects.Delete(entity);
        await _uow.SaveAsync();
    }
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

    public async Task<IEnumerable<DepartmentDto>> GetAllAsync()
    {
        var items = await _uow.Departments.GetAllAsync();
        return _mapper.Map<IEnumerable<DepartmentDto>>(items);
    }

    public async Task<DepartmentDto> GetByIdAsync(int id)
    {
        var item = await _uow.Departments.GetByIdAsync(id);
        if (item == null) throw new NotFoundException($"Кафедру з ID {id} не знайдено.");
        return _mapper.Map<DepartmentDto>(item);
    }

    public async Task<DepartmentDto> CreateAsync(DepartmentDto dto)
    {
        var entity = _mapper.Map<Department>(dto);
        await _uow.Departments.AddAsync(entity);
        await _uow.SaveAsync();
        dto.Id = entity.Id;
        return dto;
    }

    public async Task UpdateAsync(DepartmentDto dto)
    {
        var entity = await _uow.Departments.GetByIdAsync(dto.Id);
        if (entity == null) throw new NotFoundException($"Кафедру з ID {dto.Id} не знайдено для оновлення.");

        _mapper.Map(dto, entity);
        _uow.Departments.Update(entity);
        await _uow.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _uow.Departments.GetByIdAsync(id);
        if (entity == null) throw new NotFoundException($"Кафедру з ID {id} не знайдено для видалення.");

        _uow.Departments.Delete(entity);
        await _uow.SaveAsync();
    }
}
