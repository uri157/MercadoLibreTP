using Microsoft.EntityFrameworkCore;

public class ProductStateDbService : IProductStateService
{
    private readonly DbContext _context; // Contexto de la base de datos

    public ProductStateDbService(DbContext context)
    {
        _context = context;
    }

    // Crear un nuevo estado de producto
    public ProductStateDTO Create(ProductStatePostPutDTO productStateDto)
    {
        ProductState productState = new()
        {
            Name = productStateDto.Name,
            Description = productStateDto.Description
        };

        _context.ProductStates.Add(productState);
        _context.SaveChanges(); // Guardar cambios en la base de datos

        // Retornar el DTO correspondiente
        return new ProductStateDTO
        {
            Id = productState.Id,
            Name = productState.Name,
            Description = productState.Description
        };
    }

    // Eliminar un estado de producto por su ID
    public void Delete(int id)
    {
        var productState = _context.ProductStates.Find(id);
        if (productState != null)
        {
            _context.ProductStates.Remove(productState);
            _context.SaveChanges(); // Guardar los cambios
        }
    }

    // Obtener todos los estados de producto
    public IEnumerable<ProductStateDTO> GetAll()
    {
        return _context.ProductStates
            .Select(ps => new ProductStateDTO
            {
                Id = ps.Id,
                Name = ps.Name,
                Description = ps.Description
            })
            .ToList();
    }

    // Obtener un estado de producto por su ID
    public ProductStateDTO? GetById(int id)
    {
        var productState = _context.ProductStates.Find(id);
        if (productState != null)
        {
            return new ProductStateDTO
            {
                Id = productState.Id,
                Name = productState.Name,
                Description = productState.Description
            };
        }
        return null;
    }

    // Actualizar un estado de producto existente
    public ProductStateDTO? Update(int id, ProductStatePostPutDTO productStateDto)
    {
        var existingProductState = _context.ProductStates.Find(id);
        if (existingProductState != null)
        {
            existingProductState.Name = productStateDto.Name;
            existingProductState.Description = productStateDto.Description;

            _context.Entry(existingProductState).State = EntityState.Modified;
            _context.SaveChanges(); // Guardar cambios

            return new ProductStateDTO
            {
                Id = existingProductState.Id,
                Name = existingProductState.Name,
                Description = existingProductState.Description
            };
        }

        return null;
    }
}
