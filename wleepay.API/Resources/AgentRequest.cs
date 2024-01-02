namespace wleepay.API.Resources
{
    public class AgentRequest
    {
        public int Index { get; set; }
        public int Size { get; set; }
        public string CreatedBy { get; set; }
        public Core.Enums.Role Role { get; set; }

    }
}
