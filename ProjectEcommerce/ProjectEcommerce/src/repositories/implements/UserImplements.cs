using ProjectEcommerce.src.data;
using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;
using System.Collections.Generic;
using System.Linq;

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
        public void AddUser(AddUserDTO user)
        {
            _context.Users.Add(new UserModel
            {
                Email = user.Email,
                Name = user.Name,
                Password = user.Password,
                Type = user.Type,
                Address = user.Address
            });
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            _context.Users.Remove(GetUserById(id));
            _context.SaveChanges();
        }

        public UserModel GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public UserModel GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public List<UserModel> GetUserByName(string name)
        {
            return _context.Users.Where(u => u.Name.Contains(name)).ToList();
        }

        public List<UserModel> GetUserByType(string type)
        {
            return _context.Users.Where(u => u.Type.Contains(type)).ToList();
        }

        public void UpdateUser(UpdateUserDTO user)
        {
            var oldUser = GetUserById(user.Id);
            oldUser.Name = user.Name;
            oldUser.Password = user.Password;
            oldUser.Type = user.Type;
            oldUser.Address = user.Address;
            _context.Users.Update(oldUser);
            _context.SaveChanges();
        }
        #endregion Methods
    }
}