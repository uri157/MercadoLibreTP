using System.ComponentModel.DataAnnotations;

public class UserPublicationDTO
{
    [Required(ErrorMessage = "El campo Id es requerido.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo IdUser es requerido.")]
    public int IdUser { get; set; }

    [Required(ErrorMessage = "El campo IdPublication es requerido.")]
    public int IdPublication { get; set; }
}
