using Microsoft.AspNetCore.Mvc;
using TestOnlineStore.Persistence.DTO.Product.Commands;
using TestOnlineStore.Persistence.DTO.Product.Queries;
using TestOnlineStore.Persistence.Repositories.Interfaces;

namespace TestOnlineStore.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController(IRepositoryProduct repositoryProduct)
    : ControllerBase
{
    [HttpGet]
    public async Task<List<AllProduct>> GetAllAsync()
    {
        var products = await repositoryProduct.GetAllAsync();

        return products;
    }

    [HttpGet("{id}")]
    public async Task<DetailsProduct> GetDetailsAsync(int id)
    {
        var product = await repositoryProduct.GetDetailsAsync(id);

        return product;
    }

    [HttpGet("{countSkip}/{countTake}")]
    public async Task<List<RangeProduct>> GetRangeAsync(int countSkip, int countTake)
    {
        var rangeProducts = await repositoryProduct.GetRangeAsync(countSkip, countTake);

        return rangeProducts;
    }

    [HttpPost]
    public async Task<int> AddAsync([FromBody] CreateProduct product)
    {
        var id = await repositoryProduct.AddAsync(product);

        return id;
    }

    [HttpDelete]
    public async Task DeleteAsync(int id)
    {
        await repositoryProduct.DeleteAsync(id);
    }

    [HttpPut]
    public async Task UpdateAsync([FromBody] UpdateProduct product)
    {
        await repositoryProduct.UpdateAsync(product);
    }
}
