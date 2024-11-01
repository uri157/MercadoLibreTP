public interface INotificationService
{
    Notification Create(NotificationPutPostDTO notificationDto);
    void Delete(int id);
    IEnumerable<Notification> GetAll();
    Notification? GetById(int id);
    Notification? Update(int id, NotificationPutPostDTO notificationToUpdate);
}