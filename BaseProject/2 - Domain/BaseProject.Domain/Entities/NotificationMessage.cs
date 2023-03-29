namespace BaseProject.Domain.Entities
{
    public class NotificationMessage
    {
        public NotificationMessage(string value)
        {
            Value = value;
        }

        public string Value { get; private set; }
    }
}
