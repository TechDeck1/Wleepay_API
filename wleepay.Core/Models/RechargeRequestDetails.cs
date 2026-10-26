namespace wleepay.Core.Models
{
    public class RechargeRequestDetails
    {
        public string username { get; set; }
        public string salt { get; set; }
        public string password { get; set; }
        public string Operator { get; set; }
        public string ProductId { get; set; }      
        public int OperatorId { get; set; }


        public string Msisdn { get; set; }      
        public bool Simulate { get; set; }     
        public string? UserReference { get; set; }       
        public decimal Amount { get; set; }             
        public string Command { get; set; }             




    }
}
