using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.repositories;
using System.Threading.Tasks;

namespace ProjectEcommerce.src.controllers
{
    [ApiController]
    [Route("api/Purchases")]
    [Produces("application/json")]
    public class PurchaseController : ControllerBase
    {
        #region Attributes
        private readonly IPurchase _repository;
        #endregion

        #region Constructors
        public PurchaseController(IPurchase repository)
        {
            _repository = repository;
        }
        #endregion

        #region Methods

        [HttpPost]
        [Authorize(Roles = "REGULAR, VULNERABILITY,")]
        public async Task<ActionResult> NewPurchaseAsync([FromBody] NewPurchaseDTO purchase)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.NewPurchaseAsync(purchase);
            return Created($"api/Purchases", purchase);
        }

        [HttpDelete("delete/{idPurchase}")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public async Task<ActionResult> DeletePurchaseAsync([FromRoute] int idPurchase)
        {
            await _repository.DeletePurchaseAsync(idPurchase);
            return NoContent();
        }

        [HttpGet("list")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> GetAllPurchases()
        {
            var list = await _repository.GetAllPurchasesAsync();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        [HttpGet("id/{idPurchase}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> GetPurchaseByIdAsync([FromRoute]int idPurchase)
        {
            var purchase = await _repository.GetPurchaseByIdAsync(idPurchase);
            if (purchase == null) return NotFound();
            return Ok(purchase);
            
        }

        [HttpGet("prod/{idProduct}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> GetQuantityPurchaseProductAsync([FromRoute]int idProduct)
        {
            var purchase = await _repository.GetQuantityPurchaseProductAsync(idProduct);
              
            return Ok(purchase);
        }


        #endregion
    }
}
