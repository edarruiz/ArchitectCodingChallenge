using System.Reflection;
using ArchitectCodingChallenge.Domain.Models;
using ArchitectCodingChallenge.Infrastructure.Persistence.Abstractions;

namespace ArchitectCodingChallenge.Infrastructure.Persistence.InMemoryDatabase.Abstractions;

/// <summary>
/// Represents the interface for the implementation of a JSON In-memory database context.
/// </summary>
/// <remarks>
/// The .json file used in this implementation is located in the Application layer, as an embedded resource file.
/// </remarks>
public interface IJsonInMemoryDatabaseContext : IPersistenceDatabaseContext {
    #region Properties
    /// <summary>
    /// Gets the data directory where the json files should exist or should be created.
    /// </summary>
    /// <remarks>This directory is automatically created if not exists, and its location root is the same of the current running assembly.
    /// </remarks>
    string? DataDirectory { get; }

    /// <summary>
    /// Gets the full path and file name of the dataset Json file name where the people information is stored.
    /// </summary>
    string? PeopleJsonFilename { get; }

    /// <summary>
    /// Gets or sets the full qualified name of the dataset named "people.json", as an embedded resource file inside an assembly.
    /// </summary>
    string? FullQualifiedNamePeopleJsonFile { get; }

    /// <summary>
    /// Gets or sets the <see cref="Assembly"/> containing the file named "people.json" as an embedded resource file.
    /// </summary>
    Assembly? ResourceAssembly { get; }

    /// <summary>
    /// Gets the <see cref="List{T}"/> representing the collection of <see cref="PersonModel"/>.
    /// </summary>
    List<PersonModel>? People { get; }

    /// <summary>
    /// Checks if the In-memory database is ready and available for operations.
    /// </summary>
    bool Connected { get; }
    #endregion

    #region Methods
    /// <summary>
    /// Connect to the assembly containing the file named "people.json" and try to load all the file data.
    /// </summary>
    /// <returns>Returns <c>true</c> when the containing assembly is available, the file name "people.json"
    /// exists in this assembly as an embedded resource and the contents of the file was loaded sucessfully;
    /// <c>false</c> otherwise.</returns>
    bool Connect();

    /// <summary>
    /// Loads from the assembly the "people.json" file contents in memory, if it exists.
    /// </summary>
    /// <returns>Returns <c>true</c> when the "people.json" file contents was loaded sucessfully in-memory; <c>false</c> otherwise.</returns>
    bool LoadPeopleDataSetFromAssembly();

    /// <summary>
    /// Loads from disk the "people.json" file, if it exists.
    /// </summary>
    /// <param name="filename">Path and filename where the "people.json" dataset is located.</param>
    /// <returns>Returns <c>true</c> when the "people.json" file contents was loaded sucessfully in-memory; <c>false</c> otherwise.</returns>
    bool LoadPeopleDataSetFromFile(string? filename);

    Task<List<PersonModel>?> GetPeople();
    #endregion
}
