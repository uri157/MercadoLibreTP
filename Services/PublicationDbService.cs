using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class PublicationDbService : IPublicationService
{
    private readonly DbContext _context;

    public PublicationDbService(DbContext context)
    {
        _context = context;
    }

    // Obtener todas las publicaciones
    public IEnumerable<PublicationDTO> GetAll()
    {
        return _context.Publications
            .Select(publication => new PublicationDTO
            {
                Id = publication.Id,
                IdUsuario = publication.UserId,
                IdCategoria = publication.IdCategoria,
                Description = publication.Description,
                Price = publication.Price,
                Title = publication.Title,
                IdPublicationState = publication.IdPublicationState,
                IdProductState = publication.IdProductState,
                IdColor = publication.IdColor,
                Stock = publication.Stock
            })
            .ToList();
    }


    // Obtener una publicación por ID
    public PublicationDTO? GetById(int id)
    {
        Publication pub = _context.Publications.Find(id);
        return new PublicationDTO
        {
            IdUsuario = pub.UserId,
            IdCategoria = pub.IdCategoria,
            Description = pub.Description,
            Price = pub.Price,
            Title = pub.Title,
            IdPublicationState = pub.IdPublicationState,
            IdProductState = pub.IdProductState,
            IdColor = pub.IdColor,
            Stock = pub.Stock
        };
    }

    public IEnumerable<PublicationDTO> GetByCategoryName(string categoryName)
    {

        // Buscar el ID de la categoría usando el nombre
        var categoryId = _context.Categories
                            .Where(c => c.Name == categoryName)
                            .Select(c => c.Id)
                            .FirstOrDefault();

        if (categoryId == 0) return new List<PublicationDTO>();


        return _context.Publications
                    .Where(p => p.Category.Name == categoryName)
                    .Select(p => new PublicationDTO
                    {
                        Price = p.Price,
                        Title = p.Title,
                        IdPublicationState = p.IdPublicationState,
                        IdProductState = p.IdProductState,
                        IdColor = p.IdColor,
                        Stock = p.Stock
                    })
                    .ToList();
    }





    // Crear una nueva publicación
    public PublicationDTO Create(int userId, PublicationPostDTO publicationDto)
    {
        Publication publication = new()
        {
            UserId = userId,
            IdCategoria = publicationDto.IdCategoria,
            Description = publicationDto.Description,
            Price = publicationDto.Price,
            Title = publicationDto.Title,
            IdPublicationState = publicationDto.IdPublicationState,
            IdProductState = publicationDto.IdProductState,
            IdColor = publicationDto.IdColor,
            Stock = publicationDto.Stock
        };

        _context.Publications.Add(publication);
        _context.SaveChanges();
        return new PublicationDTO{
            Id=publication.Id,
            IdUsuario = userId,
            IdCategoria = publicationDto.IdCategoria,
            Description = publicationDto.Description,
            Price = publicationDto.Price,
            Title = publicationDto.Title,
            IdPublicationState = publicationDto.IdPublicationState,
            IdProductState = publicationDto.IdProductState,
            IdColor = publicationDto.IdColor,
            Stock = publicationDto.Stock
        };
    }

    // Actualizar una publicación
    public PublicationDTO? Update(int id, PublicationPutDTO publicationToUpdate)
    {
        var existingPublication = _context.Publications.Find(id);
        if (existingPublication == null)
        {
            return null;
        }

         // Actualizamos los campos que podrían haber cambiado solo si no son nulos
    if (publicationToUpdate.IdCategoria.HasValue)
    {
        existingPublication.IdCategoria = publicationToUpdate.IdCategoria.Value;
    }
    if (!string.IsNullOrEmpty(publicationToUpdate.Description))
    {
        existingPublication.Description = publicationToUpdate.Description;
    }
    if (publicationToUpdate.Price.HasValue)
    {
        existingPublication.Price = publicationToUpdate.Price.Value;
    }
    if (!string.IsNullOrEmpty(publicationToUpdate.Title))
    {
        existingPublication.Title = publicationToUpdate.Title;
    }
    if (publicationToUpdate.IdProductState.HasValue)
    {
        existingPublication.IdProductState = publicationToUpdate.IdProductState.Value;
    }
    if (publicationToUpdate.IdPublicationState.HasValue)
    {
        existingPublication.IdPublicationState = publicationToUpdate.IdPublicationState.Value;
    }
    if (publicationToUpdate.IdColor.HasValue)
    {
        existingPublication.IdColor = publicationToUpdate.IdColor.Value;
    }
    if (publicationToUpdate.Stock.HasValue)
    {
        existingPublication.Stock = publicationToUpdate.Stock.Value;
    }

    _context.Entry(existingPublication).State = EntityState.Modified;
    _context.SaveChanges();

    return new PublicationDTO
    {
        Id = id,
        IdUsuario = existingPublication.UserId,
        IdCategoria = existingPublication.IdCategoria, // Ahora toma el valor actualizado
        Description = existingPublication.Description,
        Price = existingPublication.Price,
        Title = existingPublication.Title,
        IdPublicationState = existingPublication.IdPublicationState,
        IdProductState = existingPublication.IdProductState,
        IdColor = existingPublication.IdColor,
        Stock = existingPublication.Stock
    };
}
    // Eliminar una publicación por ID
    public void Delete(int id)
    {
        var publication = _context.Publications.Find(id);
        if (publication != null)
        {
            _context.Publications.Remove(publication);
            _context.SaveChanges();
        }
    }
}
