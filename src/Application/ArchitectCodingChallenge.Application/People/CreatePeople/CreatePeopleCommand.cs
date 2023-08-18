using ArchitectCodingChallenge.Domain.Models;
using MediatR;
using Newtonsoft.Json;

namespace ArchitectCodingChallenge.Application.People.CreatePeople;

/// <summary>
/// Represents the command pattern for the creation of a new person.
/// </summary>
public sealed class CreatePeopleCommand : IRequest<CreatePeopleResponse> {
    #region Properties
    /// <summary>
    /// Gets or sets the <see cref="List{T}"/> of <see cref="PersonModel"/> 
    /// representing the list of people with the highest chance of becoming clients.
    /// </summary>
    [JsonProperty(nameof(People))]
    public List<PersonModel> People { get; set; } = new List<PersonModel>();
    #endregion
}
