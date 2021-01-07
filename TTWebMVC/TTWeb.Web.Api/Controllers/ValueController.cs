using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;
using TTWeb.BusinessLogic.Models.Entities;

namespace TTWeb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : BaseController
    {
        [HttpGet("aes")]
        [Authorize(Policy = Startup.RequireManageDeploymentPermissionPolicy)]
        public EncryptionKeyModel GetEncryptionKeyModel()
        {
            var aes = EncryptProvider.CreateAesKey();
            return new EncryptionKeyModel(aes.Key, aes.IV);
        }
    }
}