using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WordWave.Api.Application.Interfaces.Repository;
using WordWave.Infrastructure.Persistence.Context;
using WordWave.Infrastructure.Persistence.Repositories;

namespace WordWave.Infrastructure.Persistence.Extensions;
public static class Registration
{
    public static IServiceCollection AddApplicationRegistration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WordWaveContext>(conf => 
        {
            var connStr = configuration["WordWaveContextConnectionStrings"].ToString();
            conf.UseSqlServer(connStr, opt => 
            {
                opt.EnableRetryOnFailure();
            });
        });

        var seedData = new SeedData();
        seedData.SeedAsync(configuration).GetAwaiter().GetResult();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEmailConfirmationRepository, EmailConfirmationRepository>();
        services.AddScoped<IEntryRepository, EntryRepository>();
        services.AddScoped<IEntryCommentRepository, EntryCommentRepository>();

        return services;
    }
    
}

   // AddApplicationRegistration
    
     //   AddInfrastructureRegistration