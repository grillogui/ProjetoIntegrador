using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.repositories;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult DeleteProduct([FromRoute] int idProduct)
        {
            _repository.DeleteProduct(idProduct);
            return NoContent();
        }

        [HttpGet("list")]
        public IActionResult GetAllProducts()
        {
            var list = _repository.GetAllProducts();
            if (list.Count < 1) return NoContent();
            return Ok(list);
        }

        [HttpGet("id/{idProduct}")]
        public IActionResult GetProductById([FromRoute] int idProduct)
        {
            var Product = _repository.GetProductById(idProduct);
            if (Product == null) return NotFound();
            return Ok(Product);
        }

        [HttpGet("search")] 
        public IActionResult GetProductBySearch(
                [FromQuery] string nameProduct,
                [FromQuery] string descriptionProduct)
                
        {
            var products = _repository.GetProductBySearch(nameProduct, descriptionProduct);

            if (products.Count< 1) return NoContent();
            return Ok(products);
        }

        [HttpPost]
        public IActionResult NewProduct([FromBody] NewProductDTO product)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.NewProduct(product);
            return Created($"api/Products/id/{product.Name}", product);
        }

        [HttpPut]
        public IActionResult UpdateProduct([FromBody] UpdateProductDTO product)
        {
            if (!ModelState.IsValid) return BadRequest();
            _repository.UpdateProduct(product);
            return Ok(product);
        }

        #endregion
    }
}

    

