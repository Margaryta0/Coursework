using ScheduleSystem.DAL.Enums;

namespace ScheduleSystem.ConsoleUI.Menus;

public class TeacherMenu
{
    private readonly ApiClient _client;

    public TeacherMenu(ApiClient client)
    {
        _client = client;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Меню викладача ===");
            Console.WriteLine("1. Мій розклад");
            Console.WriteLine("2. Розклад групи");
            Console.WriteLine("0. Вийти з системи");
            Console.Write("\nВаш вибір: ");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1": await ShowMyScheduleAsync(); break;
                case "2": await ShowGroupScheduleAsync(); break;
                case "0": return;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }
    }

    private async Task ShowMyScheduleAsync()
    {
        Console.Clear();
        Console.WriteLine("=== МІЙ РОЗКЛАД ===\n");

        try
        {
            // Дізнаємось власний TeacherId через /api/auth/me (на основі JWT-токена)
            var me = await _client.GetAsync<CurrentUserView>("api/auth/me");
            if (me?.TeacherId is null)
            {
                Console.WriteLine("Не вдалось визначити вашого викладача (TeacherId відсутній).");
            }
            else
            {
                var entries = await _client.GetAsync<List<ScheduleEntryView>>(
                    $"api/schedule/teacher/{me.TeacherId}");
                PrintEntries(entries);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }

        Console.WriteLine("\nНатисніть будь-яку клавішу...");
        Console.ReadKey();
    }

    private async Task ShowGroupScheduleAsync()
    {
        Console.Clear();
        Console.WriteLine("=== РОЗКЛАД ГРУПИ ===");
        Console.Write("Введіть ID групи: ");
        string? groupId = Console.ReadLine();

        try
        {
            var entries = await _client.GetAsync<List<ScheduleEntryView>>($"api/schedule/group/{groupId}");
            Console.WriteLine($"\nРозклад групи ID {groupId}:\n");
            PrintEntries(entries);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка: {ex.Message}");
        }

        Console.WriteLine("\nНатисніть будь-яку клавішу...");
        Console.ReadKey();
    }

    private static void PrintEntries(List<ScheduleEntryView>? entries)
    {
        if (entries is null || entries.Count == 0)
        {
            Console.WriteLine("Записів не знайдено.");
            return;
        }

        foreach (var e in entries)
        {
            Console.WriteLine(
                $"#{e.Id} | {e.DayOfWeek}, пара {e.LessonNumber} ({e.WeekType}) | " +
                $"{e.SubjectName} | {e.TeacherFullName} | гр. {e.GroupName} | ауд. {e.ClassroomName} | " +
                $"{e.Semester} семестр, {e.Year}");
        }
    }
}

/// <summary>
/// Дані поточного користувача з /api/auth/me.
/// </summary>
public class CurrentUserView
{
    public int Id { get; set; }
    public string Login { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public int? TeacherId { get; set; }
    public int? GroupId { get; set; }
}

