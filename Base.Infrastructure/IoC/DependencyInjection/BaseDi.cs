using Base.Aplication.Services;
using Base.Domain.Repositories;
using Base.Infrastructure.Database.EntityFramework.Context;
using Base.Infrastructure.Database.EntityFramework.Repositories;
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
    
    public static IServiceCollection RegisterServices(this IServiceCollection collection)
    {
        collection.AddTransient<TestTableService>();
        return collection;
    }
    public static IServiceCollection RegisterRepositories(this IServiceCollection collection)
    {
        collection.AddTransient<ITestTableRepository, TestTableRepository>();
        return collection;
    }
}