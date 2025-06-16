using Moq;
using System.Net;
using System.Net.Http.Json;
using Teste_RH.Application.DTOs;
using Teste_RH.Core.Entities;
using Teste_RH.Core.Interfaces;

namespace Teste_RH.Test.Api.Controllers;

public class UsersControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;
    private readonly Mock<IUserReadRepository> _userReadRepositoryMock;
    private readonly Mock<IUserWriteRepository> _userWriteRepositoryMock;

    public UsersControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();

        _userReadRepositoryMock = _factory.UserReadRepositoryMock;
        _userWriteRepositoryMock = _factory.UserWriteRepositoryMock;

        _userReadRepositoryMock.Invocations.Clear();
        _userReadRepositoryMock.Reset(); 
        _userWriteRepositoryMock.Invocations.Clear();
        _userWriteRepositoryMock.Reset();
    }


    [Fact]
    [Trait("Category", "UsersController - POST")]
    public async Task Create_ReturnsOkWithGuid_WhenUserIsAddedSuccessfully()
    {
        var addDto = new AddUserDto
        {
            FullName = "Test New User",
            Email = "test.new@example.com",
            Password = "SecurePassword123"
        };
        var expectedUserId = Guid.NewGuid();

        _userWriteRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>()))
                                .ReturnsAsync(expectedUserId);

        var response = await _client.PostAsJsonAsync("/api/Users", addDto);

        response.EnsureSuccessStatusCode(); 
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var returnedGuid = await response.Content.ReadFromJsonAsync<Guid>();
        Assert.Equal(expectedUserId, returnedGuid);

        _userWriteRepositoryMock.Verify(repo => repo.AddAsync(
            It.Is<User>(u => u.FullName == addDto.FullName && u.Email == addDto.Email)),
            Times.Once);
    }

    [Fact]
    [Trait("Category", "UsersController - POST")]
    public async Task Create_ReturnsBadRequest_WhenDtoIsInvalid()
    {
        var invalidAddDto = new AddUserDto
        {
            FullName = "", 
            Email = "invalid-email", 
            Password = "123" 
        };

        var response = await _client.PostAsJsonAsync("/api/Users", invalidAddDto);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        _userWriteRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Never);
    }

    [Fact]
    [Trait("Category", "UsersController - PUT")]
    public async Task Update_ReturnsNoContent_WhenUserIsUpdatedSuccessfully()
    {
        var updateDto = new UpdateUserDto
        {
            Id = Guid.NewGuid(),
            FullName = "Updated User Name",
            Email = "updated.email@example.com",
            Password = "NewSecurePassword456"
        };

        _userWriteRepositoryMock.Setup(repo => repo.UpdateAsync(It.IsAny<User>()))
                                .Returns(Task.CompletedTask);

        var response = await _client.PutAsJsonAsync("/api/Users", updateDto);

        response.EnsureSuccessStatusCode(); 
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        _userWriteRepositoryMock.Verify(repo => repo.UpdateAsync(
            It.Is<User>(u => u.Id == updateDto.Id && u.FullName == updateDto.FullName)),
            Times.Once);
    }

    [Fact]
    [Trait("Category", "UsersController - PUT")]
    public async Task UpdateCompany_ReturnsNoContent_WhenCompanyIsUpdatedSuccessfully()
    {
        var companyDto = new CompanyDto
        {
            UserId = Guid.NewGuid(),
            CompanyType = "Test Type",
            CompanyName = "Test Company",
            DocumentNumber = "78072464000109",
            AdministratorName = "Admin Test",
            AdministratorDocumentNumber = "04020028060",
            Email = "company@test.com",
            MobilePhone = "11987585552",
            Address = new CompanyAddressDto
            {
                ZipCode = "03151208",
                Address = "Test St",
                Neighborhood = "Test Neighborhood",
                State = "TS",
                City = "Test City",
                AddressComplement = "Apt 1"
            }
        };

        _userWriteRepositoryMock.Setup(repo => repo.UpdateCompanyAsync(It.IsAny<Company>()))
                                .Returns(Task.CompletedTask);

        var response = await _client.PutAsJsonAsync("/api/Users/company", companyDto);

        response.EnsureSuccessStatusCode(); 
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);

        _userWriteRepositoryMock.Verify(repo => repo.UpdateCompanyAsync(
            It.Is<Company>(c => c.UserId == companyDto.UserId && c.CompanyName == companyDto.CompanyName)),
            Times.Once);
    }

    [Fact]
    [Trait("Category", "UsersController - PUT")]
    public async Task UpdateCompany_ReturnsBadRequest_WhenDtoIsInvalid()
    {
        var invalidCompanyDto = new CompanyDto
        {
            UserId = Guid.Empty, 
            CompanyName = "",
            DocumentNumber = "invalid" 
        };

        var response = await _client.PutAsJsonAsync("/api/Users/company", invalidCompanyDto);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        _userWriteRepositoryMock.Verify(repo => repo.UpdateCompanyAsync(It.IsAny<Company>()), Times.Never);
    }

    [Fact]
    [Trait("Category", "UsersController - GET")]
    public async Task GetAll_ReturnsOk_WithListOfUsers()
    {
        var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), FullName = "List User 1", Email = "list1@example.com" },
                new User { Id = Guid.NewGuid(), FullName = "List User 2", Email = "list2@example.com" }
            };

        _userReadRepositoryMock.Setup(repo => repo.GetAllAsync())
                               .ReturnsAsync(users);

        var response = await _client.GetAsync("/api/Users/list");

        response.EnsureSuccessStatusCode(); 
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var resultDtos = await response.Content.ReadFromJsonAsync<List<ListUserDto>>();

        Assert.NotNull(resultDtos);
        Assert.Equal(2, resultDtos.Count);
        Assert.Contains(resultDtos, dto => dto.FullName == "List User 1");
        Assert.Contains(resultDtos, dto => dto.FullName == "List User 2");

        _userReadRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Fact]
    [Trait("Category", "UsersController - GET")]
    public async Task GetAll_ReturnsOk_WithEmptyList_WhenNoUsersExist()
    {
        _userReadRepositoryMock.Setup(repo => repo.GetAllAsync())
                               .ReturnsAsync(new List<User>());

        var response = await _client.GetAsync("/api/Users/list");

        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var resultDtos = await response.Content.ReadFromJsonAsync<List<ListUserDto>>();

        Assert.NotNull(resultDtos);
        Assert.Empty(resultDtos);

        _userReadRepositoryMock.Verify(repo => repo.GetAllAsync(), Times.Once);
    }

    [Fact]
    [Trait("Category", "UsersController - GET")]
    public async Task GetById_ReturnsOkWithUserDto_WhenUserExists()
    {
        var userId = Guid.NewGuid();
        var user = new User
        {
            Id = userId,
            FullName = "Specific User",
            Email = "specific@test.com",
            Company = new Company 
            {
                UserId = userId,
                CompanyName = "Test Company",
                DocumentNumber = "00000000000100",
                AdministratorName = "Admin",
                AdministratorDocumentNumber = "123",
                Email = "comp@test.com",
                MobilePhone = "123",
                Address = new CompanyAddress
                {
                    ZipCode = "12345678",
                    Address = "Rua Teste",
                    Neighborhood = "Bairro Teste",
                    State = "SP",
                    City = "Cidade Teste",
                    AddressComplement = ""
                }
            }
        };

        _userReadRepositoryMock.Setup(repo => repo.GetUserByIdAsync(userId))
                               .ReturnsAsync(user);

        var response = await _client.GetAsync($"/api/Users/{userId}");

        response.EnsureSuccessStatusCode(); 
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var resultDto = await response.Content.ReadFromJsonAsync<GetUserDto>();

        Assert.NotNull(resultDto);
        Assert.Equal(userId, resultDto.Id);
        Assert.Equal(user.FullName, resultDto.FullName);
        Assert.NotNull(resultDto.Company);
        Assert.Equal(user.Company.CompanyName, resultDto.Company.CompanyName);

        _userReadRepositoryMock.Verify(repo => repo.GetUserByIdAsync(userId), Times.Once);
    }


    [Fact]
    [Trait("Category", "UsersController - GET")]
    public async Task GetById_ReturnsBadRequest_WhenIdIsNotGuid()
    {
        var invalidId = "not-a-guid";

        var response = await _client.GetAsync($"/api/Users/{invalidId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        _userReadRepositoryMock.Verify(repo => repo.GetUserByIdAsync(It.IsAny<Guid>()), Times.Never); 
    }


    [Fact]
    [Trait("Category", "UsersController - DELETE")]
    public async Task DeleteUser_ReturnsOk_WhenUserIsDeletedSuccessfully()
    {
        var userId = Guid.NewGuid();

        _userWriteRepositoryMock.Setup(repo => repo.DeleteUserAsync(userId))
                                .Returns(Task.CompletedTask);

        var response = await _client.DeleteAsync($"/api/Users/{userId}");

        response.EnsureSuccessStatusCode(); 
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        _userWriteRepositoryMock.Verify(repo => repo.DeleteUserAsync(userId), Times.Once);
    }

    [Fact]
    [Trait("Category", "UsersController - DELETE")]
    public async Task DeleteUser_ReturnsBadRequest_WhenIdIsNotGuid()
    {
        var invalidId = "not-a-guid";

        var response = await _client.DeleteAsync($"/api/Users/{invalidId}");

        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        _userWriteRepositoryMock.Verify(repo => repo.DeleteUserAsync(It.IsAny<Guid>()), Times.Never);
    }
}