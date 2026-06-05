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

    private Task ManageScheduleAsync() => throw new NotImplementedException();
    private Task ManageClassroomsAsync() => throw new NotImplementedException();
    private Task ManageGroupsAsync() => throw new NotImplementedException();
    private Task ManageTeachersAsync() => throw new NotImplementedException();
    private Task ManageSubjectsAsync() => throw new NotImplementedException();
    private Task ManageDepartmentsAsync() => throw new NotImplementedException();
    private Task ManageUsersAsync() => throw new NotImplementedException();
}
