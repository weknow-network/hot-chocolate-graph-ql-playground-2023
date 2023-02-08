﻿using GreenDonut;

using HotChocolate.Execution.Configuration;

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
        this IServiceCollection services, IRequestExecutorBuilder builder)
    {
        services.AddSingleton<IBookRepository, BookRepository>();
        services.AddSingleton<IPersonRepository, PersonRepository>();
        //services.AddScoped<BookByRankDataloader>();
        //services.AddScoped<IPersonBatchDataLoader, PersonBatchDataLoader >();
        //services.AddSingleton<IBatchScheduler, AutoBatchScheduler>();
        services.AddSingleton<IBatchScheduler, ImmediateBatchScheduler>();

        //builder.AddQueryType<Query>();

        // builder.AddDataLoader<PersonBatchDataLoader>()
        //.AddDataLoader<BookByRankDataloader>();

        // register all type extensions within the assembly
        builder.AddLogicTypes() // source generated by HotChocolate.Types.Analyzers
               .AddQueryType()
               //.AddMutationType()
               .AddMutationConventions()
               //.AddSubscriptionType()
               .AddFiltering()
               .AddSorting()
               .AddProjections()
               ;

        return services;
    }
}
