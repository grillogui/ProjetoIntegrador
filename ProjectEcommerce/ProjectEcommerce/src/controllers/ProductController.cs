using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ProjectEcommerce.src.controllers
{
    [ApiController]
    [Route("api/Products")]
    [Produces("application/json")]
    public class ProductController : ControllerBase
    {
        #region Atribute
        private readonly IProduct _repository;
        #endregion Atribute


        #region Constructors
        public ProductController(IProduct repository)
        {
            _repository = repository;
        }

        #endregion Constructors

        #region Methods

        [HttpDelete("delete/{idProduct}")]
        public async Task<ActionResult> DeleteProductAsync([FromRoute] int idProduct)
        {
            await _repository.DeleteProductAsync(idProduct);
            return NoContent();
        }

        [HttpGet("list")]
        public async Task<ActionResult> GetAllProductsAsync()
        {
            var list = await _repository.GetAllProductsAsync();

            if (list.Count < 1) return NoContent();
            return Ok(list);
        }

        [HttpGet("id/{idProduct}")]
        public async Task<ActionResult> GetProductByIdAsync([FromRoute] int idProduct)
        {
            var Product = await _repository.GetProductByIdAsync(idProduct);

            if (Product == null) return NotFound();
            return Ok(Product);
        }

        [HttpGet("search")]
        public async Task<ActionResult> GetProductBySearchAsync(
                [FromQuery] string nameProduct,
                [FromQuery] string descriptionProduct)

        {
            var products = await _repository.GetProductBySearchAsync(nameProduct, descriptionProduct);

            if (products.Count < 1) return NoContent();
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> NewProductAsync([FromBody] NewProductDTO product)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.NewProductAsync(product);
            return Created($"api/Products/id/{product.Name}", product);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateProductAsync([FromBody] UpdateProductDTO product)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.UpdateProductAsync(product);
            return Ok(product);
        }

        #endregion
    }
}