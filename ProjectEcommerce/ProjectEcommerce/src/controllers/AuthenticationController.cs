using BlogPessoal.src.dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectEcommerce.src.services;
using System;
using System.Threading.Tasks;

namespace ProjectEcommerce.src.controllers
{
    /// <summary>
    /// <para>Resume: Creating Controllers for authentication class</para>
    /// <para>Created by: Joceline Gutierrez</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 12/05/2022</para>
    /// </summary>


    [ApiController]
    [Route("api/Authentication")]
    [Produces("application/json")]
    public class AuthenticationController : ControllerBase
    {
        #region Attributes
        private readonly IAuthentication _services;
        #endregion

        #region Constructors
        public AuthenticationController(IAuthentication services)
        {
            _services = services;
        }
        #endregion

        #region Mhetods
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AuthenticationAsync([FromBody] AuthenticationDTO authentication)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                var authorization = await _services.GetAuthorizationAsync(authentication);
                return Ok(authorization);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
        #endregion
    }
}