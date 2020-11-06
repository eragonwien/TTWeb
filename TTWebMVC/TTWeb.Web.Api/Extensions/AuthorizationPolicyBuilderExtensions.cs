using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace TTWeb.Web.Api.Extensions
{
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