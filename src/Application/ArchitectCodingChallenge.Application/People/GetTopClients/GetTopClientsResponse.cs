using ArchitectCodingChallenge.Domain.Models;

namespace ArchitectCodingChallenge.Application.People.GetTopClients;

/// <summary>
/// Represents the response for the query for searching the people with the highest chance of becoming clients.
/// </summary>
public sealed class GetTopClientsResponse {
    #region Properties
    /// <summary>
    /// Gets or sets the <see cref="List{T}"/> of <see cref="PersonIdModel"/> 
    /// representing the list of people with the highest chance of becoming clients.
    /// </summary>
    public List<PersonIdModel> TopClients { get; set; } = new List<PersonIdModel>();
    #endregion
}
