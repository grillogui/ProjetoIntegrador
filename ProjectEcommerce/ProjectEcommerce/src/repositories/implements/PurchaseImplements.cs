using ProjectEcommerce.src.data;
using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEcommerce.src.repositories.implements
{
    public class PurchaseImplements : IPurchase
    {
        #region Attributes
        private readonly ProjectEcommerceContext _context;
        #endregion Attributes

        #region Builders
        public PurchaseImplements(ProjectEcommerceContext context)
        {
            _context = context;
        }
        #endregion Builders

        #region Methods
        public void NewPurchase(NewPurchaseDTO purchase)
        {
            _context.Purchases.Add(new PurchaseModel
            {
                Buyer = _context.Users.FirstOrDefault(
                    u => u.Email == purchase.EmailBuyer),
                Items = _context.Products.FirstOrDefault(
                    p => p.Name == purchase.NameItems)
            });
            _context.SaveChanges();
        }
        public void DeletPurchase(int id)
        {
            _context.Purchases.Remove(GetPurchaseById(id));
            _context.SaveChanges();
        }

        public List<PurchaseModel> GetAllPurchases()
        {
            throw new System.NotImplementedException();
        }

        public PurchaseModel GetPurchaseById(int id)
        {
            throw new System.NotImplementedException();
        }

        public List<PurchaseModel> GetPurchaseProduct(int productId)
        {
            throw new System.NotImplementedException();
        }

        public List<PurchaseModel> GetPurchasesByUser(int userId)
        {
            throw new System.NotImplementedException();
        }
        #endregion Methods
    }
}
