using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectEcommerce.src.repositories
{
    /// <summary>
    /// <para>Resume: Interface to represent CRUD actions in purchase</para>
    /// <para>Created by: Leonardo Sarto</para>
    /// <para>Version: 1.0</para>
    /// <para>Date: 05/05/2022</para>
    /// </summary>
    
    public interface IPurchase
    {
        Task NewPurchaseAsync(NewPurchaseDTO purchase);
        Task DeletePurchaseAsync(int id);
        Task <PurchaseModel> GetPurchaseByIdAsync(int id);
        Task<List<PurchaseModel>> GetAllPurchasesAsync();
        Task <int> GetQuantityPurchaseProduct (int productId);
    }
}
