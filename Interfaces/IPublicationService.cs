using System.Collections.Generic;

public interface IPublicationService
{
    // Obtener todas las publicaciones
    IEnumerable<PublicationDTO> GetAll();

    // Obtener una publicación por ID
    PublicationDTO? GetById(int id);
    IEnumerable<PublicationDTO> GetByCategoryName(string categoryName);

    // Crear una nueva publicación
    PublicationDTO Create(int UserId, PublicationPostDTO publication);

    // Actualizar una publicación
    PublicationDTO? Update(int id, PublicationPutDTO publication);

    // Eliminar una publicación por ID
    void Delete(int id);
}
