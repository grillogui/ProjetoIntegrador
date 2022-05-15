using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
        /// <para>Resume: Asynchronous method to delete a product.</para>
        /// <para>Created by: Ítalo Penha</para>
        /// </summary>
        public async Task DeleteProductAsync(int id)
        {
            _context.Products.Remove(await GetProductByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to get all products by list.</para>
        /// <para>Created by: Ítalo Penha</para>
        /// </summary>
        public async Task<List<ProductModel>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to get product by Id.</para>
        /// <para>Created by: Ítalo Penha</para>
        /// </summary>
        public async Task<ProductModel> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to get product by search.</para>
        /// <para>Created by: Joceline Gutierrez</para>
        /// </summary>
        public async Task<List<ProductModel>> GetProductBySearchAsync(string nameProduct, string descriptionProduct)
        {
            switch (nameProduct, descriptionProduct)
            {
                case (null, null):
                    return await GetAllProductsAsync();

                case (null, _):
                    return await _context.Products
                            .Where(p => p.Description.Contains(descriptionProduct))
                            .ToListAsync();

                case (_, null):
                    return await _context.Products
                            .Where(p => p.Name.Contains(nameProduct))
                            .ToListAsync();

                case (_, _):
                    return await _context.Products
                            .Where(p => p.Description.Contains(descriptionProduct) & p.Name.Contains(nameProduct))
                            .ToListAsync();         
            }
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to add a new product.</para>
        /// <para>Created by: Ítalo Penha</para>
        /// </summary>
        public async Task NewProductAsync(NewProductDTO product)
        {
            await _context.Products.AddAsync(new ProductModel
            {
                Name = product.Name,
                Price = product.Price,
                Image = product.Image,
                Description = product.Description,
                Quantity = product.Quantity

            });
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Resume: Asynchronous method to update a product.</para>
        /// <para>Created by: Joceline Gutierrez</para>
        /// </summary>
        public async Task UpdateProductAsync(UpdateProductDTO product)
        {
            var existingProduct = await GetProductByIdAsync(product.Id);
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Image = product.Image;
            existingProduct.Description = product.Description;
            existingProduct.Quantity = product.Quantity;
            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
        }

        #endregion Methods
    }
}
