using EventsHub.Api.Controllers;
using EventsHub.Application.Core;
using EventsHub.Application.Core.Mapping;
using EventsHub.Application.Events.Queries;
using EventsHub.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EventsHub.UnitTests.Controllers;

[TestFixture]
public class EventsControllerTests
{
    private EventsController _eventsController = null!;
    private ServiceProvider _services = null!;

    [SetUp]
    public void Setup()
    {
        _services = new ServiceCollection()
            .AddLogging()
            .AddSingleton(GlobalTestSetup.AppDbContext)
            .AddMediatR(options => options.RegisterServicesFromAssemblyContaining<GetEventList.Handler>())
            .AddMapper(typeof(MappingProfiles).Assembly)
            .BuildServiceProvider();
        _eventsController = new EventsController
        {
            ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { RequestServices = _services }
            }
        };
    }

    [TearDown]
    public void TearDown() => _services.Dispose();

    [Test]
    public async Task GetEventsAsync_WhenEventsExist_ReturnsAllEvents()
    {
        // Arrange
        var expectedCount = await GlobalTestSetup.AppDbContext.Events.CountAsync();
        // Act
        var result = await _eventsController.GetEventsAsync(CancellationToken.None);
        // Assert
        Assert.That(result.Value, Is.Not.Null);
        Assert.That(result.Value, Has.Count.EqualTo(expectedCount));
    }

    [Test]
    public void GetEventsAsync_WhenRequestIsCancelled_PropagatesCancellation()
    {
        using var cancellation = new CancellationTokenSource();
        cancellation.Cancel();

        Assert.ThrowsAsync<OperationCanceledException>(async () =>
            await _eventsController.GetEventsAsync(cancellation.Token));
    }

    [Test]
    public async Task GetEventDetailAsync_WhenEventExists_ReturnsMatchingEvent()
    {
        // Arrange
        var existing = await GlobalTestSetup.AppDbContext.Events.FirstAsync();
        // Act
        var result = await _eventsController.GetEventDetailAsync(existing.Id);
        // Assert
        Assert.That(result.Value, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(result.Value.Id, Is.EqualTo(existing.Id));
            Assert.That(result.Value.Title, Is.EqualTo(existing.Title));
        });
    }

    [Test]
    public async Task GetEventDetailAsync_WhenEventDoesntExist_ReturnsNotFound()
    {
        var nonExistentId = Guid.NewGuid().ToString();

        var result = await _eventsController.GetEventDetailAsync(nonExistentId);
        
        Assert.That(result.Result, Is.InstanceOf<NotFoundObjectResult>());

        var notFoundResult = (NotFoundObjectResult)result.Result;

        Assert.Multiple(() =>
        {
            Assert.That(notFoundResult.Value, Is.EqualTo("The event was not found"));
            Assert.That(notFoundResult.StatusCode, Is.EqualTo(404));
        });
    }

    [Test]
    public async Task CreateEventAsync_WhenEventIsValid_PersistsEventAndReturnsItsId()
    {
        var newEvent = NewEvent();

        try
        {
            var result = await _eventsController.CreateEventAsync(newEvent);

            Assert.That(result.Value, Is.EqualTo(newEvent.Id));
            var persisted = await GlobalTestSetup.AppDbContext.Events
                .AsNoTracking()
                .SingleAsync(item => item.Id == newEvent.Id);
            Assert.That(persisted.Title, Is.EqualTo(newEvent.Title));
        }
        finally
        {
            await RemoveEventIfPresentAsync(newEvent.Id);
        }
    }

    [Test]
    public async Task EditEventAsync_WhenEventExists_UpdatesFieldsAndPreservesUnchangedFields()
    {
        var existing = NewEvent();
        existing.Date = new DateTime(2032, 6, 7, 8, 9, 10, DateTimeKind.Utc);
        existing.Category = $"preserved-category-{Guid.NewGuid():N}";
        existing.City = $"Preserved city {Guid.NewGuid():N}";
        existing.Venue = $"Preserved venue {Guid.NewGuid():N}";
        existing.Latitude = "20.123456";
        existing.Longitude = "-102.654321";
        GlobalTestSetup.AppDbContext.Events.Add(existing);
        await GlobalTestSetup.AppDbContext.SaveChangesAsync();

        var expectedPreserved = new
        {
            existing.Date,
            existing.Category,
            existing.City,
            existing.Venue,
            existing.Latitude,
            existing.Longitude,
        };
        var updated = NewEvent(existing.Id);
        updated.Title = "Updated title";
        updated.Description = "Updated description";
        updated.IsCancelled = true;
        updated.Date = expectedPreserved.Date;
        updated.Category = expectedPreserved.Category;
        updated.City = expectedPreserved.City;
        updated.Venue = expectedPreserved.Venue;
        updated.Latitude = expectedPreserved.Latitude;
        updated.Longitude = expectedPreserved.Longitude;

        try
        {
            var result = await _eventsController.EditEventAsync(updated);

            Assert.That(result, Is.InstanceOf<NoContentResult>());
            var persisted = await GlobalTestSetup.AppDbContext.Events
                .AsNoTracking()
                .SingleAsync(item => item.Id == existing.Id);
            Assert.Multiple(() =>
            {
                Assert.That(persisted.Title, Is.EqualTo("Updated title"));
                Assert.That(persisted.Description, Is.EqualTo("Updated description"));
                Assert.That(persisted.IsCancelled, Is.True);
                Assert.That(persisted.Date, Is.EqualTo(expectedPreserved.Date));
                Assert.That(persisted.Category, Is.EqualTo(expectedPreserved.Category));
                Assert.That(persisted.City, Is.EqualTo(expectedPreserved.City));
                Assert.That(persisted.Venue, Is.EqualTo(expectedPreserved.Venue));
                Assert.That(persisted.Latitude, Is.EqualTo(expectedPreserved.Latitude));
                Assert.That(persisted.Longitude, Is.EqualTo(expectedPreserved.Longitude));
            });
        }
        finally
        {
            await RemoveEventIfPresentAsync(existing.Id);
        }
    }

    [Test]
    public async Task EditEventAsync_WhenEventDoesNotExist_ReturnsNotFound()
    {
        var result = await _eventsController.EditEventAsync(NewEvent());

        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        Assert.That(((NotFoundObjectResult)result).Value, Is.EqualTo("The event was not found"));
    }

    [Test]
    public async Task DeleteEvent_WhenEventExists_RemovesEvent()
    {
        var existing = NewEvent();
        GlobalTestSetup.AppDbContext.Events.Add(existing);
        await GlobalTestSetup.AppDbContext.SaveChangesAsync();

        try
        {
            var result = await _eventsController.DeleteEvent(existing.Id);

            Assert.That(result, Is.InstanceOf<OkResult>());
            Assert.That(await GlobalTestSetup.AppDbContext.Events
                .AsNoTracking()
                .AnyAsync(item => item.Id == existing.Id), Is.False);
        }
        finally
        {
            await RemoveEventIfPresentAsync(existing.Id);
        }
    }

    [Test]
    public async Task DeleteEvent_WhenEventDoesNotExist_ReturnsNotFound()
    {
        var result = await _eventsController.DeleteEvent(Guid.NewGuid().ToString());

        Assert.That(result, Is.InstanceOf<NotFoundObjectResult>());
        Assert.That(((NotFoundObjectResult)result).Value, Is.EqualTo("The event was not found"));
    }

    private static Event NewEvent(string? id = null) => new()
    {
        Id = id ?? Guid.NewGuid().ToString(),
        Title = "Unit test event",
        Date = new DateTime(2030, 1, 2, 3, 4, 5, DateTimeKind.Utc),
        Description = "Created by a controller unit test",
        Category = "unit-test",
        IsCancelled = false,
        City = "Test city",
        Venue = "Test venue",
        Latitude = "19.4326",
        Longitude = "-99.1332",
    };

    private static async Task RemoveEventIfPresentAsync(string id)
    {
        var existing = await GlobalTestSetup.AppDbContext.Events.FindAsync(id);
        if (existing is null)
        {
            return;
        }

        GlobalTestSetup.AppDbContext.Events.Remove(existing);
        await GlobalTestSetup.AppDbContext.SaveChangesAsync();
    }
}
