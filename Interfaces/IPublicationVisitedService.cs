public interface IPublicationVisitedService
{
    public IEnumerable<PublicationVisited> GetAll(); // Obtener todos los intereses de usuario
    public PublicationVisited? GetById(int id); // Obtener un interés de usuario por su ID
    public PublicationVisited Create(PublicationVisited userInterest); // Crear un nuevo interés de usuario
    public void Delete(int id); // Eliminar un interés de usuario
    public PublicationVisited? Update(int id, PublicationVisited userInterest); // Actualizar un interés de usuario existente
}
