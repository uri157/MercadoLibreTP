using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

public class ColorDbService : IColorService
{
    private readonly DbContext _context; // Contexto de la base de datos

    public ColorDbService(DbContext context)
    {
        _context = context;
    }

    // Crear un nuevo estado de publicación
    public ColorDTO Create(ColorPostPutDTO ColorDto)
    {
        Color Color = new()
        {
            Name = ColorDto.Name,
        };

        _context.Colors.Add(Color);
        _context.SaveChanges(); // Guardar cambios en la base de datos

        return new ColorDTO
        {
            Id = Color.Id,
            Name = Color.Name,
        };
    }

    // Eliminar un estado de publicación por su ID
    public void Delete(int id)
    {
        var Color = _context.Colors.Find(id);
        if (Color != null)
        {
            _context.Colors.Remove(Color);
            _context.SaveChanges(); // Guardar los cambios
        }
    }

    // Obtener todos los estados de publicación
    public IEnumerable<ColorDTO> GetAll()
    {
        return _context.Colors
            .Select(ps => new ColorDTO
            {
                Id = ps.Id,
                Name = ps.Name,
            })
            .ToList();
    }

    // Obtener un estado de publicación por su ID
    public ColorDTO? GetById(int id)
    {
        var Color = _context.Colors.Find(id);
        if (Color != null)
        {
            return new ColorDTO
            {
                Id = Color.Id,
                Name = Color.Name,
            };
        }
        return null;
    }

    // Actualizar un estado de publicación existente
    public ColorDTO? Update(int id, ColorPostPutDTO ColorDto)
    {
        var existingColor = _context.Colors.Find(id);
        if (existingColor != null)
        {
            existingColor.Name = ColorDto.Name;

            _context.Entry(existingColor).State = EntityState.Modified;
            _context.SaveChanges(); // Guardar cambios

            return new ColorDTO
            {
                Id = existingColor.Id,
                Name = existingColor.Name,
            };
        }

        return null;
    }
}
