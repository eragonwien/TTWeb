using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TTWebCommon.Models
{
   public class ProcessingResult
   {
      public string Result { get; set; }
      public Exception Exception { get; set; }

      public bool Succeed
      {
         get
         {
            return Exception == null; 
         }
      }
   }
}