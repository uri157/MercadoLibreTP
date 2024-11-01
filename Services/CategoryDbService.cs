using Microsoft.EntityFrameworkCore;

public class CategoryDbService : ICategoryService
{
    private readonly DbContext _context;

    public CategoryDbService(DbContext context)
    {
        _context = context;
    }

    // Crear una nueva categoría
    public Category Create(CategoryPostDTO categoryDto)
    {
        Category category = new()
        {
            Name = categoryDto.Name
        };

        _context.Categories.Add(category);
        _context.SaveChanges(); // Guardar cambios en la base de datos
        return category;
    }

    // Eliminar una categoría por su ID
    public void Delete(int id)
    {
        var category = _context.Categories.Find(id);
        if (category != null)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges(); // Guardar los cambios
        }
    }

    // Obtener todas las categorías
    public IEnumerable<Category> GetAll()
    {
        return _context.Categories;
    }

    // Obtener una categoría por su ID
    public Category? GetById(int id)
    {
        return _context.Categories.Find(id);
    }

    // Actualizar una categoría existente
    public Category? Update(int id, Category categoryToUpdate)
    {
        var existingCategory = _context.Categories.Find(id);
        if (existingCategory != null)
        {
            existingCategory.Name = categoryToUpdate.Name;

            _context.Entry(existingCategory).State = EntityState.Modified;
            _context.SaveChanges(); // Guardar cambios
            return existingCategory;
        }

        return null;
    }
}
