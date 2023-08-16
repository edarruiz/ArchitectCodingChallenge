using ArchitectCodingChallenge.Domain.Errors;
using ArchitectCodingChallenge.Domain.Primitives;
using FluentResults;

namespace ArchitectCodingChallenge.Domain.Entities;

/// <summary>
/// Represents a person, a talent that could be potentially hired by the company. 
/// </summary>
public sealed class Person : Entity {
    #region Ctor
    /// <summary>
    /// Initializes a new instance of the class <see cref="Person"/>, defining its <see cref="Guid"/>.
    /// </summary>
    /// <param name="guid">Global unique identifier of the entity. When <c>null</c>, will be assigned automatically.</param>
    /// <remarks>
    /// The constructor should be private to prevent the creation of a new instance of <see cref="Person"/> ouside the 
    /// factory method, where all the object validations are made.
    /// </remarks>
    private Person(Guid? guid) : base(guid) {
    }
    #endregion

    #region Properties
    /// <summary>
    /// Gets or sets the person's unique identifier number.
    /// </summary>
    public long? PersonId { get; set; } = null;

    /// <summary>
    /// Gets or sets the person's first name.
    /// </summary>
    public string? FirstName { get; set; } = null;

    /// <summary>
    /// Gets or sets the person's last name.
    /// </summary>
    public string? LastName { get; set; } = null;

    /// <summary>
    /// Gets or sets the person's current role.
    /// </summary>
    public string? CurrentRole { get; set; } = null;

    /// <summary>
    /// Gets or sets the person's country origin.
    /// </summary>
    public string? Country { get; set; } = null;

    /// <summary>
    /// Gets or sets the person's industry.
    /// </summary>
    public string? Industry { get; set; } = null;

    /// <summary>
    /// Gets or sets the person's number of LinkedIn recomendations.
    /// </summary>
    public int? NumberOfRecommendations { get; set; } = null;

    /// <summary>
    /// Gets or sets the person's number of LinkedIn connections.
    /// </summary>
    public int? NumberOfConnections { get; set; } = null;
    #endregion

    #region Methods
    /// <summary>
    /// Factory method that creates a new instance of the class <see cref="Person"/>.
    /// </summary>
    /// <param name="guid">Person's global unique identifier.</param>
    /// <param name="id">Person's unique identifier number.</param>
    /// <param name="firstName">Person's first name.</param>
    /// <param name="lastName">Person's last name.</param>
    /// <param name="currentRole">Person's current role.</param>
    /// <param name="country">Person's country origin.</param>
    /// <param name="industry">Person's industry.</param>
    /// <param name="numberOfRecomendations">Person's number of LinkedIn recomendations.</param>
    /// <param name="numberOfConnections">Person's number of LinkedIn connections.</param>
    /// <returns></returns>
    public static Result<Person> Create(
        Guid? guid,
        long? id,
        string? firstName,
        string? lastName,
        string? currentRole,
        string? country,
        string? industry,
        int? numberOfRecomendations,
        int? numberOfConnections
    ) {
        // Pre-validation 
        if (id is null) {
            return Result.Fail<Person>(DomainErrors.Person.IdIsNull);
        }
        if (string.IsNullOrWhiteSpace(firstName)) {
            return Result.Fail<Person>(DomainErrors.Person.NameIsNullEmptyOrWhiteSpace);
        }
        if (string.IsNullOrWhiteSpace(lastName)) {
            return Result.Fail<Person>(DomainErrors.Person.LastNameIsNullEmptyOrWhiteSpace);
        }
        if (string.IsNullOrWhiteSpace(currentRole)) {
            return Result.Fail<Person>(DomainErrors.Person.CurrentRoleIsNullEmptyOrWhiteSpace);
        }
        if (string.IsNullOrWhiteSpace(country)) {
            return Result.Fail<Person>(DomainErrors.Person.CountryIsNullEmptyOrWhiteSpace);
        }
        if (string.IsNullOrWhiteSpace(industry)) {
            return Result.Fail<Person>(DomainErrors.Person.IndustryIsNullEmptyOrWhiteSpace);
        }

        Person person = new(guid) {
            PersonId = id,
            FirstName = firstName,
            LastName = lastName,
            CurrentRole = currentRole,
            Country = country,
            Industry = industry,
            NumberOfConnections = numberOfConnections,
            NumberOfRecommendations = numberOfRecomendations
        };

        return Result.Ok(person);
    }
    #endregion
}

