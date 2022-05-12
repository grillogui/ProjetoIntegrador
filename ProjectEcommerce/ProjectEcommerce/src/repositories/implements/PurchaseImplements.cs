using ProjectEcommerce.src.data;
using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;
using System.Collections.Generic;
using System.Linq;
using ProjectEcommerce.src.repositories.implements;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

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
        public async Task NewPurchaseAsync(NewPurchaseDTO purchase)
        {
           await _context.Purchases.AddAsync(new PurchaseModel
            {
                Buyer =  _context.Users.FirstOrDefault(
                    u => u.Email == purchase.EmailBuyer),
                Items = _context.Products.FirstOrDefault(
                    p => p.Name == purchase.NameItems)
            });
            await _context.SaveChangesAsync();
        }
        public async Task DeletePurchaseAsync(int id)
        {
            _context.Purchases.Remove(await GetPurchaseByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        public async Task<List<PurchaseModel>> GetAllPurchasesAsync()
        {
            return await _context.Purchases.ToListAsync();
        }

        public async Task <PurchaseModel> GetPurchaseByIdAsync(int id)
        {
            return await _context.Purchases
                .Include(p => p.Items)
                .Include(p => p.Buyer)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task <int> GetQuantityPurchaseProductAsync (int productId)
        {
            var list = await _context.Purchases 
                .Include(p => p.Items)
                .Include(p => p.Buyer)
                .Where(p => p.Items.Id == productId).ToListAsync();
            if(list.Count == 0)  return 0;

            return list.Count + 1;
        }
        #endregion
    }
}