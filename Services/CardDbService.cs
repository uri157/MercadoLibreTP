using Microsoft.EntityFrameworkCore;

public class CardDbService : ICardService
{
    private readonly DbContext _context; // Contexto de la base de datos

    public CardDbService(DbContext context)
    {
        _context = context;
    }

    // Crear una nueva tarjeta
    public Card Create(CardPutPostDTO cardDto, int userId)
{
    // Buscar el CardType en la base de datos por el ID del tipo de tarjeta
    var cardType = _context.CardTypes.FirstOrDefault(ct => ct.Id == cardDto.CardTypeId);

    if (cardType == null)
    {
        throw new ArgumentException($"CardType with ID '{cardDto.CardTypeId}' not found");
    }

    Card card = new()
    {
        UserId = userId,
        CardNumber = cardDto.CardNumber,
        CardholderName = cardDto.CardholderName,
        ExpirationDate = cardDto.ExpirationDate,
        CVV = cardDto.Cvv,
        CardTypeId = cardDto.CardTypeId,  // Asignar el objeto CardType encontrado
    };

    _context.Cards.Add(card);
     try
    {
        _context.SaveChanges();
    }
    catch (DbUpdateException dbEx)
    {
        var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
        throw new InvalidOperationException($"Error al guardar en la base de datos: {innerMessage}", dbEx);
    }


    return card;
}

    // Eliminar una tarjeta por su ID
    public void Delete(int id)
    {
        var card = _context.Cards.Find(id);
        if (card != null)
        {
            _context.Cards.Remove(card);
            _context.SaveChanges(); // Guardar los cambios
        }
    }

    // Obtener todas las tarjetas
    public IEnumerable<Card> GetAll()
    {
        return _context.Cards.Include(c => c.CardType);  // Incluir el tipo de tarjeta en la respuesta
    }

    // Obtener una tarjeta por su ID
    public Card? GetById(int id)
    {
        return _context.Cards.Include(c => c.CardType).FirstOrDefault(c => c.Id == id);
    }

    // Actualizar una tarjeta existente
    public Card? Update(int id, CardPutPostDTO cardToUpdate, int userId)
    {
        var existingCard = _context.Cards.Find(id);
        if (existingCard != null)
        {
            // Buscar el nuevo CardType si se está actualizando
            var cardType = _context.CardTypes.FirstOrDefault(ct => ct.Id == cardToUpdate.CardTypeId);
            if (cardType == null)
            {
                throw new Exception($"CardType with ID '{cardToUpdate.CardTypeId}' not found");
            }

            existingCard.UserId = userId;
            existingCard.CardNumber = cardToUpdate.CardNumber;
            existingCard.CardholderName = cardToUpdate.CardholderName;
            existingCard.ExpirationDate = cardToUpdate.ExpirationDate;
            existingCard.CVV = cardToUpdate.Cvv;
            existingCard.CardType = cardType;  // Asignar el nuevo CardType

            _context.Entry(existingCard).State = EntityState.Modified;
            _context.SaveChanges(); // Guardar cambios
            return existingCard;
        }

        return null;
    }

    // Obtener todas las tarjetas por UserId
    public IEnumerable<Card> GetByUserId(int userId)
    {
        return _context.Cards
            .Include(c => c.CardType) // Incluir el tipo de tarjeta en la respuesta
            .Where(c => c.UserId == userId) // Filtrar por UserId
            .ToList();
    }
}
