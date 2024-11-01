using System.ComponentModel.DataAnnotations;

public class PublicationDTO
{
    [Required(ErrorMessage = "El campo Id es requerido.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo IdUsuario es requerido.")]
    public int IdUsuario { get; set; }

    [Required(ErrorMessage = "El campo IdCategoria es requerido.")]
    public int IdCategoria { get; set; }

    [MaxLength(500, ErrorMessage = "La descripción no debe exceder los 500 caracteres.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "El campo Price es requerido.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "El campo Title es requerido.")]
    [MaxLength(100, ErrorMessage = "El título no debe exceder los 100 caracteres.")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "El campo IdProductState es requerido.")]
    public int IdProductState { get; set; }

    [Required(ErrorMessage = "El campo IdPublicationState es requerido.")]
    public int IdPublicationState { get; set; }

    [Required(ErrorMessage = "El campo IdColor es requerido.")]
    public int IdColor { get; set; }

    [Required(ErrorMessage = "El campo Stock es requerido.")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
    public int Stock { get; set; }
}


public class PublicationPostDTO
{
    [Required(ErrorMessage = "El campo IdCategoria es requerido.")]
    public int IdCategoria { get; set; }

    [MaxLength(500, ErrorMessage = "La descripción no debe exceder los 500 caracteres.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "El campo Price es requerido.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "El campo Title es requerido.")]
    [MaxLength(100, ErrorMessage = "El título no debe exceder los 100 caracteres.")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "El campo IdProductState es requerido.")]
    public int IdProductState { get; set; }

    [Required(ErrorMessage = "El campo IdPublicationState es requerido.")]
    public int IdPublicationState { get; set; }

    [Required(ErrorMessage = "El campo IdColor es requerido.")]
    public int IdColor { get; set; }

    [Required(ErrorMessage = "El campo Stock es requerido.")]
    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
    public int Stock { get; set; }
}

public class PublicationPutDTO
{
    
    public int? IdCategoria { get; set; }

    [MaxLength(500, ErrorMessage = "La descripción no debe exceder los 500 caracteres.")]
    public string? Description { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0.")]
    public decimal? Price { get; set; }

    [MaxLength(100, ErrorMessage = "El título no debe exceder los 100 caracteres.")]
    public string? Title { get; set; }

    public int? IdProductState { get; set; }

    public int? IdPublicationState { get; set; }

    public int? IdColor { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "El stock no puede ser negativo.")]
    public int? Stock { get; set; }
}
