using ArchitectCodingChallenge.Domain.Entities;

namespace ArchitectCodingChallenge.Domain.Abstractions;

/// <summary>
/// Represents the repository of <see cref="Person"/>.
/// </summary>
public interface IPeopleRepository {
    /// <summary>
    /// Gets an <see cref="ICollection{T}"/> of <see cref="Person"/>
    /// representing the people with the highest chance of becoming a company client.
    /// </summary>
    /// <param name="maxIndex">Maximum number of results that should be returned,
    /// ordered from the highest priority to the lowest priority.</param>
    /// <returns>Returns an <see cref="ICollection{T}"/> of <see cref="Person"/>
    /// representing the people with the highest chance of becoming a company client, 
    /// defining the maximum number of <see cref="Person"/>'s returned,
    /// ordered from the highest priority to the lowest priority.</returns>
    Task<ICollection<Person>> GetTopClients(int maxIndex);

    /// <summary>
    /// Gets a <see cref="Person"/> identified by the unique identifier number,
    /// related to the position on the priority list.
    /// </summary>
    /// <param name="personId">The person's unique identifier number.</param>
    /// <returns>Returns a <see cref="Person"/> identified by their unique identifier 
    /// number, if it exists in the people's list; <c>null</c> if the unique identifier
    /// number was not found in the people's list.</returns>
    Task<int> GetClientPosition(long personId);

    /// <summary>
    /// Adds a new <see cref="Person"/> to the people's list and save the changes
    /// to the "people.json" dataset file.
    /// </summary>
    /// <param name="newPerson">The <see cref="Person"/> to be added in the list.</param>
    /// <returns>Returns the newly created <see cref="Person"/>; <c>null</c> if any
    /// if any error occurred during the addition of the <see cref="Person"/> 
    /// in the list.</returns>
    Task<Person?> AddPerson(Person? newPerson);

    /// <summary>
    /// Save to file the changes made on the People's list.
    /// </summary>
    /// <returns>Returns <c>true</c> when the changes are saved successfully to the file named "people.json".
    void SaveChanges();
}
