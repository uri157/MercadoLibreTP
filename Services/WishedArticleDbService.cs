using Microsoft.EntityFrameworkCore;

public class WishedArticleDbService : IWishedArticleService
{
    private readonly DbContext _context;

    public WishedArticleDbService(DbContext context)
    {
        _context = context;
    }

    // Crear un WishedArticle
    public WishedArticleDTO Create(int idUser, WishedArticlePostPutDTO dto)
    {
        WishedArticle newWishedArticle = new()
        {
            IdUser = idUser,
            IdPublication = dto.IdPublication
        };

        _context.WishedArticles.Add(newWishedArticle);
        _context.SaveChanges();

        return new WishedArticleDTO
            {
                Id = newWishedArticle.Id,
                IdUser = newWishedArticle.IdUser,
                IdPublication = newWishedArticle.IdPublication
            };

    }

    // Obtener todos los WishedArticles
    public IEnumerable<WishedArticleDTO> GetAll()
    {
        return _context.WishedArticles
            .Select(wa => new WishedArticleDTO
            {
                Id = wa.Id,
                IdUser = wa.IdUser,
                IdPublication = wa.IdPublication
            }).ToList();
    }

    // Obtener un WishedArticle por ID
    public WishedArticleDTO? GetById(int id)
    {
        var wa = _context.WishedArticles.Find(id);
        if (wa == null)
        {
            return null;
        }

        return new WishedArticleDTO
        {
            Id = wa.Id,
            IdUser = wa.IdUser,
            IdPublication = wa.IdPublication
        };
    }

    // Eliminar un WishedArticle
    public void Delete(int id)
    {
        var wa = _context.WishedArticles.Find(id);
        if (wa != null)
        {
            _context.WishedArticles.Remove(wa);
            _context.SaveChanges();
        }
    }

    // Actualizar un WishedArticle
    public WishedArticleDTO? Update(int id, int idUser, WishedArticlePostPutDTO dto)
    {
        var wa = _context.WishedArticles.Find(id);
        if (wa != null)
        {
            wa.IdUser = idUser;
            wa.IdPublication = dto.IdPublication;

            _context.Entry(wa).State = EntityState.Modified;
            _context.SaveChanges();
            return new WishedArticleDTO
            {
                Id = wa.Id,
                IdUser = wa.IdUser,
                IdPublication = wa.IdPublication
            };
        }

        return null;
    }
}
