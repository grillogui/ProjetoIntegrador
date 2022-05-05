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

        [TestMethod]
        public void A01_CreateFourProductsOnDatabaseReturnFour()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
                .UseInMemoryDatabase(databaseName: "db_projectecommerce1")
                .Options;
             _context = new ProjectEcommerceContext(opt);
            _repository = new ProductImplements(_context);

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
            Assert.AreEqual(4, _repository.GetAllProducts().Count);
        }

        [TestMethod]
        public  void UpdateProductReturnProductUpdated()
       {
           var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
                .UseInMemoryDatabase(databaseName: "db_projectecommerce2")
                .Options;
             _context = new ProjectEcommerceContext(opt);
            _repository = new ProductImplements(_context);

           // GIVEN - Dado que o produto esta no sistema
           _repository.NewProduct(
                new NewProductDTO(
                    "Maracujá",
                    7.50f,
                    "fotinho maracujá.img",
                    "Maracujá amarelinha",
                    5));


           // WHEN - Quando eu atualizado o produto
            _repository.UpdateProduct(
               new UpdateProductDTO(
                   1,
                   "Laranja",
                    5.00f,
                    "foto laranja.img",
                    "Laranja Lima",
                    6));

           // THEN - Eu tenho o produto atualizado
           Assert.AreEqual(
               "Laranja",
               _repository.GetProductById(1).Name
           );
           
           Assert.AreEqual(
               5.00f,
               _repository.GetProductById(1).Price
           );
           
           Assert.AreEqual(
               "foto laranja.img",
               _repository.GetProductById(1).Image
           );
           
           Assert.AreEqual(
               "Laranja Lima",
               _repository.GetProductById(1).Description
           );

           Assert.AreEqual(
               6,
               _repository.GetProductById(1).Quantity
           );
       }

       [TestMethod]
       public void DeleteProductReturnNull()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
                .UseInMemoryDatabase(databaseName: "db_projectecommerce3")
                .Options;
             _context = new ProjectEcommerceContext(opt);
            _repository = new ProductImplements(_context);

            //GIVEN - Dado que tenho um produto no sistema
           _repository.NewProduct(
                new NewProductDTO(
                    "Maracujá",
                    7.50f,
                    "fotinho maracujá.img",
                    "Maracujá amarelinha",
                    5));

            // WHEN - Quando deleto o produto 
            _repository.DeleteProduct(1);

            //THEN - Entao deve retornar nulo
            Assert.IsNull(_repository.GetProductById(1));
        }

        [TestMethod]
        public  void GetAllProducts()
       {
           var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
                .UseInMemoryDatabase(databaseName: "db_projectecommerce2")
                .Options;
             _context = new ProjectEcommerceContext(opt);
            _repository = new ProductImplements(_context);
        }
    }
}