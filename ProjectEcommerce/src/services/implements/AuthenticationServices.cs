using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;
using ProjectEcommerce.src.repositories;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEcommerce.src.services.implements
{
    /// <summary>
    /// <para>Resume: Class responsible for implement IAuthentication</para>
    /// <para>Created by: Joceline Gutierrez e Matheus Brazolin </para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 11/05/2022</para>
    /// </summary>

    public class AuthenticationServices : IAuthentication
    {
        #region Attributes
        private readonly IUser _repository;
        public IConfiguration Configuration { get; }
        #endregion

        #region Constructors
        public AuthenticationServices(IUser repository, IConfiguration configuration)
        {
            _repository = repository;
            Configuration = configuration;
        }
        #endregion

        #region Methods

        /// <summary>
        /// <para>Resume: Method for encode password</para>
        /// </summary>
        /// <param name="password">Password to be encrypted</param>
        /// <returns>string</returns>

        public string EncodePassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// <para>Resume: Asynchronous method responsible for create user without duplicate on database</para>
        /// </summary>
        /// <param name="dto">AddUserDTO</param>
        public async Task CreatedUserNotDuplicatedAsync(AddUserDTO dto)
        {
            var user = await _repository.GetUserByEmailAsync(dto.Email);
            if (user != null) throw new Exception("Este email já está sendo utilizado");
            dto.Password = EncodePassword(dto.Password);
            await _repository.AddUserAsync(dto);
        }

        /// <summary>
        /// <para>Resume: Method responsible for generate token JWT</para>
        /// </summary>
        /// <param name="user">UserModel</param>
        /// <returns>string</returns>
        public string GenerateToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration["Settings:Secret"]);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                new Claim[]
                {
                new Claim(ClaimTypes.Email, user.Email.ToString()),
                new Claim(ClaimTypes.Role, user.Type.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
            )
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// <para>Resume: Asynchronous method responsible to return authorization to authenticated user</para>
        /// </summary>
        /// <param name="dto">AuthenticationDTO</param>
        /// <returns>AuthorizationDTO</returns>
        /// <exception cref="Exception">User not found</exception>
        /// <exception cref="Exception">Incorrect Password</exception>
        public async Task<AuthorizationDTO> GetAuthorizationAsync(AuthenticationDTO dto)
        {
            var user = await _repository.GetUserByEmailAsync(dto.Email);

            if (user == null) throw new Exception("Usuário não encontrado");

            if (user.Password != EncodePassword(dto.Password)) throw new
            Exception("Senha incorreta");
            return new AuthorizationDTO(user.Id, user.Email, user.Type, GenerateToken(user));
        }
        #endregion



    }
}