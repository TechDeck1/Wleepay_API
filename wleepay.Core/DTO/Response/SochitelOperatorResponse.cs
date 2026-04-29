using System;
using System.Collections.Generic;
using System.Text;

namespace wleepay.Core.DTO.Response
{
    public class SochitelOperatorResponse
    {
        public Statuss Status { get; set; }
        public string Command { get; set; }
        public long Timestamp { get; set; }
        public long Reference { get; set; }
        public Dictionary<string, Operator> Result { get; set; }
    }

    public class Statuss
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
    }

    public class ProductType
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class MsisdnLength
    {
        public string Min { get; set; }
        public string Max { get; set; }
    }

    public class Country
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Prefixes { get; set; }
        public List<string> OperatorPrefixes { get; set; }
        public MsisdnLength MsisdnLength { get; set; }
    }

    public class Operator
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string BrandId { get; set; }
        public ProductType ProductType { get; set; }
        public List<string> ProductTypes { get; set; }
        public List<string> ProductCategories { get; set; }
        public string Currency { get; set; }
        public List<string> Locations { get; set; }
        public List<string> Prefixes { get; set; }
        public Country Country { get; set; }
    }

    public class ApiResponse
    {
        public Statuss Status { get; set; }
        public string Command { get; set; }
        public long Timestamp { get; set; }
        public long Reference { get; set; }
        public Dictionary<string, Operator> Result { get; set; }
    }
}
