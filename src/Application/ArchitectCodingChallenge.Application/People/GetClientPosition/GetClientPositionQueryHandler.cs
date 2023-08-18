using ArchitectCodingChallenge.Domain.Abstractions;
using FluentResults;
using MediatR;

namespace ArchitectCodingChallenge.Application.People.GetClientPosition;

/// <summary>
/// Represents the mediator pattern handler for the <see cref="GetClientPositionQuery"/>.
/// </summary>
public sealed class GetClientPositionQueryHandler : IRequestHandler<GetClientPositionQuery, Result<GetClientPositionResponse>> {
    #region Fields
    /// <summary>
    /// Represents the service for the people repository operations.
    /// </summary>
    private IPeopleRepository _peopleRepository;
    #endregion

    #region Ctor
    /// <summary>
    /// Initializes a new instance of the class <see cref="GetClientPositionQueryHandler"/>.
    /// </summary>
    /// <param name="peopleRepository"><see cref="IPeopleRepository"/> representing the people repository service.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="peopleRepository"/> is <c>null</c>.</exception>
    public GetClientPositionQueryHandler(IPeopleRepository peopleRepository) {
        _peopleRepository = peopleRepository ?? throw new ArgumentNullException(nameof(peopleRepository));
    }

    #endregion

    #region IRequestHandler
    /// <inheritdoc/>
    public async Task<Result<GetClientPositionResponse>> Handle(GetClientPositionQuery request, CancellationToken cancellationToken) {
        ArgumentNullException.ThrowIfNull(nameof(request));

        var position = await _peopleRepository.GetClientPosition(request.PersonId);

        return Result.Ok(new GetClientPositionResponse() { Position = position });
    }
    #endregion
}
