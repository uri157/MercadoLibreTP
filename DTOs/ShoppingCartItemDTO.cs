
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components.Web;

public class ShoppingCartItemDTO
{
    public int Id { get; set; }
    [Required]
    public int ShoppingCartId { get; set; } // Clave foránea al carrito
    [Required]
    public int PublicationId { get; set; } // Clave foránea a Publication
    [Required]
    public int Quantity { get; set; } // Cantidad de la publicación en el carrito
    public string Title {get;set;}
    public decimal Price {get;set;}
}
