using Microsoft.EntityFrameworkCore;

public class PublicationVisitedDbService : IPublicationVisitedService
{
    private readonly DbContext _context; // Cambia YourDbContext por el nombre de tu DbContext

    public PublicationVisitedDbService(DbContext context)
    {
        _context = context;
    }

    public IEnumerable<PublicationVisited> GetAll()
    {
        return _context.UserHistory.ToList(); // Devuelve todos los registros
    }

    public PublicationVisited? GetById(int id)
    {
        return _context.UserHistory.Find(id); // Busca un registro por su ID
    }

    public PublicationVisited Create(PublicationVisited userInterest)
    {
        _context.UserHistory.Add(userInterest); // Agrega un nuevo registro
        _context.SaveChanges(); // Guarda los cambios
        return userInterest; // Devuelve el registro creado
    }

    public void Delete(int id)
    {
        var publicationVisited = _context.UserHistory.Find(id); // Busca el registro por su ID
        if (publicationVisited != null)
        {
            _context.UserHistory.Remove(publicationVisited); // Elimina el registro
            _context.SaveChanges(); // Guarda los cambios
        }
    }

    public PublicationVisited? Update(int id, PublicationVisited userInterest)
    {
        var existingPublicationVisited = _context.UserHistory.Find(id); // Busca el registro existente
        if (existingPublicationVisited == null)
        {
            return null; // Si no se encuentra, devuelve null
        }

        // Actualiza los campos
        existingPublicationVisited.IdUser = userInterest.IdUser;
        existingPublicationVisited.IdPublication = userInterest.IdPublication;

        _context.Entry(existingPublicationVisited).State = EntityState.Modified; // Marca como modificado
        _context.SaveChanges(); // Guarda los cambios
        return existingPublicationVisited; // Devuelve el registro actualizado
    }
}
