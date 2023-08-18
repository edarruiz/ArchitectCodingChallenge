using ArchitectCodingChallenge.Domain.Errors;
using FluentAssertions;

namespace ArchitectCodingChallenge.Tests.DomainTests.Errors;

/// <summary>
/// Represents the tests for the <see cref="DomainErrors.Person"/> class.
/// </summary>
public class PersonErrorTests : IDisposable {
    #region Ctor and Dtor
    public PersonErrorTests() {
        // Startup
    }

    public void Dispose() {
        // Cleanup
    }
    #endregion

    #region Tests
    /// <summary>
    /// Check if the <see cref="DomainErrors.Person.IdIsNull"/> error message is
    /// 'The person's unique identifier number cannot be null.'.
    /// </summary>
    [Fact(DisplayName = "IdIsNull error message should be 'The person's unique identifier number cannot be null.'")]
    public void IdIsNull_Error_Message_Should_Be_Specific_Value() {
        // Arrange
        var actual = DomainErrors.Person.IdIsNull.Message;
        var expected = "The person's unique identifier number cannot be null.";

        // Act
        var result = actual == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the <see cref="DomainErrors.Person.IdIsNegative"/> error message is
    /// 'The person's unique identifier number cannot be a negative number.'.
    /// </summary>
    [Fact(DisplayName = "IdIsNegative error message should be 'The person's unique identifier number cannot be a negative number.'")]
    public void IdIsNegative_Error_Message_Should_Be_Specific_Value() {
        // Arrange
        var actual = DomainErrors.Person.IdIsNegative.Message;
        var expected = "The person's unique identifier number cannot be a negative number.";

        // Act
        var result = actual == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the <see cref="DomainErrors.Person.NameIsNullEmptyOrWhiteSpace"/> error message is
    /// 'The person's name cannot be null, empty or consists exclusively of white-space characters.'.
    /// </summary>
    [Fact(DisplayName = "NameIsNullEmptyOrWhiteSpace error message should be 'The person's first name cannot be null, empty or consists exclusively of white-space characters.'")]
    public void FirstNameIsNullEmptyOrWhiteSpace_Error_Message_Should_Be_Specific_Value() {
        // Arrange
        var actual = DomainErrors.Person.NameIsNullEmptyOrWhiteSpace.Message;
        var expected = "The person's first name cannot be null, empty or consists exclusively of white-space characters.";

        // Act
        var result = actual == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the <see cref="DomainErrors.Person.LastNameIsNullEmptyOrWhiteSpace"/> error message is
    /// 'The person's last name cannot be null, empty or consists exclusively of white-space characters.'.
    /// </summary>
    [Fact(DisplayName = "LastNameIsNullEmptyOrWhiteSpace error message should be 'The person's last name cannot be null, empty or consists exclusively of white-space characters.'")]
    public void LastNameIsNullEmptyOrWhiteSpace_Error_Message_Should_Be_Specific_Value() {
        // Arrange
        var actual = DomainErrors.Person.LastNameIsNullEmptyOrWhiteSpace.Message;
        var expected = "The person's last name cannot be null, empty or consists exclusively of white-space characters.";

        // Act
        var result = actual == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the <see cref="DomainErrors.Person.CurrentRoleIsNullEmptyOrWhiteSpace"/> error message is
    /// 'The person's current role cannot be null, empty or consists exclusively of white-space characters.'.
    /// </summary>
    [Fact(DisplayName = "CurrentRoleIsNullEmptyOrWhiteSpace error message should be 'The person's current role cannot be null, empty or consists exclusively of white-space characters.'")]
    public void CurrentRoleIsNullEmptyOrWhiteSpace_Error_Message_Should_Be_Specific_Value() {
        // Arrange
        var actual = DomainErrors.Person.CurrentRoleIsNullEmptyOrWhiteSpace.Message;
        var expected = "The person's current role cannot be null, empty or consists exclusively of white-space characters.";

        // Act
        var result = actual == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the <see cref="DomainErrors.Person.CountryIsNullEmptyOrWhiteSpace"/> error message is
    /// 'The person's country origin cannot be null, empty or consists exclusively of white-space characters.'.
    /// </summary>
    [Fact(DisplayName = "CountryIsNullEmptyOrWhiteSpace error message should be 'The person's country origin cannot be null, empty or consists exclusively of white-space characters.'")]
    public void CountryIsNullEmptyOrWhiteSpace_Error_Message_Should_Be_Specific_Value() {
        // Arrange
        var actual = DomainErrors.Person.CountryIsNullEmptyOrWhiteSpace.Message;
        var expected = "The person's country origin cannot be null, empty or consists exclusively of white-space characters.";

        // Act
        var result = actual == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the <see cref="DomainErrors.Person.IndustryIsNullEmptyOrWhiteSpace"/> error message is
    /// 'The person's industry cannot be null, empty or consists exclusively of white-space characters.'.
    /// </summary>
    [Fact(DisplayName = "IndustryIsNullEmptyOrWhiteSpace error message should be 'The person's industry cannot be null, empty or consists exclusively of white-space characters.'")]
    public void IndustryIsNullEmptyOrWhiteSpace_Error_Message_Should_Be_Specific_Value() {
        // Arrange
        var actual = DomainErrors.Person.IndustryIsNullEmptyOrWhiteSpace.Message;
        var expected = "The person's industry cannot be null, empty or consists exclusively of white-space characters.";

        // Act
        var result = actual == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the <see cref="DomainErrors.Person.NumberOfRecomendationsIsNegative"/> error message is
    /// 'The person's number of recomendations cannot be a negative number.'.
    /// </summary>
    [Fact(DisplayName = "NumberOfRecomendationsIsNegative error message should be 'The person's number of recomendations cannot be a negative number.'")]
    public void NumberOfRecomendationsIsNegative_Error_Message_Should_Be_Specific_Value() {
        // Arrange
        var actual = DomainErrors.Person.NumberOfRecomendationsIsNegative.Message;
        var expected = "The person's number of recomendations cannot be a negative number.";

        // Act
        var result = actual == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the <see cref="DomainErrors.Person.NumberOfConnectionsIsNegative"/> error message is
    /// 'The person's number of connections cannot be a negative number.'.
    /// </summary>
    [Fact(DisplayName = "NumberOfConnectionsIsNegative error message should be 'The person's number of connections cannot be a negative number.'")]
    public void NumberOfConnections_Error_Message_Should_Be_Specific_Value() {
        // Arrange
        var actual = DomainErrors.Person.NumberOfConnectionsIsNegative.Message;
        var expected = "The person's number of connections cannot be a negative number.";

        // Act
        var result = actual == expected;

        // Assert
        result.Should().BeTrue();
    }
    #endregion
}