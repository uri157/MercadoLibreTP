using System.ComponentModel.DataAnnotations;

public class UserQuestionDTO
{
    [Required(ErrorMessage = "El campo Id es requerido.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo IdUser es requerido.")]
    public int IdUser { get; set; }

    [Required(ErrorMessage = "El campo IdPublication es requerido.")]
    public int IdPublication { get; set; }

    [Required(ErrorMessage = "El campo Question es requerido.")]
    [MaxLength(1000, ErrorMessage = "La pregunta no debe exceder los 1000 caracteres.")]
    public string? Question { get; set; }

    [MaxLength(1000, ErrorMessage = "La respuesta no debe exceder los 1000 caracteres.")]
    public string? Answer { get; set; } // Opcional
}
