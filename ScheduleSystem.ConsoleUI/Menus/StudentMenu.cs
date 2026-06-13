namespace ScheduleSystem.ConsoleUI.Menus;

public class StudentMenu
{
    private readonly ApiClient _client;

    public StudentMenu(ApiClient client)
    {
        _client = client;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Меню студента ===");
            Console.WriteLine("1. Мій розклад");
            Console.WriteLine("0. Вийти з системи");
            Console.Write("\nВаш вибір: ");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1": await ShowMyScheduleAsync(); break;
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
            var me = await _client.GetAsync<CurrentUserView>("api/auth/me");
            if (me?.GroupId is null)
            {
                Console.WriteLine("Не вдалось визначити вашу групу (GroupId відсутній).");
            }
            else
            {
                var entries = await _client.GetAsync<List<ScheduleEntryView>>(
                    $"api/schedule/group/{me.GroupId}");
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

