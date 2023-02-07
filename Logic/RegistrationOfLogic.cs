using GreenDonut;

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
        services.AddScoped<IPersonBatchDataLoader, PersonBatchDataLoader >();
        //services.AddSingleton<IBatchScheduler, AutoBatchScheduler>();
        services.AddSingleton<IBatchScheduler, ImmediateBatchScheduler>();
        return services;
    }
}
