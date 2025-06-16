using MediatR;
using Moq;
using Teste_RH.Application.Commands;
using Teste_RH.Application.Queries;
using Teste_RH.Application.Services;
using Teste_RH.Core.Entities;

namespace Teste_RH.Test.Application.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IMediator> _mediatorMock;
        private readonly UserService _userService;

        public UserServiceTests()
        {
            _mediatorMock = new Mock<IMediator>();
            _userService = new UserService(_mediatorMock.Object);
        }

        [Fact]
        public async Task AddAsync_ShouldSendCreateUserCommandAndReturnGuid()
        {
            var user = new User{ Id = Guid.NewGuid(), FullName = "Test User", Email = "test@example.com", PasswordHash = "password123" }; 
            var expectedGuid = Guid.NewGuid();

            _mediatorMock.Setup(m => m.Send(It.IsAny<CreateUserCommand>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedGuid);

            var result = await _userService.AddAsync(user);

            _mediatorMock.Verify(m => m.Send(
                It.Is<CreateUserCommand>(c => c.User == user), 
                It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(expectedGuid, result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldSendUpdateUserCommand()
        {
            var user = new User{ Id = Guid.NewGuid(), FullName = "Updated User", Email = "updated@example.com", PasswordHash = "newpassword" }; 

            await _userService.UpdateAsync(user);

            _mediatorMock.Verify(m => m.Send(
                It.Is<UpdateUserCommand>(c => c.User == user),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateCompanyAsync_ShouldSendUpdateCompanyCommand()
        {
            var company = new Company { Id = Guid.NewGuid(), CompanyName = "Test Company", DocumentNumber = "12345678000100" }; 

            await _userService.UpdateCompanyAsync(company);

            _mediatorMock.Verify(m => m.Send(
                It.Is<UpdateCompanyCommand>(c => c.company == company),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ShouldSendGetAllUsersQueryAndReturnListOfUsers()
        {
            var expectedUsers = new List<User>
            {
                new User{Id = Guid.NewGuid(), FullName = "User1", Email = "user1@example.com", PasswordHash = "pass1" },
                new User{Id = Guid.NewGuid(), FullName = "User2", Email = "user2@example.com", PasswordHash = "pass2" }
            };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetAllUsersQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedUsers);

            var result = await _userService.GetAllAsync();

            _mediatorMock.Verify(m => m.Send(
                It.IsAny<GetAllUsersQuery>(),
                It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(expectedUsers, result);
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldSendGetUserByIdQueryAndReturnUser()
        {
            var userId = Guid.NewGuid();
            var expectedUser = new User{ Id = userId, FullName = "Found User", Email = "found@example.com", PasswordHash = "foundpass" };

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedUser);

            var result = await _userService.GetUserByIdAsync(userId);

            _mediatorMock.Verify(m => m.Send(
                It.Is<GetUserByIdQuery>(q => q.userId == userId),
                It.IsAny<CancellationToken>()), Times.Once);
            Assert.Equal(expectedUser, result);
        }

        [Fact]
        public async Task GetUserByIdAsync_ShouldReturnNull_WhenUserNotFound()
        {
            var userId = Guid.NewGuid();

            _mediatorMock.Setup(m => m.Send(It.IsAny<GetUserByIdQuery>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync((User?)null); 

            var result = await _userService.GetUserByIdAsync(userId);

            _mediatorMock.Verify(m => m.Send(
                It.Is<GetUserByIdQuery>(q => q.userId == userId),
                It.IsAny<CancellationToken>()), Times.Once);
            Assert.Null(result);
        }

        [Fact]
        public async Task DeleteUserAsync_ShouldSendDeleteUserCommand()
        {
            var userId = Guid.NewGuid();

            await _userService.DeleteUserAsync(userId);

            _mediatorMock.Verify(m => m.Send(
                It.Is<DeleteUserCommand>(c => c.userId == userId),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
