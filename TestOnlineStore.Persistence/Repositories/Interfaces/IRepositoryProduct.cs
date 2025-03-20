using TestOnlineStore.Domain;
using TestOnlineStore.Persistence.DTO.Product.Commands;
using TestOnlineStore.Persistence.DTO.Product.Queries;

namespace TestOnlineStore.Persistence.Repositories.Interfaces;

public interface IRepositoryProduct
{
    Task<List<AllProduct>> GetAllAsync();
    Task<List<RangeProduct>> GetRangeAsync(int countSkip, int countTake);
    Task<Product> GetByIdAsync(int id);
    Task<DetailsProduct> GetDetailsAsync(int id);
    Task<int> AddAsync(CreateProduct product);
    Task DeleteAsync(int id);
    Task UpdateAsync(UpdateProduct product);
}
