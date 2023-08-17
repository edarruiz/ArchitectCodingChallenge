using System.Reflection;
using System.Text;
using ArchitectCodingChallenge.Domain.Models;
using ArchitectCodingChallenge.Infrastructure.Persistence.InMemoryDatabase.Abstractions;
using Newtonsoft.Json;

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

    /// <summary>
    /// Represents the dataset Json file name where the people information is stored.
    /// </summary>
    private const string S_DEFAULT_PEOPLE_JSON_FILENAME = "people.json";
    #endregion

    #region Ctor
    /// <summary>
    /// Initializes a new instance of the class <see cref="JsonInMemoryDatabaseContext"/>.
    /// </summary>
    private JsonInMemoryDatabaseContext() {
        if (!string.IsNullOrWhiteSpace(DataDirectory) && !Directory.Exists(DataDirectory)) {
            Directory.CreateDirectory(DataDirectory);
        }
        ResourceAssembly = typeof(Application.AssemblyReference).Assembly;
        FullQualifiedNamePeopleJsonFile = S_DEFAULT_FULL_QUALIFIED_NAME_PEOPLE_JSON_FILE;
    }
    #endregion

    #region IJsonInMemoryDatabaseContext
    /// <inheritdoc/>
    public Guid DatabaseContextId { get; init; } = Guid.NewGuid(); // This Guid exists just for connection trace and log purposes.

    /// <inheritdoc/>
    public DatabaseContextTarget DatabaseContextTarget { get; private set; } = DatabaseContextTarget.InMemory; // Here we can choose the target database system

    /// <inheritdoc/>
    public string? DataDirectory => $"{Path.GetDirectoryName(typeof(AssemblyReference).Assembly.Location)}/data";

    /// <inheritdoc/>
    public string? PeopleJsonFilename => $"{DataDirectory}/{S_DEFAULT_PEOPLE_JSON_FILENAME}";

    /// <inheritdoc/>
    public string? FullQualifiedNamePeopleJsonFile { get; init; }

    /// <inheritdoc/>
    public Assembly? ResourceAssembly { get; init; }

    /// <inheritdoc/>
    public List<PersonModel>? People { get; private set; } = new List<PersonModel>();

    /// <inheritdoc/>
    public bool Connected { get; private set; } = false;

    /// <inheritdoc/>
    public bool Connect() {
        Connected = false;
        if (!LoadPeopleDataSetFromFile(PeopleJsonFilename)) {
            if (PeopleFileExistsAsEmbeddedResource() && AssemblyExists()) {
                Connected = LoadPeopleDataSetFromAssembly() && LoadPeopleDataSetFromFile(PeopleJsonFilename);
            }
        } else {
            Connected = true;
        }

        return Connected;
    }

    /// <inheritdoc/>
    public bool LoadPeopleDataSetFromAssembly() {
        if (DatabaseContextTarget != DatabaseContextTarget.InMemory) {
            return false;
        }
        using Stream? peopleStream = ResourceAssembly?.GetManifestResourceStream(FullQualifiedNamePeopleJsonFile ?? string.Empty);
        if (peopleStream is null) {
            return false;
        }
        using StreamReader peopleReader = new(peopleStream);
        if (peopleReader is null) {
            return false;
        }

        string peopleRawData = peopleReader.ReadToEnd();
        if (!string.IsNullOrWhiteSpace(PeopleJsonFilename) && !File.Exists(PeopleJsonFilename)) {
            File.AppendAllText(PeopleJsonFilename, peopleRawData, Encoding.UTF8);
        }

        return true;
    }

    /// <inheritdoc/>
    public bool LoadPeopleDataSetFromFile(string? filename) {
        if (string.IsNullOrWhiteSpace(filename) || !File.Exists(filename) || DatabaseContextTarget != DatabaseContextTarget.InMemory) {
            return false;
        }

        try {
            using StreamReader peopleReader = File.OpenText(filename);
            string peopleRawData = peopleReader.ReadToEnd();
            if (!string.IsNullOrWhiteSpace(peopleRawData)) {
                People = JsonConvert.DeserializeObject<List<PersonModel>?>(peopleRawData);
            }
        } catch (Exception ex) {
            // TODO: log the exception
            return false;
        }

        return true;
    }

    /// <inheritdoc/>
    public bool PeopleFileExistsAsEmbeddedResource() => !(string.IsNullOrWhiteSpace(FullQualifiedNamePeopleJsonFile));

    /// <inheritdoc/>
    public bool AssemblyExists() => ResourceAssembly is not null;

    /// <inheritdoc/>
    public async Task<List<PersonModel>?> GetPeople() => People;
    #endregion

    #region Methods
    /// <summary>
    /// Factory method that creates a a new instance of the class <see cref="JsonInMemoryDatabaseContext"/>.
    /// </summary>
    /// <returns>Returns a new instance of the class <see cref="JsonInMemoryDatabaseContext"/> already configured for use.</returns>
    public static JsonInMemoryDatabaseContext Create() => new();
    #endregion
}
