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
    /// <summary>
    /// <para>Class responsible for implementing IPurchase</para>
    /// <para>Criado por: Leonardo Sarto </para>
    /// <para>Versão 1.0</para>
    /// <para>Data </para>
    /// </summary>
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

        /// <summary>
        /// <para>Summary: Asynchronous method to save a new purchase</para>
        /// </summary>
        /// <param name="purchase">NewPurchaseDTO</param>
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

        /// <summary>
        /// <para>Sumary: Asynchronous method to delete a purchase</para>
        /// </summary>
        /// <param name="id">purchase id</param>
        public async Task DeletePurchaseAsync(int id)
        {
            _context.Purchases.Remove(await GetPurchaseByIdAsync(id));
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <para>Sumary: Asynchronous method to get all purchases</para>
        /// </summary>
        /// <returns>PurchaseModel List</returns>
        public async Task<List<PurchaseModel>> GetAllPurchasesAsync()
        {
            return await _context.Purchases
                .Include(p => p.Items)
                .Include(p => p.Buyer)
                .ToListAsync();
        }

        /// <summary>
        /// <para>Sumary: Asynchronous method to get a purchase by Id</para>
        /// </summary>
        /// <param name="id">Purchase id</param>
        /// <returns>PurchaseModel</returns>
        public async Task <PurchaseModel> GetPurchaseByIdAsync(int id)
        {
            return await _context.Purchases
                .Include(p => p.Items)
                .Include(p => p.Buyer)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        /// <summary>
        /// <para>Asynchronous method to get the purchase quantity of the product</para>
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <returns>PurchaseModel</returns>
        public async Task <int> GetQuantityPurchaseProductAsync (int productId)
        {
            var list = await _context.Purchases 
                .Include(p => p.Items)
                .Include(p => p.Buyer)
                .Where(p => p.Items.Id == productId).ToListAsync();
            if(list.Count == 0)  return 0;

            return list.Count;
        }
        #endregion
    }
}