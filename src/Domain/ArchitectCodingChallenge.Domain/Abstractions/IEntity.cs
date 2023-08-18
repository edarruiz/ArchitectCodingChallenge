namespace ArchitectCodingChallenge.Domain.Abstractions;

/// <summary>
/// Represents the implementation interface for the base Entity class used for inheritance.
/// </summary>
public interface IEntity {
    #region Properties
    /// <summary>
    /// Gets the global unique identifier of the Entity.
    /// </summary>
    Guid Guid { get; }
    #endregion
}
