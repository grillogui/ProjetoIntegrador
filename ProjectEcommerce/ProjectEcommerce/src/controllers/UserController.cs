using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.repositories;
using Microsoft.AspNetCore.Mvc;


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

        #endregion

        #region Constructors

        public UserController(IUser repository)
        {
            _repository = repository;
        }


        #endregion

        #region Methods

        [HttpPost]
        public IActionResult AddUser([FromBody] AddUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.AddUser(user);

            return Created($"api/Users/{user.Email}", user);
        }

        [HttpGet("email/{emailUser}")]
        public IActionResult GetUserByEmail([FromRoute] string emailUser)
        {
            var user = _repository.GetUserByEmail(emailUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet("id/{idUser}")]
        public IActionResult GetUserById([FromRoute] int idUser)
        {
            var user = _repository.GetUserById(idUser);

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpGet]
        public IActionResult GetUserByName([FromQuery] string nameUser)
        {
            var users = _repository.GetUserByName(nameUser);

            if (users.Count < 1) return NoContent();

            return Ok(users);
        }

        [HttpGet]
        public IActionResult GetUserByType([FromQuery] string typeUser)
        {
            var users = _repository.GetUserByType(typeUser);

            if (users.Count < 1) return NoContent();

            return Ok(users);
        }

        [HttpPut]
        public IActionResult UpdateUser([FromBody] UpdateUserDTO user)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.UpdateUser(user);
            return Ok(user);
        }

        #endregion
    }
}