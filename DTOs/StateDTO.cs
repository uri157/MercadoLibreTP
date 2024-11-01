using System.ComponentModel.DataAnnotations;

public class StateDTO
{
    [Required(ErrorMessage = "El campo Id es requerido.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Name es requerido.")]
    [MaxLength(100, ErrorMessage = "El nombre no debe exceder los 100 caracteres.")]
    public string? Name { get; set; }

    [MaxLength(500, ErrorMessage = "La descripción no debe exceder los 500 caracteres.")]
    public string? Description { get; set; }
}
