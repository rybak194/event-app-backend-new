namespace EventAppBackend_RestAPI.Models
{
    public class PushNotificationRequest
    {
        public string OcPassword { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }
}
