using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.Data.Models;

namespace TTWeb.Web.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected int LoginUserId => int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : 0;

        protected IEnumerable<UserPermission> LoginUserPermissions =>
            User.FindAll(ClaimTypes.Role)
                .Where(c => Enum.TryParse(c.Value, out UserPermission parsedValue))
                .Select(c => (UserPermission) Enum.Parse(typeof(UserPermission), c.Value))
                .AsEnumerable();

        protected void ThrowExceptionOnUnauthorizedAccess(int? loginUserId)
        {
            if (loginUserId.HasValue 
                && loginUserId != LoginUserId
                && !LoginUserPermissions.Contains(UserPermission.AccessAllResources)) 
                throw new ResourceAccessDeniedException();
        }
    }
}