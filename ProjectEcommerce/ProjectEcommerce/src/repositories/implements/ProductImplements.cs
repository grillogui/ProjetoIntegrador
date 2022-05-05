using System.Collections.Generic;
using System.Linq;
using BlogPessoal.src.dtos;
using ProjectEcommerce.src.data;
using ProjectEcommerce.src.models;

namespace ProjectEcommerce.src.repositories.implements
{
    public class ProductImplements : IProduct
    {
        #region Atributos
       
        private readonly ProjectEcommerceContext _context;
        
        #endregion Atributos

            
        #region Construtores
		
        public ProductImplements(ProjectEcommerceContext context)
        {
        	_context = context;
        }
        
        #endregion Construtores

     
        #region Métodos
        public void DeleteProduct(int id)
        {
            _context.Products.Remove(GetProductById(id));
            _context.SaveChanges();
        }

        public List<ProductModel> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public ProductModel GetProductById(int id)
        {
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }

        public List<ProductModel> GetProductBySearch(string name, string descriptionProduct)
        {
            throw new System.NotImplementedException();
        }

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

        public void UpdateProduct(UpdateProductDTO post)
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
