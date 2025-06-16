namespace Teste_RH.Application.DTOs;

public class CompanyDto
{
    public Guid UserId { get; set; }
    public string CompanyType { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public string AdministratorName { get; set; } = string.Empty;
    public string AdministratorDocumentNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MobilePhone { get; set; } = string.Empty;
    public CompanyAddressDto? Address { get; set; }
}
