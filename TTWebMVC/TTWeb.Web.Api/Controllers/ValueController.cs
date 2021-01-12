using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Services;

namespace TTWeb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : BaseController
    {
        private readonly IHelperService _helper;

        public ValueController(IHelperService helper)
        {
            _helper = helper;
        }

        [HttpGet("aes")]
        [Authorize(Policy = Startup.RequireManageDeploymentPermissionPolicy)]
        public EncryptionKeyModel GetEncryptionKeyModel()
        {
            var aes = EncryptProvider.CreateAesKey();
            return new EncryptionKeyModel(aes.Key, aes.IV);
        }

        [HttpGet("otp")]
        [Authorize]
        public string GetOptCode([FromQuery] string seed)
        {
            return _helper.GetOtpCode(seed);
        }
    }
}