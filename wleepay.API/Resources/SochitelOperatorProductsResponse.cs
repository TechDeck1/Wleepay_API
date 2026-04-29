using System.Collections.Generic;
using wleepay.Core.DTO.Response;
namespace wleepay.API.Resources
{
    public class SochitelOperatorProductsResponse
    {
        public Statuss Status { get; set; }
        public string Command { get; set; }
        public long Timestamp { get; set; }
        public long Reference { get; set; }
        public OperatorProductResult Result { get; set; }

    }

    public class ProductCategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class PriceMin
    {
        public string Operator { get; set; }
        public string User { get; set; }
    }

    public class PriceMax
    {
        public string Operator { get; set; }
        public string User { get; set; }
    }

    public class Price
    {
        public PriceMin Min { get; set; }
        public PriceMax Max { get; set; }
        public string Operator { get; set; }
        public string User { get; set; }
    }

    public class Product
    {
        public string Id { get; set; }
        public ProductType ProductType { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public string PriceType { get; set; }
        public string Name { get; set; }
        public Price Price { get; set; }
    }

    public class OperatorProductResult
    {
        public ProductType Type { get; set; }
        public List<string> ProductTypes { get; set; }
        public Dictionary<string,Product> Products { get; set; }
        public Currency Currency { get; set; }
    }

    public class Currency
    {
        public string User { get; set; }
        public string Operator { get; set; }
    }
}
