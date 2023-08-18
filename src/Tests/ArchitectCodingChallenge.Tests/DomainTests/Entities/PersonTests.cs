using ArchitectCodingChallenge.Domain.Entities;
using FluentAssertions;

namespace ArchitectCodingChallenge.Tests.DomainTests.Entities;

/// <summary>
/// Represents the tests for the <see cref="Person"/> class.
/// </summary>
public class PersonTests : IDisposable {
    #region Ctor and Dtor
    public PersonTests() {
        // Startup
    }

    public void Dispose() {
        // Cleanup
    }
    #endregion

    #region Tests
    /// <summary>
    /// Check if the factory method Create() does not allow the PersonId to be null.
    /// </summary>
    [Fact(DisplayName = "Person's Id cannot be null")]
    public void Person_Id_Cannot_Be_Null() {
        // Arrange
        var actual = Person.Create(
            Constants.EntityGuid,
            null,
            "First Name",
            "Last Name",
            "Software Developer",
            "Brazil",
            "Information Technology Services",
            5,
            5
        );
        var expected = "The person's unique identifier number cannot be null.";

        // Act
        var result = actual.IsFailed && actual.Errors.First().Message == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the factory method Create() does not allow the PersonId to be negative.
    /// </summary>
    [Fact(DisplayName = "Person's Id cannot be negative")]
    public void Person_Id_Cannot_Be_Negative() {
        // Arrange
        var actual = Person.Create(
            Constants.EntityGuid,
            -1000,
            "First Name",
            "Last Name",
            "Software Developer",
            "Brazil",
            "Information Technology Services",
            5,
            5
        );
        var expected = "The person's unique identifier number cannot be a negative number.";

        // Act
        var result = actual.IsFailed && actual.Errors.First().Message.Contains(expected);

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the factory method Create() does not allow the FirtName to be null.
    /// </summary>
    [Fact(DisplayName = "Person's first name cannot be null")]
    public void Person_FirstName_Cannot_Be_Null() {
        // Arrange
        var actualNull = Person.Create(
            Constants.EntityGuid,
            1000,
            null,
            "Last Name",
            "Software Developer",
            "Brazil",
            "Information Technology Services",
            5,
            5
        );
        var actualEmpty = Person.Create(
            Constants.EntityGuid,
            1000,
            "",
            "Last Name",
            "Software Developer",
            "Brazil",
            "Information Technology Services",
            5,
            5
        );
        var actualWhiteSpace = Person.Create(
            Constants.EntityGuid,
            1000,
            "     ",
            "Last Name",
            "Software Developer",
            "Brazil",
            "Information Technology Services",
            5,
            5
        );
        var expected = "The person's first name cannot be null, empty or consists exclusively of white-space characters.";

        // Act
        var result =
            actualNull.IsFailed
            && actualNull.Errors.First().Message.Contains(expected)
            && actualEmpty.IsFailed
            && actualEmpty.Errors.First().Message.Contains(expected)
            && actualWhiteSpace.IsFailed
            && actualWhiteSpace.Errors.First().Message.Contains(expected);

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the factory method Create() does not allow the LastName to be null.
    /// </summary>
    [Fact(DisplayName = "Person's last name cannot be null")]
    public void Person_LastName_Cannot_Be_Null() {
        // Arrange
        var actualNull = Person.Create(
            Constants.EntityGuid,
            1000,
            "First Name",
            null,
            "Software Developer",
            "Brazil",
            "Information Technology Services",
            5,
            5
        );
        var actualEmpty = Person.Create(
            Constants.EntityGuid,
            1000,
            "First Name",
            "",
            "Software Developer",
            "Brazil",
            "Information Technology Services",
            5,
            5
        );
        var actualWhiteSpace = Person.Create(
            Constants.EntityGuid,
            1000,
            "First Name",
            "     ",
            "Software Developer",
            "Brazil",
            "Information Technology Services",
            5,
            5
        );
        var expected = "The person's last name cannot be null, empty or consists exclusively of white-space characters.";

        // Act
        var result =
            actualNull.IsFailed
            && actualNull.Errors.First().Message.Contains(expected)
            && actualEmpty.IsFailed
            && actualEmpty.Errors.First().Message.Contains(expected)
            && actualWhiteSpace.IsFailed
            && actualWhiteSpace.Errors.First().Message.Contains(expected);

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the factory method Create() does not allow the CurrentRole to be null.
    /// </summary>
    [Fact(DisplayName = "Person's current role cannot be null")]
    public void Person_CurrentRole_Cannot_Be_Null() {
        // Arrange
        var actualNull = Person.Create(
            Constants.EntityGuid,
            1000,
            "First Name",
            "Last Name",
            null,
            "Brazil",
            "Information Technology Services",
            5,
            5
        );
        var actualEmpty = Person.Create(
            Constants.EntityGuid,
            1000,
            "First Name",
            "Last Name",
            "",
            "Brazil",
            "Information Technology Services",
            5,
            5
        );
        var actualWhiteSpace = Person.Create(
            Constants.EntityGuid,
            1000,
            "First Name",
            "Last Name",
            "     ",
            "Brazil",
            "Information Technology Services",
            5,
            5
        );

        var expected = "The person's current role cannot be null, empty or consists exclusively of white-space characters.";

        // Act
        var result =
            actualNull.IsFailed
            && actualNull.Errors.First().Message.Contains(expected)
            && actualEmpty.IsFailed
            && actualEmpty.Errors.First().Message.Contains(expected)
            && actualWhiteSpace.IsFailed
            && actualWhiteSpace.Errors.First().Message.Contains(expected);
        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the factory method Create() does not allow the NumberOfRecomendations to be negative.
    /// </summary>
    [Fact(DisplayName = "Person's number of recomendations cannot be a negative number.")]
    public void Person_Number_Of_Recomendations_Cannot_Be_Negative() {
        // Arrange
        var actual = Person.Create(
            Constants.EntityGuid,
            1000,
            "First Name",
            "Last Name",
            "Software Developer",
            "Brazil",
            "Information Technology Services",
            -1,
            5
        );

        var expected = "The person's number of recomendations cannot be a negative number.";

        // Act
        var result =
            actual.IsFailed
            && actual.Errors.First().Message.Contains(expected);
        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the factory method Create() does not allow the NumberOfConnections to be negative.
    /// </summary>
    [Fact(DisplayName = "Person's number of connections cannot be a negative number.")]
    public void Person_Number_Of_Connections_Cannot_Be_Negative() {
        // Arrange
        var actual = Person.Create(
            Constants.EntityGuid,
            1000,
            "First Name",
            "Last Name",
            "Software Developer",
            "Brazil",
            "Information Technology Services",
            5,
            -1
        );

        var expected = "The person's number of connections cannot be a negative number.";

        // Act
        var result =
            actual.IsFailed
            && actual.Errors.First().Message.Contains(expected);
        // Assert
        result.Should().BeTrue();
    }

    [Fact(DisplayName = "Person is valid")]
    public void Person_Is_Valid() {
        // Arrange
        var actual = Person.Create(
            Constants.EntityGuid,
            1000,
            "First Name",
            "Last Name",
            "Software Developer",
            "Brazil",
            "Information Technology Services",
            5,
            50
        );
        var expected = actual.Value;

        // Act
        var result =
            expected is not null
            && actual.Value.Guid == Constants.EntityGuid
            && actual.Value.PersonId == 1000
            && actual.Value.FirstName == "First Name"
            && actual.Value.LastName == "Last Name"
            && actual.Value.CurrentRole == "Software Developer"
            && actual.Value.Country == "Brazil"
            && actual.Value.Industry == "Information Technology Services"
            && actual.Value.NumberOfRecommendations == 5
            && actual.Value.NumberOfConnections == 50;

        // Assert
        result.Should().BeTrue();
    }
    #endregion
}