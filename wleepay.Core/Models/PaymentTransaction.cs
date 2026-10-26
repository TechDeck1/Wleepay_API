using Microsoft.SqlServer.Management.Smo;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace wleepay.Core.Models
{
    public class PaymentTransaction
    {
        public int Id { get; set; }

        public string UserId { get; set; }  

        public string TransactionId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public string? ReceiverPhoneNumber { get; set; }

        public int TransferTypeId { get; set; }

        public int? CurrencyId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        //public string? Country { get; set; } = null;

        [NotMapped]
        public string? ErrorMessage { get; set; }

        public ApplicationUser User { get; set; }
        public Currency Currency { get; set; }

    }
}
