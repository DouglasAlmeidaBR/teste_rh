using Teste_RH.Core.Entities;

namespace Teste_RH.Application.DTOs;

public class GetCompanyDto
{
    public string CompanyType { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public string AdministratorName { get; set; } = string.Empty;
    public string AdministratorDocumentNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MobilePhone { get; set; } = string.Empty;
    public GetCompanyAddressDto? Address { get; set; }

    public static explicit operator GetCompanyDto(Company company)
    {
        return new GetCompanyDto
        {
            CompanyType = company.CompanyType,
            CompanyName = company.CompanyName,
            DocumentNumber = company.DocumentNumber,
            AdministratorName = company.AdministratorName,
            AdministratorDocumentNumber = company.AdministratorDocumentNumber,
            Email = company.Email,
            MobilePhone = company.MobilePhone,
            Address = company.Address != null ? (GetCompanyAddressDto)company.Address : null
        };
    }
}
