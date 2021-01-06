using System;
using System.Linq;
using TTWeb.Data.Models;

namespace TTWeb.Data.Extensions
{
    public static class QueryableExtensions
    {
        #region IUserOwnedEntity

        public static IQueryable<T> FilterByOwnerId<T>(this IQueryable<T> query, int? ownerId) where T : IUserOwnedEntity
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            if (ownerId.HasValue)
                query = query.Where(m => m.OwnerId == ownerId);

            return query;
        }

        #endregion

        #region IHasIdEntity
        public static IQueryable<T> FilterById<T>(this IQueryable<T> query, int? id) where T : IHasIdEntity
        {
            if (query == null) throw new ArgumentNullException(nameof(query));
            if (!id.HasValue) throw new ArgumentNullException(nameof(id));
            return query.Where(m => m.Id == id);
        }

        #endregion

        #region Schedule
        public static IQueryable<T> FilterOpenSchedules<T>(this IQueryable<T> query, DateTime planningStartTime) where T : Schedule
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            query = query.HasLockValue();

            query = query.Where(s =>

                // New entry
                s.PlanningStatus == ProcessingStatus.New

                // Entry queued for retry
                || s.PlanningStatus == ProcessingStatus.Retry

                // Schedule stucks in in-progress
                || (s.PlanningStatus == ProcessingStatus.InProgress
                    && s.LockedUntil.Value < planningStartTime)

                // Existing entry which has not been processed today
                || (s.PlanningStatus == ProcessingStatus.Completed
                    && s.CompletedAt.HasValue
                    && s.CompletedAt.Value.Date < planningStartTime.Date)
                );

            return query;
        }

        private static IQueryable<T> HasLockValue<T>(this IQueryable<T> query) where T : Schedule
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            return query.Where(s => s.LockAt.HasValue && s.LockedUntil.HasValue && s.LockedUntil > s.LockAt);
        }

        #endregion

        #region ScheduleJob

        public static IQueryable<T> FilterOpenJobs<T>(this IQueryable<T> query) where T : ScheduleJob
        {
            if (query == null) throw new ArgumentNullException(nameof(query));

            query = query.Where(j => j.Status == ProcessingStatus.New || j.Status == ProcessingStatus.Retry);

            return query;
        }

        #endregion
    }
}
