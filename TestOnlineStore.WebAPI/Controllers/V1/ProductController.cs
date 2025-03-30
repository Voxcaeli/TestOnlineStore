using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TestOnlineStore.Persistence.DTO.Product.Commands;
using TestOnlineStore.Persistence.DTO.Product.Queries;
using TestOnlineStore.Persistence.Repositories.Interfaces;

namespace TestOnlineStore.WebAPI.Controllers.V1;

/// <summary>
/// Controller of products
/// </summary>
/// <param name="repositoryProduct">Repository of products</param>
/// <param name="validator">Validator of create product</param>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class ProductController(IRepositoryProduct repositoryProduct,
                               IValidator<CreateProduct> validator)
    : ControllerBase
{
    /// <summary>
    /// Gets all products
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /api/products
    /// </remarks>
    /// <returns>
    /// returns list of products (products)
    /// </returns>
    /// <response code="200">Success</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<AllProduct>>> GetAllAsync()
    {
        var products = await repositoryProduct.GetAllAsync();

        return Ok(products);
    }

    /// <summary>
    /// Gets product by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /api/products/5
    /// </remarks>
    /// <returns>
    /// returns info about product (product)
    /// </returns>
    /// <response code="200">Success</response>
    /// <response code="404">If product is not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetailsProduct>> GetDetailsAsync(int id)
    {
        var product = await repositoryProduct.GetDetailsAsync(id);

        return Ok(product);
    }

    /// <summary>
    /// Gets range of products
    /// </summary>
    /// <param name="countSkip">Number of products to skip</param>
    /// <param name="countTake">Number of products to take</param>
    /// <returns>returns list of products (products)</returns>
    /// <response code="200">Success</response>
    [HttpGet("{countSkip}/{countTake}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<RangeProduct>>> GetRangeAsync(int countSkip, int countTake)
    {
        var rangeProducts = await repositoryProduct.GetRangeAsync(countSkip, countTake);

        return Ok(rangeProducts);
    }

    /// <summary>
    /// Creates product
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /api/products
    /// {
    ///     name: "name of product",
    ///     description: "description of product",
    ///     price: "price of product",
    ///     category id: "id of product category",
    /// }
    /// </remarks>
    /// <param name="createProduct">CreateProduct object</param>
    /// <returns>
    /// returns id (int)
    /// </returns>
    /// <response code="200">Success</response>
    /// <response code="400">
    /// If name is empty or length exceeds 100 characters,
    /// If decrtiption is empty or length exceeds 400 characters
    /// If price is zero or less than zero
    /// If category id is not set
    /// </response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> AddAsync([FromBody] CreateProduct createProduct)
    {
        validator.ValidateAndThrow(createProduct);
        var id = await repositoryProduct.AddAsync(createProduct);

        return Ok(id);
    }

    /// <summary>
    /// Updates product
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /api/products
    /// {
    ///     name: "name of product",
    ///     description: "description of product",
    ///     price: "price of product",
    ///     category id: "id of product category",
    /// }
    /// </remarks>
    /// <param name="updateProduct">UpdateProduct object</param>
    /// <response code="200">Success</response>
    /// <response code="400">
    /// If name is empty or length exceeds 100 characters,
    /// If decrtiption is empty or length exceeds 400 characters
    /// If price is zero or less than zero
    /// If category id is not set
    /// </response>
    /// <response code="404">Product is not found</response>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task UpdateAsync([FromBody] UpdateProduct updateProduct)
    {
        await repositoryProduct.UpdateAsync(updateProduct);
    }

    /// <summary>
    /// Deletes product
    /// </summary>
    /// <param name="id">Product object</param>
    /// <response code="200">Success</response>
    /// <response code="404">Product is not found</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task DeleteAsync(int id)
    {
        await repositoryProduct.DeleteAsync(id);
    }
}
