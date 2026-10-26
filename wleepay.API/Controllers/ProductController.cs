using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using wleepay.Core.Models;
using wleepay.Core.Services;
using wleepay.Core.Services.wleepay.Services.Interfaces;

namespace wleepay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ISystemActivityLogsService _activityLogService;

        public ProductController(IProductService productService, ISystemActivityLogsService activityLogService)
        {
            _productService = productService ?? throw new ArgumentNullException(nameof(productService));
            _activityLogService = activityLogService ?? throw new ArgumentNullException(nameof(activityLogService));
        }

        [HttpGet("operators")]
        [ProducesResponseType(typeof(IEnumerable<OperatorDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetOperators()
        {
            try
            {
                var operators = await _productService.GetAllOperatorsAsync();
                return Ok(operators);
            }
            catch (Exception ex)
            {
                await LogFailureAsync("Get Operators Failed", $"Failed to retrieve operators. Error: {ex.Message}");
                return StatusCode(500, new { message = "Error retrieving operators", error = ex.Message });
            }
        }

        [HttpGet("operators/{sochitelId}")]
        [ProducesResponseType(typeof(OperatorDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetOperatorBySochitelId(string sochitelId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sochitelId))
                    return BadRequest(new { message = "Sochitel ID is required" });

                var operatorDto = await _productService.GetOperatorBySochitelIdAsync(sochitelId);
                return Ok(operatorDto);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                await LogFailureAsync("Get Operator Failed", $"Failed to retrieve operator with Sochitel ID {sochitelId}. Error: {ex.Message}");
                return StatusCode(500, new { message = "Error retrieving operator", error = ex.Message });
            }
        }
        [HttpGet("operators/{sochitelOperatorId}/products")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
        public async Task<IActionResult> GetProductsByOperator(string sochitelOperatorId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sochitelOperatorId))
                    return BadRequest(new { message = "Sochitel Operator ID is required" });

                var products = await _productService.GetProductsByOperatorAsync(sochitelOperatorId);
                return Ok(products);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                await LogFailureAsync("Get Products Failed", $"Failed to retrieve products for operator {sochitelOperatorId}. Error: {ex.Message}");
                return StatusCode(500, new { message = "Error retrieving products", error = ex.Message });
            }
        }

        /// <summary>
        /// GET api/product/{id}
        /// Returns a specific product by its ID
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDto), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id.ToString());
                return Ok(product);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                await LogFailureAsync("Get Product Failed", $"Failed to retrieve product {id}. Error: {ex.Message}");
                return StatusCode(500, new { message = "Error retrieving product", error = ex.Message });
            }
        }

        /// <summary>
        /// GET api/product/all
        /// Returns all products
        /// </summary>
        [HttpGet("all")]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), 200)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var products = await _productService.GetAllProductsAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                await LogFailureAsync("Get All Products Failed", $"Failed to retrieve all products. Error: {ex.Message}");
                return StatusCode(500, new { message = "Error retrieving products", error = ex.Message });
            }
        }

        private async Task LogFailureAsync(string action, string description)
        {
            try
            {
                await _activityLogService.AddSystemLogs(new SystemActivityLog
                {
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    Action = action,
                    Description = description,
                    CreatedDate = DateTime.UtcNow
                });
            }
            catch
            {
                // A logging failure must never change the response returned to the caller.
            }
        }
    }
}
