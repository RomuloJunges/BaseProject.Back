using BaseProject.Domain.Entities;
using FluentValidation.Results;

namespace BaseProject.Service.Interfaces
{
    public interface INotificationService
    {
        IReadOnlyCollection<NotificationMessage> Notifications { get; }
        List<NotificationMessage> GetNotifications();
        bool HasNotifications { get; }
        void AddNotification(string message);
        void AddNotification(IEnumerable<NotificationMessage> notifications);
        void AddNotification(ValidationResult validationResult);
    }
}
