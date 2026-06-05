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
            Console.WriteLine("5. Повний розклад");
            Console.WriteLine("0. Вийти з системи");
            Console.Write("\nВаш вибір: ");

            var input = Console.ReadLine();
            switch (input)
            {
                case "1": await FilterByGroupAsync(); break;
                case "2": await FilterByTeacherAsync(); break;
                case "3": await FilterByClassroomAsync(); break;
                case "4": await FilterBySubjectAsync(); break;
                case "5": await ShowAllAsync(); break;
                case "0": return;
                default: Console.WriteLine("Невірний вибір."); break;
            }
        }
    }

    private Task FilterByGroupAsync() => throw new NotImplementedException();
    private Task FilterByTeacherAsync() => throw new NotImplementedException();
    private Task FilterByClassroomAsync() => throw new NotImplementedException();
    private Task FilterBySubjectAsync() => throw new NotImplementedException();
    private Task ShowAllAsync() => throw new NotImplementedException();
}
