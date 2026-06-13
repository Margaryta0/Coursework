using ScheduleSystem.DAL.Enums;

namespace ScheduleSystem.ConsoleUI.Menus;

public class AdminMenu
{
    private readonly ApiClient _client;

    public AdminMenu(ApiClient client)
    {
        _client = client;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Адмін панель ===");
            Console.WriteLine("1. Розклад");
            Console.WriteLine("2. Аудиторії");
            Console.WriteLine("3. Групи");
            Console.WriteLine("4. Викладачі");
            Console.WriteLine("5. Дисципліни");
            Console.WriteLine("6. Кафедри");
            Console.WriteLine("7. Користувачі");
            Console.WriteLine("0. Вийти з системи");
            Console.Write("\nВаш вибір: ");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1": await ManageScheduleAsync(); break;
                case "2": await ManageClassroomsAsync(); break;
                case "3": await ManageGroupsAsync(); break;
                case "4": await ManageTeachersAsync(); break;
                case "5": await ManageSubjectsAsync(); break;
                case "6": await ManageDepartmentsAsync(); break;
                case "7": await ManageUsersAsync(); break;
                case "0": return;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }
    }

    // ── Розклад ───────────────────────────────────────────────────────────

    private async Task ManageScheduleAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Розклад ===");
            Console.WriteLine("1. Переглянути весь розклад");
            Console.WriteLine("2. Додати запис");
            Console.WriteLine("3. Редагувати запис");
            Console.WriteLine("4. Видалити запис");
            Console.WriteLine("0. Назад");
            Console.Write("\nВаш вибір: ");

            switch (Console.ReadLine())
            {
                case "1": await ListScheduleAsync(); break;
                case "2": await CreateScheduleEntryAsync(); break;
                case "3": await UpdateScheduleEntryAsync(); break;
                case "4": await DeleteScheduleEntryAsync(); break;
                case "0": return;
                default: Console.WriteLine("Невірний вибір."); Pause(); break;
            }
        }
    }

    private async Task ListScheduleAsync()
    {
        try
        {
            var entries = await _client.GetAsync<List<ScheduleEntryView>>("api/schedule");
            Console.Clear();
            Console.WriteLine("=== Весь розклад ===\n");

            if (entries is null || entries.Count == 0)
            {
                Console.WriteLine("Розклад порожній.");
            }
            else
            {
                foreach (var e in entries)
                    PrintEntry(e);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }

        Pause();
    }

    private async Task CreateScheduleEntryAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Додати запис розкладу ===");

        var request = new ScheduleEntryRequestModel
        {
            SubjectId = ReadInt("ID дисципліни: "),
            TeacherId = ReadInt("ID викладача: "),
            GroupId = ReadInt("ID групи: "),
            ClassroomId = ReadInt("ID аудиторії: "),
            DayOfWeek = ReadEnum<SchoolDayOfWeek>("День тижня"),
            LessonNumber = (int)ReadEnum<LessonNumber>("Номер пари"),
            WeekType = ReadEnum<WeekType>("Тип тижня"),
            Semester = ReadInt("Семестр (1 або 2): "),
            Year = ReadInt("Навчальний рік (напр. 2024): ")
        };

        try
        {
            var created = await _client.PostAsync<ScheduleEntryView>("api/schedule", request);
            Console.WriteLine("\nЗапис успішно додано:");
            if (created is not null)
                PrintEntry(created);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nПомилка: {ex.Message}");
        }

        Pause();
    }

    private async Task UpdateScheduleEntryAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Редагувати запис розкладу ===");
        var id = ReadInt("ID запису: ");

        var request = new ScheduleEntryRequestModel
        {
            SubjectId = ReadInt("ID дисципліни: "),
            TeacherId = ReadInt("ID викладача: "),
            GroupId = ReadInt("ID групи: "),
            ClassroomId = ReadInt("ID аудиторії: "),
            DayOfWeek = ReadEnum<SchoolDayOfWeek>("День тижня"),
            LessonNumber = (int)ReadEnum<LessonNumber>("Номер пари"),
            WeekType = ReadEnum<WeekType>("Тип тижня"),
            Semester = ReadInt("Семестр (1 або 2): "),
            Year = ReadInt("Навчальний рік (напр. 2024): ")
        };

        try
        {
            await _client.PutAsync($"api/schedule/{id}", request);
            Console.WriteLine("\nЗапис успішно оновлено.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nПомилка: {ex.Message}");
        }

        Pause();
    }

    private async Task DeleteScheduleEntryAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Видалити запис розкладу ===");
        var id = ReadInt("ID запису: ");

        try
        {
            await _client.DeleteAsync($"api/schedule/{id}");
            Console.WriteLine("\nЗапис успішно видалено.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"\nПомилка: {ex.Message}");
        }

        Pause();
    }

    private static void PrintEntry(ScheduleEntryView e)
    {
        Console.WriteLine(
            $"#{e.Id} | {e.DayOfWeek}, пара {e.LessonNumber} ({e.WeekType}) | " +
            $"{e.SubjectName} | {e.TeacherFullName} | гр. {e.GroupName} | ауд. {e.ClassroomName} | " +
            $"{e.Semester} семестр, {e.Year}");
    }

    // ── Аудиторії ─────────────────────────────────────────────────────────

    private async Task ManageClassroomsAsync()
    {
        await ManageDictionaryAsync(
            entityName: "Аудиторії",
            apiRoute: "api/classrooms",
            listAction: async () =>
            {
                var items = await _client.GetAsync<List<ClassroomView>>("api/classrooms");
                if (items is null || items.Count == 0)
                {
                    Console.WriteLine("Список порожній.");
                    return;
                }
                foreach (var c in items)
                    Console.WriteLine($"#{c.Id} | {c.Name} | місткість: {c.Capacity} | корпус: {c.Building}");
            },
            createAction: async () =>
            {
                var dto = new ClassroomView
                {
                    Name = ReadString("Назва аудиторії: "),
                    Capacity = ReadInt("Місткість: "),
                    Building = ReadString("Корпус: ")
                };
                await _client.PostAsync<ClassroomView>("api/classrooms", dto);
            },
            updateAction: async (id) =>
            {
                var dto = new ClassroomView
                {
                    Name = ReadString("Нова назва: "),
                    Capacity = ReadInt("Нова місткість: "),
                    Building = ReadString("Новий корпус: ")
                };
                await _client.PutAsync($"api/classrooms/{id}", dto);
            });
    }

    // ── Групи ─────────────────────────────────────────────────────────────

    private async Task ManageGroupsAsync()
    {
        await ManageDictionaryAsync(
            entityName: "Групи",
            apiRoute: "api/groups",
            listAction: async () =>
            {
                var items = await _client.GetAsync<List<GroupView>>("api/groups");
                if (items is null || items.Count == 0)
                {
                    Console.WriteLine("Список порожній.");
                    return;
                }
                foreach (var g in items)
                    Console.WriteLine($"#{g.Id} | {g.Name} | кафедра: {g.DepartmentName}");
            },
            createAction: async () =>
            {
                var dto = new GroupView
                {
                    Name = ReadString("Назва групи: "),
                    DepartmentId = ReadInt("ID кафедри: ")
                };
                await _client.PostAsync<GroupView>("api/groups", dto);
            },
            updateAction: async (id) =>
            {
                var dto = new GroupView
                {
                    Name = ReadString("Нова назва: "),
                    DepartmentId = ReadInt("ID кафедри: ")
                };
                await _client.PutAsync($"api/groups/{id}", dto);
            });
    }

    // ── Викладачі ─────────────────────────────────────────────────────────

    private async Task ManageTeachersAsync()
    {
        await ManageDictionaryAsync(
            entityName: "Викладачі",
            apiRoute: "api/teachers",
            listAction: async () =>
            {
                var items = await _client.GetAsync<List<TeacherView>>("api/teachers");
                if (items is null || items.Count == 0)
                {
                    Console.WriteLine("Список порожній.");
                    return;
                }
                foreach (var t in items)
                    Console.WriteLine($"#{t.Id} | {t.FullName} | кафедра: {t.DepartmentName}");
            },
            createAction: async () =>
            {
                var dto = new TeacherView
                {
                    FullName = ReadString("ПІБ викладача: "),
                    DepartmentId = ReadInt("ID кафедри: ")
                };
                await _client.PostAsync<TeacherView>("api/teachers", dto);
            },
            updateAction: async (id) =>
            {
                var dto = new TeacherView
                {
                    FullName = ReadString("Нове ПІБ: "),
                    DepartmentId = ReadInt("ID кафедри: ")
                };
                await _client.PutAsync($"api/teachers/{id}", dto);
            });
    }

    // ── Дисципліни ────────────────────────────────────────────────────────

    private async Task ManageSubjectsAsync()
    {
        await ManageDictionaryAsync(
            entityName: "Дисципліни",
            apiRoute: "api/subjects",
            listAction: async () =>
            {
                var items = await _client.GetAsync<List<SubjectView>>("api/subjects");
                if (items is null || items.Count == 0)
                {
                    Console.WriteLine("Список порожній.");
                    return;
                }
                foreach (var s in items)
                    Console.WriteLine($"#{s.Id} | {s.Name}");
            },
            createAction: async () =>
            {
                var dto = new SubjectView { Name = ReadString("Назва дисципліни: ") };
                await _client.PostAsync<SubjectView>("api/subjects", dto);
            },
            updateAction: async (id) =>
            {
                var dto = new SubjectView { Name = ReadString("Нова назва: ") };
                await _client.PutAsync($"api/subjects/{id}", dto);
            });
    }

    // ── Кафедри ───────────────────────────────────────────────────────────

    private async Task ManageDepartmentsAsync()
    {
        await ManageDictionaryAsync(
            entityName: "Кафедри",
            apiRoute: "api/departments",
            listAction: async () =>
            {
                var items = await _client.GetAsync<List<DepartmentView>>("api/departments");
                if (items is null || items.Count == 0)
                {
                    Console.WriteLine("Список порожній.");
                    return;
                }
                foreach (var d in items)
                    Console.WriteLine($"#{d.Id} | {d.Name}");
            },
            createAction: async () =>
            {
                var dto = new DepartmentView { Name = ReadString("Назва кафедри: ") };
                await _client.PostAsync<DepartmentView>("api/departments", dto);
            },
            updateAction: async (id) =>
            {
                var dto = new DepartmentView { Name = ReadString("Нова назва: ") };
                await _client.PutAsync($"api/departments/{id}", dto);
            });
    }

    // ── Користувачі ───────────────────────────────────────────────────────
    // Implemented by colleague (depends on AuthController.Register)

    private Task ManageUsersAsync() => throw new NotImplementedException(
        "Реалізується колегою разом з AuthController.Register");

    // ── Спільний шаблон CRUD меню для довідників ─────────────────────────

    private async Task ManageDictionaryAsync(
        string entityName,
        string apiRoute,
        Func<Task> listAction,
        Func<Task> createAction,
        Func<int, Task> updateAction)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"=== {entityName} ===");
            Console.WriteLine("1. Переглянути всі");
            Console.WriteLine("2. Додати");
            Console.WriteLine("3. Редагувати");
            Console.WriteLine("4. Видалити");
            Console.WriteLine("0. Назад");
            Console.Write("\nВаш вибір: ");

            switch (Console.ReadLine())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine($"=== {entityName} ===\n");
                    try { await listAction(); }
                    catch (Exception ex) { Console.WriteLine($"Помилка: {ex.Message}"); }
                    Pause();
                    break;

                case "2":
                    Console.Clear();
                    Console.WriteLine($"=== Додати: {entityName} ===");
                    try
                    {
                        await createAction();
                        Console.WriteLine("\nУспішно додано.");
                    }
                    catch (Exception ex) { Console.WriteLine($"\nПомилка: {ex.Message}"); }
                    Pause();
                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine($"=== Редагувати: {entityName} ===");
                    var updateId = ReadInt("ID запису: ");
                    try
                    {
                        await updateAction(updateId);
                        Console.WriteLine("\nУспішно оновлено.");
                    }
                    catch (Exception ex) { Console.WriteLine($"\nПомилка: {ex.Message}"); }
                    Pause();
                    break;

                case "4":
                    Console.Clear();
                    Console.WriteLine($"=== Видалити: {entityName} ===");
                    var deleteId = ReadInt("ID запису: ");
                    try
                    {
                        await _client.DeleteAsync($"{apiRoute}/{deleteId}");
                        Console.WriteLine("\nУспішно видалено.");
                    }
                    catch (Exception ex) { Console.WriteLine($"\nПомилка: {ex.Message}"); }
                    Pause();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Невірний вибір.");
                    Pause();
                    break;
            }
        }
    }

    // ── Допоміжні методи введення з консолі ──────────────────────────────

    private static int ReadInt(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            if (int.TryParse(Console.ReadLine(), out var value))
                return value;
            Console.WriteLine("Невірне число, спробуйте ще раз.");
        }
    }

    private static string ReadString(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine() ?? string.Empty;
    }

    private static T ReadEnum<T>(string prompt) where T : struct, Enum
    {
        var names = Enum.GetNames<T>();
        while (true)
        {
            Console.WriteLine($"{prompt} ({string.Join(", ", names)}):");
            Console.Write("> ");
            var input = Console.ReadLine();
            if (Enum.TryParse<T>(input, true, out var value))
                return value;
            Console.WriteLine("Невірне значення, спробуйте ще раз.");
        }
    }

    private static void Pause()
    {
        Console.WriteLine("\nНатисніть будь-яку клавішу щоб продовжити...");
        Console.ReadKey();
    }
}

