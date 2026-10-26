using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace wleepay.Core.Models
{
    [Table("Operators")]
    public class Operator
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
}