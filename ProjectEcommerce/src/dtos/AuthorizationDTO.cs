using ProjectEcommerce.src.utilities;
using System.ComponentModel.DataAnnotations;

namespace ProjectEcommerce.src.dtos
{
        /// <summary>
    /// <para>Resume:AuthenticationDTO</para>
    /// <para>Created by: Matheus Brazolin</para>
    /// <para>version: 1.0</para>
    /// <para>Date: 13/05/2022</para>
    /// </summary>
    public class AuthenticationDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public AuthenticationDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
    /// <summary>
    /// <para>Resume:AuthorizationDTO</para>
    /// <para>Created by: Matheus Brazolin</para>
    /// <para>version: 1.0</para>
    /// <para>Date: 13/05/2022</para>
    /// </summary>
    public class AuthorizationDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public TypeUser Type { get; set; }
        public string Token { get; set; }
        public AuthorizationDTO(int id, string email, TypeUser type, string token)
        {
            Id = id;
            Email = email;
            Type = type;
            Token = token;
        }
    }
}
