using FluentValidation;
using Main.API.DtoModels;

namespace Main.API.Validators
{
    public class UserRegistrationDtoValidator: AbstractValidator<UserRegistrationDto>
    {
        public UserRegistrationDtoValidator()
        {
            RuleFor(x=>x.FirstName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please ensure that you have entered {PropertyName}");

            RuleFor(x => x.LastName)
                .NotNull()
                .NotEmpty()
                .WithMessage("Please ensure that you have entered {PropertyName}");

            RuleFor(x => x.Email)
                .NotNull()
                .EmailAddress()
                .WithMessage("Not valid email address")
                .NotEmpty()
                .WithMessage("Please ensure that you have entered {PropertyName}");

            RuleFor(x => x.Password)
                .NotNull()
                .Equal(x => x.ConfirmPassword)
                .WithMessage("Confirm password do not match a password")
                .NotEmpty()
                .WithMessage("Please ensure that you have entered {PropertyName}");
        }
    }
}
