using FluentAssertions;

namespace ArchitectCodingChallenge.Tests.DomainTests.Primitives;

/// <summary>
/// Represents the tests for the <see cref="Domain.Primitives.Entity"/> class.
/// </summary>
public class EntityTests : IDisposable {
    #region Ctor and Dtor
    public EntityTests() {
        // Startup
    }

    public void Dispose() {
        // Cleanup
    }
    #endregion

    #region Tests
    /// <summary>
    /// Test if the constructor auto-generate a guid value if not provided in the parameters.
    /// </summary>
    [Fact(DisplayName = "Ctor should auto-generate a Guid if not provided")]
    public void The_Constructor_Should_AutoGenerate_a_Guid_If_Not_Provided() {
        // Arrange
        var actual = new DummyEntityLeft();
        var expected = actual.Guid;

        // Act
        var result = !string.IsNullOrWhiteSpace(expected.ToString());

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Test if the Equals(Entity) method return true when comparing two equal entities, 
    /// which are considered equal when their Guid has the same value.
    /// </summary>
    [Fact(DisplayName = "Equals(Entity) should return true when comparing two equal entities")]
    public void EqualsEntity_Should_Return_True_When_Comparing_Two_Equal_Entities() {
        // Arrange
        DummyEntityLeft actual = new(Constants.EntityGuid);
        DummyEntityRight expected = new(Constants.EntityGuid);

        // Act
        var result = actual.Equals(expected);

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Test if the Equals(object) method return true when comparing two equal entities, 
    /// which are considered equal when their Guid has the same value.
    /// </summary>
    [Fact(DisplayName = "Equals(object) should return true when comparing two equal entities")]
    public void EqualsObject_Should_Return_True_When_Comparing_Two_Equal_Entities() {
        // Arrange
        object actual = new DummyEntityLeft(Constants.EntityGuid);
        object expected = new DummyEntityRight(Constants.EntityGuid);

        // Act
        var result = actual.Equals(expected);

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Test if the == operator return true when comparing two equal entities, 
    /// which are considered equal when their Guid has the same value.
    /// </summary>
    [Fact(DisplayName = "== operator should return true when comparing two equal entities")]
    public void Equality_Operator_Should_Return_True_When_Comparing_Two_Equal_Entities() {
        // Arrange
        var actual = new DummyEntityLeft(Constants.EntityGuid);
        var expected = new DummyEntityRight(Constants.EntityGuid);

        // Act
        var result = actual == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Test if the Equals(Entity) method return false when comparing two distinct entities, 
    /// which are considered equal when their Guid has the same value.
    /// </summary>
    [Fact(DisplayName = "Equals(Entity) should return false when comparing two distinct entities")]
    public void Equals_Should_Return_False_When_Comparing_Two_Distinct_Entities() {
        // Arrange
        DummyEntityLeft actual = new();
        DummyEntityRight expected = new();

        // Act
        var result = actual.Equals(expected);

        // Assert
        result.Should().BeFalse();
    }

    /// <summary>
    /// Test if the Equals(object) method return false when comparing two distinct entities, 
    /// which are considered equal when their Guid has the same value.
    /// </summary>
    [Fact(DisplayName = "Equals(object) should return false when comparing two distinct entities")]
    public void EqualsObject_Should_Return_False_When_Comparing_Two_Distinct_Entities() {
        // Arrange
        object actual = new DummyEntityLeft();
        object expected = new DummyEntityRight();

        // Act
        var result = actual.Equals(expected);

        // Assert
        result.Should().BeFalse();
    }

    /// <summary>
    /// Test if the != operator return false when comparing two distinct entities, 
    /// which are considered equal when their Guid has the same value.
    /// </summary>
    [Fact(DisplayName = "!= should return false when comparing two distinct entities")]
    public void Inequality_Operator_Should_Return_False_When_Comparing_Two_Distinct_Entities() {
        // Arrange
        var actual = new DummyEntityLeft();
        var expected = new DummyEntityRight();

        // Act
        var result = actual != expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Test if GetHashCode() return the same values for two equal entities,
    /// which are considered equal when their Guid has the same value.
    /// </summary>
    [Fact(DisplayName = "GetHashCode() should return the same value for two equal entities")]
    public void GetHashCode_Should_Return_Same_Value_For_Two_Equal_Entities() {
        // Arrange
        var actual = new DummyEntityLeft(Constants.EntityGuid);
        var expected = new DummyEntityRight(Constants.EntityGuid);

        // Act
        var result = actual.GetHashCode() == expected.GetHashCode();

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Test if GetHashCode() return the same values for two equal entities,
    /// which are considered equal when their Guid has the same value.
    /// </summary>
    [Fact(DisplayName = "GetHashCode() should not return the same value for two distinct entities")]
    public void GetHashCode_Should_Not_Return_Same_Value_For_Two_Distinct_Entities() {
        // Arrange
        var actual = new DummyEntityLeft();
        var expected = new DummyEntityRight();

        // Act
        var result = actual.GetHashCode() != expected.GetHashCode();

        // Assert
        result.Should().BeTrue();
    }

    #endregion
}