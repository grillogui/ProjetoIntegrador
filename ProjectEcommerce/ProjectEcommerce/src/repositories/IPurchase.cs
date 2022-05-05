using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;
using System.Collections.Generic;

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
        void NewPurchase(NewPurchaseDTO purchase);
        void DeletPurchase(int id);
        PurchaseModel GetPurchaseById(int id);
        List<PurchaseModel> GetAllPurchases();
        List <PurchaseModel> GetPurchasesByUser(int userId);
        List <PurchaseModel> GetPurchaseProduct(int productId);
    }
}
