using EventsHub.Application.Events.Commands;
using EventsHub.Application.Events.Queries;
using EventsHub.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EventsHub.Api.Controllers;

[Produces("application/json")]
public class EventsController : EventsHubBaseController
{
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<Event>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<Event>>> GetEventsAsync(CancellationToken cancellationToken)
    {
        return await Mediator.Send(new GetEventList.Query(), cancellationToken);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Event>> GetEventDetailAsync(string id)
    {
        try
        {
            return await Mediator.Send(new GetEventDetails.Query { Id = id });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<string>> CreateEventAsync(Event @event)
    {
        return await Mediator.Send(new CreateEvent.Command { Event = @event });
    }

    [HttpPut]
    [ProducesResponseType(typeof(void), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(void), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> EditEventAsync(Event @event)
    {
        try
        {
            await Mediator.Send(new EditEvent.Command { Event = @event });
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    public async Task<ActionResult> DeleteEvent(string id)
    {
        try
        {
            await Mediator.Send(new DeleteEvent.Command { Id = id });
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
