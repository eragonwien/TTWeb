﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TTWebCommon.Models
{
   public class FacebookFriend
   {
      public int Id { get; set; } = 0;
      public string Name { get; set; }
      public string ProfileLink { get; set; }
      public bool Active { get; set; } = false;
      public bool Disabled { get; set; } = false;
      public static FacebookFriend Empty = new FacebookFriend();
   }
}
