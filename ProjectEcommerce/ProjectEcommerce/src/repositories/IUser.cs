using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;
using System.Collections.Generic;
using System.Linq;


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
        void AddUser(AddUserDTO user);
        void UpdateUser(UpdateUserDTO user);
        void DeleteUser(int id);
        UserModel GetUserById(int id);
        UserModel GetUserByEmail(string email);
        List<UserModel> GetUserByName(string name);
        List<UserModel> GetUserByType(string type);
    }
}