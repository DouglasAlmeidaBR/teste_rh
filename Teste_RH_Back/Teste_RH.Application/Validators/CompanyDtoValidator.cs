using FluentValidation;
using Teste_RH.Application.DTOs;
using Teste_RH.Application.Validators.Extensions;

namespace Teste_RH.Application.Validators;

public class CompanyDtoValidator : AbstractValidator<CompanyDto>
{
    public CompanyDtoValidator()
    {
        RuleFor(c => c.CompanyName)
            .NotEmpty()
            .WithMessage("O nome da empresa é obrigatório.");
        
        RuleFor(c => c.CompanyType)
            .NotEmpty()
            .WithMessage("O tipo da empresa é obrigatório.");
        
        RuleFor(c => c.DocumentNumber)
            .NotEmpty()
            .Must(doc => doc.IsValidCnpj())
            .WithMessage("Documento inválido. Informe CNPJ válido.");

        RuleFor(c => c.AdministratorName)
            .NotEmpty()
            .WithMessage("O nome do administrador é obrigatório.");

        RuleFor(c => c.AdministratorDocumentNumber)
            .NotEmpty()
            .Must(doc => doc.IsValidCpf())
            .WithMessage("CPF do administrador inválido.");

        RuleFor(c => c.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("O e-mail da empresa deve ser válido.");
        
        RuleFor(c => c.MobilePhone)
            .NotEmpty()
            .Matches(@"^\(?\d{2}\)?\s?\d{4,5}-?\d{4}$")
            .WithMessage("O celular deve estar válido.");

        RuleFor(c => c.Address)
            .NotNull().WithMessage("O endereço é obrigatório.")
            .SetValidator(new CompanyAddressDtoValidator());
    }
}