using System.ComponentModel.DataAnnotations;

public class PublicationStateDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo Nombre es requerido.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "El campo Descripción es requerido.")]
    public string Description { get; set; }
}

public class PublicationStatePostPutDTO
{
    [Required(ErrorMessage = "El campo Nombre es requerido.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "El campo Descripción es requerido.")]
    public string Description { get; set; }
}
