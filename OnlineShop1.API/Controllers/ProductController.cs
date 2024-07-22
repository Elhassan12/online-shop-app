using Microsoft.AspNetCore.Mvc;
using OnlineShop1.API.Extensions;
using OnlineShop1.API.Repository.Contracts;
using ShopOnline.Models.Dtos;

namespace OnlineShop1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController:ControllerBase
    {
        private readonly IProductRepository productRepository;

        public ProductController(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItems()
        {
            try
            {
                var products = await this.productRepository.GetItems();
                var productsCategories = await this.productRepository.GetCategories();


                if (products == null)
                {
                    return NotFound();
                }
                else
                {
                    var productDtos = products.ConvertToDto(productsCategories);

                    return Ok(productDtos);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetItem(int id)
        {
            try
            {
                var product = await this.productRepository.GetItem(id);

                if (product == null)
                {
                    return BadRequest();
                }
                else
                {

                    var productDto = product.ConvertToDto();

                    return Ok(productDto);
                }

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");

            }
        }

        /*[HttpGet]
        [Route(nameof(GetProductCategories))]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetProductCategories()
        {
            try
            {
                var productCategories = await productRepository.GetCategories();

               *//* var productCategoryDtos = productCategories.ConvertToDto();*//*

                return Ok(productCategoryDtos);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }

        }*/

        /*[HttpGet]
        [Route("{categoryId}/GetItemsByCategory")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetItemsByCategory(int categoryId)
        {
            try
            {
                var products = await productRepository.GetItemsByCategory(categoryId);

                var productDtos = products.ConvertToDto();

                return Ok(productDtos);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                "Error retrieving data from the database");
            }
        }*/

    }
}
