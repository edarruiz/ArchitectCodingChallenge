namespace ArchitectCodingChallenge.Domain.Models;

/// <summary>
/// Represents a person, a talent that could be potentially hired by the company. 
/// </summary>
/// <remarks>
/// This model should represent the raw data read and write when loading the people information from
/// the dataset.
/// </remarks>
public sealed class PersonModel {
    #region Properties
    /// <summary>
    /// Gets or sets the person's unique identifier number.
    /// </summary>
    public long PersonId { get; set; }

    /// <summary>
    /// Gets or sets the person's first name.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the person's last name.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the person's current role.
    /// </summary>
    public string CurrentRole { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the person's country origin.
    /// </summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the person's industry.
    /// </summary>
    public string Industry { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the person's number of LinkedIn recomendations.
    /// </summary>
    public int? NumberOfRecommendations { get; set; }

    /// <summary>
    /// Gets or sets the person's number of LinkedIn connections.
    /// </summary>
    public int? NumberOfConnections { get; set; }
    #endregion
}