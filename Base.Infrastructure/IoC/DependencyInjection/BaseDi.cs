using Base.Aplication.Services;
using Base.Aplication.Services.Authentication;
using Base.Aplication.Services.Formalities;
using Base.Domain.Repositories;
using Base.Domain.Repositories.Authentication;
using Base.Domain.Repositories.Formalities;
using Base.Infrastructure.Database.EntityFramework.Context;
using Base.Infrastructure.Database.EntityFramework.Repositories;
using Base.Infrastructure.Database.EntityFramework.Repositories.Authentication;
using Base.Infrastructure.Database.EntityFramework.Repositories.Formalities;
using Base.Infrastructure.Database.EntityFramework.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Base.Infrastructure.IoC.DependencyInjection;

public static class BaseDi
{
    public static IServiceCollection RegisterDataBase(this IServiceCollection collection, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("RemoteConnection");
        collection.AddDbContext<BaseDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            }
        );
        return collection;
    }

    public static IServiceCollection RegisterLibraries(this IServiceCollection collection)
    {
        // collection.AddValidatorsFromAssembly(Assembly.Load("Academic.Application"));
        // ValidatorOptions.Global.DisplayNameResolver = (type, memberInfo, expression) => memberInfo?.Name;
        return collection;
    }

    public static IServiceCollection RegisterProviders(this IServiceCollection collection, IConfiguration configuration)
    {
        return collection;
    }
    
    // internal sealed class NoOpNotificationScheduler : INotificationScheduler
    // {
    //     public Task ScheduleSendAsync(int notificationMessageId, DateTime scheduledAtUtc) => Task.CompletedTask;
    //     public Task CancelScheduledSendAsync(int notificationMessageId) => Task.CompletedTask;
    // }
    //
    // internal sealed class NoOpScheduleSignal : IInMemoryScheduleSignal
    // {
    //     private readonly Channel<DateTime> _ch = Channel.CreateUnbounded<DateTime>();
    //     public ChannelReader<DateTime> Reader => _ch.Reader;
    //     public void NotifyNewCandidate(DateTime scheduledAtUtc) {}
    // }
    
    public static IServiceCollection RegisterServices(this IServiceCollection collection, IConfiguration configuration)
    {
        collection.AddTransient<TestTableService>();
        collection.AddTransient<AuthService>();
        collection.AddTransient<IPasswordHasher, PasswordHasher>();
        collection.AddTransient<ITokenService, JwtTokenService>();
        
        collection.Configure<JwtSettings>(configuration.GetSection("Jwt"));

        collection.AddTransient<CitizenRequestService>();

        return collection;
    }
    public static IServiceCollection RegisterRepositories(this IServiceCollection collection)
    {
        collection.AddTransient<ITestTableRepository, TestTableRepository>();
        collection.AddTransient<IUserRepository, UserRepository>();
        collection.AddTransient<IRoleRepository, RoleRepository>();
        collection.AddTransient<IUserRoleRepository, UserRoleRepository>();
        collection.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();

        collection.AddTransient<ICitizenRequestRepository, CitizenRequestRepository>();
        collection.AddTransient<IRequestTypeRepository, RequestTypeRepository>();
        return collection;
    }
}