using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        Task NewProductAsync(NewProductDTO product);
        Task UpdateProductAsync(UpdateProductDTO product);
        Task DeleteProductAsync(int id);
        Task<ProductModel> GetProductByIdAsync(int id);
        Task<List<ProductModel>> GetAllProductsAsync();
        Task<List<ProductModel>> GetProductBySearchAsync(string nameProduct, string descriptionProduct);

    }
}
