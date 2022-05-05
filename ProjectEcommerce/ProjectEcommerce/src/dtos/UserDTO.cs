using System.ComponentModel.DataAnnotations;

namespace ProjectEcommerce.src.dtos
{
    public class UserDTO
    {
        /// <summary>
        /// <para>Resume: Mirror class to add a new user</para>
        /// <para>Created by: Karol Oliveira and Guilherme Grillo</para>
        /// <para>Version: 1.0</para>
        /// <para>Date: 05/05/2022</para>
        /// </summary>

        public class AddUserDTO
        {
            [Required, StringLength(30)]
            public string Email { get; set; }

            [Required, StringLength(50)]
            public string Name { get; set; }

            [Required, StringLength(30)]
            public string Password { get; set; }

            [Required, StringLength(30)]
            public string Type { get; set; }

            [Required, StringLength(255)]
            public string Address { get; set; }

            public AddUserDTO(string email, string name, string password, string type, string address)
            {
                Email = email;
                Name = name;
                Password = password;
                Type = type;
                Address = address;
            }
        }
        /// <summary>
        /// <para>Sumary: Mirror class to update an user</para>
        /// <para>Created by: Karol Oliveira and Guilherme Grillo</para>
        /// <para>Version: 1.0</para>
        /// <para>Date: 05/05/2022</para>
        /// </summary>

        public class UpdateUserDTO
        {
            [Required, StringLength(50)]
            public string Name { get; set; }

            [Required, StringLength(30)]
            public string Password { get; set; }

            [Required, StringLength(30)]
            public string Type { get; set; }

            [Required, StringLength(255)]
            public string Address { get; set; }

            public UpdateUserDTO(string name, string password, string type, string address)
            {
                Name = name;
                Password = password;
                Type = type;
                Address = address;
            }
        }
    }
}

