using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Card
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Esto indica que el ID será autogenerado
    public int Id { get; set; }  // ID de la tarjeta (Primary Key)

    public int UserId { get; set; }  // Clave foránea al usuario
    
    [JsonIgnore]
    public User User { get; set; }  // Propiedad de navegación hacia User

    public string CardNumber { get; set; }  // Número de la tarjeta
    public string CardholderName { get; set; }  // Nombre del titular de la tarjeta
    public string ExpirationDate { get; set; }  // Fecha de expiración de la tarjeta (MM/YY)
    public string CVV { get; set; }  // Código de seguridad de la tarjeta (CVV)

    public int CardTypeId { get; set; }  // Clave foránea hacia CardType
    
    [JsonIgnore]
    public CardType CardType { get; set; }  // Propiedad de navegación hacia CardType

    // Constructor por defecto
    public Card()
    {
    }

    // Constructor con parámetros
    public Card(int id, int userId, string cardNumber, string cardholderName, string expirationDate, string cvv, int idCardType)
    {
        Id = id;
        UserId = userId;
        CardNumber = cardNumber;
        CardholderName = cardholderName;
        ExpirationDate = expirationDate;
        CVV = cvv;
        CardTypeId = idCardType;
    }

    // Método ToString sobreescrito
    public override string ToString()
    {
        return $"Card ID: {Id}, Cardholder: {CardholderName}, Card Type: {CardType.TypeName}";
    }
}
