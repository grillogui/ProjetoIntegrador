using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectEcommerce.src.data;
using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;
using ProjectEcommerce.src.repositories;
using ProjectEcommerce.src.repositories.implements;
using ProjectEcommerce.src.utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectEcommerceTest.tests.repositories
{
    [TestClass]

    public class PurchaseImplementsTest
    {
        private ProjectEcommerceContext _context;
        private IPurchase _repository;

        //criando 6 novas compras
        [TestMethod]
        public async Task CrateSixNewPurchasesReturnSixPurchases1()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
               .UseInMemoryDatabase(databaseName: "db_projectecommerce1")
               .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new PurchaseImplements(_context);

            await _repository.NewPurchaseAsync(
                new NewPurchaseDTO(
                    "léo@gmail.com",
                    "Laranja"));

            await _repository.NewPurchaseAsync(
                new NewPurchaseDTO(
                    "math@gmail.com",
                    "Pera"));

            await _repository.NewPurchaseAsync(
                new NewPurchaseDTO(
                    "karol@gmail.com",
                    "Uva"));

            await _repository.NewPurchaseAsync(
                new NewPurchaseDTO(
                    "gui@gmail.com",
                    "Melância"));

            await _repository.NewPurchaseAsync(
                new NewPurchaseDTO(
                    "joci@gmail.com",
                    "Jaboticaba"));

            await _repository.NewPurchaseAsync(
                new NewPurchaseDTO(
                    "italo@gmail.com",
                    "Amora"));

            Assert.AreEqual(6, _context.Purchases.Count());
            Assert.AreEqual(6, _context.Purchases.Include(p => p.Items).ToList().Count);
        }

        //deletando uma compra da lista acima e retornando a lista atualizada
        [TestMethod]
        public async Task DeletingPurchaseAndReturningUpdatedPurchases2()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
               .UseInMemoryDatabase(databaseName: "db_projectecommerce2")
               .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new PurchaseImplements(_context);

            await _repository.NewPurchaseAsync(
               new NewPurchaseDTO(
                   "italo@gmail.com",
                   "Amora"));

            await _repository.NewPurchaseAsync(
              new NewPurchaseDTO(
                  "gui@gmail.com",
                  "Uva"));

            await _repository.NewPurchaseAsync(
              new NewPurchaseDTO(
                  "joce@gmail.com",
                  "Pêra"));

            await _repository.DeletePurchaseAsync(1);

            Assert.AreEqual(2, _context.Purchases.Count());

        }

        //pegando compra pelo id e retornando seu email
        [TestMethod]
        
        public async Task GetPurchaseByIdAndReturnBuyersEmail3()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
               .UseInMemoryDatabase(databaseName: "db_projectecommerce3")
               .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new PurchaseImplements(_context);

            //GIVEN that I have a user and product created in the bank

            await _context.Users.AddAsync(new UserModel
            {
                Email = "leonardo@email.com",
                Name = "leonardo",
                Password = "123456",
                TypeUser.REGULAR,
                Address = "Rua das Flores"
            });

           await _context.Users.AddAsync(new UserModel
            {
                Email = "matheus@email.com",
                Name = "matheus",
                Password = "654321",
               TypeUser.REGULAR,
               Address = "Travessa dos Jardins"
            });

            await _context.Products.AddAsync(new ProductModel
            {
                Name = "Banana",
                Price = 6.00f,
                Image = "URLIMAGE",
                Description = "Banana Nanica",
                Quantity = 1.000f
            });

            await _context.Products.AddAsync(new ProductModel
            {
                Name = "Laranja",
                Price = 4.50f,
                Image = "URLIMAGE",
                Description = "Laranja Pera",
                Quantity = 1.500f
            });

            await _repository.NewPurchaseAsync(new NewPurchaseDTO("leonardo@email.com", "Banana"));
            await _repository.NewPurchaseAsync(new NewPurchaseDTO("matheus@email.com", "Laranja"));

            var user = await _repository.GetPurchaseByIdAsync(2);

            Assert.AreEqual("matheus@email.com", user.Buyer.Email);

        }

        //pegando todas as compras 
        [TestMethod]
        public async Task GetAllPurchases4()
        {
           var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
               .UseInMemoryDatabase(databaseName: "db_projectecommerce4")
               .Options;

           _context = new ProjectEcommerceContext(opt);
           _repository = new PurchaseImplements(_context);

            //GIVEN that I have a user and product created in the bank

            await _context.Users.AddAsync(new UserModel
            {
                Email = "leonardo@email.com",
                Name = "leonardo",
                Password = "123456",
                TypeUser.REGULAR,
                Address = "Rua das Flores"
            });

            await _context.Users.AddAsync(new UserModel
            {
                Email = "matheus@email.com",
                Name = "matheus",
                Password = "654321",
                TypeUser.REGULAR,
                Address = "Travessa dos Jardins"
            });

            await _context.Products.AddAsync(new ProductModel
            {
                Name = "Banana",
                Price = 6.00f,
                Image = "URLIMAGE",
                Description = "Banana Nanica",
                Quantity = 1.000f
            });

            await _context.Products.AddAsync(new ProductModel
            {
                Name = "Laranja",
                Price = 4.50f,
                Image = "URLIMAGE",
                Description = "Laranja Pera",
                Quantity = 1.500f
            });

            await _context.Products.AddAsync(new ProductModel
            {
                Name = "Abacaxi",
                Price = 10.0f,
                Image = "URLIMAGE",
                Description = "Abacaxi Perola",
                Quantity = 2.000f
            });

            //AND - I have sales

            await _repository.NewPurchaseAsync(new NewPurchaseDTO("leonardo@email.com", "Banana"));
            await _repository.NewPurchaseAsync(new NewPurchaseDTO("matheus@email.com", "Abacaxi"));
            await _repository.NewPurchaseAsync(new NewPurchaseDTO("leonardo@email.com", "Laranja"));


            //WHEN I search all purchases
            var list = await _repository.GetAllPurchasesAsync();

            //THEN return list of all purchases
            Assert.AreEqual(3, list.Count);
        }

        //pegando todas as compras
        [TestMethod]
        public async Task GetPurchaseCountByProductName5()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
               .UseInMemoryDatabase(databaseName: "db_projectecommerce5")
               .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new PurchaseImplements(_context);

            //GIVEN - Dado que tenho usuario e produto criado no banco
            await _context.Users.AddAsync(new UserModel
            {
                Email = "gustavo@email.com",
                Name = "Gustavo Boaz",
                Password = "134652",
                TypeUser.REGULAR,
                Address = "Rua São Paulo"
            });

            await _context.Products.AddAsync(new ProductModel
            {
                Name = "Acerola",
                Price = 5.75f,
                Image = "URLIMAGE",
                Description = "Acerola Paulista",
                Quantity = 1.500f
            });

            //AND - E tenho Vendas
            await _repository.NewPurchaseAsync(new NewPurchaseDTO("gustavo@email.com", "Acerola"));
            await _repository.NewPurchaseAsync(new NewPurchaseDTO("gustavo@email.com", "Acerola"));
            await _repository.NewPurchaseAsync(new NewPurchaseDTO("gustavo@email.com", "Acerola"));

            //WHEN - Quando pesquiso GetPurchaseProduct
            var count = await _repository.GetQuantityPurchaseProductAsync(1);

            //var list2 = _context.Purchases.ToList();

            //THEN - Devo obter quantidade 3
            Assert.AreEqual(3, count);
        }


    }
}


