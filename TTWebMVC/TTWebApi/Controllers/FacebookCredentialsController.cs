using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TTWebCommon.Models;
using TTWebCommon.Models.Common.Exceptions;
using TTWebCommon.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TTWebApi.Controllers
{
    [Route("facebook_credentials")]
    public class FacebookCredentialsController : BaseController
    {
        private readonly IFacebookCredentialService facebookCredentialService;
        private readonly IExceptionService exceptionService;

        public FacebookCredentialsController(IFacebookCredentialService facebookCredentialService, IExceptionService exceptionService)
        {
            this.facebookCredentialService = facebookCredentialService;
            this.exceptionService = exceptionService;
        }

        // GET: api/<FacebookCredentialsController>
        [HttpGet]
        public async Task<IEnumerable<FacebookCredential>> GetFacebookCredentials()
        {
            return await facebookCredentialService.GetFacebookCredentialsAsync();
        }

        // GET api/<FacebookCredentialsController>/5
        [HttpGet("{id}", Name = nameof(GetFacebookCredentialById))]
        public async Task<FacebookCredential> GetFacebookCredentialById(int id, int appuser_id)
        {
            var facebookCredential = await facebookCredentialService.GetFacebookCredentialByIdAsync(id, appuser_id);

            if (facebookCredential == null)
                throw new WebApiNotFoundException("Facebook credential id={0}, appuser_id={1} not found", id, appuser_id);

            return facebookCredential;
        }

        // POST api/<FacebookCredentialsController>
        [HttpPost]
        public async Task<IActionResult> CreateFacebookCredential([FromBody] FacebookCredential facebookCredential)
        {
            await facebookCredentialService.CreateFacebookCredentialAsync(facebookCredential);
            return CreatedAtRoute(nameof(GetFacebookCredentialById), new { id = facebookCredential.Id, appuser_id = facebookCredential.AppUserId }, facebookCredential);
        }

        // PUT api/<FacebookCredentialsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFacebookCredential([FromBody] FacebookCredential facebookCredential)
        {
            await facebookCredentialService.UpdateFacebookCredentialAsync(facebookCredential);
            return NoContent();
        }

        // DELETE api/<FacebookCredentialsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
