﻿using BigDinner.Application.Common.Abstractions.Repository;
using BigDinner.Domain.Models.Customers;
using BigDinner.Domain.Models.Menus;
using BigDinner.Persistence.BackgroundJobs;
using BigDinner.Persistence.Interceptors;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace BigDinner.Persistence;

public static class PersistenceModuleDependencies
{
    public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IUserRepo, UserRepo>();

        services.AddScoped<IMenuRepository, MenuRepository>();

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        services.AddQuartz(configure =>
        {
            var jobKey = new JobKey(nameof(OutboxMessageBackgroundJob));

            configure
                .AddJob<OutboxMessageBackgroundJob>(jobKey)
                .AddTrigger(
                   trigger =>
                       trigger.ForJob(jobKey)
                           .WithSimpleSchedule(
                                schedule =>
                                   schedule.WithIntervalInSeconds(10)
                                       .RepeatForever()));

            configure.UseMicrosoftDependencyInjectionJobFactory();
        });

        services.AddQuartzHostedService();

        return services;
    }
}

