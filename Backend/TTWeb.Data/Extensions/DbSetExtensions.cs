using System.Linq;
using Microsoft.EntityFrameworkCore;
using TTWeb.Data.Models;

namespace TTWeb.Data.Extensions
{
    public static class DbSetExtensions
    {
        public static T GetOrAttachById<T>(this DbSet<T> collection, int id)
            where T : class, IHasIdEntity, new()
        {
            T localEntity = collection.Local.FirstOrDefault(c => c.Id == id);

            if (localEntity == null)
            {
                localEntity = new T();
                collection.Attach(localEntity);
            }

            return localEntity;
        }
    }
}
