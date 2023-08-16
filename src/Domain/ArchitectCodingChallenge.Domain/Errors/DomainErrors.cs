namespace ArchitectCodingChallenge.Domain.Errors;

/// <summary>
/// Represents the domain business rules error messages, used by validations.
/// </summary>
public static partial class DomainErrors {
    /// <summary>
    /// Represents the composite string for null validations.
    /// </summary>
    public const string S_CANNOT_BE_NULL = "cannot be null";

    /// <summary>
    /// Represents the composite string for string null validations.
    /// </summary>
    public const string S_CANNOT_BE_NULL_EMPTY_WHITESPACE = $"{S_CANNOT_BE_NULL}, empty or consists exclusively of white-space characters";
}
