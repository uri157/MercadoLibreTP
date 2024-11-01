public interface IPhotoService
{
    List<PhotoDTO> GetAll();
    PhotoDTO GetById(int id);
    PhotoDTO Create(PhotoPostPutDTO photoDto);
    void Delete(int id);
}