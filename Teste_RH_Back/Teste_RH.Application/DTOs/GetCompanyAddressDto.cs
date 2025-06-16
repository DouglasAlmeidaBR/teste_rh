using Teste_RH.Core.Entities;

namespace Teste_RH.Application.DTOs;

public class GetCompanyAddressDto
{
    public string ZipCode { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string AddressComplement { get; set; } = string.Empty;

    public static explicit operator GetCompanyAddressDto(CompanyAddress companyAddress)
    {
        return new GetCompanyAddressDto
        {
            ZipCode = companyAddress.ZipCode,
            Address = companyAddress.Address,
            Neighborhood = companyAddress.Neighborhood,
            State = companyAddress.State,
            City = companyAddress.City,
            AddressComplement = companyAddress.AddressComplement
        };
    }
}