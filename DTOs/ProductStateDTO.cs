using System.ComponentModel.DataAnnotations;

public class ProductStateDTO
{
    [Required(ErrorMessage = "El campo Id es requerido.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Name es requerido.")]
    [MaxLength(100, ErrorMessage = "El campo Name no puede exceder los 100 caracteres.")]
    public string Name { get; set; }

    [MaxLength(200, ErrorMessage = "El campo Description no puede exceder los 200 caracteres.")]
    public string Description { get; set; }
}

public class ProductStatePostPutDTO
{
    [Required(ErrorMessage = "El campo Name es requerido.")]
    [MaxLength(100, ErrorMessage = "El campo Name no puede exceder los 100 caracteres.")]
    public string Name { get; set; }

    [MaxLength(200, ErrorMessage = "El campo Description no puede exceder los 200 caracteres.")]
    public string Description { get; set; }
}
