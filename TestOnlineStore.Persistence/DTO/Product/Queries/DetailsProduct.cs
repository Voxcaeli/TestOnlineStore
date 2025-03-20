namespace TestOnlineStore.Persistence.DTO.Product.Queries;

public class DetailsProduct
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }

    public required int CategoryId { get; set; }
    public required string CategoryName { get; set; }
    public required string CategoryDescription { get; set; }
}
