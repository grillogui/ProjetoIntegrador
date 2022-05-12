using System.Linq;
using System.Threading.Tasks;
using ProjectEcommerce.src.data;
using ProjectEcommerce.src.repositories;
using ProjectEcommerce.src.repositories.implements;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectEcommerce.src.dtos;

namespace ProjectEcommerceTest.tests.repositories
{
    /// <summary>
    /// <para>Resume: Test to verify if the implementation of users' repository worked</para>
    /// <para>Created by: Guilherme Grillo and Karol Oliveira</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 05/05/2022</para>
    /// </summary>

    [TestClass]
    public class UserImplementsTest
    {
        private ProjectEcommerceContext _context;
        private IUser _repository;


        // Test AddUser
        [TestMethod]
        public async Task CreateThreeUsersIntoDatabaseReturnThreeUsers()
        {

            // Defining context
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
            .UseInMemoryDatabase(databaseName: "db_projectecommerce")
            .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new UserImplements(_context);

            //Given that I register 3 users into database
            await _repository.AddUserAsync(
                new AddUserDTO(
                "guigrillo@email.com",
                "Guilherme Grillo",
                "134652",
                "REGULAR",
                "Rua das Paineiras, 122, apto 8"));

            await _repository.AddUserAsync(
                new AddUserDTO(
                "karolcoli@email.com",
                "Karol Oliveira",
                "135247",
                "REGULAR",
                "Rua dos Ipês, 351"));

            await _repository.AddUserAsync(
                new AddUserDTO(
                "jocelineg@email.com",
                "Joceline Gutierrez",
                "235837",
                "REGULAR",
                "Rua das Margaridas, 9668"));

            //When searching full list. Then I get 3 users.
            Assert.AreEqual(3, _context.Users.Count());
        }


        // Test UpdateUser
        [TestMethod]
        public async Task UpdateUserReturnUpdatedUser()
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
            .UseInMemoryDatabase(databaseName: "db_projectecommerce")
            .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new UserImplements(_context);


            // Given that I register an user in the database
            await _repository.AddUserAsync(
                new AddUserDTO((
                "brazolas@email.com",
                "Matheus Brazolin",
                "brabra1234",
                "REGULAR",
                "Rua das Rosas, 123"));

            //When we update the user
            await _repository.UpdateUserAsync(
                new UpdateUserDTO(_context.Users.FirstOrDefault(u => u.Email == "brazolas@email.com").Id,
                "Brazolin",
                "mat00",
                "REGULAR",
                "Rua das Jararacas, 80"));

            var old = await _repository.GetUserByEmailAsync("brazolas@email.com");

            //Then, it should return a new name
            Assert.AreEqual("Brazolin", _context.Users.FirstOrDefault(u => u.Id == old.Id).Name);

            // And, it should return password mat00
            Assert.AreEqual("mat00", _context.Users.FirstOrDefault(u => u.Id == old.Id).Password);
        }


        // Test GetUserByID
        [TestMethod]
        public async Task GetUserByIdReturnNotNullNameAsync()
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
            .UseInMemoryDatabase(databaseName: "db_projectecommerce")
            .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new UserImplements(_context);


            //Given that I register an user in the database
            await _repository.AddUserAsync(
                new AddUserDTO(
                "jocelineg@email.com",
                "Joceline Gutierrez",
                "235837",
                "REGULAR",
                "Rua das Margaridas, 9668"));

            //When I search for id number 6
            var user = await _repository.GetUserByIdAsync(6);

            //Then, it should return a not null element

            Assert.IsNotNull(user);

            //And the element should be Joceline Gutierrez

            Assert.AreEqual("Joceline Gutierrez", user.Name);
        }


        // Test GetUserByEmail
        [TestMethod]
        public async Task GetUserByEmailReturnNotNullAsync()
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
            .UseInMemoryDatabase(databaseName: "db_projectecommerce")
            .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new UserImplements(_context);

            //Given that I register an user in the database
            await _repository.AddUserAsync(
                new AddUserDTO(
                "leleo@email.com",
                "Leonardo Sarto",
                "senha123",
                "REGULAR",
                "Rua das Maritacas, 98"));

            //When I search for this user's email
            var user = await  _repository.GetUserByEmailAsync("leleo@email.com");

            //Then, I get this user
            Assert.IsNotNull(user);
        }


        // Test GetUserByName
        [TestMethod]
        public async Task GetUsersByNameReturnListAsync()
        {
            // Defining context
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
            .UseInMemoryDatabase(databaseName: "db_projectecommerce")
            .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new UserImplements(_context);

            //Given that I register 3 users into database
            await _repository.AddUserAsync(
                new AddUserDTO(
                "AnaPaula@email.com",
                "Ana Paula",
                "senha123",
                "REGULAR",
                "Rua das Paineiras, 122, apto 8"));

            await _repository.AddUserAsync(
                new AddUserDTO(
                "Maryany@email.com",
                "Ana Maria",
                "senha123",
                "REGULAR",
                "Rua dos Ipês, 351"));

            await _repository.AddUserAsync(
                new AddUserDTO(
                "fefe@email.com",
                "Fernanda Fatima",
                "senha123",
                "COMUM",
                "Rua das Margaridas, 9668"));

            //When searching for name (Ana). 
            var list = _repository.GetUserByName("Ana");

            // Then I get all users with this name (Ana).
            Assert.AreEqual(2, list.Count);
        }


        // Test GetUserByType
        [TestMethod]
        public async Task GetUsersByTypeReturnTwo()
        {
             // Defining context
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
            .UseInMemoryDatabase(databaseName: "db_projectecommerce")
            .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new UserImplements(_context);

            //Given that I register 3 users into databasE
            await _repository.AddUserAsync(
                new AddUserDTO(
                "Maridias@email.com",
                "Mariana Dias",
                "senha123",
                "VULNERABILITY",
                "Rua dos Ipês, 898"));

            await _repository.AddUserAsync(
                new AddUserDTO(
                "felima@email.com",
                "Fernando Lima",
                "senha123",
                "VULNERABILITY",
                "Rua das Margaridas, 98"));

            await _repository.AddUserAsync(
                new AddUserDTO(
                "barbara@email.com",
                "Barbara Paz",
                "094628",
                "REGULAR",
                "Rua das Araras, 666"));

            //When searching for type(VULNERABILITY).
            var list = await _repository.GetUserByTypeAsync("VULNERABILITY");

            //Then I get all users with this type (VULNERABILITY).
            Assert.AreEqual(2, list.Count);
        }
    }
}