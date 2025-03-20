using TestOnlineStore.Domain;
using TestOnlineStore.Persistence.DTO.Category.Commands;
using TestOnlineStore.Persistence.DTO.Category.Queries;

namespace TestOnlineStore.Persistence.Repositories.Interfaces;

public interface IRepositoryCategory
{
    Task<List<AllCategory>> GetAllAsync();
    Task<DetailsCategory> GetDetailsAsync(int id);
    Task<Category> GetByIdAsync(int id);
    Task<int> AddAsync(Category category);
    Task DeleteAsync(int id);
    Task UpdateAsync(Category category);
}
