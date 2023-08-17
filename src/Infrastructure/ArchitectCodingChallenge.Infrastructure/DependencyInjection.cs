using System.Reflection;
using ArchitectCodingChallenge.Infrastructure.Persistence.Abstractions;
using ArchitectCodingChallenge.Infrastructure.Persistence.InMemoryDatabase;
using ArchitectCodingChallenge.Infrastructure.Persistence.InMemoryDatabase.Abstractions;
using ArchitectCodingChallenge.Infrastructure.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ArchitectCodingChallenge.Infrastructure;

/// <summary>
/// Represents the class responsible for the dependency injection of the infrastructure layer services.
/// </summary>
public static class DependencyInjection {

    /// <summary>
    /// Adds the infrastructure layer services to the pipeline.
    /// </summary>
    /// <returns>Returns a <see cref="IServiceCollection"/> representing the collection of the infrastructure layer services.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) {
        Assembly assembly = typeof(AssemblyReference).Assembly;

        services.AddSingleton<IFileIOWrapper, FileIOWrapper>();

        // Adds the custom In-memory Json database to the pipeline, with its default behavior, which is:
        // 1 - If the json file already exists in the "data" folder existing inside the running application assembly folder, load this file.
        //     This already existing file could be changed by adding new people to the json file.
        // 2 - If the json file does not exists in the "data" folder inside the running application assembly folder,
        //     then loads the "people.json" from the application assembly and save it to the json file inside the "data" folder
        //      existing inside the running application assembly folder.
        // 3 - Load the people.json data to the In-memory database context to use it inside the API.
        services.AddSingleton<IJsonInMemoryDatabaseContext, JsonInMemoryDatabaseContext>(
            provider => new JsonInMemoryDatabaseContext(provider.GetRequiredService<IFileIOWrapper>())); // default behavior

        return services;
    }
}