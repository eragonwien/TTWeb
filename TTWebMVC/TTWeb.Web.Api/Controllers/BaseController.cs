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
        protected int LoginUserId => int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : 0;
        protected int? OwnerId => !User.IsInRole(UserPermission.ManageUsers) ? LoginUserId : (int?) null;

        protected IEnumerable<UserPermission> LoginUserPermissions =>
            User.FindAll(ClaimTypes.Role)
                .Where(c => Enum.TryParse(c.Value, out UserPermission parsedValue))
                .Select(c => (UserPermission) Enum.Parse(typeof(UserPermission), c.Value))
                .AsEnumerable();

        protected void ThrowExceptionOnWrongOwner(int? ownerId)
        {
            if (User.IsInRole(UserPermission.AccessAllResources))
                return;

            if (!User.IsInRole(UserPermission.AccessOwnResources))
                throw new ResourceAccessDeniedException();

            if (ownerId.HasValue && ownerId != LoginUserId)
                throw new ResourceAccessDeniedException();
        }
    }
}