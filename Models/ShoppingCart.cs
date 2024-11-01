using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class ShoppingCart
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int IdUser { get; set; } // Clave foránea a User

    public decimal Total { get; set; } // Total acumulado del carrito

    [JsonIgnore]
    public User User { get; set; }  // Propiedad de navegación hacia User

    public ICollection<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>(); // Colección de ítems en el carrito
}
