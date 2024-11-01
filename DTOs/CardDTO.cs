using System.ComponentModel.DataAnnotations;

public class CardDTO
{
    [Required(ErrorMessage = "El campo Id es requerido.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "El campo userId es requerido.")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "El campo cardNumber es requerido.")]
    [RegularExpression(@"\d{16}", ErrorMessage = "El número de tarjeta debe tener 16 dígitos.")]
    public string CardNumber { get; set; }

    [Required(ErrorMessage = "El campo cardHolderName es requerido.")]
    [MaxLength(100, ErrorMessage = "El nombre del titular no debe exceder los 100 caracteres.")]
    public string CardholderName { get; set; }

    [Required(ErrorMessage = "El campo expirationDate es requerido.")]
    [RegularExpression(@"\d{2}/\d{4}", ErrorMessage = "La fecha de expiración debe tener el formato MM/YYYY.")]
    public string ExpirationDate { get; set; }

    [Required(ErrorMessage = "El campo cvv es requerido.")]
    [RegularExpression(@"\d{3,4}", ErrorMessage = "El CVV debe tener 3 o 4 dígitos.")]
    public string Cvv { get; set; }

    [Required(ErrorMessage = "El campo cardType es requerido.")]
    public int CardTypeId { get; set; }
}

public class CardPutPostDTO
{
    [Required(ErrorMessage = "El campo cardNumber es requerido.")]
    [RegularExpression(@"\d{16}", ErrorMessage = "El número de tarjeta debe tener 16 dígitos.")]
    public string CardNumber { get; set; }

    [Required(ErrorMessage = "El campo cardHolderName es requerido.")]
    [MaxLength(100, ErrorMessage = "El nombre del titular no debe exceder los 100 caracteres.")]
    public string CardholderName { get; set; }

    [Required(ErrorMessage = "El campo expirationDate es requerido.")]
    [RegularExpression(@"\d{2}/\d{4}", ErrorMessage = "La fecha de expiración debe tener el formato MM/YYYY.")]
    public string ExpirationDate { get; set; }

    [Required(ErrorMessage = "El campo cvv es requerido.")]
    [RegularExpression(@"\d{3,4}", ErrorMessage = "El CVV debe tener 3 o 4 dígitos.")]
    public string Cvv { get; set; }

    [Required(ErrorMessage = "El campo cardType es requerido.")]
    public int CardTypeId { get; set; }
}
