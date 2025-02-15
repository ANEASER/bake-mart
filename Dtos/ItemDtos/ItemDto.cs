namespace BakeMart.Dtos.ItemDtos;

public record class ItemDto
    (
        int Id,
        string ItemName,
        string Description,
        int Price,
        string ImageLink
    );
