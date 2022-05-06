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

            _repository.DeletePurchase(1);

            Assert.AreEqual(0, _context.Purchases.Count());

        }

        //pegando compra pelo id e retornando seu email
        [TestMethod]
        [Ignore]
        public void GetPurchaseByIdAndReturnBuyersEmail3()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
               .UseInMemoryDatabase(databaseName: "db_projectecommerce3")
               .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new PurchaseImplements(_context);

            var purchase= _repository.GetPurchaseById(5);

            Assert.AreEqual("joci@gmail.com", purchase.Buyer);

        }

        //pegando todas as compras 
        //[TestMethod]
        //public void GetAllPurchasesByIdAndReturnGrapeBuyerEmail()
        //{
        //
        //   var purchase =_reposository.GetPurchaseBy     
        //
        //}


        //pegando todas as compras
        [TestMethod]
        public void GetPurchaseProduct()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
               .UseInMemoryDatabase(databaseName: "db_projectecommerce4")
               .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new PurchaseImplements(_context);

            //GIVEN - Dado que tenho usuario e produto criado no banco
            _context.Users.Add(new UserModel{
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
            var count = _repository.GetPurchaseCountByProductName(1);

            //var list2 = _context.Purchases.ToList();

            //THEN - Devo obter quantidade 3
            Assert.AreEqual(3, count);
        }



    }
}


