using Microsoft.EntityFrameworkCore;
using ProjectEcommerce.src.data;
using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;
using ProjectEcommerce.src.utilities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectEcommerce.src.repositories.implements
{
    /// <summary>
    /// <para>Resume: Implementing methods and constructors for users classr</para>
    /// <para>Created by: Guilherme Grillo and Karol Oliveira</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 05/05/2022</para>
    /// </summary>
    public class UserImplements : IUser
    {
        #region Atributes

        private readonly ProjectEcommerceContext _context;

        #endregion Atributes

        #region Constructors

        /// <summary>
        /// <para>Resume: Constructor of class.</para>
        /// </summary>
        /// <param name="context">ProjectEcommerceContext</param>
        public UserImplements(ProjectEcommerceContext context)

        { _context = context; }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// <para>Resume: Asynchronous method to add a new user.</para>
        /// </summary>
        /// <param name="user">AddUserDTO</param>
        public async Task AddUserAsync (AddUserDTO user)
        {
            await _context.Users.AddAsync(new UserModel
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Type = user.Type,
                Address = user.Address
            });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to get a user by email.</para>
        /// </summary>
        /// <param name="email">Email of user</param>
        /// <returns>UserModel</returns>
        public async Task <UserModel> GetUserByEmailAsync (string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to get a user by Id.</para>
        /// </summary>
        /// <param name="id">Id of user</param>
        /// <returns>UserModel</returns>
        public async Task <UserModel> GetUserByIdAsync (int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to get a user by name.</para>
        /// </summary>
        /// <param name="name">Name of user</param>
        /// <returns>List UserModel</returns>
        public async Task <List<UserModel>> GetUserByNameAsync (string name)
        {
            return await _context.Users
                        .Where(u => u.Name.Contains(name))
                        .ToListAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to get a user by type.</para>
        /// </summary>
        /// <param name="type">Type of user</param>
        /// <returns>List UserModel</returns>
        public async Task <List<UserModel>> GetUserByTypeAsync (TypeUser type)
        {
            return await _context.Users
                .Where(u => u.Type == (type))
                .ToListAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to update a user.</para>
        /// </summary>
        /// <param name="user">UpdateUserDTO</param>
        public async Task UpdateUserAsync (UpdateUserDTO user)
        {
            var oldUser = await GetUserByIdAsync (user.Id);
            oldUser.Name = user.Name;
            oldUser.Password = user.Password;
            oldUser.Type = user.Type;
            oldUser.Address = user.Address;
            _context.Users.Update(oldUser);
            await _context.SaveChangesAsync();
        }

        #endregion Methods
    }
}