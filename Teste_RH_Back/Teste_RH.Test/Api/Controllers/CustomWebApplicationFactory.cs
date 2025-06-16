using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Teste_RH.Core.Interfaces;

namespace Teste_RH.Test.Api.Controllers;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    public Mock<IUserReadRepository> UserReadRepositoryMock { get; private set; }
    public Mock<IUserWriteRepository> UserWriteRepositoryMock { get; private set; }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<IUserReadRepository>();
            services.RemoveAll<IUserWriteRepository>();

            UserReadRepositoryMock = new Mock<IUserReadRepository>();
            UserWriteRepositoryMock = new Mock<IUserWriteRepository>();

            services.AddScoped(sp => UserReadRepositoryMock.Object);
            services.AddScoped(sp => UserWriteRepositoryMock.Object);
        });
    }
}
