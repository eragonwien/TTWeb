using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TTWebMVCV2.Models
{
   public class AppSettings
   {
      public int PasswordIterlation { get; set; }
      public int PasswordSaltBytesLength { get; set; }
      public int PasswordBytesLength { get; set; }
   }
}
