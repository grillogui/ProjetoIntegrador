using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.repositories;
using Microsoft.AspNetCore.Mvc;
using ProjectEcommerce.src.services;
using Microsoft.AspNetCore.Authorization;
using System;
using ProjectEcommerce.src.utilities;

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
        public IActionResult AddUser([FromBody] AddUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                _services.CreatedUserNotDuplicated(user);
                return Created($"api/Users/{user.Email}", user);
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }

        }

        [HttpGet("email/{emailUser}")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public IActionResult GetUserByEmail([FromRoute] string emailUser)
        {
            var user = _repository.GetUserByEmail(emailUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet("id/{idUser}")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public IActionResult GetUserById([FromRoute] int idUser)
        {
            var user = _repository.GetUserById(idUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet("name")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public IActionResult GetUserByName([FromQuery] string nameUser)
        {
            var users = _repository.GetUserByName(nameUser);

            if (users.Count < 1) return NoContent();

            return Ok(users);
        }

        [HttpGet("type")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public IActionResult GetUserByType([FromQuery] TypeUser typeUser)
        {
            var users = _repository.GetUserByType(typeUser);

            if (users.Count < 1) return NoContent();

            return Ok(users);
        }

        [HttpPut]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();
            user.Password = _services.EncodePassword(user.Password);
            _repository.UpdateUser(user);
            return Ok(user);
        }

        #endregion
    }
}