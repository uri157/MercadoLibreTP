using System.ComponentModel.DataAnnotations;
public class NotificationDTO
{
    [Required(ErrorMessage = "El campo Id es requerido.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Text es requerido.")]
    public string Text { get; set; }
}

public class NotificationPutPostDTO
{
    
    [Required(ErrorMessage = "El campo Text es requerido.")]
    public string Text { get; set; }
}