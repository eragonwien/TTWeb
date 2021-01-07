using System;
using System.Security.Claims;

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

                return (T) Convert.ChangeType(stringValue, typeof(T));
            }
            catch (Exception)
            {
                return default;
            }
        }
    }
}