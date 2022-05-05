using BlogPessoal.src.dtos;
using ProjectEcommerce.src.models;
using System.Collections.Generic;

namespace ProjectEcommerce.src.repositories
{
    public interface IProduct 
    {
        void NewProduct(NewProductDTO post);
        void UpdateProduct(UpdateProductDTO post);
        void DeleteProduct(int id);
        ProductModel GetProductById(int id);
        List<ProductModel> GetAllProducts();
        List<ProductModel> GetProductBySearch(string name, string descriptionProduct);

    }
}
