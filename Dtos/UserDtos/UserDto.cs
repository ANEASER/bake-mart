
namespace BakeMart.Dtos.UserDtos;
public record class  UserDto
    (
        int Id ,
        string Name,
        string Email,
        string Phone,
        string Role,
        string Address,
        string ActiveStatus,
        string ImageLink
    );
