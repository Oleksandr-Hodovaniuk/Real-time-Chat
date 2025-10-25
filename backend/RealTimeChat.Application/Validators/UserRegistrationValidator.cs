using FluentValidation;
using RealTimeChat.Application.Dtos;

namespace RealTimeChat.Application.Validators;

public class UserRegistrationValidator : AbstractValidator<UserRegisterDto>
{
    public UserRegistrationValidator()
    {
        RuleFor(x => x.Username)
            .NotEmpty()
            .Length(2, 40).WithMessage("Username must be between 2 and 40 characters.")
            .Matches("^[A-Za-z0-9' -]+$")
            .WithMessage("Username can only contain letters, digits, spaces, hyphens, and apostrophes.");

        RuleFor(x => x.Password)
           .NotEmpty()
           .Length(8, 40).WithMessage("Password must must be between 8 and 40 characters.")
           .Matches("^[A-Za-z0-9]+$").WithMessage("Password can only contain letters and numbers.");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty()
            .Equal(x => x.Password)
            .WithMessage("Password and Confirm Password must match.");
    }
}
