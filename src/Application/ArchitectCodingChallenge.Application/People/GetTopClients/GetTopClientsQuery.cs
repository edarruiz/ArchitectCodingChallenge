using FluentResults;
using MediatR;

namespace ArchitectCodingChallenge.Application.People.GetTopClients;

/// <summary>
/// Represents the query for searching the people with the highest chance of becoming clients.
/// </summary>
public sealed class GetTopClientsQuery : IRequest<Result<GetTopClientsResponse>> {
    #region Properties
    /// <summary>
    /// Represents the maximum number of people to be found, with the highets chance of becoming clients.
    /// </summary>
    public int N { get; set; }
    #endregion
}
