using Microsoft.EntityFrameworkCore;

public class CardTypeDbService : ICardTypeService
{
    private readonly DbContext _context; // Contexto de la base de datos

    public CardTypeDbService(DbContext context)
    {
        _context = context;
    }

    // Crear un nuevo tipo de tarjeta
    public CardType Create(CardTypePutDTO cardTypeDto)
    {
        CardType cardType = new()
        {
            TypeName = cardTypeDto.TypeName,
            Description = cardTypeDto.Description
        };

        _context.CardTypes.Add(cardType);
        _context.SaveChanges(); // Guardar cambios en la base de datos
        return cardType;
    }

    // Eliminar un tipo de tarjeta por su ID
    public void Delete(int id)
    {
        var cardType = _context.CardTypes.Find(id);
        if (cardType != null)
        {
            _context.CardTypes.Remove(cardType);
            _context.SaveChanges(); // Guardar los cambios
        }
    }

    // Obtener todos los tipos de tarjetas
    public IEnumerable<CardType> GetAll()
    {
        return _context.CardTypes.ToList();
    }

    // Obtener un tipo de tarjeta por su ID
    public CardType? GetById(int id)
    {
        return _context.CardTypes.Find(id);
    }

    // Actualizar un tipo de tarjeta existente
    public CardType? Update(int id, CardTypePutDTO cardTypeDto)
    {
        var existingCardType = _context.CardTypes.Find(id);
        if (existingCardType != null)
        {
            existingCardType.TypeName = cardTypeDto.TypeName;
            existingCardType.Description = cardTypeDto.Description;

            _context.Entry(existingCardType).State = EntityState.Modified;
            _context.SaveChanges(); // Guardar cambios
            return existingCardType;
        }

        return null;
    }
}
