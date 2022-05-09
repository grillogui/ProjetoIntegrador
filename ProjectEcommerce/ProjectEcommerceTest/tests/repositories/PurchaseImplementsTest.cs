using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectEcommerce.src.data;
using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;
using ProjectEcommerce.src.repositories;
using ProjectEcommerce.src.repositories.implements;
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
        public void CrateSixNewPurchasesReturnSixPurchases1()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
               .UseInMemoryDatabase(databaseName: "db_projectecommerce1")
               .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new PurchaseImplements(_context);

            _repository.NewPurchase(
                new NewPurchaseDTO(
                    "léo@gmail.com",
                    "Laranja"));

            _repository.NewPurchase(
                new NewPurchaseDTO(
                    "math@gmail.com",
                    "Pera"));

            _repository.NewPurchase(
                new NewPurchaseDTO(
                    "karol@gmail.com",
                    "Uva"));

            _repository.NewPurchase(
                new NewPurchaseDTO(
                    "gui@gmail.com",
                    "Melância"));

            _repository.NewPurchase(
                new NewPurchaseDTO(
                    "joci@gmail.com",
                    "Jaboticaba"));

            _repository.NewPurchase(
                new NewPurchaseDTO(
                    "italo@gmail.com",
                    "Amora"));

            Assert.AreEqual(6, _context.Purchases.Count());
            Assert.AreEqual(6, _context.Purchases.Include(p => p.Items).ToList().Count);
        }

        //deletando uma compra da lista acima e retornando a lista atualizada
        [TestMethod]
        public void DeletingPurchaseAndReturningUpdatedPurchases2()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
               .UseInMemoryDatabase(databaseName: "db_projectecommerce2")
               .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new PurchaseImplements(_context);

            _repository.NewPurchase(
               new NewPurchaseDTO(
                   "italo@gmail.com",
                   "Amora"));

            _repository.NewPurchase(
              new NewPurchaseDTO(
                  "gui@gmail.com",
                  "Uva"));

            _repository.NewPurchase(
              new NewPurchaseDTO(
                  "joce@gmail.com",
                  "Pêra"));

            _repository.DeletePurchase(1);

            Assert.AreEqual(2, _context.Purchases.Count());

        }

        //pegando compra pelo id e retornando seu email
        [TestMethod]
        
        public void GetPurchaseByIdAndReturnBuyersEmail3()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
               .UseInMemoryDatabase(databaseName: "db_projectecommerce3")
               .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new PurchaseImplements(_context);

            //GIVEN that I have a user and product created in the bank

            _context.Users.Add(new UserModel
            {
                Email = "leonardo@email.com",
                Name = "leonardo",
                Password = "123456",
                Type = "COMUM",
                Address = "Rua das Flores"
            });

            _context.Users.Add(new UserModel
            {
                Email = "matheus@email.com",
                Name = "matheus",
                Password = "654321",
                Type = "COMUM",
                Address = "Travessa dos Jardins"
            });

            _context.Products.Add(new ProductModel
            {
                Name = "Banana",
                Price = 6.00f,
                Image = "URLIMAGE",
                Description = "Banana Nanica",
                Quantity = 1.000f
            });

            _context.Products.Add(new ProductModel
            {
                Name = "Laranja",
                Price = 4.50f,
                Image = "URLIMAGE",
                Description = "Laranja Pera",
                Quantity = 1.500f
            });

            _repository.NewPurchase(new NewPurchaseDTO("leonardo@email.com", "Banana"));
            _repository.NewPurchase(new NewPurchaseDTO("matheus@email.com", "Laranja"));

            var user = _repository.GetPurchaseById(2);

            Assert.AreEqual("matheus@email.com",);

        }

        //pegando todas as compras 
        [TestMethod]
        public void GetAllPurchases4()
        {
           var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
               .UseInMemoryDatabase(databaseName: "db_projectecommerce4")
               .Options;

           _context = new ProjectEcommerceContext(opt);
           _repository = new PurchaseImplements(_context);

            //GIVEN that I have a user and product created in the bank

            _context.Users.Add(new UserModel
            {
                Email = "leonardo@email.com",
                Name = "leonardo",
                Password = "123456",
                Type = "COMUM",
                Address = "Rua das Flores"
            });

            _context.Users.Add(new UserModel
            {
                Email = "matheus@email.com",
                Name = "matheus",
                Password = "654321",
                Type = "COMUM",
                Address = "Travessa dos Jardins"
            });

            _context.Products.Add(new ProductModel
            {
                Name = "Banana",
                Price = 6.00f,
                Image = "URLIMAGE",
                Description = "Banana Nanica",
                Quantity = 1.000f
            });

            _context.Products.Add(new ProductModel
            {
                Name = "Laranja",
                Price = 4.50f,
                Image = "URLIMAGE",
                Description = "Laranja Pera",
                Quantity = 1.500f
            });

            _context.Products.Add(new ProductModel
            {
                Name = "Abacaxi",
                Price = 10.0f,
                Image = "URLIMAGE",
                Description = "Abacaxi Perola",
                Quantity = 2.000f
            });

            //AND - I have sales

            _repository.NewPurchase(new NewPurchaseDTO("leonardo@email.com", "Banana"));
            _repository.NewPurchase(new NewPurchaseDTO("matheus@email.com", "Abacaxi"));
            _repository.NewPurchase(new NewPurchaseDTO("leonardo@email.com", "Laranja"));


            //WHEN I search all purchases
            var list = _repository.GetAllPurchases();

            //THEN return list of all purchases
            Assert.AreEqual(3, list.Count);
        }

        //pegando todas as compras
        [TestMethod]
        public void GetPurchaseCountByProductName5()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
               .UseInMemoryDatabase(databaseName: "db_projectecommerce5")
               .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new PurchaseImplements(_context);

            //GIVEN - Dado que tenho usuario e produto criado no banco
            _context.Users.Add(new UserModel
            {
                Email = "gustavo@email.com",
                Name = "Gustavo Boaz",
                Password = "134652",
                Type = "COMUN",
                Address = "Rua São Paulo"
            });

            _context.Products.Add(new ProductModel
            {
                Name = "Acerola",
                Price = 5.75f,
                Image = "URLIMAGE",
                Description = "Acerola Paulista",
                Quantity = 1.500f
            });

            //AND - E tenho Vendas
            _repository.NewPurchase(new NewPurchaseDTO("gustavo@email.com", "Acerola"));
            _repository.NewPurchase(new NewPurchaseDTO("gustavo@email.com", "Acerola"));
            _repository.NewPurchase(new NewPurchaseDTO("gustavo@email.com", "Acerola"));

            //WHEN - Quando pesquiso GetPurchaseProduct
            var count = _repository.GetPurchaseProduct(1);

            //var list2 = _context.Purchases.ToList();

            //THEN - Devo obter quantidade 3
            Assert.AreEqual(3, count);
        }


    }
}


