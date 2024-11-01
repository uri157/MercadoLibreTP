using System.ComponentModel.DataAnnotations;

public class ColorDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Nombre es requerido.")]
    public string Name { get; set; }
}

public class ColorPostPutDTO
{
    [Required(ErrorMessage = "El campo Nombre es requerido.")]
    public string Name { get; set; }
}