namespace wleepay.API.Resources
{
    public class CashFlowHistoryResource
    {
        public int Id { get; set; }
        public long? WalletId { get; set; }
        public double? TotalAmount { get; set; }
        public double? DepositAmount { get; set; }
        public double? WithdrawAmount { get; set; }
    }
}
