namespace ArchitectCodingChallenge.Application.People.CreatePeople;

/// <summary>
/// Represents the response for the command pattern for the creation of a new person.
/// </summary>
public sealed class CreatePeopleResponse {
    #region Properties
    /// <summary>
    /// Gets or sets the list of each result returned when adding new people to the list.
    /// </summary>
    public List<CreatedPersonResponseResult> Results { get; set; } = new List<CreatedPersonResponseResult>();
    #endregion
}