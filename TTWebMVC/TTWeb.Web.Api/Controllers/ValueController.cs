using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;
using TTWeb.BusinessLogic.Extensions;
using TTWeb.BusinessLogic.Models.Entities;
using TTWeb.BusinessLogic.Services;
using TTWeb.Helper.Otp;

namespace TTWeb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : BaseController
    {
        private readonly IOtpHelperService _otp;

        public ValueController(IOtpHelperService otp)
        {
            _otp = otp;
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
            return _otp.GetCode(seed?.RemoveWhiteSpace().ToUpper(CultureInfo.InvariantCulture));
        }
    }
}