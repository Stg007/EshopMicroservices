using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Oredering.Infrastructure.Data.Interceptors;

public class DispatchDomainEventInterceptor(IMediator mediator) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        await DispatchDomainEvent(eventData.Context);
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
    public override InterceptionResult<int> SavingChanges(
        DbContextEventData eventData,
        InterceptionResult<int> result)
    {
        DispatchDomainEvent(eventData.Context).GetAwaiter().GetResult();

        return base.SavingChanges(eventData, result);
    }
    public async Task DispatchDomainEvent(DbContext? context)
    {
        if (context == null) return;

        var aggregates = context.ChangeTracker
            .Entries<IAggregate>()
            .Where(e => e.Entity.DomainEvents != null && e.Entity.DomainEvents.Any())
            .Select(e => e.Entity);

        var domainEvents = aggregates
            .SelectMany(e => e.DomainEvents)
            .ToList();

        aggregates.ToList().ForEach(e => e.ClearDomainEvents());

        foreach (var entity in domainEvents)
        {
            await mediator.Publish(entity);
        }
    }
}
