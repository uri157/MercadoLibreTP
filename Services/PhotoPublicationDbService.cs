using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class PhotoPublicationDbService : IPhotoPublicationService
{
    private readonly DbContext _context;

    public PhotoPublicationDbService(DbContext context)
    {
        _context = context;
    }

    // Crear una nueva relación PhotoPublication
    public PhotoPublicationDTO Create(PhotoPublicationPostPutDTO photoPublicationDto)
    {
        var photoPublication = new PhotoPublication
        {
            IdPublication = photoPublicationDto.IdPublication,
            IdPhoto = photoPublicationDto.IdPhoto
        };

        _context.PhotoPublications.Add(photoPublication);
        _context.SaveChanges();

        return new PhotoPublicationDTO
        {
            Id = photoPublication.Id,
            IdPublication = photoPublication.IdPublication,
            IdPhoto = photoPublication.IdPhoto
            // Aquí puedes agregar otras propiedades del DTO si es necesario
        };
    }

    // Obtener una relación PhotoPublication por ID
    public PhotoPublicationDTO GetById(int id)
    {
        var photoPublication = _context.PhotoPublications
                                       .Include(pp => pp.Photo)
                                       .Include(pp => pp.Publication)
                                       .FirstOrDefault(pp => pp.Id == id);

        return photoPublication != null ? new PhotoPublicationDTO
        {
            Id = photoPublication.Id,
            IdPublication = photoPublication.IdPublication,
            IdPhoto = photoPublication.IdPhoto
            // Aquí puedes agregar otras propiedades del DTO si es necesario
        } : null;
    }

    // Obtener todas las relaciones PhotoPublication
    public List<PhotoPublicationDTO> GetAll()
    {
        return _context.PhotoPublications
                       .Include(pp => pp.Photo)
                       .Include(pp => pp.Publication)
                       .Select(pp => new PhotoPublicationDTO
                       {
                           Id = pp.Id,
                           IdPublication = pp.IdPublication,
                           IdPhoto = pp.IdPhoto
                           // Aquí puedes agregar otras propiedades del DTO si es necesario
                       })
                       .ToList();
    }

    // Eliminar una relación PhotoPublication
    public void Delete(int id)
    {
        var photoPublication = _context.PhotoPublications.Find(id);
        if (photoPublication != null)
        {
            _context.PhotoPublications.Remove(photoPublication);
            _context.SaveChanges();
        }
        // Considera lanzar una excepción si no se encuentra la relación
    }
}
