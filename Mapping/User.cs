using BakeMart.Entities;
using System;
using BakeMart.Dtos.UserDtos;

namespace BakeMart.Mapping
{
    public static class UserMapping
    {
        public static User ToEntity(this CreateUserDto userDto)
        {
            return new User()
            {
                Name = userDto.Name,
                Password = userDto.Password,
                Email = userDto.Email,
                Phone = userDto.Phone,
                Role = userDto.Role,
                Address = userDto.Address,
                ActiveStatus = userDto.ActiveStatus,
                ImageLink = userDto.ImageLink
            };
        }
        
        public static UserDto FromDto(this User user)
        {
            return new UserDto(
                user.Id,
                user.Name,
                user.Email,
                user.Phone,
                user.Role,
                user.Address,
                user.ActiveStatus,
                user.ImageLink
            );
        }
    }
}
 