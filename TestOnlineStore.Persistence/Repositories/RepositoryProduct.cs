using Microsoft.EntityFrameworkCore;
using TestOnlineStore.Domain;
using TestOnlineStore.Persistence.Common.Exceptions;
using TestOnlineStore.Persistence.DTO.Product.Commands;
using TestOnlineStore.Persistence.DTO.Product.Queries;
using TestOnlineStore.Persistence.Repositories.Interfaces;

namespace TestOnlineStore.Persistence.Repositories;

public class RepositoryProduct(TestOnlineStoreDBContext context, IRepositoryCategory repositoryCategory)
    : IRepositoryProduct
{
    public async Task<List<AllProduct>> GetAllAsync()
    {
        var products = await context.Products
            .Select(product => new AllProduct
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            })
            .AsNoTracking()
            .ToListAsync();

        return products;
    }

    public async Task<List<RangeProduct>> GetRangeAsync(int countSkip, int countTake)
    {
        var products = await context.Products
            .Select(product => new RangeProduct
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            })
            .AsNoTracking()
            .Skip(countSkip)
            .Take(countTake)
            .ToListAsync();

        return products;
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        var product = await context.Products
            .SingleOrDefaultAsync(product => product.Id == id)
            ?? throw new NotFoundException(nameof(Product), id);

        return product;
    }

    public async Task<DetailsProduct> GetDetailsAsync(int id)
    {
        var product = await context.Products
            .Include(product => product.Category)
            .Select(product => new DetailsProduct
            {
                Id = product.Id,
                Name = product.Name,
                Description= product.Description,
                Price= product.Price,

                CategoryId = product.CategoryId,
                CategoryName = product.Category.Name,
                CategoryDescription = product.Category.Description
            })
            .AsNoTracking()
            .SingleOrDefaultAsync(product => product.Id == id)
            ?? throw new NotFoundException(nameof(Product), id);

        return product;
    }

    public async Task<int> AddAsync(CreateProduct product)
    {
        var category = await GetCategoryAsync(product.CategoryId);

        var newProduct = new Product
        {
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Category = category
        };

        await context.AddAsync(newProduct);
        await context.SaveChangesAsync();

        return newProduct.Id;
    }

    public async Task DeleteAsync(int id)
    {
        var product = await GetByIdAsync(id);

        context.Products.Remove(product);
        // alternative: context.Remove(product);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UpdateProduct product)
    {
        var category = await GetCategoryAsync(product.CategoryId);
        var updatedProduct = await GetByIdAsync(product.Id);

        updatedProduct.Name = product.Name;
        updatedProduct.Description = product.Description;
        updatedProduct.Price = product.Price;
        updatedProduct.Category = category;

        await context.SaveChangesAsync();
    }

    private async Task<Category> GetCategoryAsync(int id)
    {
        //var category = await context.Categories
        //    .SingleOrDefaultAsync(x => x.Id == id)
        //    ?? throw new NotFoundException(nameof(ProductCategory), id);

        var category = await repositoryCategory.GetByIdAsync(id);

        return category;
    }
}
