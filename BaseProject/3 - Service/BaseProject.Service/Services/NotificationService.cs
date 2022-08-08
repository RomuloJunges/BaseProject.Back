using BaseProject.Domain.Entities;
using BaseProject.Service.Interfaces;
using FluentValidation.Results;

namespace BaseProject.Service.Services
{
    public abstract class NotificationService : INotificationService
    {
        private readonly List<NotificationMessage> _notifications;

        protected NotificationService()
        {
            _notifications = new List<NotificationMessage>();
        }

        public IReadOnlyCollection<NotificationMessage> Notifications => _notifications;

        public bool HasNotifications => _notifications.Any();

        public List<NotificationMessage> GetNotifications()
        {
            return _notifications;
        }
        
        public void AddNotification(string message)
        {
            _notifications.Add(new NotificationMessage(message));
        }

        public void AddNotification(IEnumerable<NotificationMessage> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotification(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                _notifications.Add(new NotificationMessage(error.ErrorMessage));
            }
        }
    }
}
