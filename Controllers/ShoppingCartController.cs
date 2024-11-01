using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
        
        
    }

    // Obtener el carrito de un usuario
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetCart()
    {
         // Obtención del ID de usuario
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            return Unauthorized("No se pudo obtener el ID del usuario.");
        }

        int userId;
        if (!int.TryParse(userIdClaim.Value, out userId))
        {
            return BadRequest("El ID del usuario no es válido.");
        }

        var cart = await _shoppingCartService.GetCartByUserId(userId);
        if (cart == null)
        {
            return NotFound("Carrito no encontrado.");
        }
        return Ok(cart);
    }

    // Agregar una publicación al carrito
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddToCart(ShoppingCartPostDTO shoppingCartDTO)
    {
        // Validación de cantidad
        if (shoppingCartDTO.Quantity <= 0)
        {
            return BadRequest("La cantidad debe ser mayor a 0.");
        }

        try
        {
            // Obtención del ID de usuario
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("No se pudo obtener el ID del usuario.");
            }

            int userId;
            if (!int.TryParse(userIdClaim.Value, out userId))
            {
                return BadRequest("El ID del usuario no es válido.");
            }

            // Agregar al carrito
            var cart = await _shoppingCartService.AddToCart(userId, shoppingCartDTO.IdPublication, shoppingCartDTO.Quantity);
            
            if (cart == null)
            {
                return NotFound("El carrito no pudo ser encontrado o creado.");
            }

            return Ok(cart);
        }
        catch (ArgumentNullException ex)
        {
            // En caso de argumentos nulos específicos
            return BadRequest($"Error en los datos proporcionados: {ex.Message}");
        }
        catch (InvalidOperationException ex)
        {
            // En caso de operaciones inválidas, como un usuario o publicación no existente
            return NotFound($"Operación inválida: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Incluye el mensaje de excepción completo en la respuesta
            return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
        }
    }

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> RemoveFromCart(int publicationId)
    {
        
        int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var result = await _shoppingCartService.RemoveFromCart(userId, publicationId);
        if (!result)
        {
            return NotFound("Producto no encontrado en el carrito.");
        }
        return Ok("Producto eliminado del carrito.");
    }

    // Vaciar el carrito
    [Authorize]
    [HttpDelete("/clear")]
    public async Task<IActionResult> ClearCart(int userId)
    {
        var result = await _shoppingCartService.ClearCart(userId);
        if (!result)
        {
            return NotFound("No hay ítems en el carrito para vaciar.");
        }
        return Ok("Carrito vaciado.");
    }
}
