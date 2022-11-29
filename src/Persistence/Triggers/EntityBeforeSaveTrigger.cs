using System;
using BogusStore.Domain.Common;
using EntityFrameworkCore.Triggered;

namespace BogusStore.Persistence.Triggers;

/// <summary>
/// Acts like a database trigger but is database agnostic and executes on <see cref="BogusDbContext"/> SaveChanged(async) is called right before executing the query.
/// Trigger sets the <see cref="Entity.CreatedAt"/> and <see cref="Entity.UpdatedAt"/> of the underlying <see cref="Entity"/>.
/// </summary>
public class EntityBeforeSaveTrigger : IBeforeSaveTrigger<Entity>
{
    public Task BeforeSave(ITriggerContext<Entity> context, CancellationToken cancellationToken)
    {
        if (context.ChangeType == ChangeType.Added)
        {
            context.Entity.CreatedAt = DateTime.UtcNow;
            context.Entity.UpdatedAt = DateTime.UtcNow;
        }

        if (context.ChangeType == ChangeType.Modified)
        {
            context.Entity.UpdatedAt = DateTime.UtcNow;
        }

        return Task.CompletedTask;
    }
}

