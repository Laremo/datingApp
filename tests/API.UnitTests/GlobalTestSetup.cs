using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.UnitTests;

[SetUpFixture]
public class GlobalTestSetup
{
   

    public static AppDataContext AppDataContext { get; private set; }

    [OneTimeSetUp]
    public async Task Setup()
    {
        DbContextOptions<AppDataContext> options = new DbContextOptionsBuilder<AppDataContext>()
        .UseSqlite("Data source=dating.db").Options;

        AppDataContext = new AppDataContext(options);

        await AppDataContext.Database.MigrateAsync();
        await Seed.Seed.SeedUsers(AppDataContext);

    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        await AppDataContext.DisposeAsync();
    }

}
