﻿namespace TestOnlineStore.Persistence.DTO.Category.Commands;

public class CreateCategory
{
    public required string Name { get; set; }
    public string? Description { get; set; }
}
