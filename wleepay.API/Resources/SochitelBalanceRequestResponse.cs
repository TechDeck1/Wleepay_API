namespace wleepay.API.Resources
{
    public class BalanceResult
    {
        public string Value { get; set; }
        public string Currency { get; set; }
    }

    public class SochitelBalanceResponse
    {
        public Status Status { get; set; }
        public string Command { get; set; }
        public long Timestamp { get; set; }
        public long Reference { get; set; }
        public BalanceResult Result { get; set; }
    }

}
