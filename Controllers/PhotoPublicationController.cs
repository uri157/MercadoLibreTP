using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class PhotoPublicationController : ControllerBase
{
    private readonly IPhotoPublicationService _photoPublicationService;

    public PhotoPublicationController(IPhotoPublicationService photoPublicationService)
    {
        _photoPublicationService = photoPublicationService;
    }

    // GET: api/PhotoPublication
    [AllowAnonymous]
    [HttpGet]
    public ActionResult<IEnumerable<PhotoPublicationDTO>> GetPhotoPublications()
    {
        var photoPublications = _photoPublicationService.GetAll();
        return Ok(photoPublications);
    }

    // GET: api/PhotoPublication/{id}
    [AllowAnonymous]
    [HttpGet("{id}")]
    public ActionResult<PhotoPublicationDTO> GetPhotoPublication(int id)
    {
        var photoPublication = _photoPublicationService.GetById(id);
        if (photoPublication == null)
        {
            return NotFound();
        }
        return Ok(photoPublication);
    }

    // POST: api/PhotoPublication
    [Authorize]
    [HttpPost]
    public ActionResult<PhotoPublicationDTO> CreatePhotoPublication(PhotoPublicationPostPutDTO createDTO)
    {
        var createdPhotoPublication = _photoPublicationService.Create(createDTO);
        return CreatedAtAction(nameof(GetPhotoPublication), new { id = createdPhotoPublication.Id }, createdPhotoPublication);
    }

    // DELETE: api/PhotoPublication/{id}
    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult DeletePhotoPublication(int id)
    {
        _photoPublicationService.Delete(id);
        return NoContent();
    }
}
