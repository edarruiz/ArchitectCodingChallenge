using ArchitectCodingChallenge.Domain.Errors;
using FluentAssertions;

namespace ArchitectCodingChallenge.Tests.DomainTests.Errors;

/// <summary>
/// Represents the tests for the <see cref="DomainErrors"/> class.
/// </summary>
public class DomainErrorTests : IDisposable {
    #region Ctor and Dtor
    public DomainErrorTests() {
        // Startup
    }

    public void Dispose() {
        // Cleanup
    }
    #endregion

    #region Tests
    /// <summary>
    /// Check if the S_CANNOT_BE_NULL constant value is 'cannot be null'.
    /// </summary>
    [Fact(DisplayName = "S_CANNOT_BE_NULL constant value should be 'cannot be null'")]
    public void SCannot_Be_Null_Constant_Value_Should_Be_Specific_Value() {
        // Arrange
        var actual = DomainErrors.S_CANNOT_BE_NULL;
        var expected = "cannot be null";

        // Act
        var result = actual == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the S_CANNOT_BE_NULL_EMPTY_WHITESPACE constant value is
    /// 'cannot be null, empty or  exclusively of white-space characters'.
    /// </summary>
    [Fact(DisplayName = "S_CANNOT_BE_NULL_EMPTY_WHITESPACE constant value should be 'cannot be null, empty or  exclusively of white-space characters'")]
    public void SCannot_Be_Null_Empty_Whitespace_Constant_Value_Should_Be_Specific_Value() {
        // Arrange
        var actual = DomainErrors.S_CANNOT_BE_NULL_EMPTY_WHITESPACE;
        var expected = "cannot be null, empty or consists exclusively of white-space characters";

        // Act
        var result = actual == expected;

        // Assert
        result.Should().BeTrue();
    }
    #endregion
}