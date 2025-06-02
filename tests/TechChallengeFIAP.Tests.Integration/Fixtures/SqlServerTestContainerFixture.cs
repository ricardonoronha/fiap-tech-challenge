using Testcontainers.MsSql;

namespace TechChallengeFIAP.Tests.Integration.Fixtures;

public class SqlServerTestContainerFixture : IAsyncLifetime
{
    public MsSqlContainer Container { get; private set; } = null!;

    public string ConnectionString => Container.GetConnectionString();

    public async Task InitializeAsync()
    {
        Container = new MsSqlBuilder()
            .WithPassword("Integration@123") // senha obrigatória forte
            .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
            .Build();

        await Container.StartAsync();
    }

    public async Task DisposeAsync()
    {
        await Container.DisposeAsync();
    }
}
