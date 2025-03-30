﻿namespace TestConsoleAppOnlineStore.Product;

public class UpdateProduct
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required decimal Price { get; set; }
    public required int CategoryId { get; set; }
}
