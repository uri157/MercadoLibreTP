public interface IUserPublicationService
{
    public IEnumerable<UserPublication> GetAll(); // Obtener todas las relaciones usuario-publicación
    public UserPublication? GetById(int id); // Obtener una relación usuario-publicación por su ID
    public UserPublication Create(UserPublication userPublication); // Crear una nueva relación usuario-publicación
    public void Delete(int id); // Eliminar una relación usuario-publicación
    public UserPublication? Update(int id, UserPublication userPublication); // Actualizar una relación usuario-publicación existente
}
