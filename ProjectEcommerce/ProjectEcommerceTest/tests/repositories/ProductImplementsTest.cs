using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectEcommerce.src.data;
using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.repositories;
using ProjectEcommerce.src.repositories.implements;
using System.Linq;

namespace ProjectEcommerceTest.tests.repositories
{
    /// <summary>
    /// <para>Resume: Test to verify if the implementation of products worked</para>
    /// <para>Created by: Ítalo Penha and Joceline Gutierrez</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 05/05/2022</para>
    /// </summary>     

    [TestClass]
    public class ProductImplementsTest
    {
        private ProjectEcommerceContext _context;
        private IProduct _repository;

        [TestMethod]

        public void CreateFourProductsOnDatabaseReturnFour()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
                .UseInMemoryDatabase(databaseName: "db_projectecommerce1")
                .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new ProductImplements(_context);

            //GIVEN that I register 4 products into Database
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

            //WHEN searching full list
            //THEN returns 4 products 
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

           // GIVEN that the product is in the system
           _repository.NewProduct(
                new NewProductDTO(
                    "Maracujá",
                    7.50f,
                    "fotinho maracujá.img",
                    "Maracujá amarelinha",
                    5));


           // WHEN I update the product
            _repository.UpdateProduct(
               new UpdateProductDTO(
                   1,
                   "Laranja",
                    5.00f,
                    "foto laranja.img",
                    "Laranja Lima",
                    6));

           // THEN I have the updated product
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

            //GIVEN that the product is in the system
            _repository.NewProduct(
                new NewProductDTO(
                    "Maracujá",
                    7.50f,
                    "fotinho maracujá.img",
                    "Maracujá amarelinha",
                    5));

            // WHEN I delete the product with id equals 1 
            _repository.DeleteProduct(1);

            //THEN should return null
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

            //GIVEN that I register 3 products
            _repository.NewProduct(
                new NewProductDTO(
                    "Alface",
                    3.33f,
                    "fotinho alface.img",
                    "Alface Verde",
                    14));

            _repository.NewProduct(
                new NewProductDTO(
                    "Cebola",
                    7.80f,
                    "cebola.img",
                    "Cebola roxa",
                    8));

            _repository.NewProduct(
                new NewProductDTO(
                    "Cenoura",
                    5.50f,
                    "fotinho cenorinha.img",
                    "Cenoura laranja",
                    13));

            //WHEN I search all the products
            var list = _repository.GetAllProducts();

            //THEN return list of all products
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void GetProductByIdReturnNotNull()
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
                .UseInMemoryDatabase(databaseName: "db_projectecommerce2")
                .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new ProductImplements(_context);

            //GIVE that I register 1 products 
            _repository.NewProduct(
                new NewProductDTO(
                    "Limão",
                    4.50f,
                    "limãozinho.png",
                    "Limão verdinho",
                    20));

            //WHEN I search this product by id 5 
            var product = _repository.GetProductById(1);

            //THEN I obtain a product
            Assert.IsNotNull(product);
        }

        [TestMethod]
        [DataRow("Ma", null)]
        public void GetProductBySearchReturnCustom(
            string nameProduct,
            string descriptionProduct)
        {
            var opt = new DbContextOptionsBuilder<ProjectEcommerceContext>()
                .UseInMemoryDatabase(databaseName: "db_projectecommerce3")
                .Options;

            _context = new ProjectEcommerceContext(opt);
            _repository = new ProductImplements(_context);

            //GIVEN that I register 4 products into Database
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


            // WHEN I search in the name "Ma" without description
            var products = _repository
            .GetProductBySearch(nameProduct, descriptionProduct);

            // THEN I receive 3 products
            Assert.AreEqual(3, products.Count());
        }
    }
}