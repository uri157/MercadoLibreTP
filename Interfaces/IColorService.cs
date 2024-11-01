using System.Collections.Generic;

public interface IColorService
{
    IEnumerable<ColorDTO> GetAll(); // Obtener todos los Colors de publicación
    ColorDTO? GetById(int id); // Obtener un Color por su ID
    ColorDTO Create(ColorPostPutDTO ColorDto); // Crear un nuevo Color de publicación
    void Delete(int id); // Eliminar un Color por su ID
    ColorDTO? Update(int id, ColorPostPutDTO ColorDto); // Actualizar un Color existente
}
