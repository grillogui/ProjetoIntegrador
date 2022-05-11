using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult DeleteProduct([FromRoute] int idProduct)
        {
            _repository.DeleteProduct(idProduct);
            return NoContent();
        }

        [HttpGet("list")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public IActionResult GetAllProducts()
        {
            var list = _repository.GetAllProducts();
            if (list.Count < 1) return NoContent();
            return Ok(list);
        }

        [HttpGet("id/{idProduct}")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public IActionResult GetProductById([FromRoute] int idProduct)
        {
            var Product = _repository.GetProductById(idProduct);
            if (Product == null) return NotFound();
            return Ok(Product);
        }

        [HttpGet("search")]
        [Authorize(Roles = "REGULAR, VULNERABILITY, ADMINISTRATOR")]
        public IActionResult GetProductBySearch(
                [FromQuery] string nameProduct,
                [FromQuery] string descriptionProduct)
                
        {
            var products = _repository.GetProductBySearch(nameProduct, descriptionProduct);

            if (products.Count< 1) return NoContent();
            return Ok(products);
        }

        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult NewProduct([FromBody] NewProductDTO product)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.NewProduct(product);
            return Created($"api/Products/id/{product.Name}", product);
        }

        [HttpPut]
        [Authorize(Roles = "ADMINISTRATOR")]
        public IActionResult UpdateProduct([FromBody] UpdateProductDTO product)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.UpdateProduct(product);
            return Ok(product);
        }

        #endregion
    }
}

    

