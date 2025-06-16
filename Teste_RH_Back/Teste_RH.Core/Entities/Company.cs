namespace Teste_RH.Core.Entities;

public class Company
{
    public Guid Id { get; set; } = new();
    public DateTime InsertDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public Guid UserId { get; set; }
    public string CompanyType { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string DocumentNumber { get; set; } = string.Empty;
    public string AdministratorName { get; set; } = string.Empty;
    public string AdministratorDocumentNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string MobilePhone { get; set; } = string.Empty;
    public virtual User? User { get; set; }
    public virtual CompanyAddress? Address { get; set; }
}
