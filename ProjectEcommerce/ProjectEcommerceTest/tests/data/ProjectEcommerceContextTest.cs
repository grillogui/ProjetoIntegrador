using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectEcommerce.src.data;
using ProjectEcommerce.src.models;
using ProjectEcommerce.src.utilities;
using System.Linq;

namespace ProjectEcommerceTest.tests.data
{
    [Ignore]
    [TestClass]
    public class ProjectEcommerceContextTest
    {
        private ProjectEcommerceContext _context;

        [TestInitialize]
        public void init()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
            .UseInMemoryDatabase(databaseName: "db_projectecommerce")
            .Options;

            _context = new ProjectEcommerceContext(opt);
        }

        [TestMethod]
        public void InsertNewUserIntoDatabaseReturnUser()
        {
            UserModel user = new UserModel();
		
		    user.Email = "italo@email.com";
            user.Name = "Ítalo Penha";
            user.Password = "134652";
            user.Type = TypeUser.REGULAR;
		    user.Address = "Rua Itália, nº 53";

            _context.Users.Add(user); // Adicionando usuário

            _context.SaveChanges(); // Comita Criação

            Assert.IsNotNull(_context.Users.FirstOrDefault(u => u.Email == "italo@email.com"));
        }
    }

}