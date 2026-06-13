using ScheduleSystem.DAL.Enums;

namespace ScheduleSystem.ConsoleUI.Menus;

public class ManagementMenu
{
    private readonly ApiClient _client;

    public ManagementMenu(ApiClient client)
    {
        _client = client;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Перегляд розкладу ===");
            Console.WriteLine("1. Фільтр за групою");
            Console.WriteLine("2. Фільтр за викладачем");
            Console.WriteLine("3. Фільтр за аудиторією");
            Console.WriteLine("4. Фільтр за дисципліною");
            Console.WriteLine("5. Повний розклад (з фільтрами на вибір)");
            Console.WriteLine("0. Вийти з системи");
            Console.Write("\nВаш вибір: ");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1": await FilterByGroupAsync(); break;
                case "2": await FilterByTeacherAsync(); break;
                case "3": await FilterByClassroomAsync(); break;
                case "4": await FilterBySubjectAsync(); break;
                case "5": await ShowFilteredAsync(); break;
                case "0": return;
                default: Console.WriteLine("Невірний вибір."); Pause(); break;
            }
        }
    }

    private async Task FilterByGroupAsync()
    {
        var groupId = ReadInt("ID групи: ");
        await FetchAndPrintAsync($"api/schedule/group/{groupId}", "Розклад групи");
    }

    private async Task FilterByTeacherAsync()
    {
        var teacherId = ReadInt("ID викладача: ");
        await FetchAndPrintAsync($"api/schedule/teacher/{teacherId}", "Розклад викладача");
    }

    private async Task FilterByClassroomAsync()
    {
        var classroomId = ReadInt("ID аудиторії: ");
        var query = $"api/schedule?classroomId={classroomId}";
        await FetchAndPrintAsync(query, "Розклад аудиторії");
    }

    private async Task FilterBySubjectAsync()
    {
        var subjectId = ReadInt("ID дисципліни: ");
        var query = $"api/schedule?subjectId={subjectId}";
        await FetchAndPrintAsync(query, "Розклад дисципліни");
    }

    /// <summary>
    /// Повний розклад з можливістю комбінувати кілька фільтрів одночасно
    /// (відповідає GetFilteredAsync на бекенді).
    /// </summary>
    private async Task ShowFilteredAsync()
    {
        Console.Clear();
        Console.WriteLine("=== Повний розклад — фільтри (Enter щоб пропустити) ===\n");

        var groupId = ReadOptionalInt("ID групи: ");
        var teacherId = ReadOptionalInt("ID викладача: ");
        var classroomId = ReadOptionalInt("ID аудиторії: ");
        var subjectId = ReadOptionalInt("ID дисципліни: ");
        var dayOfWeek = ReadOptionalEnum<SchoolDayOfWeek>("День тижня");
        var weekType = ReadOptionalEnum<WeekType>("Тип тижня");

        var query = BuildQuery(groupId, teacherId, classroomId, subjectId, dayOfWeek, weekType);
        await FetchAndPrintAsync($"api/schedule{query}", "Розклад за фільтрами");
    }

    private static string BuildQuery(
        int? groupId, int? teacherId, int? classroomId, int? subjectId,
        SchoolDayOfWeek? dayOfWeek, WeekType? weekType)
    {
        var parts = new List<string>();

        if (groupId.HasValue) parts.Add($"groupId={groupId}");
        if (teacherId.HasValue) parts.Add($"teacherId={teacherId}");
        if (classroomId.HasValue) parts.Add($"classroomId={classroomId}");
        if (subjectId.HasValue) parts.Add($"subjectId={subjectId}");
        if (dayOfWeek.HasValue) parts.Add($"dayOfWeek={dayOfWeek}");
        if (weekType.HasValue) parts.Add($"weekType={weekType}");

        return parts.Count == 0 ? string.Empty : "?" + string.Join("&", parts);
    }

    private async Task FetchAndPrintAsync(string url, string title)
    {
        Console.Clear();
        Console.WriteLine($"=== {title} ===\n");

        try
        {
            var entries = await _client.GetAsync<List<ScheduleEntryView>>(url);

            if (entries is null || entries.Count == 0)
            {
                Console.WriteLine("Записів не знайдено.");
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

    private static void PrintEntry(ScheduleEntryView e)
    {
        Console.WriteLine(
            $"#{e.Id} | {e.DayOfWeek}, пара {e.LessonNumber} ({e.WeekType}) | " +
            $"{e.SubjectName} | {e.TeacherFullName} | гр. {e.GroupName} | ауд. {e.ClassroomName} | " +
            $"{e.Semester} семестр, {e.Year}");
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

    private static int? ReadOptionalInt(string prompt)
    {
        Console.Write(prompt);
        var input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
            return null;
        return int.TryParse(input, out var value) ? value : null;
    }

    private static T? ReadOptionalEnum<T>(string prompt) where T : struct, Enum
    {
        var names = Enum.GetNames<T>();
        Console.WriteLine($"{prompt} ({string.Join(", ", names)}, або Enter щоб пропустити):");
        Console.Write("> ");
        var input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
            return null;
        return Enum.TryParse<T>(input, true, out var value) ? value : null;
    }

    private static void Pause()
    {
        Console.WriteLine("\nНатисніть будь-яку клавішу щоб продовжити...");
        Console.ReadKey();
    }
}

