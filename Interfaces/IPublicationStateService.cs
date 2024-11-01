using System.Collections.Generic;

public interface IPublicationStateService
{
    IEnumerable<PublicationStateDTO> GetAll(); // Obtener todos los estados de publicación
    PublicationStateDTO? GetById(int id); // Obtener un estado por su ID
    PublicationStateDTO Create(PublicationStatePostPutDTO publicationStateDto); // Crear un nuevo estado de publicación
    void Delete(int id); // Eliminar un estado por su ID
    PublicationStateDTO? Update(int id, PublicationStatePostPutDTO publicationStateDto); // Actualizar un estado existente
}
