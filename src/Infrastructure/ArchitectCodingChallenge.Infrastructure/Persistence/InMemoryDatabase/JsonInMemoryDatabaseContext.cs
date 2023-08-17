using System.Reflection;
using ArchitectCodingChallenge.Infrastructure.Persistence.InMemoryDatabase.Abstractions;

namespace ArchitectCodingChallenge.Infrastructure.Persistence.InMemoryDatabase;

/// <summary>
/// Represents the in-memory database context for the Json datasets available in the application.
/// </summary>
public class JsonInMemoryDatabaseContext : IJsonInMemoryDatabaseContext {
    #region Constants
    /// <summary>
    /// Represents the full qualified name of the file named "people.json" inside the assembly as an embedded resource.
    /// </summary>
    private const string S_DEFAULT_FULL_QUALIFIED_NAME_PEOPLE_JSON_FILE = "ArchitectCodingChallenge.Application.Data.people.json";
    #endregion

    #region Ctor
    /// <summary>
    /// Initializes a new instance of the class <see cref="JsonInMemoryDatabaseContext"/> with the
    /// full qualified name of the file named "people.json" inside the assembly as an embedded resource
    /// and with the <see cref="Assembly"/> containing this file.
    /// </summary>
    /// <param name="fullQualifiedNamePeopleJsonFile">The full qualified name of the dataset named "people.json", 
    /// as an embedded resource file inside an assembly.</param>
    /// <param name="resourceAssembly">The <see cref="Assembly"/> containing the file named "people.json" 
    /// as an embedded resource file.</param>
    private JsonInMemoryDatabaseContext(
        string fullQualifiedNamePeopleJsonFile,
        Assembly resourceAssembly
    ) {
        ResourceAssembly = (resourceAssembly is null)
            ? typeof(Application.AssemblyReference).Assembly
            : resourceAssembly;
        FullQualifiedNamePeopleJsonFile = (string.IsNullOrWhiteSpace(fullQualifiedNamePeopleJsonFile))
            ? S_DEFAULT_FULL_QUALIFIED_NAME_PEOPLE_JSON_FILE
            : fullQualifiedNamePeopleJsonFile;
    }
    #endregion

    #region IJsonInMemoryDatabaseContext
    /// <inheritdoc/>
    public Guid DatabaseContextId { get; init; } = Guid.NewGuid(); // This Guid exists just for connection trace and log purposes.

    /// <inheritdoc/>
    public DatabaseContextTarget DatabaseContextTarget { get; private set; } = DatabaseContextTarget.Unknown; // Here we can choose the target database system

    /// <inheritdoc/>
    public string? FullQualifiedNamePeopleJsonFile { get; }

    /// <inheritdoc/>
    public Assembly? ResourceAssembly { get; }

    /// <inheritdoc/>
    public bool Connected { get; private set; } = false;

    /// <inheritdoc/>
    public bool Connect() {
        Connected = PeopleFileExistsAsEmbeddedResource() && AssemblyExists();
        DatabaseContextTarget = Connected
            ? DatabaseContextTarget.InMemory
            : DatabaseContextTarget.Unknown;

        return Connected;
    }

    /// <inheritdoc/>
    public bool LoadPeopleDataSetFromAssembly() {
        Connect();
        if (Connected && (DatabaseContextTarget == DatabaseContextTarget.InMemory)) {
            using Stream? peopleFileStream = ResourceAssembly?.GetManifestResourceStream(FullQualifiedNamePeopleJsonFile ?? string.Empty);
            if (peopleFileStream is null) {
                return false;
            }

            using StreamReader peopleFileReader = new(peopleFileStream);
            if (peopleFileReader is null) {
                return false;
            }

            string peopleRawData = peopleFileReader.ReadToEnd();
        }

        return false;
    }

    /// <inheritdoc/>
    public bool LoadPeopleDataSetFromFile(string? filename) {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public bool PeopleFileExistsAsEmbeddedResource() => !(string.IsNullOrWhiteSpace(FullQualifiedNamePeopleJsonFile));

    /// <inheritdoc/>
    public bool AssemblyExists() => ResourceAssembly is not null;
    #endregion

    #region Methods
    /// <summary>
    /// Factory method that creates a a new instance of the class <see cref="JsonInMemoryDatabaseContext"/> with the
    /// full qualified name of the file named "people.json" inside the assembly as an embedded resource
    /// and with the <see cref="Assembly"/> containing this file.
    /// </summary>
    /// <param name="fullQualifiedNamePeopleDatasetJsonFile">The full qualified name of the dataset named "people.json", 
    /// as an embedded resource file inside an assembly.</param>
    /// <param name="assemblyResourceDataSet">The <see cref="Assembly"/> containing the file named "people.json" 
    /// as an embedded resource file.</param>
    /// <returns>Returns a new instance of the class <see cref="JsonInMemoryDatabaseContext"/> already
    /// configured for use if the embedded resource file exists inside the assembly; If the embedded 
    /// resource file doest not exists inside the assembly as an embedded resource, returns a new
    /// <see cref="JsonInMemoryDatabaseContext"/> pointing to the Application layer assembly, and use it
    /// as the default data source for the database context.</returns>
    public static JsonInMemoryDatabaseContext Create(
        string fullQualifiedNamePeopleDatasetJsonFile,
        Assembly assemblyResourceDataSet
    ) => new(fullQualifiedNamePeopleDatasetJsonFile, assemblyResourceDataSet);
    #endregion
}
