using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ShoppingCartItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int ShoppingCartId { get; set; } // Clave foránea al carrito

    public int PublicationId { get; set; } // Clave foránea a Publication

    public int Quantity { get; set; } // Cantidad de la publicación en el carrito

    [ForeignKey("PublicationId")]
    public Publication Publication { get; set; } // Relación con la publicación
}
