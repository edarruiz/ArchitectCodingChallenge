namespace ArchitectCodingChallenge.Infrastructure.Persistence.Abstractions;

/// <summary>
/// Represents the interface for the implementation of the database context of the the persist layer.
/// </summary>
/// <remarks>
/// This interface should be used for the interface abstraction when changing the infrastructure target for the dataset,
/// which can be an in-memory database or a real database system, being it relational or NoSql.
/// </remarks>
public interface IPersistenceDatabaseContext {
    #region Properties
    /// <summary>
    /// Gets the database context id of the instance.
    /// </summary>
    Guid DatabaseContextId { get; }

    /// <summary>
    /// Gets the database context target, which can be an in-memory database or a real database system, being it relational or NoSql.
    /// </summary>
    DatabaseContextTarget DatabaseContextTarget { get; }
    #endregion
}
