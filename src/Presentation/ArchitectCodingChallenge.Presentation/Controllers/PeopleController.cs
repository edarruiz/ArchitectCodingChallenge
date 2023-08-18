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
    /// Problem 1: An endpoint that finds the N people with the highest chance of becoming our clients.
    /// </summary>
    /// <param name="n">Maximum number of people to be found, with the highets chance of becoming clients.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <response code="200">The N people with the highest change of becoming our clients.</response>
    /// <response code="400">The error message when a search problem occurs.</response>
    [HttpGet]
    [Route("topclients/{n}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
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
    /// Problem 2: An endpoint that finds, for a given <c>PersonId</c>, the position on the priority potential clients list.
    /// </summary>
    /// <param name="personId">The person unique identification number.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <response code="200">The position on the priority clients list.</response>
    /// <response code="400">The error message when a search problem occurs.</response>
    [HttpGet]
    [Route("clientposition/{personId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
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

    /// <summary>
    /// Bonus implementation: Another endpoint that allows the insertion of a new Person object and calculates its priority value.
    /// </summary>
    /// <param name="request">The list containing the new people to be added in the list.</param>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <response code="200">The results on every persons create action.</response>
    /// <response code="400">The error message when a search problem occurs.</response>
    [HttpPost]
    [Route("create")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
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
