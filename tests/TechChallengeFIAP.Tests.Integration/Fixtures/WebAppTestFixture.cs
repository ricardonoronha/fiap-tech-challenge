using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using TechChallengeFIAP.Data.Repositorios;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using TechChallengeFIAP.Domain.Interfaces;

namespace TechChallengeFIAP.Tests.Integration.Fixtures;

public class WebAppTestFixture : WebApplicationFactory<Program>
{
    private readonly string _connectionString;

    public WebAppTestFixture(string connectionString)
    {
        _connectionString = connectionString;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTests");

        builder.ConfigureServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(_connectionString));

            // Setup inicial opcional
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            db.Database.Migrate();
        });
    }

    public async Task SaveEntities(params object[] entities)
    {
        using var scope = Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dbContext.AddRange(entities);
        await dbContext.SaveChangesAsync();
    }

    public T GetRequiredService<T>()
        where T : notnull
        => Services.GetRequiredService<T>();
}