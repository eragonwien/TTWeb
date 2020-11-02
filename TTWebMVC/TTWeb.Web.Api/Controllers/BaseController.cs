using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TTWeb.Web.Api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected int LoginUserId => int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : 0;
    }
}