using Microsoft.AspNetCore.Http;
using System;
using System.Reflection;
using Timesheet.Api.Repositories.Utils;

namespace Timesheet.Api.Repositories.Extensions;

public static class EntityExtension
{
    public static T SetAudit<T>(this T entity, HttpContext context, bool isCreatedAction, int userId) where T : class
    {
        Type objectType = entity.GetType();

        if (isCreatedAction)
        {
            PropertyInfo createdByField = objectType.GetProperty("CreatedBy");
            createdByField?.SetValue(entity, userId, null);

            PropertyInfo createdAtField = objectType.GetProperty("CreatedDate");
            createdAtField?.SetValue(entity, DateTime.Now, null);

            PropertyInfo localIpField = objectType.GetProperty("CreatedIp");
            localIpField?.SetValue(entity, Audit.GetLocalIP(), null);

            PropertyInfo macAddressField = objectType.GetProperty("CreatedMacAddress");
            macAddressField?.SetValue(entity, Audit.GetMACAddress(), null);
        }
        else
        {
            PropertyInfo UpdatedByField = objectType.GetProperty("UpdatedBy");
            UpdatedByField?.SetValue(entity, userId, null);

            PropertyInfo updatedAtField = objectType.GetProperty("UpdatedDate");
            updatedAtField?.SetValue(entity, DateTime.Now, null);

            PropertyInfo localIpField = objectType.GetProperty("UpdatedIp");
            localIpField?.SetValue(entity, Audit.GetLocalIP(), null);

            PropertyInfo macAddressField = objectType.GetProperty("UpdatedMacAddress");
            macAddressField?.SetValue(entity, Audit.GetMACAddress(), null);
        }

        return entity;
    }
}
