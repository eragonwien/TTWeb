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

        public static IQueryable<T> FilterOpenSchedules<T>(this IQueryable<T> query, DateTime currentDate) where T : Schedule
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            query = query.Where(s => s.PlanningStatus == ProcessingStatus.New
                                     || s.PlanningStatus == ProcessingStatus.Retry
                                     || s.PlanningStatus == ProcessingStatus.New && !s.LockAt.HasValue
                                     || (s.PlanningStatus == ProcessingStatus.Completed && s.LockAt.HasValue &&
                                         s.LockAt.Value.Date < currentDate.Date)
                                     || (s.PlanningStatus == ProcessingStatus.InProgress && s.LockAt.HasValue &&
                                         s.LockAt.Value.Date < currentDate.Date && s.LockedUntil.HasValue &&
                                         s.LockedUntil.Value < currentDate));

            return query;
        }
    }
}
