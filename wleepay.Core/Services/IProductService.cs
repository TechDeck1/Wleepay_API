using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace wleepay.Core.Services
{
  

    namespace wleepay.Services.Interfaces
    {
        public interface IProductService
        {
            /// <summary>
            /// Gets all operators
            /// </summary>
            Task<IEnumerable<OperatorDto>> GetAllOperatorsAsync();

            /// <summary>
            /// Gets an operator by its Sochitel ID
            /// </summary>
            Task<OperatorDto> GetOperatorBySochitelIdAsync(string sochitelId);

            /// <summary>
            /// Gets products by operator ID
            /// </summary>
            Task<IEnumerable<ProductDto>> GetProductsByOperatorAsync(string sochitelOperatorId);

            /// <summary>
            /// Gets a product by its ID
            /// </summary>
            Task<ProductDto> GetProductByIdAsync(string id);

            /// <summary>
            /// Gets all products
            /// </summary>
            Task<IEnumerable<ProductDto>> GetAllProductsAsync();

        }

        public class OperatorDto
        {
            public Guid Id { get; set; }

            public string SochitelId { get; set; }

            public string Name { get; set; }

            public string BrandId { get; set; }

            public string ProductTypeId { get; set; }

            public string ProductTypeName { get; set; }

            public List<string> ProductTypes { get; set; } = new List<string>();

            public List<string> ProductCategories { get; set; } = new List<string>();

            public List<string> Locations { get; set; } = new List<string>();

            public List<string> Prefixes { get; set; } = new List<string>();

            public string Currency { get; set; }

            public string CountryId { get; set; }

            public string CountryName { get; set; }

            public List<string> CountryPrefixes { get; set; } = new List<string>();

            public List<string> OperatorPrefixes { get; set; } = new List<string>();

            public int? MsisdnLengthMin { get; set; }

            public int? MsisdnLengthMax { get; set; }

            public bool IsDeleted { get; set; } = false;

            public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

            public DateTime? UpdatedAt { get; set; }
        }

        public class ProductDto
        {
            public Guid Id { get; set; }
            public string StripeProductId { get; set; }
            public string SochitelProductId { get; set; }
            public string SochitelOperatorId { get; set; }
            public decimal? PriceMin { get; set; }
            public decimal? PriceMax { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Category { get; set; }
            public string PriceType { get; set; }
            public decimal? Price { get; set; }
            public string Currency { get; set; }
        }

      
    }
}
