using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace ArchitectCodingChallenge.Application;

/// <summary>
/// Represents the class responsible for the dependency injection of the application layer services.
/// </summary>
public static class DependencyInjection {

    /// <summary>
    /// Adds the application layer services to the pipeline.
    /// </summary>
    /// <returns>Returns a <see cref="IServiceCollection"/> representing the collection of the application layer services.</returns>
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        Assembly assembly = typeof(AssemblyReference).Assembly;

        // Add MediatR Services
        services.AddMediatR(configuration => configuration.RegisterServicesFromAssembly(assembly));

        // Add Fluent Validation Services
        services.AddValidatorsFromAssembly(assembly);

        return services;
    }
}