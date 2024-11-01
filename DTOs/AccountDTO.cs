using System.ComponentModel.DataAnnotations;

using System.ComponentModel.DataAnnotations;

public class UserDTO
{
    public int? Id { get; set; }

    [Required(ErrorMessage = "El campo Username es requerido.")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "El campo FirstName es requerido.")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "El campo LastName es requerido.")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "El campo Email es requerido.")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "El campo Password es requerido.")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string? Password { get; set; }

    public int? ProfilePhotoId { get; set; } // Este campo es opcional

    [RegularExpression(@"^\d{8,}$", ErrorMessage = "El DNI debe contener al menos 8 dígitos.")]
    public string? DNI { get; set; } // Este campo es opcional y validamos que tenga al menos 8 dígitos

    public string? Address { get; set; } // Este campo también es opcional
}
public class UserPostPutDTO
{
    [Required(ErrorMessage = "El campo Username es requerido.")]
    public string? Username { get; set; }

    [Required(ErrorMessage = "El campo FirstName es requerido.")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "El campo LastName es requerido.")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "El campo Email es requerido.")]
    [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido.")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "El campo Password es requerido.")]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string? Password { get; set; }

    public int? ProfilePhotoId { get; set; } // Este campo es opcional

    [RegularExpression(@"^\d{8,}$", ErrorMessage = "El DNI debe contener al menos 8 dígitos.")]
    public string? DNI { get; set; } // Este campo es opcional y validamos que tenga al menos 8 dígitos

    public string? Address { get; set; } // Este campo también es opcional
}