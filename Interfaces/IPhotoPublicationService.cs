using System.Collections.Generic;

public interface IPhotoPublicationService
{
    List<PhotoPublicationDTO> GetAll();                     // Obtener todas las PhotoPublication
    PhotoPublicationDTO GetById(int id);                    // Obtener una PhotoPublication por ID
    PhotoPublicationDTO Create(PhotoPublicationPostPutDTO photoPublicationDto); // Crear una nueva PhotoPublication
    void Delete(int id);                                     // Eliminar una PhotoPublication
}
