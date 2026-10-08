using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventsHub.Persistence;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EventsHub.UnitTests;

[SetUpFixture]
public class GlobalTestSetup
{
    private SqliteConnection _connection = null!;

    public static AppDbContext AppDbContext { get; private set; }

    [OneTimeSetUp]
    public async Task Setup()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        await _connection.OpenAsync();

        DbContextOptions<AppDbContext> options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlite(_connection)
            .Options;

        AppDbContext = new AppDbContext(options);
        await AppDbContext.Database.MigrateAsync();
        await DbInitializer.SeedDataAsync(AppDbContext);
    }

    [OneTimeTearDown]
    public async Task TearDown()
    {
        await AppDbContext.DisposeAsync();
        await _connection.DisposeAsync();
    }
}
