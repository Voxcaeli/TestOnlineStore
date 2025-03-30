using Asp.Versioning;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TestOnlineStore.Domain;
using TestOnlineStore.Persistence.DTO.Category.Commands;
using TestOnlineStore.Persistence.DTO.Category.Queries;
using TestOnlineStore.Persistence.Repositories.Interfaces;

namespace TestOnlineStore.WebAPI.Controllers.V2;

/// <summary>
/// Controller of categories
/// </summary>
/// <param name="repositoryCategory">Repository of categories</param>
/// <param name="categoryvalidator">Validator of create category</param>
[ApiController]
[ApiVersion("2.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CategoryController(IRepositoryCategory repositoryCategory,
                                IValidator<CreateCategory> categoryvalidator)
    : ControllerBase
{
    /// <summary>
    /// Gets category by id
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// GET /api/categories/5
    /// </remarks>
    /// <returns>
    /// returns info about category (category)
    /// </returns>
    /// <response code="200">Success</response>
    /// <response code="404">If category is not found</response>
    [HttpGet("{id}", Name = "GetById")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DetailsCategory>> GetDetailsAsync(int id)
    {
        var category = await repositoryCategory.GetDetailsAsync(id);

        return Ok(category);
    }

    /// <summary>
    /// Creates category
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /api/categories
    /// {
    ///     name: "name of category",
    ///     description: "description of category",
    /// }
    /// </remarks>
    /// <param name="createCategory">CreateCategory object</param>
    /// <returns>
    /// returns id (int)
    /// </returns>
    /// <response code="200">Success</response>
    /// <response code="400">
    /// If name is empty or length exceeds 100 characters,
    /// If decrtiption is empty or length exceeds 400 characters
    /// </response>
    [HttpPost(Name = "Add")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<int>> AddAsync([FromBody] CreateCategory createCategory)
    {
        categoryvalidator.ValidateAndThrow(createCategory);

        var newCategory = new Category
        {
            Name = createCategory.Name,
            Description = createCategory.Description!
        };

        var id = await repositoryCategory.AddAsync(newCategory);

        return Ok(id);
    }

    /// <summary>
    /// Updates category
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// POST /api/categories
    /// {
    ///     name: "name of category",
    ///     description: "description of category",
    /// }
    /// </remarks>
    /// <param name="updateCategory">UpdateCategory object</param>
    /// <response code="200">Success</response>
    /// <response code="400">
    /// If name is empty or length exceeds 100 characters,
    /// If decrtiption is empty or length exceeds 400 characters
    /// </response>
    /// <response code="404">Category is not found</response>
    [HttpPut(Name = "Update")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task UpdateAsync([FromBody] Category updateCategory)
    {
        await repositoryCategory.UpdateAsync(updateCategory);
    }

    /// <summary>
    /// Deletes category
    /// </summary>
    /// <param name="id">Category object</param>
    /// <response code="200">Success</response>
    /// <response code="404">Category is not found</response>
    [HttpDelete]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task DeleteAsync(int id)
    {
        await repositoryCategory.DeleteAsync(id);
    }
}
