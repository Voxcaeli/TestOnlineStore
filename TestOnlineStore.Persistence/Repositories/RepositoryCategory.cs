using Microsoft.EntityFrameworkCore;
using TestOnlineStore.Domain;
using TestOnlineStore.Persistence.Common.Exceptions;
using TestOnlineStore.Persistence.DTO.Category.Commands;
using TestOnlineStore.Persistence.DTO.Category.Queries;
using TestOnlineStore.Persistence.Repositories.Interfaces;

namespace TestOnlineStore.Persistence.Repositories;

public class RepositoryCategory(TestOnlineStoreDBContext context) : IRepositoryCategory
{
    public async Task<List<AllCategory>> GetAllAsync()
    {
        var categories = await context.Categories
            .Select(category => new AllCategory
            {
                Id = category.Id,
                Name = category.Name
            })
            .AsNoTracking()
            .ToListAsync();

        return categories;
    }

    public async Task<DetailsCategory> GetDetailsAsync(int id)
    {
        var category = await context.Categories
            .Select(productCategory => new DetailsCategory
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == id)
            ?? throw new NotFoundException(nameof(DetailsCategory), id);

        return category;
    }

    public async Task<Category> GetByIdAsync(int id)
    {
        var category = await context.Categories
            .SingleOrDefaultAsync(category => category.Id == id)
            ?? throw new NotFoundException(nameof(Category), id);

        return category;
    }

    public async Task<int> AddAsync(Category category)
    {
        await context.AddAsync(category);
        await context.SaveChangesAsync();

        return category.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var category = await GetByIdAsync(id);

        context.Categories.Remove(category);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        var updatedCategory = await GetByIdAsync(category.Id);

        updatedCategory.Name = category.Name;
        updatedCategory.Description = category.Description;

        await context.SaveChangesAsync();
    }
}
