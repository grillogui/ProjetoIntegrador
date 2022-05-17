using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.models;
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

        /// <summary>
        /// Create new purchase
        /// </summary>
        /// <param name="purchase"></param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        /// 
        /// POST /api/Purchases
        /// 
        /// {
        /// "emailBuyer": usuario@email.com
        /// "nameItems": morango
        /// }
        /// 
        /// </remarks>
        /// <response code="201">Return purchase made</response>
        /// <response code="400">Request error</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PurchaseModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize(Roles = "REGULAR, VULNERABILITY,")]
        public async Task<ActionResult> NewPurchaseAsync([FromBody] NewPurchaseDTO purchase)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.NewPurchaseAsync(purchase);
            return Created($"api/Purchases", purchase);
        }

        /// <summary>
        /// Delete purchase by Id
        /// </summary>
        /// <param name="idPurchase"></param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Purchase deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idPurchase}")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public async Task<ActionResult> DeletePurchaseAsync([FromRoute] int idPurchase)
        {
            await _repository.DeletePurchaseAsync(idPurchase);
            return NoContent();
        }

        /// <summary>
        /// Get all purchases
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">Purchases list</response>
        /// <response code="204">Empty listt</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("list")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> GetAllPurchases()
        {
            var list = await _repository.GetAllPurchasesAsync();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        /// <summary>
        /// Get purchase by Id
        /// </summary>
        /// <param name="idPurchase"></param>
        /// <returns>ActionResult</returns>
        /// <response code="200">return purchase</response>
        /// <response code="404">purchase does not exist</response> 
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof (PurchaseModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idPurchase}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> GetPurchaseByIdAsync([FromRoute]int idPurchase)
        {
            var purchase = await _repository.GetPurchaseByIdAsync(idPurchase);
            if (purchase == null) return NotFound();
            return Ok(purchase);
            
        }

        /// <summary>
        /// Get the purchase quantity of the product
        /// </summary>
        /// <param name="idProduct"></param>
        /// <returns>ActionResult</returns>
        /// <response code="200">returns the purchase amount of that product</response>
        /// <response code="204">no purchases recorded on that item</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof (PurchaseModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("prod/{idProduct}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> GetQuantityPurchaseProductAsync([FromRoute]int idProduct)
        {
            var purchase = await _repository.GetQuantityPurchaseProductAsync(idProduct);

            if (purchase == 0) return NoContent();    
              
            return Ok(purchase);
        }


        #endregion
    }
}
