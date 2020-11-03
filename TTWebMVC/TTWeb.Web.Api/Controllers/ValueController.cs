using Microsoft.AspNetCore.Mvc;
using NETCore.Encrypt;
using TTWeb.BusinessLogic.Models.Entities.Encryption;

namespace TTWeb.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : BaseController
    {
        [HttpGet("aes")]
        public EncryptionKeyModel GetEncryptionKeyModel()
        {
            var aes = EncryptProvider.CreateAesKey();
            return new EncryptionKeyModel(aes.Key, aes.IV);
        }
    }
}
