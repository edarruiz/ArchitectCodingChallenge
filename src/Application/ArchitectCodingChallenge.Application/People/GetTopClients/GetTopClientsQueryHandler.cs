using ArchitectCodingChallenge.Domain.Abstractions;
using ArchitectCodingChallenge.Domain.Models;
using FluentResults;
using MediatR;
using Serilog;

namespace ArchitectCodingChallenge.Application.People.GetTopClients;

/// <summary>
/// Represents the mediator pattern handler for the <see cref="GetTopClientsQuery"/>.
/// </summary>
public sealed class GetTopClientsQueryHandler : IRequestHandler<GetTopClientsQuery, Result<GetTopClientsResponse>> {
    #region Fields
    /// <summary>
    /// Represents the service for the people repository operations.
    /// </summary>
    private IPeopleRepository _peopleRepository;
    #endregion

    #region Ctor
    /// <summary>
    /// Initializes a new instance of the class <see cref="GetTopClientsQueryHandler"/>.
    /// </summary>
    /// <param name="peopleRepository"><see cref="IPeopleRepository"/> representing the people repository service.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="peopleRepository"/> is <c>null</c>.</exception>
    public GetTopClientsQueryHandler(IPeopleRepository peopleRepository) {
        _peopleRepository = peopleRepository ?? throw new ArgumentNullException(nameof(peopleRepository));
    }
    #endregion

    #region IRequestHandler
    ///<inheritdoc/>
    public async Task<Result<GetTopClientsResponse>> Handle(GetTopClientsQuery request, CancellationToken cancellationToken) {
        ArgumentNullException.ThrowIfNull(nameof(request));
        if (request.N < 0) {
            var msg = "The number of people to be found with the highest chance of becoming our clients cannot be negative.";
            Log.Error(msg);
            return Result.Fail(msg);
        }

        var topClients = await _peopleRepository.GetTopClients(request.N);
        if (topClients.Count == 0) {
            return Result.Ok(new GetTopClientsResponse());
        }

        var response = new GetTopClientsResponse();
        foreach (var person in topClients) {
            response.TopClients.Add(new PersonIdModel { PersonId = person.PersonId!.Value });
        }

        return Result.Ok(response);
    }

    #endregion
}
