using Newtonsoft.Json;

namespace ArchitectCodingChallenge.Domain.Models;

/// <summary>
/// Represents a person unique identifier number.
/// </summary>
public sealed class PersonIdModel {
    #region Properties
    /// <summary>
    /// Gets or sets the person's unique identifier number.
    /// </summary>
    [JsonProperty(nameof(PersonId))]
    public long PersonId { get; set; }
    #endregion
}
