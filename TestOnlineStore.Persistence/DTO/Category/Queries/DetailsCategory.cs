﻿namespace TestOnlineStore.Persistence.DTO.Category.Queries;

public class DetailsCategory
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
