using Newtonsoft.Json;

namespace ArchitectCodingChallenge.Domain.Models;

/// <summary>
/// Represents a person, a talent that could be potentially hired by the company. 
/// </summary>
/// <remarks>
/// This model should represent the raw data read and write when loading the people information from
/// the dataset.
/// </remarks>
[Serializable]
public sealed class PersonModel {
    #region Properties
    /// <summary>
    /// Gets or sets the person's unique identifier number.
    /// </summary>
    [JsonProperty(nameof(PersonId))]
    public long PersonId { get; set; }

    /// <summary>
    /// Gets or sets the person's first name.
    /// </summary>
    [JsonProperty(nameof(FirstName))]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the person's last name.
    /// </summary>
    [JsonProperty(nameof(LastName))]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the person's current role.
    /// </summary>
    [JsonProperty(nameof(CurrentRole))]
    public string CurrentRole { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the person's country origin.
    /// </summary>
    [JsonProperty(nameof(Country))]
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the person's industry.
    /// </summary>
    [JsonProperty(nameof(Industry))]
    public string Industry { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the person's number of LinkedIn recomendations.
    /// </summary>
    [JsonProperty(nameof(NumberOfRecommendations))]
    public int? NumberOfRecommendations { get; set; }

    /// <summary>
    /// Gets or sets the person's number of LinkedIn connections.
    /// </summary>
    [JsonProperty(nameof(NumberOfConnections))]
    public int? NumberOfConnections { get; set; }
    #endregion
}