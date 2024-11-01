using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/photos")]
public class PhotoController : ControllerBase
{
    private readonly IPhotoService _photoService;

    public PhotoController(IPhotoService photoService)
    {
        _photoService = photoService;
    }

    // Obtener todas las fotos
    [Authorize]
    [HttpGet]
    public ActionResult<List<PhotoDTO>> GetAllPhotos()
    {
        return Ok(_photoService.GetAll());
    }

    // Obtener una foto por ID
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var photo = _photoService.GetById(id);
        if (photo == null)
        {
            return NotFound("Photo not found");
        }

        return Ok(photo);  // Retorna la cadena Base64
    }

    // Subir una nueva foto
    [HttpPost]
    public ActionResult<PhotoDTO> NewPhoto([FromBody] PhotoPostPutDTO photoDto)
    {
        if (string.IsNullOrEmpty(photoDto.ImageData))
        {
            return BadRequest("Invalid image data.");
        }

        var newPhoto = _photoService.Create(photoDto);
        return CreatedAtAction(nameof(GetById), new { id = newPhoto.Id }, newPhoto);
    }

    // Eliminar una foto por ID
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var photo = _photoService.GetById(id);
        if (photo == null)
        {
            return NotFound("Photo not found");
        }

        _photoService.Delete(id);
        return NoContent();
    }
}
