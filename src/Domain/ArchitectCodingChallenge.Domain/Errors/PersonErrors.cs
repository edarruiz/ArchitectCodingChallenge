using FluentResults;

namespace ArchitectCodingChallenge.Domain.Errors;

/// <summary>
/// Represents the domain business rules error messages, used by validations.
/// </summary>
public static partial class DomainErrors {
    /// <summary>
    /// Represents the domain business rules error messages for the <see cref="Person"/> entities.
    /// </summary>
    public static class Person {
        /// <summary>
        /// Represents the error message prefix for all person data validations.
        /// </summary>
        private const string S_PREFIX = "The person's";

        /// <summary>
        /// Represents the error message when the person's unique identifier number is null.
        /// </summary>
        public static Error IdIsNull => new($"{S_PREFIX} unique identifier number {S_CANNOT_BE_NULL}.");

        /// <summary>
        /// Represents the error message when the person first name is null, empty or filled with whitespaces. 
        /// </summary>
        public static Error NameIsNullEmptyOrWhiteSpace => new($"{S_PREFIX} name {S_CANNOT_BE_NULL_EMPTY_WHITESPACE}.");

        /// <summary>
        /// Represents the error message when the person's last name is null, empty or filled with whitespaces.
        /// </summary>
        public static Error LastNameIsNullEmptyOrWhiteSpace => new($"{S_PREFIX} last name {S_CANNOT_BE_NULL_EMPTY_WHITESPACE}.");

        /// <summary>
        /// Represents the error message when the person's current role is null, empty or filled with whitespaces.
        /// </summary>
        public static Error CurrentRoleIsNullEmptyOrWhiteSpace => new($"{S_PREFIX} current role {S_CANNOT_BE_NULL_EMPTY_WHITESPACE}.");

        /// <summary>
        /// Represents the error message when the person's country origin is null, empty or filled with whitespaces.
        /// </summary>
        public static Error CountryIsNullEmptyOrWhiteSpace => new($"{S_PREFIX} country origin {S_CANNOT_BE_NULL_EMPTY_WHITESPACE}.");

        /// <summary>
        /// Represents the error message when the person's industry origin is null, empty or filled with whitespaces.
        /// </summary>
        public static Error IndustryIsNullEmptyOrWhiteSpace => new($"{S_PREFIX} industry {S_CANNOT_BE_NULL_EMPTY_WHITESPACE}.");
    }
}