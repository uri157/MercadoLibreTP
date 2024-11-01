public interface IWishedArticleService
{
    public IEnumerable<WishedArticleDTO> GetAll(); // Obtener todos los artículos deseados
    public WishedArticleDTO? GetById(int id); // Obtener un artículo deseado por su ID
    public WishedArticleDTO Create(int userId, WishedArticlePostPutDTO wishedArticle); // Agregar un nuevo artículo a la lista de deseos
    public void Delete(int id); // Eliminar un artículo de la lista de deseos
    public WishedArticleDTO? Update(int id, int userId, WishedArticlePostPutDTO wishedArticle); // Actualizar un artículo deseado existente
}
