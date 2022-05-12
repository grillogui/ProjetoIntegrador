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
    /// 
    public class UserImplements : IUser
    {
        #region Atributes

        private readonly ProjectEcommerceContext _context;

        #endregion Atributes

        #region Constructors

        public UserImplements(ProjectEcommerceContext context)

        { _context = context; }

        #endregion Constructors

        #region Methods
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

        public async Task <UserModel> GetUserByEmailAsync (string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task <UserModel> GetUserByIdAsync (int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task <List<UserModel>> GetUserByNameAsync (string name)
        {
            return await _context.Users
                        .Where(u => u.Name.Contains(name))
                        .ToListAsync();
        }

        public async Task <List<UserModel>> GetUserByTypeAsync (TypeUser type)
        {
            return await _context.Users
                .Where(u => u.Type == (type))
                .ToListAsync();
        }

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