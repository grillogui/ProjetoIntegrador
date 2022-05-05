using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;
using System.Collections.Generic;

namespace ProjectEcommerce.src.repositories
{
    /// <summary>
    /// <para>Resume: Interface responsible for representing CRUD actions products</para>
    /// <para>Created by: Joceline Gutierrez</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 05/05/2022</para>
    /// </summary>
    public interface IProduct 
    {
        void NewProduct(NewProductDTO product);
        void UpdateProduct(UpdateProductDTO product);
        void DeleteProduct(int id);
        ProductModel GetProductById(int id);
        List<ProductModel> GetAllProducts();
        List<ProductModel> GetProductBySearch(string nameProduct, string descriptionProduct);

    }
}
