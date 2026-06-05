using AutoMapper;
using ScheduleSystem.BLL.Models;
using ScheduleSystem.DAL.Entities;

namespace ScheduleSystem.BLL.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ScheduleEntry → ScheduleEntryDto (resolves navigation property names)
        CreateMap<ScheduleEntry, ScheduleEntryDto>()
            .ForMember(d => d.SubjectName, o => o.MapFrom(s => s.Subject.Name))
            .ForMember(d => d.TeacherFullName, o => o.MapFrom(s => s.Teacher.FullName))
            .ForMember(d => d.GroupName, o => o.MapFrom(s => s.Group.Name))
            .ForMember(d => d.ClassroomName, o => o.MapFrom(s => s.Classroom.Name));
        CreateMap<ScheduleEntryDto, ScheduleEntry>();

        // Classroom
        CreateMap<Classroom, ClassroomDto>().ReverseMap();

        // Group
        CreateMap<Group, GroupDto>()
            .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department.Name));
        CreateMap<GroupDto, Group>();

        // Teacher
        CreateMap<Teacher, TeacherDto>()
            .ForMember(d => d.DepartmentName, o => o.MapFrom(s => s.Department.Name));
        CreateMap<TeacherDto, Teacher>();

        // Subject
        CreateMap<Subject, SubjectDto>().ReverseMap();

        // Department
        CreateMap<Department, DepartmentDto>().ReverseMap();

        // User (PasswordHash is intentionally excluded from DTO)
        CreateMap<User, UserDto>();
        CreateMap<UserDto, User>()
            .ForMember(d => d.PasswordHash, o => o.Ignore());
    }
}
