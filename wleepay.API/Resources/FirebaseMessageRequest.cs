namespace wleepay.API.Resources
{
    public class FirebaseMessageRequest
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string DeviceToken { get; set; }
        public string DeviceId { get; set; }

        public string UserId { get; set; }
        // Add more properties as needed based on your notification requirements
        
    }
}
