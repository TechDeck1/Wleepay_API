namespace wleepay.Core.Models
{
    public class SochitelTransactions
    {
        public int Id { get; set; }
        public string TransactionId { get; set; }
        public string TransactionType { get; set; }
        public double TransactionAmount { get; set; }
        public double CommissionRate { get; set; }
        public string PhoneNumber {  get; set; }
        public string OperatorId { get; set; }
        public string ProductId { get; set; }
        public string UserReference { get; set; }   

    }
}
