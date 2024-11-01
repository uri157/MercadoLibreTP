public interface ICardService
{
    public IEnumerable<Card> GetAll(); // Obtener todas las tarjetas
    public Card? GetById(int id); // Obtener una tarjeta por su ID
    public IEnumerable<Card> GetByUserId(int id); // Obtener una tarjeta por su ID
    public Card Create(CardPutPostDTO cardDto, int userId); // Crear una nueva tarjeta
    public void Delete(int id); // Eliminar una tarjeta
    public Card? Update(int id, CardPutPostDTO card, int userId); // Actualizar una tarjeta existente
}
