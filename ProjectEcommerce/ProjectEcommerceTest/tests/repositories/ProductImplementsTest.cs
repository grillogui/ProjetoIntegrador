using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectEcommerce.src.data;
using ProjectEcommerce.src.dtos;
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
    public class ProductImplementsTest
    {
        private ProjectEcommerceContext _context;
        private IProduct _repository;

        [TestInitialize]
        public void InitialConfiguration()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
                .UseInMemoryDatabase(databaseName: "db_projectecommerce")
                .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new ProductImplements(_context);
        }

        [TestMethod]
        public void A01_CreateFourProductsOnDatabaseReturnFour()
        {

            //GIVEN - Dado que registro 4 produtos no banco
            _repository.NewProduct(
                new NewProductDTO(
                    "Banana",
                    8.00f,
                    "foto de bananinha",
                    "Banana nanica",
                    12));

            _repository.NewProduct(
                new NewProductDTO(
                    "Maçã",
                    5.54f,
                    "fotinho maçã.img",
                    "Maçã Gala",
                    8));

            _repository.NewProduct(
                new NewProductDTO(
                    "Manga",
                    10.00f,
                    "imagem manga",
                    "Manga Palmer",
                    20));

            _repository.NewProduct(
                new NewProductDTO(
                    "Maracujá",
                    7.50f,
                    "fotinho maracujá.img",
                    "Maracujá amarelinha",
                    5));

            //WHEN - Quando pesquiso lista total
            //THEN - Então recebo 4 produtos
            Assert.AreEqual(4, _context.Products.Count());
        }


    }
}