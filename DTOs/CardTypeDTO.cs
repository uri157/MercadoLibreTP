using System.ComponentModel.DataAnnotations;

public class CardTypeDTO
{
    [Required(ErrorMessage = "El campo Id es requerido.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo TypeName es requerido.")]
    [MaxLength(100, ErrorMessage = "El nombre del tipo de tarjeta no debe exceder los 100 caracteres.")]
    public string TypeName { get; set; }

    [MaxLength(200, ErrorMessage = "La descripción no debe exceder los 200 caracteres.")]
    public string? Description { get; set; }
}


public class CardTypePutDTO
{
    [Required(ErrorMessage = "El campo TypeName es requerido.")]
    [MaxLength(100, ErrorMessage = "El nombre del tipo de tarjeta no debe exceder los 100 caracteres.")]
    public string TypeName { get; set; }

    [MaxLength(200, ErrorMessage = "La descripción no debe exceder los 200 caracteres.")]
    public string? Description { get; set; }
}
