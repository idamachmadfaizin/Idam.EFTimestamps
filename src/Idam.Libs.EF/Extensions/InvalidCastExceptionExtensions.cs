﻿using Idam.Libs.EF.Attributes;
using System.Reflection;

namespace Idam.Libs.EF.Extensions;
internal static class InvalidCastValidationException
{
    /// <summary>
    /// Throws if invalid time stamps.
    /// </summary>
    /// <param name="propertyName">Name of the property.</param>
    /// <param name="entityType">Type of the entity.</param>
    /// <param name="timeStampsAttribute">The time stamps attribute.</param>
    /// <exception cref="InvalidCastException">The property '{propertyName}' in {entityType.Name} is not of type {timeStampsType.Name}.</exception>
    public static void ThrowIfInvalidTimeStamps(string? propertyName, Type entityType, TimeStampsAttribute timeStampsAttribute)
    {
        var useField = !string.IsNullOrWhiteSpace(propertyName);

        var isDeletedAtField = propertyName?.Equals(timeStampsAttribute.DeletedAtField) ?? false;

        Type timeStampsType = isDeletedAtField
            ? timeStampsAttribute.TimeStampsType.GetNullableMapType()
            : timeStampsAttribute.TimeStampsType.GetMapType();

        PropertyInfo? property = useField ? entityType.GetProperty(propertyName!) : null;

        if (useField)
        {
            if (property is null)
            {
                throw new InvalidCastException($"The property '{propertyName}' doesn't exist in {entityType.Name}.");
            }
            else if (property.PropertyType != timeStampsType)
            {
                throw new InvalidCastException($"The property '{propertyName}' in {entityType.Name} is not of type {timeStampsType.FullName}.");
            }
        }
    }
}