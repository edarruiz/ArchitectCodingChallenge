using System.Text;
using ArchitectCodingChallenge.Infrastructure.Persistence;
using ArchitectCodingChallenge.Infrastructure.Persistence.Abstractions;
using ArchitectCodingChallenge.Infrastructure.Persistence.InMemoryDatabase;
using FluentAssertions;
using Moq;

namespace ArchitectCodingChallenge.Tests.InfrastructureTests.Persistence.InMemoryDatabase;

/// <summary>
/// Represents the tests for the <see cref="JsonInMemoryDatabaseContext"/> class.
/// </summary>
public class JsonInMemoryDatabaseContextTests : IDisposable {
    #region Ctor and Dtor
    public JsonInMemoryDatabaseContextTests() {
        // Startup
    }

    public void Dispose() {
        // Cleanup
    }
    #endregion

    #region Fields
    private const string json = """
        [
        {
          "PersonId": 573435640,
          "FirstName": "jean",
          "LastName": "harion",
          "CurrentRole": "vice president",
          "Country": "Dominica",
          "Industry": "Telecommunications",
          "NumberOfRecommendations": 0,
          "NumberOfConnections": 0
        },
        {
          "PersonId": 337983069,
          "FirstName": "meredith",
          "LastName": "kopit-levien",
          "CurrentRole": "chief revenue officer",
          "Country": "United States",
          "Industry": "Publishing",
          "NumberOfRecommendations": 0,
          "NumberOfConnections": 0
        },
        {
          "PersonId": 556570894,
          "FirstName": "daniel",
          "LastName": "roe",
          "CurrentRole": "vp, customer operations and support",
          "Country": "United States",
          "Industry": "Computer Software",
          "NumberOfRecommendations": 0,
          "NumberOfConnections": 270
        }
        ]
        """;
    #endregion

