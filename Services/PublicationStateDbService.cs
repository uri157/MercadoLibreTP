using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class PublicationStateDbService : IPublicationStateService
{
    private readonly DbContext _context; // Contexto de la base de datos

    public PublicationStateDbService(DbContext context)
    {
        _context = context;
    }

    // Crear un nuevo estado de publicación
    public PublicationStateDTO Create(PublicationStatePostPutDTO publicationStateDto)
    {
        PublicationState publicationState = new()
        {
            Name = publicationStateDto.Name,
            Description = publicationStateDto.Description
        };

        _context.PublicationStates.Add(publicationState);
        _context.SaveChanges(); // Guardar cambios en la base de datos

        return new PublicationStateDTO
        {
            Id = publicationState.Id,
            Name = publicationState.Name,
            Description = publicationState.Description
        };
    }

    // Eliminar un estado de publicación por su ID
    public void Delete(int id)
    {
        var publicationState = _context.PublicationStates.Find(id);
        if (publicationState != null)
        {
            _context.PublicationStates.Remove(publicationState);
            _context.SaveChanges(); // Guardar los cambios
        }
    }

    // Obtener todos los estados de publicación
    public IEnumerable<PublicationStateDTO> GetAll()
    {
        return _context.PublicationStates
            .Select(ps => new PublicationStateDTO
            {
                Id = ps.Id,
                Name = ps.Name,
                Description = ps.Description
            })
            .ToList();
    }

    // Obtener un estado de publicación por su ID
    public PublicationStateDTO? GetById(int id)
    {
        var publicationState = _context.PublicationStates.Find(id);
        if (publicationState != null)
        {
            return new PublicationStateDTO
            {
                Id = publicationState.Id,
                Name = publicationState.Name,
                Description = publicationState.Description
            };
        }
        return null;
    }

    // Actualizar un estado de publicación existente
    public PublicationStateDTO? Update(int id, PublicationStatePostPutDTO publicationStateDto)
    {
        var existingPublicationState = _context.PublicationStates.Find(id);
        if (existingPublicationState != null)
        {
            existingPublicationState.Name = publicationStateDto.Name;
            existingPublicationState.Description = publicationStateDto.Description;

            _context.Entry(existingPublicationState).State = EntityState.Modified;
            _context.SaveChanges(); // Guardar cambios

            return new PublicationStateDTO
            {
                Id = existingPublicationState.Id,
                Name = existingPublicationState.Name,
                Description = existingPublicationState.Description
            };
        }

        return null;
    }
}
