using EventsHub.Api.Controllers;
using EventsHub.Application.Events.Queries;
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
}
