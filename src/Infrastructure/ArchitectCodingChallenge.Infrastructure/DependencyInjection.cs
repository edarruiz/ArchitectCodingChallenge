using System.Reflection;
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

        return services;
    }
}