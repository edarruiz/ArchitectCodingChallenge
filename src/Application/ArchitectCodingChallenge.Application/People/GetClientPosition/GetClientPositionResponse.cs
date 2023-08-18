namespace ArchitectCodingChallenge.Application.People.GetClientPosition;

/// <summary>
/// Represents the response for the query for searching a person's the position on the priority potential clients list.
/// </summary>
public sealed class GetClientPositionResponse {
    #region Properties
    /// <summary>
    /// Gets or sets the person position on the priority potential clients list.
    /// </summary>
    public int Position { get; set; }
    #endregion
}
