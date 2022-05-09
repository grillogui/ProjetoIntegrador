using ProjectEcommerce.src.data;
using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;
using System.Collections.Generic;
using System.Linq;
using ProjectEcommerce.src.repositories.implements;
using Microsoft.EntityFrameworkCore;

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
        public void DeletePurchase(int id)
        {
            _context.Purchases.Remove(GetPurchaseById(id));
            _context.SaveChanges();
        }

        public List<PurchaseModel> GetAllPurchases()
        {
            return _context.Purchases.ToList();
        }

        public PurchaseModel GetPurchaseById(int id)
        {
            return _context.Purchases.FirstOrDefault(p => p.Id == id);
        }

        public List<PurchaseModel> GetPurchaseProduct (int productId)
        {
            return _context.Purchases
                .Include(p => p.Items)
                .Where(p => p.Items.Id == productId);
        }
        #endregion
    }
}