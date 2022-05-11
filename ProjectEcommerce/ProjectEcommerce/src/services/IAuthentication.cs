using System.Threading.Tasks;
using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;

namespace ProjectEcommerce.src.services
{

    /// <summary>
    /// <para>Resume: Interface responsible for enterprise logic of user service.</para>
    /// <para>Created by: Ítalo Penha</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 11/05/2022</para>
    /// </summary>
    public interface IAuthentication
    {
        string EncodePassword(string password);
        Task CreatedUserNotDuplicatedAsync(AddUserDTO dto);
        string GenerateToken(UserModel user);
        Task<AuthorizationO> GetAuthorizationAsync(AuthenticationDTO dto);
    }
}