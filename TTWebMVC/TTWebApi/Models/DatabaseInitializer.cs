using Microsoft.EntityFrameworkCore.Internal;
using SNGCommon.Authentication;
using TTWebCommon.Models;

namespace TTWebApi.Models
{
   public class DatabaseInitializer
   {
      public static void Initialize(TTWebDbContext dbContext, IAuthenticationService authenticationService)
      {
         if (dbContext.Database.CanConnect())
         {
            InitializeAppUser(dbContext, authenticationService);
         }
      }

      private static void InitializeAppUser(TTWebDbContext dbContext, IAuthenticationService authenticationService)
      {
         if (dbContext.AppUserSet.Any())
         {
            return;
         }
         var appUser = new AppUser
         {
            Email = "tester@test.com",
            Password = authenticationService.GetEncodedPassword("1234"),
            Firstname = "Steak",
            Lastname = "Tester",
            Title = "Dr.",
            Active = 1,
            Disabled = 0
         };
         dbContext.AppUserSet.Add(appUser);
         dbContext.SaveChanges();
      }
   }
}