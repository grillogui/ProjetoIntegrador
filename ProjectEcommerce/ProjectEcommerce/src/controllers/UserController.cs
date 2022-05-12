using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.repositories;
using ProjectEcommerce.src.utilities;
using ProjectEcommerce.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace ProjectEcommerce.src.controllers
{

    /// <summary>
    /// <para>Resume: Creating Controllers for users class</para>
    /// <para>Created by: Guilherme Grillo and Ítalo Penha</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 09/05/2022</para>
    /// </summary>

    [ApiController]
    [Route("api/Users")]
    [Produces("application/json")]

    public class UserController : ControllerBase
    {
        #region Atributes

        private readonly IUser _repository;
        private readonly IAuthentication _services;

        #endregion

        #region Constructors

        public UserController(IUser repository, IAuthentication service)
        {
            _repository = repository;
            _services = service; 
        }

        #endregion

        #region Methods

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> AddUserAsync([FromBody] AddUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                await _services.CreatedUserNotDuplicatedAsync(user);
                return Created($"api/Users/{user.Email}", user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        [HttpGet("email/{emailUser}")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByEmailAsync([FromRoute] string emailUser)
        {
            var user = await _repository.GetUserByEmailAsync(emailUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet("id/{idUser}")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByIdAsync([FromRoute] int idUser)
        {
            var user = await _repository.GetUserByIdAsync(idUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet("name")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByNameAsync([FromQuery] string nameUser)
        {
            var users = await _repository.GetUserByNameAsync(nameUser);

            if (users.Count < 1) return NoContent();

            return Ok(users);
        }

        [HttpGet("type")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByTypeAsync([FromQuery] TypeUser typeUser)
        {
            var users = await _repository.GetUserByTypeAsync(typeUser);

            if (users.Count < 1) return NoContent();

            return Ok(users);
        }

        [HttpPut]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public async Task<ActionResult> UpdateUserAsync([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            user.Password = _services.EncodePassword(user.Password);

            await _repository.UpdateUserAsync(user);
            return Ok(user);
        }

        #endregion
    }
}