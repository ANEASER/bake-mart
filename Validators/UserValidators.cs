using FluentValidation;
using BakeMart.Dtos.UserDtos;

namespace BakeMart.Validators;

internal sealed class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(dto => dto.Name).NotEmpty().MaximumLength(50).WithMessage("Name must be less than 50 characters long.");
        RuleFor(dto => dto.Email).NotEmpty().EmailAddress().WithMessage("Invalid email address.");
        RuleFor(dto => dto.Password).NotEmpty().MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
    }
}