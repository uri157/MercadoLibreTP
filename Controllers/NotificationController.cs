using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/notifications")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    // Obtener todas las notificaciones
    //[Authorize]
    [HttpGet]
    public ActionResult<List<Notification>> GetAllNotifications()
    {
        return Ok(_notificationService.GetAll());
    }

    // Obtener una notificación por ID
    [HttpGet("{id}")]
    public ActionResult<Notification> GetById(int id)
    {
        var notification = _notificationService.GetById(id);
        if (notification == null)
        {
            return NotFound("Notification not found");
        }
        return Ok(notification);
    }

    // Crear una nueva notificación
    [HttpPost]
    public ActionResult<Notification> NewNotification(NotificationPutPostDTO notificationDto)
    {
        var newNotification = _notificationService.Create(notificationDto);
        return CreatedAtAction(nameof(GetById), new { id = newNotification.Id }, newNotification);
    }

    // Eliminar una notificación por ID
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var notification = _notificationService.GetById(id);
        if (notification == null)
        {
            return NotFound("Notification not found");
        }

        _notificationService.Delete(id);
        return NoContent();
    }

    // Actualizar una notificación por ID
    [HttpPut("{id}")]
    public ActionResult<Notification> UpdateNotification(int id, NotificationPutPostDTO notificationToUpdate)
    {
        
        var updatedNotification = _notificationService.Update(id, notificationToUpdate);
        if (updatedNotification == null)
        {
            return NotFound("Notification not found");
        }

        return CreatedAtAction(nameof(GetById), new { id = updatedNotification.Id }, updatedNotification);
    }
}
