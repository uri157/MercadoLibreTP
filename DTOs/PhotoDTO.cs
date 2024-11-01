using System.ComponentModel.DataAnnotations;

public class PhotoDTO
{
    [Required(ErrorMessage = "El campo Id es requerido.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo ImageData es requerido.")]
    public string ImageData { get; set; } // Validación de datos opcional
}

public class PhotoPostPutDTO
{
    [Required(ErrorMessage = "El campo ImageData es requerido.")]
    public string ImageData { get; set; } // Validación de datos opcional
}
