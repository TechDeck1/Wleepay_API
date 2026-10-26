using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace wleepay.Core.Models
{
    [Table("Products")]
    public class Product
    {
        public Guid Id { get; set; }
        public string StripeProductId { get; set; }
        public string SochitelProductId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public string Category { get; set; }

        public string PriceType { get; set; }   

        public decimal? Price { get; set; }      
        public decimal? PriceMin { get; set; }   
        public decimal? PriceMax { get; set; }   

        public string Currency { get; set; }

        public string SochitelOperatorId { get; set; }

        public bool IsDeleted { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastSyncedAt { get; set; } = DateTime.UtcNow;
    }
}