// ── DTO/ViewModels для ConsoleUI (відповідають моделям WebAPI) ─────────────
// Console-side mirror of WebAPI request/response models, used only
// for JSON (de)serialization in HTTP calls via ApiClient.

public class ScheduleEntryRequestModel
{
    public int SubjectId { get; set; }
    public int TeacherId { get; set; }
    public int GroupId { get; set; }
    public int ClassroomId { get; set; }
    public SchoolDayOfWeek DayOfWeek { get; set; }
    public int LessonNumber { get; set; }
    public WeekType WeekType { get; set; }
    public int Semester { get; set; }
    public int Year { get; set; }
}

public class ScheduleEntryView
{
    public int Id { get; set; }
    public string SubjectName { get; set; } = string.Empty;
    public string TeacherFullName { get; set; } = string.Empty;
    public string GroupName { get; set; } = string.Empty;
    public string ClassroomName { get; set; } = string.Empty;
    public string DayOfWeek { get; set; } = string.Empty;
    public int LessonNumber { get; set; }
    public string WeekType { get; set; } = string.Empty;
    public int Semester { get; set; }
    public int Year { get; set; }
}

public class ClassroomView
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public string Building { get; set; } = string.Empty;
}

public class GroupView
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
}

public class TeacherView
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = string.Empty;
}

public class SubjectView
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

public class DepartmentView
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}
