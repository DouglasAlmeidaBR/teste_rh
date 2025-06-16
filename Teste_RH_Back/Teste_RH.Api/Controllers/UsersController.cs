using Microsoft.AspNetCore.Mvc;
using Teste_RH.Application.DTOs;
using Teste_RH.Core.Entities;
using Teste_RH.Core.Interfaces;

namespace Teste_RH.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create([FromBody] AddUserDto dto)
    {
        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email
        };
        user.SetPassword(dto.Password);

        var result = await _userService.AddAsync(user);

        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
    {
        var user = new User
        {
            Id = dto.Id,
            FullName = dto.FullName,
            Email = dto.Email
        };

        user.SetPassword(dto.Password);

        await _userService.UpdateAsync(user);

        return NoContent();
    }

    [HttpPut("company")]
    public async Task<IActionResult> UpdateCompany([FromBody] CompanyDto dto)
    {
        var company = new Company
        {
            UserId = dto.UserId,
            CompanyType = dto.CompanyType,
            CompanyName = dto.CompanyName,
            DocumentNumber = dto.DocumentNumber,
            AdministratorName = dto.AdministratorName,
            AdministratorDocumentNumber = dto.AdministratorDocumentNumber,
            Email = dto.Email,
            MobilePhone = dto.MobilePhone,
            Address = dto.Address == null ? null : new CompanyAddress
            {
                ZipCode = dto.Address.ZipCode,
                Address = dto.Address.Address,
                Neighborhood = dto.Address.Neighborhood,
                State = dto.Address.State,
                City = dto.Address.City,
                AddressComplement = dto.Address.AddressComplement
            }
        };

        await _userService.UpdateCompanyAsync(company);

        return NoContent();
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<ListUserDto>>> GetAll()
    {
        var result = await _userService.GetAllAsync();

        return Ok(result.Select(_ => (ListUserDto)_));
    }

    [HttpGet("{userId:Guid}")]
    public async Task<ActionResult<GetUserDto>> GetById([FromRoute] Guid userId)
    {
        var result = await _userService.GetUserByIdAsync(userId);
        
        return Ok(result != null ? (GetUserDto)result : null);
    }

    [HttpDelete("{userId:Guid}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
    {
        await _userService.DeleteUserAsync(userId);

        return Ok();
    }
}
