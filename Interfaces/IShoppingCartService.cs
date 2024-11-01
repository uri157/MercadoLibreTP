using System.Collections.Generic;
using System.Threading.Tasks;

public interface IShoppingCartService
{
    // Obtener el carrito de compras de un usuario por su ID
    Task<ShoppingCartDTO> GetCartByUserId(int userId);

    // Agregar una publicación al carrito del usuario
    Task<ShoppingCartDTO> AddToCart(int userId, int publicationId, int quantity);

    // Remover un producto del carrito
    Task<bool> RemoveFromCart(int userId, int publicationId);

    // Vaciar el carrito de un usuario
    Task<bool> ClearCart(int userId);

}
