using ScheduleSystem.DAL.Entities;
using ScheduleSystem.DAL.Interfaces;
using ScheduleSystem.DAL.Repositories;

namespace ScheduleSystem.DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IRepository<Classroom> Classrooms { get; }
    public IRepository<Group> Groups { get; }
    public IRepository<Teacher> Teachers { get; }
    public IRepository<Subject> Subjects { get; }
    public IRepository<Department> Departments { get; }
    public IScheduleRepository ScheduleEntries { get; }
    public IRepository<User> Users { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Classrooms = new Repository<Classroom>(context);
        Groups = new Repository<Group>(context);
        Teachers = new Repository<Teacher>(context);
        Subjects = new Repository<Subject>(context);
        Departments = new Repository<Department>(context);
        ScheduleEntries = new ScheduleRepository(context);
        Users = new Repository<User>(context);
    }

    public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
