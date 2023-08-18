using FluentResults;
using MediatR;

namespace ArchitectCodingChallenge.Application.People.GetClientPosition;

/// <summary>
/// Represents the query for searching a person's the position on the priority potential clients list.
/// </summary>
public sealed class GetClientPositionQuery : IRequest<Result<GetClientPositionResponse>> {
    #region Properties
    /// <summary>
    /// Gets or sets the person's unique identifier number.
    /// </summary>
    public long PersonId { get; set; }
    #endregion
}
