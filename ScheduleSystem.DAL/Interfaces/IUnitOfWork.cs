using ScheduleSystem.DAL.Entities;

namespace ScheduleSystem.DAL.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Classroom> Classrooms { get; }
    IRepository<Group> Groups { get; }
    IRepository<Teacher> Teachers { get; }
    IRepository<Subject> Subjects { get; }
    IRepository<Department> Departments { get; }
    IScheduleRepository ScheduleEntries { get; }
    IRepository<User> Users { get; }
    Task<int> SaveAsync();
}
