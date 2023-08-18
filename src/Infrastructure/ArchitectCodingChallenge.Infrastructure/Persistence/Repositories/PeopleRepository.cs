using ArchitectCodingChallenge.Domain.Abstractions;
using ArchitectCodingChallenge.Domain.Entities;
using ArchitectCodingChallenge.Domain.Models;
using ArchitectCodingChallenge.Infrastructure.Persistence.InMemoryDatabase.Abstractions;
using Microsoft.Extensions.Logging;
using Serilog;

namespace ArchitectCodingChallenge.Infrastructure.Persistence.Repositories;

public sealed class PeopleRepository : IPeopleRepository {
    #region Fields
    private ILogger<PeopleRepository> _logger;
    private IJsonInMemoryDatabaseContext _dbContext;
    #endregion

    #region Ctor
    /// <summary>
    /// Initializes a new instance of the class <see cref="PeopleRepository"/>.
    /// </summary>
    /// <param name="dbContext"><see cref="IJsonInMemoryDatabaseContext"/> representing the database
    /// context service for handling data operations.</param>
    /// <exception cref="ArgumentNullException">When <paramref name="dbContext"/> is <c>null</c>.</exception>
    public PeopleRepository(
        ILogger<PeopleRepository> logger,
        IJsonInMemoryDatabaseContext dbContext
    ) {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _dbContext.Connect();
    }
    #endregion

    #region IPeopleRepository
    /// <inheritdoc/>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<Person?> AddPerson(Person? newPerson) {
        ArgumentNullException.ThrowIfNull(nameof(newPerson));

        if (_dbContext.RawPeopleDataSet.Any(p => p.PersonId == newPerson!.PersonId)) {
            return null;
        }

        _dbContext.RawPeopleDataSet.Add(new PersonModel {
            PersonId = newPerson!.PersonId!.Value,
            FirstName = newPerson!.FirstName!,
            LastName = newPerson!.LastName!,
            CurrentRole = newPerson!.CurrentRole!,
            Country = newPerson!.Country!,
            Industry = newPerson!.Industry!,
            NumberOfRecommendations = newPerson!.NumberOfRecommendations!,
            NumberOfConnections = newPerson!.NumberOfConnections!
        });

        SaveChanges();

        return newPerson;
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

    /// <inheritdoc/>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<ICollection<Person>> GetTopClients(int n) {
        var result = new List<Person>();
        var resultSet = _dbContext.RawPeopleDataSet
            .Where(p => p.CurrentRole.Contains("developer"))
            .Take(n)
            .OrderBy(p => p.PersonId);


        foreach (var person in resultSet) {
            var p = Person.Create(
                Guid.NewGuid(),
                person.PersonId,
                person.FirstName,
                person.LastName,
                person.CurrentRole,
                person.Country,
                person.Industry,
                person.NumberOfRecommendations,
                person.NumberOfConnections
            );
            if (p.IsFailed) {
                foreach (var error in p.Errors) {
                    Log.Error($"{error.Message}\n\r\n\r");
                    foreach (var reason in error.Reasons) {
                        Log.Error($"Reason: {reason.Message}");
                    }
                }
            } else {
                result.Add(p.Value);
            }
        }

        return result;
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

    /// <inheritdoc/>
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<int> GetClientPosition(long personId) {
        return _dbContext.RawPeopleDataSet
            .Where(p => p.CurrentRole.Contains("developer"))
            .OrderBy(p => p.PersonId)
            .ToList()
            .FindIndex(p => p.PersonId == personId) + 1;
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

    /// <inheritdoc/>
    public void SaveChanges() => _dbContext.SaveChanges();
    #endregion
}
