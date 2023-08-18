using ArchitectCodingChallenge.Application.People.CreatePeople;
using ArchitectCodingChallenge.Application.People.GetClientPosition;
using ArchitectCodingChallenge.Application.People.GetTopClients;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ArchitectCodingChallenge.Presentation.Controllers;

/// <summary>
/// Represents the API controller for the People data operations.
/// </summary>
[Route("api/people")]
[ApiController]
public class PeopleController : ControllerBase {
    #region Fields
    /// <summary>
    /// Represents the mediator service.
    /// </summary>
    private IMediator _mediator;
    #endregion

    #region Ctor
    /// <summary>
    /// Initializes a new instance of the class <see cref="PeopleController"/>.
    /// </summary>
    /// <param name="mediator"><see cref="IMediator"/> representing the mediator service.</param>
    public PeopleController(IMediator mediator) {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    #endregion

    #region HTTP Methods
    /// <summary>
    /// An endpoint that finds the N people with the highest chance of becoming our clients.
    /// </summary>
    /// <param name="n">Maximum number of people to be found, with the highets chance of becoming clients.</param>
    /// <returns></returns>
    [HttpGet]
    [Route("topclients/{n}")]
    //[Produces()]
    public async Task<ActionResult> GetTopClients(
        int n,
        CancellationToken cancellationToken
    ) {
        var request = new GetTopClientsQuery() { N = n };
        var response = await _mediator.Send(request, cancellationToken);

        if (response is null) {
            return BadRequest(new { Error = "No people were found. 'Response' is null." });
        }

#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
        return response switch { { IsFailed: true } => BadRequest(new { Error = response.Errors[0].Message }), { IsSuccess: true } => Ok(response.Value)
        };
#pragma warning restore CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
    }

    /// <summary>
    /// An endpoint that finds, for a given <c>PersonId</c>, the position on the priority potential clients list.
    /// </summary>
    /// <param name="personId">The person unique identification number.</param>
    /// <returns></returns>
    [HttpGet]
    [Route("clientposition/{personId}")]
    public async Task<ActionResult> GetClientPosition(
        long personId,
        CancellationToken cancellationToken
    ) {
        var request = new GetClientPositionQuery() { PersonId = personId };
        var response = await _mediator.Send(request, cancellationToken);

        if (response is null) {
            return BadRequest(new { Error = "No people were found. 'Response' is null." });
        }

#pragma warning disable CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
        return response switch { { IsFailed: true } => BadRequest(new { Error = response.Errors[0].Message }), { IsSuccess: true } => Ok(response.Value)
        };
#pragma warning restore CS8509 // The switch expression does not handle all possible values of its input type (it is not exhaustive).
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> CreatePeople(
        [FromBody] CreatePeopleCommand request,
        CancellationToken cancellationToken
    ) {
        var response = await _mediator.Send(request, cancellationToken);

        if (response is null) {
            return BadRequest(new { Error = "No people were found. 'Response' is null." });
        }

        return Ok(response.Results);
    }
    #endregion
}
