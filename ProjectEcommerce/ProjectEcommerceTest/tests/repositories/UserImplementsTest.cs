using System.Linq;
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
    public class UserRepositoryTest
    {
        private ProjectEcommerceContext _context;
        private IUser _repository;

        [TestInitialize]
        public void InitialSetting()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
            .UseInMemoryDatabase(databaseName: "db_projectecommerce")
            .Options;
            _context = new ProjectEcommerceContext(opt);
            _repository = new UserRepository(_context);
        }



        // Test AddUser
        [TestMethod]
        public void A01_CreateThreeUsersIntoDatabaseReturnThreeUsers()
        {
            //Given that I register 3 users into database
            _repository.AddUser(
            new AddUserDTO(
            "guigrillo@email.com",
            "Guilherme Grillo",
            "134652",
            "COMUM",
            "Rua das Paineiras, 122, apto 8"));

            _repository.AddUser(
            new AddUserDTO(
            "karolcoli@email.com",
            "Karol Oliveira",
            "135247",
            "COMUM",
            "Rua dos Ipês, 351"));

            _repository.AddUser(
            new AddUserDTO(
            "jocelineg@email.com",
            "Joceline Gutierrez",
            "235837",
            "COMUM",
            "Rua das Margaridas, 9668"));

            //When searching full list. Then I get 3 users.
            Assert.AreEqual(3, _context.Users.Count());
        }



        // Test UpdateUser
        [TestMethod]
        public void A02_UpdateUserReturnUpdatedUser()
        {
            // Given that I register a user in the database
            _repository.AddUser(
            new AddUserDTO(
            "brazolas@email.com",
            "Matheus Brazolin",
            "brabra1234",
            "COMUM",
            "Rua das Rosas, 123"));

            //When we update the user
            var old = _repository.GetUserByEmail("Matheus Brazolin");
            _repository.UpdateUser(
            new UpdateUserDTO(_context.Users.FirstOrDefault(u => u.Email == "brazolas@email.com").Id,
            "Brazolin",
            "mat00",
            "COMUM",
            "Rua das Jararacas, 80"));

            //Then, it should return a new name
            Assert.AreEqual("Brazolin", _context.Users.FirstOrDefault(u => u.Id == old.Id).Name);

            // And, it should return password mat00
            Assert.AreEqual("mat00", _context.Users.FirstOrDefault(u => u.Id == old.Id).Password);
        }



        // Test DeleteUser
        [TestMethod]
        public void A03_DeletedUserReturnTwo()
        {
            //Given that I register users in the database
            _repository.AddUser(
            new AddUserDTO(
            "joao@email.com",
            "João Silva",
            "79467828",
            "VULNERABILIDADE",
            "Rua das Amoras, 784"));

            //When I delete this user
            _repository.DeleteUser(5);

            //Then, shoulbe be null
            Assert.IsNull(_repository.GetUserById(5));
        }



        // Test GetUserByID
        [TestMethod]
        public void A04_GetUserByIdReturnNotNullName()
        {
            //Given that I register an user in the database
            _repository.AddUser(
            new AddUserDTO(
            "jocelineg@email.com",
            "Joceline Gutierrez",
            "235837",
            "COMUM",
            "Rua das Margaridas, 9668"));

            //When I search for id number 9
            var user = _repository.GetUserById(9);

            //Then, it should return a not null element

            Assert.IsNotNull(user);

            //And the element should be Joceline Gutierrez

            Assert.AreEqual("Joceline Gutierrez", user.Name);
        }



        // Test GetUserByEmail
        [TestMethod]
        public void A05_GetUserByEmailReturnNotNull()
        {
            //Given that I register an user in the database
            _repository.AddUser(
            new AddUserDTO(
            "leleo@email.com",
            "Leonardo Sarto",
            "senha123",
            "COMUM",
            "Rua das Maritacas, 98"));

            //When I search for this user's email
            var user = _repository.GetUserByEmail("leleo@email.com");

            //Then, I get this user
            Assert.IsNotNull(user);
        }



        // Test GetUserByName
        [TestMethod]
        public void A06_GetUsersByNameReturnList()
        {
            //Given that I register 3 users into database
            _repository.AddUser(
            new AddUserDTO(
            "AnaPaula@email.com",
            "Ana Paula",
            "senha123",
            "COMUM",
            "Rua das Paineiras, 122, apto 8"));

            _repository.AddUser(
            new AddUserDTO(
            "Maryany@email.com",
            "Ana Maria",
            "senha123",
            "COMUM",
            "Rua dos Ipês, 351"));

            _repository.AddUser(
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
        public void A07_GetUsersByTypeReturnList()
        {
            //Given that I register 3 users into databasE
            _repository.AddUser(
            new AddUserDTO(
            "Maridias@email.com",
            "Mariana Dias",
            "senha123",
            "COMUM",
            "Rua dos Ipês, 898"));

            _repository.AddUser(
            new AddUserDTO(
            "felima@email.com",
            "Fernando Lima",
            "senha123",
            "VULNERABILIDADE",
            "Rua das Margaridas, 98"));

            _repository.AddUser(
            new AddUserDTO(
            "barbara@email.com",
            "Barbara Paz",
            "094628",
            "VULNERABILIDADE",
            "Rua das Araras, 666"));

            //When searching for type(VULNERABILIDADE).
            var list = _repository.GetUserByType("VULNERABILIDADE");

            //Then I get all users with this type (Vulnerabilidade).
            Assert.AreEqual(2, list.Count);
        }
    }
}