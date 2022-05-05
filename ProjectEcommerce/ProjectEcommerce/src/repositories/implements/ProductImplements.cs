using System.Collections.Generic;
using System.Linq;
using ProjectEcommerce.src.data;
using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;

namespace ProjectEcommerce.src.repositories.implements
{
    /// <summary>
    /// <para>Resume: Class responsible for implement methos CRUD Product.</para>
    /// <para>Created by: Ítalo Penha</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 05/05/2022</para>
    /// </summary>
    public class ProductImplements : IProduct
    {
        #region Attributes
       
        private readonly ProjectEcommerceContext _context;

        #endregion Attributes


        #region Constructors

        /// <summary>
        /// <para>Resume: Constructor of class.</para>
        /// </summary>
        /// <param name="context">ProjectEcommerceContext</param>


        public ProductImplements(ProjectEcommerceContext context)
        {
        	_context = context;
        }

        #endregion Constructors


        #region Methods

        /// <summary>
        /// <para>Resume: method for get product by id.</para>
        /// <para>Created by: Ítalo Penha</para>
        /// </summary>
         
        public void DeleteProduct(int id)
        {
            _context.Products.Remove(GetProductById(id));
            _context.SaveChanges();
        }

        /// <summary>
        /// <para>Resume: method for get all products by list.</para>
        /// <para>Created by: Ítalo Penha</para>
        /// </summary>

        public List<ProductModel> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        /// <summary>
        /// <para>Resume: method for get product by Id.</para>
        /// <para>Created by: Ítalo Penha</para>
        /// </summary>
         
        public ProductModel GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// <para>Resume: method for get product by search.</para>
        /// <para>Created by: Joceline Gutierrez</para>
        /// </summary>

        public List<ProductModel> GetProductBySearch(string nameProduct, string descriptionProduct)
        {
            switch (nameProduct, descriptionProduct)
            {
                case (null, null):
                    return GetAllProducts();

                case (null, _):
                    return _context.Products
                            .Where(p => p.Description.Contains(descriptionProduct))
                            .ToList();

                case (_, null):
                    return _context.Products
                            .Where(p => p.Name.Contains(nameProduct))
                            .ToList();

                case (_, _):
                    return _context.Products
                            .Where(p => p.Description.Contains(descriptionProduct) & p.Name.Contains(nameProduct))
                            .ToList();         
            }
        }

        /// <summary>
        /// <para>Resume: method for add product.</para>
        /// <para>Created by: Ítalo Penha</para>
        /// </summary>

        public void NewProduct(NewProductDTO product)
        {
            _context.Products.Add(new ProductModel
            {
                Name = product.Name,
                Price = product.Price,
                Image = product.Image,
                Description = product.Description,
                Quantity = product.Quantity

            });
            _context.SaveChanges();
        }

        /// <summary>
        /// <para>Resume: method for update product.</para>
        /// <para>Created by: Joceline Gutierrez</para>
        /// </summary>

        public void UpdateProduct(UpdateProductDTO product)
        {
            var existingProduct = GetProductById(product.Id);
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Image = product.Image;
            existingProduct.Description = product.Description;
            _context.Products.Update(existingProduct);
            _context.SaveChanges();
        }

        #endregion Methods
    }
}
