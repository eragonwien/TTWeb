using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace TTWeb.Web.Api.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsInRole<T>(this ClaimsPrincipal user, T role) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(nameof(role));
            return user.IsInRole(role.ToString());
        }

        public static T FindFirstValue<T>(this ClaimsPrincipal user, string claimType)
        {
            try
            {
                var stringValue = user.FindFirstValue(claimType);

                if (typeof(T).IsEnum)
                    return (T) Enum.Parse(typeof(T), stringValue, true);

                return (T)Convert.ChangeType(stringValue, typeof(T));
            }
            catch (Exception)
            {
                return default;
            }
        }
    }

    public static class AuthorizationPolicyBuilderExtensions
    {
        public static AuthorizationPolicyBuilder RequireRole<T>(this AuthorizationPolicyBuilder builder, params T[] roles)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(nameof(roles));
            return roles == null ? builder : builder.RequireRole(roles.Select(r => r.ToString()));
        }
    }
}