using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/card-types")]
public class CardTypeController : ControllerBase
{
    private readonly ICardTypeService _cardTypeService;

    public CardTypeController(ICardTypeService cardTypeService)
    {
        _cardTypeService = cardTypeService;
    }

    // Obtener todos los tipos de tarjetas
    [AllowAnonymous]
    [HttpGet]
    public ActionResult<List<CardType>> GetAllCardTypes()
    {
        return Ok(_cardTypeService.GetAll());
    }

    // Obtener un tipo de tarjeta por ID
    [AllowAnonymous]
    [HttpGet("{id}")]
    public ActionResult<CardType> GetById(int id)
    {
        var cardType = _cardTypeService.GetById(id);
        if (cardType == null)
        {
            return NotFound("CardType not found");
        }
        return Ok(cardType);
    }

    // Crear un nuevo tipo de tarjeta
    [Authorize(Roles = "admin")]
    [HttpPost]
    public ActionResult<CardType> NewCardType(CardTypePutDTO cardTypeDto)
    {
        var newCardType = _cardTypeService.Create(cardTypeDto);
        return CreatedAtAction(nameof(GetById), new { id = newCardType.Id }, newCardType);
    }

    // Eliminar un tipo de tarjeta por ID
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var cardType = _cardTypeService.GetById(id);
        if (cardType == null)
        {
            return NotFound("CardType not found");
        }

        _cardTypeService.Delete(id);
        return NoContent();
    }

    // Actualizar un tipo de tarjeta por ID
    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public ActionResult<CardType> UpdateCardType(int id, CardTypePutDTO updatedCardTypeDto)
    {
        var cardType = _cardTypeService.Update(id, updatedCardTypeDto);
        if (cardType == null)
        {
            return NotFound("CardType not found");
        }

        return CreatedAtAction(nameof(GetById), new { id = cardType.Id }, cardType);
    }
}
