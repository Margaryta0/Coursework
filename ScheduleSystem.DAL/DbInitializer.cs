
using System.Linq;
using ScheduleSystem.DAL.Entities;
using ScheduleSystem.DAL.Enums;

namespace ScheduleSystem.DAL
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
            {
                return; 
            }


            var department = new Department { Name = "Кафедра інженерії програмного забезпечення" };
            context.Departments.Add(department);
            context.SaveChanges();


            var classroom1 = new Classroom { Name = "403-г", Capacity = 30, Building = "Головний корпус" };
            var classroom2 = new Classroom { Name = "102", Capacity = 80, Building = "Лекційний корпус" };
            context.Classrooms.AddRange(classroom1, classroom2);


            var subject1 = new Subject { Name = "Information Security Management" };
            var subject2 = new Subject { Name = "Cryptology" };
            context.Subjects.AddRange(subject1, subject2);
            context.SaveChanges();


            var group = new Group { Name = "ПІ-215", DepartmentId = department.Id };
            context.Groups.Add(group);
            context.SaveChanges();


            var teacher = new Teacher { FullName = "Ковальчук Олег Петрович", DepartmentId = department.Id };
            context.Teachers.Add(teacher);
            context.SaveChanges();


            var users = new[]
            {
                new User { Login = "admin", PasswordHash = "admin123", Role = UserRole.Admin },
                new User { Login = "teacher_kovalchuk", PasswordHash = "teacher123", Role = UserRole.Teacher, TeacherId = teacher.Id },
                new User { Login = "student_maxim", PasswordHash = "student123", Role = UserRole.Student, GroupId = group.Id }
            };
            context.Users.AddRange(users);
            context.SaveChanges();


            var scheduleEntry = new ScheduleEntry
            {
                SubjectId = subject1.Id,
                TeacherId = teacher.Id,
                GroupId = group.Id,
                ClassroomId = classroom1.Id,
                DayOfWeek = SchoolDayOfWeek.Monday,
                LessonNumber = LessonNumber.First,
                WeekType = WeekType.Odd,
                Semester = 2,
                Year = 2026
            };
            context.ScheduleEntries.Add(scheduleEntry);
            context.SaveChanges();
        }
    }
}