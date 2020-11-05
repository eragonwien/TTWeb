using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using TTWeb.BusinessLogic.Exceptions;
using TTWeb.Data.Models;
using TTWeb.Web.Api.Extensions;

namespace TTWeb.Web.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        private int LoginUserId => int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : 0;
        protected int? OwnerId => !User.IsInRole(UserPermission.ManageUsers) ? LoginUserId : OwnerId;

        protected IEnumerable<UserPermission> LoginUserPermissions =>
            User.FindAll(ClaimTypes.Role)
                .Where(c => Enum.TryParse(c.Value, out UserPermission parsedValue))
                .Select(c => (UserPermission) Enum.Parse(typeof(UserPermission), c.Value))
                .AsEnumerable();

        protected void ThrowExceptionOnUnauthorizedAccess(int? loginUserId)
        {
            if (LoginUserPermissions.Contains(UserPermission.AccessAllResources)) return;
            if (!LoginUserPermissions.Contains(UserPermission.AccessOwnResources)) return;

            if (loginUserId.HasValue && loginUserId != LoginUserId) 
                throw new ResourceAccessDeniedException();
        }
    }
}