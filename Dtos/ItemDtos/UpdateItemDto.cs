namespace BakeMart.Dtos.ItemDtos;

public record class UpdateItemDto
    (
        int Id,
        string ItemName,
        string Description,
        int Price,
        string ImageLink
    );
