using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

public class ShoppingCartDbService : IShoppingCartService
{
    private readonly DbContext _context;

    public ShoppingCartDbService(DbContext context)
    {
        _context = context;
    }

    public async Task<ShoppingCartDTO> GetCartByUserId(int userId)
    {
        var cart = await _context.Set<ShoppingCart>()
            .Where(cart => cart.IdUser == userId)
            .Include(cart => cart.Items)
            .ThenInclude(item => item.Publication)
            .Select(cart => new ShoppingCartDTO
            {
                Id = cart.Id,
                IdUser = cart.IdUser,
                Total = cart.Items.Sum(item => item.Publication.Price * item.Quantity),
                Items = cart.Items.Select(item => new ShoppingCartItemDTO
                {
                    Id = item.Id,
                    PublicationId = item.PublicationId,
                    Title = item.Publication.Title,
                    Price = item.Publication.Price,
                    Quantity = item.Quantity
                }).ToList()
            })
            .FirstOrDefaultAsync();

        return cart;
    }

    public async Task<ShoppingCartDTO> AddToCart(int userId, int publicationId, int quantity)
    {
        // Obtener o crear el carrito del usuario
        var cart = await _context.ShoppingCarts
            .Include(c => c.Items)
            .ThenInclude(i => i.Publication) // Incluir la publicación en cada ítem
            .FirstOrDefaultAsync(c => c.IdUser == userId);

        // Si el carrito no existe, crearlo y guardar para obtener el ID
        if (cart == null)
        {
            cart = new ShoppingCart { IdUser = userId };
            _context.ShoppingCarts.Add(cart);
            await _context.SaveChangesAsync(); // Guardar para obtener cart.Id
        }

        // Buscar si el ítem ya existe en el carrito
        var existingItem = cart.Items.FirstOrDefault(item => item.PublicationId == publicationId);

        if (existingItem != null)
        {
            existingItem.Quantity += quantity;
        }
        else
        {
            var newItem = new ShoppingCartItem
            {
                PublicationId = publicationId,
                Quantity = quantity,
                ShoppingCartId = cart.Id // Asignar el ID directamente
            };

            _context.ShoppingCartItems.Add(newItem); // Agregar directamente al contexto
        }

        await _context.SaveChangesAsync();

        // Retornar el carrito actualizado con todos los datos necesarios
        var updatedCart = await _context.ShoppingCarts
            .Where(c => c.Id == cart.Id)
            .Include(c => c.Items)
            .ThenInclude(i => i.Publication) // Incluir los datos de publicación nuevamente
            .Select(c => new ShoppingCartDTO
            {
                Id = c.Id,
                IdUser = c.IdUser,
                Total = c.Items.Sum(item => item.Publication.Price * item.Quantity),
                Items = c.Items.Select(item => new ShoppingCartItemDTO
                {
                    Id = item.Id,
                    ShoppingCartId = item.ShoppingCartId,
                    PublicationId = item.PublicationId,
                    Quantity = item.Quantity,
                    Title = item.Publication.Title,
                    Price = item.Publication.Price
                }).ToList()
            })
            .FirstOrDefaultAsync();

        return updatedCart;
    }


   public async Task<bool> RemoveFromCart(int userId, int publicationId)
    {
        
        var cart = await _context.Set<ShoppingCart>()
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.IdUser == userId);

        // Verificar si el carrito existe y contiene el ítem
        if (cart == null) return false;

        var cartItem = cart.Items.FirstOrDefault(item => item.PublicationId == publicationId);
        if (cartItem == null) return false;

        // Eliminar el ítem del carrito
        _context.Set<ShoppingCartItem>().Remove(cartItem);
        await _context.SaveChangesAsync();

        return true;
    }
    // Vaciar el carrito de un usuario
    public async Task<bool> ClearCart(int userId)
    {
        // Obtener el carrito del usuario
        var shoppingCart = await _context.Set<ShoppingCart>()
            .FirstOrDefaultAsync(cart => cart.IdUser == userId);

        // Si no existe el carrito, retorna false
        if (shoppingCart == null) return false;

        // Obtener todos los ítems del carrito del usuario
        var cartItems = await _context.Set<ShoppingCartItem>()
            .Where(item => item.ShoppingCartId == shoppingCart.Id) // Verificar que el ShoppingCartId coincida
            .ToListAsync();

        if (!cartItems.Any()) return false; // Si no hay ítems, retorna false

        // Eliminar todos los ítems del carrito
        _context.Set<ShoppingCartItem>().RemoveRange(cartItems);
        await _context.SaveChangesAsync();
        return true; // Retorna true si se vació el carrito
    }
    
}
