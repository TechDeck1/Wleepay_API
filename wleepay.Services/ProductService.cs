using global::wleepay.Core.Repositories;
using global::wleepay.Core.Services.wleepay.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using wleepay.Core.Models;
namespace wleepay.Services
{

        public class ProductService : IProductService
        {
            private readonly IProductRepository _productRepository;

            public ProductService(IProductRepository productRepository)
            {
                _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            }

            /// <summary>
            /// Gets all operators
            /// </summary>
            public async Task<IEnumerable<OperatorDto>> GetAllOperatorsAsync()
            {
                var operators = await _productRepository.GetAllOperatorsAsync();
                return operators.Select(MapOperatorToDto);
            }

            /// <summary>
            /// Gets an operator by its Sochitel ID
            /// </summary>
            public async Task<OperatorDto> GetOperatorBySochitelIdAsync(string sochitelId)
            {
                if (string.IsNullOrWhiteSpace(sochitelId))
                    throw new ArgumentException("Sochitel ID cannot be null or empty", nameof(sochitelId));

                var operatorEntity = await _productRepository.GetOperatorBySochitelIdAsync(sochitelId) ?? throw new InvalidOperationException($"Operator with Sochitel ID {sochitelId} not found");
                return MapOperatorToDto(operatorEntity);
            }

            /// <summary>
            /// Gets products by operator ID
            /// </summary>
            public async Task<IEnumerable<ProductDto>> GetProductsByOperatorAsync(string sochitelOperatorId)
            {
                if (string.IsNullOrWhiteSpace(sochitelOperatorId))
                    throw new ArgumentException("Sochitel Operator ID cannot be null or empty", nameof(sochitelOperatorId));

                var products = await _productRepository.GetProductsByOperatorAsync(sochitelOperatorId);
                return products.Select(MapProductToDto);
            }

            /// <summary>
            /// Gets a product by its ID
            /// </summary>
            public async Task<ProductDto> GetProductByIdAsync(string id)
            {

                var product = await _productRepository.GetProductByIdAsync(id);

                if (product == null)
                    throw new InvalidOperationException($"Product with ID {id} not found");

                return MapProductToDto(product);
            }

            /// <summary>
            /// Gets all products
            /// </summary>
            public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
            {
                var products = await _productRepository.GetAllProductsAsync();
                return products.Select(MapProductToDto);
            }

            private static OperatorDto MapOperatorToDto(Operator @operator)
            {
                return new OperatorDto
                {
                    Id = @operator.Id,
                    SochitelId = @operator.SochitelId,
                    Name = @operator.Name,
                    BrandId = @operator.BrandId,
                    ProductTypeId = @operator.ProductTypeId,
                    ProductTypeName = @operator.ProductTypeName,
                    ProductTypes = @operator.ProductTypes,
                    ProductCategories = @operator.ProductCategories,
                    Currency = @operator.Currency,
                    CountryId = @operator.CountryId,
                    CountryName = @operator.CountryName,
                    CountryPrefixes = @operator.CountryPrefixes,
                    Prefixes = @operator.Prefixes,
                    MsisdnLengthMin = @operator.MsisdnLengthMin,
                    MsisdnLengthMax = @operator.MsisdnLengthMax
                };
            }

            private static ProductDto MapProductToDto(Product product)
            {
                return new ProductDto
                {
                    Id = product.Id,
                    StripeProductId = product.StripeProductId,
                    SochitelProductId = product.SochitelProductId,
                    SochitelOperatorId = product.SochitelOperatorId,
                    Name = product.Name,
                    Type = product.Type,
                    Category = product.Category,
                    PriceType = product.PriceType,
                    Price = product.Price,
                    PriceMin = product.PriceMin,
                    PriceMax = product.PriceMax,
                    Currency = product.Currency
                };
            }

    }
}
