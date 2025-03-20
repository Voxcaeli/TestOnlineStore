using Microsoft.AspNetCore.Mvc;
using TestOnlineStore.Domain;
using TestOnlineStore.Persistence.DTO.Category.Commands;
using TestOnlineStore.Persistence.DTO.Category.Queries;
using TestOnlineStore.Persistence.Repositories.Interfaces;

namespace TestOnlineStore.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController(IRepositoryCategory repositoryCategory)
    : ControllerBase
{
    [HttpGet(Name = "GetAll")]
    public async Task<List<AllCategory>> GetAllAsync()
    {
        var categories = await repositoryCategory.GetAllAsync();

        return categories;
    }

    [HttpGet("{id}", Name = "GetById")]
    public async Task<DetailsCategory> GetDetailsAsync(int id)
    {
        var category = await repositoryCategory.GetDetailsAsync(id);

        return category;
    }

    [HttpPost(Name = "Add")]
    public async Task<int> AddAsync([FromBody] CreateCategory category)
    {
        var newCategory = new Category
        {
            Name = category.Name,
            Description = category.Description
        };

        var id = await repositoryCategory.AddAsync(newCategory);

        return id;
    }

    [HttpDelete]
    public async Task DeleteAsync(int id)
    {
        await repositoryCategory.DeleteAsync(id);
    }

    [HttpPut(Name = "Update")]
    public async Task UpdateAsync([FromBody] Category category)
    {
        await repositoryCategory.UpdateAsync(category);
    }
}