    #region Tests
    /// <summary>
    /// Check if the <see cref="JsonInMemoryDatabaseContext.FullQualifiedNamePeopleJsonFile"/> 
    /// has the value 'ArchitectCodingChallenge.Application.Data.people.json'.
    /// </summary>
    [Fact(DisplayName = "FullQualifiedNamePeopleJsonFile value should be 'ArchitectCodingChallenge.Application.Data.people.json'.")]
    public void FullQualifiedNamePeopleJsonFile_Value_Should_Be_Specific_Value() {
        // Arrange
        var mockFS = new Mock<IFileSystem>();
        var actual = new JsonInMemoryDatabaseContext(mockFS.Object);
        var expected = "ArchitectCodingChallenge.Application.Data.people.json";

        // Act
        var result = actual.FullQualifiedNamePeopleJsonFile == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if <see cref="JsonInMemoryDatabaseContext.PeopleJsonFilename"/>
    /// has the value of 'people.json'.
    /// </summary>
    [Fact(DisplayName = "PeopleJsonFilename value should be 'people.json'")]
    public void PeopleJsonFilename_Value_Should_Be_Specific_Value() {
        // Arrange
        var mockFS = new Mock<IFileSystem>();
        var actual = new JsonInMemoryDatabaseContext(mockFS.Object);
        var expected = "people.json";

        // Act
        var result = Path.GetFileName(actual.PeopleJsonFilename) == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the constructor of <see cref="JsonInMemoryDatabaseContext"/> throw an exception
    /// when the <see cref="IFileSystem"/> parameter is null.
    /// </summary>
    [Fact(DisplayName = "Null IFileIOWrapper service injection should throw exception")]
    public void Null_IFileIOWrapper_Service_Injection_Should_Throw_Exception() {
        // Arrange
        var actual = () => new JsonInMemoryDatabaseContext(null);

        // Act

        // Assert
        actual
            .Should()
            .Throw<ArgumentNullException>()
            .And
            .ParamName
            .Should()
            .Be("fileSystem");
    }

    /// <summary>
    /// Check if the <see cref="JsonInMemoryDatabaseContext.DatabaseContextId"/> has a value.
    /// </summary>
    [Fact(DisplayName = "DatabaseContextId should have a value")]
    public void DatabaseContextId_Should_Have_A_Value() {
        // Arrange
        var mockFS = new Mock<IFileSystem>();
        var actual = new JsonInMemoryDatabaseContext(mockFS.Object);
        var expected = actual.DatabaseContextId.ToString();

        // Act
        var result = string.IsNullOrWhiteSpace(expected);

        // Assert
        result.Should().BeFalse();
    }

    /// <summary>
    /// Check if <see cref="JsonInMemoryDatabaseContext.DatabaseContextTarget"/> is 
    /// <see cref="DatabaseContextTarget.InMemory"/>.
    /// </summary>
    [Fact(DisplayName = "DatabaseContextTarget should be DatabaseContextTarget.InMemory")]
    public void DatabaseContextTarget_Should_Be_DatabaseContextTargetInMemory() {
        // Arrange
        var mockFS = new Mock<IFileSystem>();
        var actual = new JsonInMemoryDatabaseContext(mockFS.Object);
        var expected = actual.DatabaseContextTarget;

        // Act
        var result = expected == DatabaseContextTarget.InMemory;

        // Assert
        result.Should().BeTrue();
    }

    [Fact(DisplayName = "DataDirectory should be 'data'")]
    public void DataDirectory_Value_Should_Be_Data() {
        // Arrange
        var mockFS = new Mock<IFileSystem>();
        mockFS.Setup(m => m.GetDirectoryName(It.IsAny<string>())).Returns(@"c:\app");
        var actual = new JsonInMemoryDatabaseContext(mockFS.Object);
        var expected = "data";

        // Act
        var result = Path.GetFileName(actual.DataDirectory)?.TrimEnd(Path.DirectorySeparatorChar) == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if the <see cref="JsonInMemoryDatabaseContext.ResourceAssembly"/> is not null.
    /// </summary>
    [Fact(DisplayName = "ResourceAssembly should not be null")]
    public void ResourceAssembly_Should_Not_Be_Null() {
        // Arrange
        var mockFS = new Mock<IFileSystem>();
        var actual = new JsonInMemoryDatabaseContext(mockFS.Object);
        var expected = "ArchitectCodingChallenge.Application";

        // Act
        var result = expected is not null && actual.ResourceAssembly?.GetName().Name == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if <see cref="JsonInMemoryDatabaseContext.Connected"/> should be true
    /// when sucessfully loading the json file.
    /// </summary>
    [Fact(DisplayName = "Connected should be true when loading json from system file")]
    public void Connected_Should_Be_True() {
        // Arrange
        var mockFS = new Mock<IFileSystem>();
        mockFS.Setup(m => m.GetDirectoryName(It.IsAny<string>())).Returns(@"c:\app");
        mockFS.Setup(m => m.FileExists(It.IsAny<string>())).Returns(true);
        mockFS.Setup(m => m.OpenText(It.IsAny<string>())).Returns(new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(json)), Encoding.UTF8, true));
        var actual = new JsonInMemoryDatabaseContext(mockFS.Object);
        var expected = true;

        // Act
        var result = actual.Connect() == expected && actual.Connected == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if <see cref="JsonInMemoryDatabaseContext.Connected"/> should be false
    /// when failed loading the json file.
    /// </summary>
    [Fact(DisplayName = "Connected should be false when loading json from system file")]
    public void Connected_Should_Be_False() {
        // Arrange
        var mockFS = new Mock<IFileSystem>();
        mockFS.Setup(m => m.GetDirectoryName(It.IsAny<string>())).Returns(@"c:\app");
        mockFS.Setup(m => m.FileExists(It.IsAny<string>())).Returns(true);
        mockFS.Setup(m => m.OpenText(It.IsAny<string>())).Returns((StreamReader)null!);
        var actual = new JsonInMemoryDatabaseContext(mockFS.Object);
        var expected = true;

        // Act
        var result = actual.Connect() == expected && actual.Connected == expected;

        // Assert
        result.Should().BeFalse();
    }

    /// <summary>
    /// Check if <see cref="JsonInMemoryDatabaseContext.LoadPeopleDataSetFromAssembly"/>
    /// sucessfully loads the json file as embedded resource from the application layer.
    /// </summary>
    [Fact(DisplayName = "LoadPeopleDataSetFromAssembly should be true when sucessfully save the json file to disk")]
    public void LoadPeopleDataSetFromAssembly_Should_Be_True() {
        // Arrange
        var mockFS = new Mock<IFileSystem>();
        var actual = new JsonInMemoryDatabaseContext(mockFS.Object);
        var expected = true;

        // Act
        var result = actual.LoadPeopleDataSetFromAssembly() == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if <see cref="JsonInMemoryDatabaseContext.LoadPeopleDataSetFromFile"/> should be true
    /// when sucessfully loading the json file.
    /// </summary>
    [Fact(DisplayName = "LoadPeopleDataSetFromFile should be true when loading json from system file")]
    public void LoadPeopleDataSetFromFile_Should_Be_True() {
        // Arrange
        var mockFS = new Mock<IFileSystem>();
        mockFS.Setup(m => m.FileExists(It.IsAny<string>())).Returns(true);
        mockFS.Setup(m => m.OpenText(It.IsAny<string>())).Returns(new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(json)), Encoding.UTF8, true));
        var actual = new JsonInMemoryDatabaseContext(mockFS.Object);
        var expected = true;

        // Act
        var result = actual.LoadPeopleDataSetFromFile(@"c:\app\file.json") == expected;

        // Assert
        result.Should().BeTrue();
    }

    /// <summary>
    /// Check if <see cref="JsonInMemoryDatabaseContext.LoadPeopleDataSetFromFile"/> should be false
    /// when failing loading the json file.
    /// </summary>
    [Fact(DisplayName = "LoadPeopleDataSetFromFile should be false when not loading json from system file")]
    public void LoadPeopleDataSetFromFile_Should_Be_False() {
        // Arrange
        var mockFS = new Mock<IFileSystem>();
        mockFS.Setup(m => m.FileExists(It.IsAny<string>())).Returns(false);
        mockFS.Setup(m => m.OpenText(It.IsAny<string>())).Returns(new StreamReader(new MemoryStream(Encoding.UTF8.GetBytes(json)), Encoding.UTF8, true));
        var actual = new JsonInMemoryDatabaseContext(mockFS.Object);
        var expected = false;

        // Act
        var result = actual.LoadPeopleDataSetFromFile(@"c:\app\file.json") == expected;

        // Assert
        result.Should().BeTrue();
    }
    #endregion
}