namespace ArchitectCodingChallenge.Infrastructure.Persistence;

/// <summary>
/// Represents the database context target, which can be an in-memory database or a real database system, being it relational or NoSql.
/// </summary>
public enum DatabaseContextTarget {
    /// <summary>
    /// Represents an unknown database system as a context target.
    /// </summary>
    Unknown = -1,

    /// <summary>
    /// Represents the in-memory database as a context target.
    /// </summary>
    InMemory,

    /// <summary>
    /// Represents the Microsoft SQL Server relation database as a context target.
    /// </summary>
    SqlServer,

    /// <summary>
    /// Represents the Oracle relational database as a context target.
    /// </summary>
    Oracle,

    /// <summary>
    /// Represents the PostgreSQL relational database as context target.
    /// </summary>
    PostgreSQL,

    /// <summary>
    /// Represents the MongoDB NoSQL database as context target.
    /// </summary>
    MongoDB,
    // etc...
}
