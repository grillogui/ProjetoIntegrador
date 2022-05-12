using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;
using ProjectEcommerce.src.utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEcommerce.src.repositories
{
    /// <summary>
    /// <para>Resume: Interface to represent CRUD actions in users</para>
    /// <para>Created by: Karol Oliveira and Guilherme Grillo</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 05/05/2022</para>
    /// </summary>

    public interface IUser
    {
        Task AddUserAsync(AddUserDTO user);
        Task UpdateUserAsync(UpdateUserDTO user);
        Task <UserModel> GetUserByIdAsync (int id);
        Task <UserModel> GetUserByEmailAsync (string email);
        Task <List<UserModel>> GetUserByNameAsync (string name);
        Task <List<UserModel>> GetUserByTypeAsync (TypeUser type);
    }
}