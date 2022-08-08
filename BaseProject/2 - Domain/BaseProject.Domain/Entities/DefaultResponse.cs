namespace BaseProject.Domain.Entities
{
    public class DefaultResponse
    {
        public bool Success { get; set; }
        public dynamic Data { get; set; }

        public List<NotificationMessage> Messages { get; set; }
    }
}
