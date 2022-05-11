using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.repositories;

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
        public IActionResult NewPurchase([FromBody] NewPurchaseDTO purchase)
        {
            if (!ModelState.IsValid) return BadRequest();

            _repository.NewPurchase(purchase);
            return Created($"api/Purchases", purchase);
        }

        [HttpDelete("delete/{idPurchase}")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public IActionResult DeletePurchase([FromRoute] int idPurchase)
        {
            _repository.DeletePurchase(idPurchase);
            return NoContent();
        }

        [HttpGet("list")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult GetAllPurchases()
        {
            var list = _repository.GetAllPurchases();

            if (list.Count < 1) return NoContent();

            return Ok(list);
        }

        [HttpGet("id/{idPurchase}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult GetPurchaseById([FromRoute]int idPurchase)
        {
            var purchase = _repository.GetPurchaseById(idPurchase);
            if (purchase == null) return NotFound();
            return Ok(purchase);
            
        }

        [HttpGet("prod/{idProduct}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult GetQuantityPurchaseProduct([FromRoute]int idProduct)
        {
            var purchase = _repository.GetQuantityPurchaseProduct(idProduct);
              
            return Ok(purchase);
        }


        #endregion
    }
}
