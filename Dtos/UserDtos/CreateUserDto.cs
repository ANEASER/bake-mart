using System.ComponentModel.DataAnnotations;
namespace BakeMart.Dtos.UserDtos;
public record class CreateUserDto(
        [Required] string Name ,
        [Required] string Password ,
        [Required] string Email ,
        [Required] string Phone ,
        [Required] string Role ,
        [Required] string Address ,
        [Required] string ActiveStatus ,
        [Required] string ImageLink 
    );