namespace TestConsoleAppOnlineStore.Product;

public class CreateProduct
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public required int CategoryId { get; set; }
}