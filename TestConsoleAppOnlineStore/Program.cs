using System.Net.Http.Json;
using TestConsoleAppOnlineStore.Category;

/// <summary>
/// Класс программы
/// </summary>
internal class Program
{
    /// <summary>
    /// Главная функция программы
    /// </summary>
    /// <param name="args">Список строковых аргументов</param>
    private static async Task Main(string[] args)
    {
        // All category [GET]
        var responseGetCategories = await new HttpClient().GetAsync("https://localhost:7298/api/v1/Category");
        var categories = await responseGetCategories.Content.ReadFromJsonAsync<List<AllCategory>>();

        foreach (var category in categories!)
        {
            Console.WriteLine($"Id: {category.Id} Name: {category.Name}");
        }



        // Details category [GET]
        // Ввод: идентификатор выбранной категории
        Console.WriteLine($"\nEnter the category ID to display information about it: ");
        int id = int.Parse(Console.ReadLine()!);
        var responseGetCategory = await new HttpClient().GetAsync($"https://localhost:7298/api/v1/Category/{id}");
        var currentCategory = await responseGetCategory.Content.ReadFromJsonAsync<DetailsCategory>();

        Console.WriteLine($"Id: {currentCategory!.Id} Name: {currentCategory.Name} Description: {currentCategory.Description}");



        // Create category [POST]
        // Ввод: название новой категории
        Console.WriteLine($"\nCreating a category");
        Console.WriteLine($"Enter the category name: ");
        string createdCategoryName = Console.ReadLine()!;
        // Ввод: описание новой категории
        Console.WriteLine($"Enter the category description: ");
        string createdCategoryDescription = Console.ReadLine()!;

        var createCategory = new CreateCategory
        {
            Name = createdCategoryName,
            Description = createdCategoryDescription
        };

        await new HttpClient().PostAsJsonAsync("https://localhost:7298/api/v1/Category", createCategory);
        //var idProductCategory = await responseGetCategories.Content.ReadFromJsonAsync<int>();
        responseGetCategories = await new HttpClient().GetAsync("https://localhost:7298/api/v1/Category");
        categories = await responseGetCategories.Content.ReadFromJsonAsync<List<AllCategory>>();

        foreach (var category in categories!)
        {
            Console.WriteLine($"Id: {category.Id} Name: {category.Name}");
        }



        // Update category [PUT]
        Console.WriteLine($"\nUpdating a category");
        // Ввод: идентификатор обновляемой категории
        Console.WriteLine($"Enter the category ID: ");
        int updatedCategoryId = int.Parse(Console.ReadLine()!);
        // Ввод: новое название категории
        Console.WriteLine($"Enter the category name: ");
        string updatedCategoryName = Console.ReadLine()!;
        // Ввод: новое описание категории
        Console.WriteLine($"Enter the category description: ");
        string updatedCategoryDescription = Console.ReadLine()!;

        var updatedCategory = new UpdateCategory
        {
            Id = updatedCategoryId,
            Name = updatedCategoryName,
            Description = updatedCategoryDescription
        };

        await new HttpClient().PutAsJsonAsync("https://localhost:7298/api/v1/Category", updatedCategory);

        responseGetCategories = await new HttpClient().GetAsync("https://localhost:7298/api/v1/Category");
        categories = await responseGetCategories.Content.ReadFromJsonAsync<List<AllCategory>>();

        foreach (var category in categories!)
        {
            Console.WriteLine($"Id: {category.Id} Name: {category.Name}");
        }



        // Delete category [DELETE]
        Console.WriteLine($"\nDeleting a category");
        Console.WriteLine($"Enter the category ID: ");
        var deletedId = int.Parse(Console.ReadLine()!);
        await new HttpClient().DeleteAsync($"https://localhost:7298/api/v1/Category/{deletedId}");

        responseGetCategories = await new HttpClient().GetAsync("https://localhost:7298/api/v1/Category");
        categories = await responseGetCategories.Content.ReadFromJsonAsync<List<AllCategory>>();

        foreach (var category in categories!)
        {
            Console.WriteLine($"Id: {category.Id} Name: {category.Name}");
        }

        Console.ReadLine();
    }
}