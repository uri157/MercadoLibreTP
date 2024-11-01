using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class CardType
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Esto indica que el ID será autogenerado
    public int Id { get; set; }  // Primary Key

    public string TypeName { get; set; }  // Nombre del tipo de tarjeta (requerido, máximo 100 caracteres)

    public string Description { get; set; }  // Descripción del tipo de tarjeta (opcional, máximo 200 caracteres)

    [JsonIgnore]
    public ICollection<Card> Cards { get; set; }  // Relación de uno a muchos con Card

    // Constructor por defecto
    public CardType()
    {
        Cards = new HashSet<Card>();  // Inicialización de la colección
    }

    // Constructor con parámetros
    public CardType(int id, string typeName, string description)
    {
        

        Id = id;
        TypeName = typeName;
        Description = description;
        Cards = new HashSet<Card>();  // Inicialización de la colección
    }

    // Método ToString sobreescrito
    public override string ToString()
    {
        return $"CardType ID: {Id}, Type: {TypeName}, Description: {Description}";
    }
}
