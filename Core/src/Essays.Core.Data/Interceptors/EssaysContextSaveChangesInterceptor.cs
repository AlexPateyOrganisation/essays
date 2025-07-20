using Essays.Core.Data.Data;
using Essays.Core.Models.Models;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Essays.Core.Data.Interceptors;

public class EssaysContextSaveChangesInterceptor : ISaveChangesInterceptor
{
    public InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is not EssaysContext context)
        {
            return result;
        }

        var tracker = context.ChangeTracker;

        var deleteEntries = tracker.Entries<Essay>()
            .Where(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Deleted);

        foreach (var entry in deleteEntries)
        {
            entry.Property<bool>("Deleted").CurrentValue = true;
            entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }

        var updatedEntries = tracker.Entries<Essay>()
            .Where(e => e.State == Microsoft.EntityFrameworkCore.EntityState.Modified);

        foreach (var entry in updatedEntries)
        {
            entry.Property<DateTimeOffset?>("LastEdited").CurrentValue = DateTimeOffset.Now;
        }

        return result;
    }

    public ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = new())
    {
        return ValueTask.FromResult(SavingChanges(eventData, result));
    }
}