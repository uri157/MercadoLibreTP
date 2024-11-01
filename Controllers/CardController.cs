using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/cards")]
public class CardController : ControllerBase
{
    private readonly ICardService _cardService;

    public CardController(ICardService cardService)
    {
        _cardService = cardService;
    }

    // Obtener todas las tarjetas del usuario autenticado
    [Authorize]
    [HttpGet]
    public ActionResult<List<Card>> GetAllCards()
    {
        // Obtener el ID del usuario autenticado
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        
        // Obtener solo las tarjetas del usuario autenticado
        return Ok(_cardService.GetByUserId(userId));
    }

     // Obtener una tarjeta por ID si pertenece al usuario autenticado
    [Authorize]
    [HttpGet("{id}")]
    public ActionResult<Card> GetById(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var card = _cardService.GetById(id);

        if (card == null || card.UserId != userId)
        {
            return NotFound("Card not found or access denied");
        }

        return Ok(card);
    }

    // // Crear una nueva tarjeta para el usuario autenticado
    // [Authorize]
    // [HttpPost]
    // public ActionResult<Card> NewCard(CardPutPostDTO cardDto)
    // {
    //     var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        
    //     var newCard = _cardService.Create(cardDto, userId); // Pasa el userId para asociar la tarjeta
    //     return CreatedAtAction(nameof(GetById), new { id = newCard.Id }, newCard);
    // }

    // Crear una nueva tarjeta para el usuario autenticado
    [Authorize]
    [HttpPost]
    public ActionResult<Card> NewCard(CardPutPostDTO cardDto)
    {
        try
        {
            // Verificar que el usuario autenticado tenga un ID válido
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("No se encontró el identificador del usuario.");
            }

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("El identificador de usuario es inválido.");
            }

            // Validar los datos de la tarjeta (ejemplo: número de tarjeta y fecha de expiración)
            if (string.IsNullOrWhiteSpace(cardDto.CardNumber) || cardDto.CardNumber.Length < 16)
            {
                return BadRequest("El número de la tarjeta es inválido.");
            }

            // Crear la tarjeta
            var newCard = _cardService.Create(cardDto, userId);
            return CreatedAtAction(nameof(GetById), new { id = newCard.Id }, newCard);
        }
        catch (ArgumentException ex)
        {
            // Manejar errores específicos del servicio (ej., tipo de tarjeta inexistente)
            return BadRequest(new { Message = "Error en los datos de la tarjeta.", Details = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            var errorMessage = ex.InnerException?.Message ?? ex.Message;
            return Conflict(new { Message = "Conflicto al crear la tarjeta.", Details = errorMessage });
        }
        catch (Exception ex)
        {
            // Log del error (opcional)
            // _logger.LogError(ex, "Error no previsto en NewCard");

            // Manejar cualquier otro error no previsto
            return StatusCode(500, new { Message = "Ocurrió un error interno en el servidor.", Details = ex.Message });
        }
    }


    // Eliminar una tarjeta por ID si pertenece al usuario autenticado
    [Authorize]
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var card = _cardService.GetById(id);

        if (card == null || card.UserId != userId)
        {
            return NotFound("Card not found or access denied");
        }

        _cardService.Delete(id);
        return NoContent();
    }

   // Actualizar una tarjeta por ID si pertenece al usuario autenticado
    [Authorize]
    [HttpPut("{id}")]
    public ActionResult<Card> UpdateCard(int id, CardPutPostDTO updatedCard)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        var card = _cardService.GetById(id);

        if (card == null || card.UserId != userId)
        {
            return NotFound("Card not found or access denied");
        }

        var updated = _cardService.Update(id, updatedCard, userId);
        return CreatedAtAction(nameof(GetById), new { id = updated.Id }, updated);
    }
}
