using Microsoft.EntityFrameworkCore;

public class NotificationDbService : INotificationService
{
    private readonly DbContext _context;

    public NotificationDbService(DbContext context)
    {
        _context = context;
    }

    // Crear una nueva notificación
    public Notification Create(NotificationPutPostDTO notificationDto)
    {
        Notification notification = new()
        {
            Text = notificationDto.Text
        };

        _context.Notifications.Add(notification);
        _context.SaveChanges(); // Guardar cambios en la base de datos
        return notification;
    }

    // Eliminar una notificación por su ID
    public void Delete(int id)
    {
        var notification = _context.Notifications.Find(id);
        if (notification != null)
        {
            _context.Notifications.Remove(notification);
            _context.SaveChanges(); // Guardar los cambios
        }
    }

    // Obtener todas las notificaciones
    public IEnumerable<Notification> GetAll()
    {
        return _context.Notifications;
    }

    // Obtener una notificación por su ID
    public Notification? GetById(int id)
    {
        return _context.Notifications.Find(id);
    }

    // Actualizar una notificación existente
    public Notification? Update(int id, NotificationPutPostDTO notificationToUpdate)
    {
        var existingNotification = _context.Notifications.Find(id);
        if (existingNotification != null)
        {
            existingNotification.Text = notificationToUpdate.Text;

            _context.Entry(existingNotification).State = EntityState.Modified;
            _context.SaveChanges(); // Guardar cambios
            return existingNotification;
        }

        return null;
    }
}
