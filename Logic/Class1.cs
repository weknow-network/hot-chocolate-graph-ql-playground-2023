using Microsoft.Extensions.DependencyInjection;

namespace Weknow.HotChocolatePlayground;

/// <summary>
/// Logic registration
/// </summary>
public static class RegistrationOfLogic
{
    /// <summary>
    /// Logic registration
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection RegisterLogic(
        this IServiceCollection services)
    {
        services.AddSingleton<IPersonRepository, PersonRepository>();
        return services;
    }
}
