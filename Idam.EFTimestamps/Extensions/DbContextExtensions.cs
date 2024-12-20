﻿using System.Linq.Expressions;
using Idam.EFTimestamps.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Idam.EFTimestamps.Extensions;

/// <summary>
///     DbContext extension class.
/// </summary>
public static class DbContextExtensions
{
    /// <summary>
    ///     Add timestamps to the Entity with TimeStampsAttribute when state is Added or Modified or Deleted.
    /// </summary>
    /// <param name="changeTracker">The change tracker.</param>
    public static void AddTimestamps(this ChangeTracker changeTracker)
    {
        foreach (var entityEntry in changeTracker.Entries()) entityEntry.AddTimestamps();
    }

    /// <summary>
    ///     Add timestamps to the Entity with TimeStampsAttribute when state is Added or Modified or Deleted.
    /// </summary>
    /// <param name="entityEntry">The entity entry.</param>
    private static void AddTimestamps(this EntityEntry? entityEntry)
    {
        if (entityEntry is null) return;

        switch (entityEntry.State)
        {
            case EntityState.Added:
            case EntityState.Modified:
                UpdateTimeStamps(entityEntry.Entity, entityEntry.State);
                break;

            case EntityState.Deleted:
                switch (entityEntry.Entity)
                {
                    case ISoftDelete { DeletedAt: null } softDelete:
                        entityEntry.State = EntityState.Modified;
                        softDelete.DeletedAt = DateTime.Now;
                        break;
                    case ISoftDeleteUtc { DeletedAt: null } softDeleteUtc:
                        entityEntry.State = EntityState.Modified;
                        softDeleteUtc.DeletedAt = DateTime.UtcNow;
                        break;
                    case ISoftDeleteUnix { DeletedAt: null } softDeleteUnix:
                        entityEntry.State = EntityState.Modified;
                        softDeleteUnix.DeletedAt = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                        break;
                }

                break;
        }
    }

    /// <summary>
    ///     Updates the time stamps.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="entityState">State of the entity.</param>
    private static void UpdateTimeStamps(object entity, EntityState entityState)
    {
        if (entityState is not EntityState.Added and not EntityState.Modified) return;
        if (entity is not ITimeStampBase) return;

        var now = DateTime.Now;
        var nowUtc = DateTime.UtcNow;
        var nowUnix = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        switch (entity)
        {
            case ITimeStamps timeStamps:
                timeStamps.UpdatedAt = now;
                if (entityState == EntityState.Added) timeStamps.CreatedAt = now;

                break;

            case ITimeStampsUtc timeStampsUtc:
                timeStampsUtc.UpdatedAt = nowUtc;
                if (entityState == EntityState.Added) timeStampsUtc.CreatedAt = nowUtc;

                break;

            case ITimeStampsUnix timeStampsUnix:
                timeStampsUnix.UpdatedAt = nowUnix;
                if (entityState == EntityState.Added) timeStampsUnix.CreatedAt = nowUnix;

                break;

            default:
                if (entityState == EntityState.Added)
                    switch (entity)
                    {
                        case ICreatedAt createdAt:
                            createdAt.CreatedAt = now;
                            break;
                        case ICreatedAtUtc createdAtUtc:
                            createdAtUtc.CreatedAt = nowUtc;
                            break;
                        case ICreatedAtUnix createdAtUnix:
                            createdAtUnix.CreatedAt = nowUnix;
                            break;
                    }

                switch (entity)
                {
                    case IUpdatedAt updatedAt:
                        updatedAt.UpdatedAt = now;
                        break;
                    case IUpdatedAtUtc updatedAtUtc:
                        updatedAtUtc.UpdatedAt = nowUtc;
                        break;
                    case IUpdatedAtUnix updatedAtUnix:
                        updatedAtUnix.UpdatedAt = nowUnix;
                        break;
                }

                break;
        }
    }

    /// <summary>
    ///     Query Filter to get model where DeletedAt field is null.
    /// </summary>
    /// <param name="builder">The builder.</param>
    public static void AddSoftDeleteFilter(this ModelBuilder builder)
    {
        var mutableEntityTypes = builder.Model.GetEntityTypes();

        foreach (var mutableEntityType in mutableEntityTypes) builder.AddSoftDeleteFilter(mutableEntityType);
    }

    /// <summary>
    ///     Query Filter to get model where DeletedAt field is null.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="mutable">The mutable entity type.</param>
    private static void AddSoftDeleteFilter(this ModelBuilder builder, IMutableEntityType? mutable)
    {
        if (mutable is null) return;

        if (!typeof(ISoftDelete).IsAssignableFrom(mutable.ClrType) &&
            !typeof(ISoftDeleteUtc).IsAssignableFrom(mutable.ClrType) &&
            !typeof(ISoftDeleteUnix).IsAssignableFrom(mutable.ClrType)) return;

        var propertyType = typeof(ISoftDelete).IsAssignableFrom(mutable.ClrType)
            ? typeof(DateTime?)
            : typeof(long?);

        var parameter = Expression.Parameter(mutable.ClrType, "e");

        var property = Expression.Property(parameter, nameof(ISoftDelete.DeletedAt));
        var body = Expression.Equal(property, Expression.Constant(null, propertyType));

        var expression = Expression.Lambda(body, parameter);

        builder.Entity(mutable.ClrType).HasQueryFilter(expression);
    }
}