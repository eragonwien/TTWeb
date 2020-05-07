using Microsoft.EntityFrameworkCore.Internal;
using SNGCommon.Authentication;
using System;
using TTWebCommon.Models;

namespace TTWebApi.Models
{
   public class DatabaseInitializer
   {
      public static void Initialize(TTWebDbContext dbContext, IAuthenticationService authenticationService)
      {
         InitializeLoginUser(dbContext, authenticationService);
      }

      private static void InitializeLoginUser(TTWebDbContext dbContext, IAuthenticationService authenticationService)
      {
         if (dbContext.LoginUserSet.Any())
         {
            return;
         }
         var loginUser = new LoginUser
         {
            Email = "tester@test.com",
            Password = authenticationService.GetEncodedPassword("1234"),
            Firstname = "Steak",
            Lastname = "Tester",
            Title = "Dr."
         };
         dbContext.LoginUserSet.Add(loginUser);
         dbContext.SaveChanges();
      }
   }
}