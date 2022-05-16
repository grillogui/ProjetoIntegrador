using ProjectEcommerce.src.dtos;
using ProjectEcommerce.src.repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProjectEcommerce.src.models;
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

        /// <summary>
        /// Delete a product by id
        /// </summary>
        /// <param name="idProduct">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="204">Product deleted</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("delete/{idProduct}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> DeleteProductAsync([FromRoute] int idProduct)
        {
            await _repository.DeleteProductAsync(idProduct);
            return NoContent();
        }

        /// <summary>
        /// Get all products
        /// </summary>
        /// <returns>ActionResult</returns>
        /// <response code="200">List of products</response>
        /// <response code="204">Empty list</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("list")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> GetAllProductsAsync()
        {
            var list = await _repository.GetAllProductsAsync();

            if (list.Count < 1) return NoContent();
            return Ok(list);
        }

        /// <summary>
        /// Get product by id
        /// </summary>
        /// <param name="idProduct">int</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the product</response>
        /// <response code="404">Product not found</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("id/{idProduct}")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> GetProductByIdAsync([FromRoute] int idProduct)
        {
            var Product = await _repository.GetProductByIdAsync(idProduct);

            if (Product == null) return NotFound();
            return Ok(Product);
        }

        /// <summary>
        /// Get products by search
        /// </summary>
        /// <param name="nameProduct">string</param>
        /// <param name="descriptionProduct">string</param>
        /// <returns>ActionResult</returns>
        /// <response code="200">Returns the products</response>
        /// <response code="204">Product does not exist for this search</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpGet("search")]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> GetProductBySearchAsync(
                [FromQuery] string nameProduct,
                [FromQuery] string descriptionProduct)

        {
            var products = await _repository.GetProductBySearchAsync(nameProduct, descriptionProduct);

            if (products.Count < 1) return NoContent();
            return Ok(products);
        }

        /// <summary>
        /// Create new product
        /// </summary>
        /// <param name="product">NewProductDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        ///     POST /api/Products
        ///     {
        ///        "name": "Limão",
        ///        "price": 6.60,
        ///        "image": "URLFotoLmao",
        ///        "description": "Limão Siciliano",
        ///        "quantity": 13
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Return created product</response>
        /// <response code="400">Error in request</response>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ProductModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> NewProductAsync([FromBody] NewProductDTO product)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.NewProductAsync(product);
            return Created($"api/Products/id/{product.Name}", product);
        }

        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="product">UpdateProductDTO</param>
        /// <returns>ActionResult</returns>
        /// <remarks>
        /// Request example:
        ///
        ///     PUT /api/Products
        ///     {
        ///        "id": 1,    
        ///        "name": "Limão",
        ///        "price": 6.60,
        ///        "image": "URLFotoLmao",
        ///        "description": "Limão Siciliano",
        ///        "quantity": 13
        ///     }
        ///
        /// </remarks>
        /// <response code="200">Return updated product</response>
        /// <response code="400">Error in request</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        [Authorize(Roles = "ADMINISTRATOR")]
        public async Task<ActionResult> UpdateProductAsync([FromBody] UpdateProductDTO product)
        {
            if (!ModelState.IsValid) return BadRequest();

            await _repository.UpdateProductAsync(product);
            return Ok(product);
        }

        #endregion
    }
}