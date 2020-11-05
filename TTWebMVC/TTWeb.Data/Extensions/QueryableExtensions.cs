using System;
using System.Linq;
using TTWeb.Data.Models;

namespace TTWeb.Data.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> FilterByOwnerId<T>(this IQueryable<T> query, int? ownerId) where T : IUserOwnedEntity
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            if (ownerId.HasValue)
                query = query.Where(m => m.OwnerId == ownerId);

            return query;
        }

        public static IQueryable<T> FilterById<T>(this IQueryable<T> query, int? id) where T : IHasIdEntity
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            if (!id.HasValue) throw new ArgumentNullException(nameof(id));
            return query.Where(m => m.Id == id);
        }
    }
}
