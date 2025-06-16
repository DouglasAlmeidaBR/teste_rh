using FluentValidation;
using Teste_RH.Application.DTOs;

namespace Teste_RH.Application.Validators;

public class CompanyAddressDtoValidator : AbstractValidator<CompanyAddressDto>
{
    public CompanyAddressDtoValidator()
    {
        RuleFor(a => a.ZipCode).NotEmpty().WithMessage("O CEP é obrigatório.");
        RuleFor(a => a.Address).NotEmpty().WithMessage("O endereço é obrigatório.");
        RuleFor(a => a.Neighborhood).NotEmpty().WithMessage("O bairro é obrigatório.");
        RuleFor(a => a.City).NotEmpty().WithMessage("A cidade é obrigatória.");
        RuleFor(a => a.State).NotEmpty().WithMessage("O estado é obrigatório.");
    }
}
