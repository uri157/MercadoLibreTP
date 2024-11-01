using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/colors")]
public class ColorController : ControllerBase
{
    private readonly IColorService _ColorService;

    public ColorController(IColorService ColorService)
    {
        _ColorService = ColorService;
    }

    // Obtener todos los estados de publicación
    [AllowAnonymous]
    [HttpGet]
    public ActionResult<IEnumerable<ColorDTO>> GetAllColors()
    {
        return Ok(_ColorService.GetAll());
    }

    // Obtener un estado de publicación por ID
    [AllowAnonymous]
    [HttpGet("{id}")]
    public ActionResult<ColorDTO> GetById(int id)
    {
        var Color = _ColorService.GetById(id);
        if (Color == null)
        {
            return NotFound("Color not found");
        }
        return Ok(Color);
    }

    // Crear un nuevo estado de publicación
    [Authorize(Roles = "admin")]
    [HttpPost]
    public ActionResult<ColorDTO> NewColor(ColorPostPutDTO ColorDto)
    {
        var newColor = _ColorService.Create(ColorDto);
        return CreatedAtAction(nameof(GetById), new { id = newColor.Id }, newColor);
    }

    // Actualizar un estado de publicación por ID
    [Authorize(Roles = "admin")]
    [HttpPut("{id}")]
    public ActionResult<ColorDTO> UpdateColor(int id, ColorPostPutDTO ColorDto)
    {

        var updatedColor = _ColorService.Update(id, ColorDto);
        if (updatedColor == null)
        {
            return NotFound("Color not found");
        }

        return Ok(updatedColor);
    }

    // Eliminar un estado de publicación por ID
    [Authorize(Roles = "admin")]
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var Color = _ColorService.GetById(id);
        if (Color == null)
        {
            return NotFound("Color not found");
        }

        _ColorService.Delete(id);
        return NoContent();
    }
}
