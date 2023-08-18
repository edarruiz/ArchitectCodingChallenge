using System.Net;

namespace ArchitectCodingChallenge.Application.People.CreatePeople;

/// <summary>
/// Represents a result from a newly created person in the list.
/// </summary>
public class CreatedPersonResponseResult {
    #region Properties
    /// <summary>
    /// Gets or sets the person unique identification number.
    /// </summary>
    public long PersonId { get; set; }

    /// <summary>
    /// Gets or sets the <see cref="HttpStatusCode"/> representing the result of the person's creation.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// Gets or sets the result message.
    /// </summary>
    public string? Message { get; set; }
    #endregion
}
