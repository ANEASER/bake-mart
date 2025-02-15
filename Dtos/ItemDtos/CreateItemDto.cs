using System.ComponentModel.DataAnnotations;

namespace BakeMart.Dtos.ItemDtos;

public record class CreateItemDto
    (
        [Required] string ItemName,
        [Required] string Description,
        [Required] int Price,
        [Required] string ImageLink
    );