using FluentValidation;
using Teste_RH.Application.DTOs;

namespace Teste_RH.Application.Validators;

public class AddUserDtoValidator : AbstractValidator<AddUserDto>
{
    public AddUserDtoValidator()
    {
        RuleFor(x => x.FullName).NotEmpty();
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}