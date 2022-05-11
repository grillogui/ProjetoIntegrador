using BlogPessoal.src.dtos;
using Microsoft.Extensions.Configuration;
using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;
using ProjectEcommerce.src.repositories;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEcommerce.src.services.implements
{
    /// <summary>
    /// <para>Resume: Class responsible for implement enterprise logic of user</para>
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
        /// <para>Resume: Method responsible for encode user password</para>
        /// </summary>
        /// <param name="password">string</param>
        /// <returns>string</returns>

        public string EncodePassword(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// <para>Resume: Method responsible for validate user email exist before creation</para>
        /// <para>Description: Method encodes user password and validate email before save in database</para>
        /// </summary>
        /// <param name="dto">UserRegisterDTO</param>
        /// <returns>UserModel</returns>

        public void CreatedUserNotDuplicated(AddUserDTO dto)
        {
            var user = _repository.GetUserByEmail(dto.Email);
            if (user != null) throw new Exception("Este email já está sendo utilizado");
            dto.Password = EncodePassword(dto.Password);
            _repository.AddUser(dto);
        }

        /// <summary>
        /// <para>Resume: Method responsible for generate token</para>
        /// <para>Description: Method claims user email and role and generate token</para>
        /// </summary>
        /// <param name="user">UserModel</param>
        /// <returns>string</returns>

        public string GenerateToken(UserModel user)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// <para>Resume: Method responsible for validate user email and password before authorization</para>
        /// <para>Description: Method encodes user password and validate email and password before authorization</para>
        /// </summary>
        /// <param name="dto">UserLoginDTO</param>
        /// <returns>AuthorizationDTO</returns>

        public AuthorizationDTO GetAuthorization(AuthenticateDTO dto)
        {
            var user = _repository.GetUserByEmail(dto.Email);

            if (user == null) throw new Exception("Usuário não encontrado");

            if (user.Password != EncodePassword(dto.Password)) throw new
            Exception("Senha incorreta");
            return new AuthorizationDTO(user.Id, user.Email, user.Type, GenerateToken(user));
        }
        #endregion



    }
}
