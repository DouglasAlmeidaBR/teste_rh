using Teste_RH.Core.Entities;

namespace Teste_RH.Application.DTOs;

public class ListUserDto
{
    public Guid Id { get; set; }
    public DateTime InsertDate { get; set; }
    public DateTime UpdateDate { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;

    public static explicit operator ListUserDto(User user)
    {
        return new ListUserDto
        {
            Id = user.Id,
            InsertDate = user.InsertDate,
            UpdateDate = user.UpdateDate,
            FullName = user.FullName,
            Email = user.Email
        };
    }
}
