using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TTWebNetCommon.Extensions
{
   public static class DictionnaryExtensions
   {
      public static T Map<T>(this Dictionary<string, string> dictionary, T obj = null) where T : class, new()
      {
         if (obj == null)
         {
            obj = new T();
         }
         var type = obj.GetType();
         foreach (var item in dictionary)
         {
            if (obj.HasProperty(item.Key))
            {
               type.GetProperty(item.Key).SetValue(obj, item.Value);
            }
         }
         return obj;
      }

      public static bool HasProperty(this object objectToCheck, string propertyName)
      {
         var type = objectToCheck.GetType();
         return type.GetProperty(propertyName) != null;
      }
   }
}
