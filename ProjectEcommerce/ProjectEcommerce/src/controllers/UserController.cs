using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.repositories;
using ProjectEcommerce.src.utilities;
using ProjectEcommerce.src.services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using ProjectEcommerce.src.models;

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

        /// <summary>
        /// Create a new user
        /// </summary>
        /// <param name="user">UserRegisterDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/User
        ///     {
        ///        "email": "italo@email.com",
        ///        "name": "Ítalo Penha",
        ///        "password": "1234",
        ///        "type": "URLPHOTO",
        ///        "address": "Rua Itália, 97"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created user</response>
        /// <response code="400">Error in request</response>
        /// <response code="401">Exist user email in database</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

        /// <summary>
        /// Get a user by email
        /// </summary>
        /// <param name="emailUser">string</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Returns a user</response>
        /// <response code="204">User not found</response>
        [HttpGet("email/{emailUser}")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByEmailAsync([FromRoute] string emailUser)
        {
            var user = await _repository.GetUserByEmailAsync(emailUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Get a user by id
        /// </summary>
        /// <param name="idUser">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the user</response>
        /// <response code="404">User not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idUser}")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByIdAsync([FromRoute] int idUser)
        {
            var user = await _repository.GetUserByIdAsync(idUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        /// <summary>
        /// Get a user by name
        /// </summary>
        /// <param name="nameUser">string</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Returns the list users</response>
        /// <response code="204">Users not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("name")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByNameAsync([FromQuery] string nameUser)
        {
            var users = await _repository.GetUserByNameAsync(nameUser);

            if (users.Count < 1) return NoContent();

            return Ok(users);
        }

        /// <summary>
        /// Get a user by type
        /// </summary>
        /// <param name="typeUser">string</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Returns the list users</response>
        /// <response code="204">Users not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("type")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public async Task<ActionResult> GetUserByTypeAsync([FromQuery] TypeUser typeUser)
        {
            var users = await _repository.GetUserByTypeAsync(typeUser);

            if (users.Count < 1) return NoContent();

            return Ok(users);
        }

        /// <summary>
        /// Update a user
        /// </summary>
        /// <param name="user">UserUpdateDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /api/User
        ///     {
        ///        "id": 1,    
        ///        "email": "italo@email.com",
        ///        "name": "Ítalo Penha",
        ///        "password": "1234",
        ///        "type": "URLPHOTO",
        ///        "address": "Rua Itália, 97"
        ///     }
        ///
        /// </remarks>
        /// <response code="200">User updated</response>
        /// <response code="400">Error in request</response>
        /// <response code="404">User not found</response
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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