namespace Teste_RH.Core.Entities;

public class CompanyAddress
{
    public Guid Id { get; set; } = new();
    public DateTime InsertDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public Guid CompanyId { get; set; }
    public string ZipCode { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Neighborhood { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string AddressComplement { get; set; } = string.Empty;
    public virtual Company? Company { get; set; }
}