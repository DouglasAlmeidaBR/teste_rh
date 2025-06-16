using Teste_RH.Core.Entities;

namespace Teste_RH.Application.DTOs;

public class GetUserDto
{
    public Guid Id { get; set; }
    public DateTime InsertDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public GetCompanyDto? Company { get; set; }

    public static explicit operator GetUserDto(User user)
    {
        return new GetUserDto
        {
            Id = user.Id,
            InsertDate = user.InsertDate,
            UpdateDate = user.UpdateDate,
            FullName = user.FullName,
            Email = user.Email,
            Company = user.Company != null ? (GetCompanyDto) user.Company : null 
        };
    }
}
