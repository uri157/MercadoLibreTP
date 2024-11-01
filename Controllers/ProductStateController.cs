using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/product-states")]
public class ProductStateController : ControllerBase
{
    private readonly IProductStateService _productStateService;

    public ProductStateController(IProductStateService productStateService)
    {
        _productStateService = productStateService;
    }

    // Obtener todos los estados de producto
    [AllowAnonymous]
    [HttpGet]
    public ActionResult<List<ProductStateDTO>> GetAllProductStates()
    {
        return Ok(_productStateService.GetAll());
    }

    // Obtener un estado de producto por ID
    [AllowAnonymous]
    [HttpGet("{id}")]
    public ActionResult<ProductStateDTO> GetById(int id)
    {
        var productState = _productStateService.GetById(id);
        if (productState == null)
        {
            return NotFound("Product state not found");
        }
        return Ok(productState);
    }

    // Crear un nuevo estado de producto
    [Authorize(Roles = "admin")]
    [HttpPost]
    public ActionResult<ProductStateDTO> NewProductState(ProductStatePostPutDTO productStateDto)
    {
        var newProductState = _productStateService.Create(productStateDto);
        return CreatedAtAction(nameof(GetById), new { id = newProductState.Id }, newProductState);
    }

    // Eliminar un estado de producto por ID
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var productState = _productStateService.GetById(id);
        if (productState == null)
        {
            return NotFound("Product state not found");
        }

        _productStateService.Delete(id);
        return NoContent();
    }

    // Actualizar un estado de producto por ID
    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public ActionResult<ProductStateDTO> UpdateProductState(int id, ProductStatePostPutDTO productStateDto)
    {
        
        var updatedProductState = _productStateService.Update(id, productStateDto);
        if (updatedProductState == null)
        {
            return NotFound("Product state not found");
        }

        return CreatedAtAction(nameof(GetById), new { id = updatedProductState.Id }, updatedProductState);
    }
}
