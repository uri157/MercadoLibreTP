using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/publications")]
public class PublicationController : ControllerBase
{
    private readonly IPublicationService _publicationService;

    public PublicationController(IPublicationService publicationService)
    {
        _publicationService = publicationService;
    }

    // Obtener todas las publicaciones
    [AllowAnonymous]
    [HttpGet]
    [Route("api/publications")]
    public ActionResult<IEnumerable<PublicationDTO>> GetAllPublications()
    {
        return Ok(_publicationService.GetAll());
    }

    // Obtener todas las publicaciones, con la opción de filtrar por nombre de categoría
    [AllowAnonymous]
    [HttpGet]
    [HttpGet("by-category")]
    public ActionResult<IEnumerable<PublicationDTO>> GetPublicationsByCategoryName(string categoryName)
    {
        IEnumerable<PublicationDTO> publications;

        if (!string.IsNullOrEmpty(categoryName))
        {
            publications = _publicationService.GetByCategoryName(categoryName);
        }
        else
        {
            publications = _publicationService.GetAll();
        }

        return Ok(publications);
    }


    // Obtener una publicación por ID
    [AllowAnonymous]
    [HttpGet("{id}")]
    public ActionResult<PublicationDTO> GetById(int id)
    {
        var publication = _publicationService.GetById(id);
        if (publication == null)
        {
            return NotFound("Publication not found");
        }


        // Verificar si el usuario está autenticado
        if (User.Identity.IsAuthenticated)
        {
            
        }


        return Ok(publication);
    }

    // Crear una nueva publicación
    [Authorize]
    [HttpPost]
    public ActionResult<PublicationDTO> NewPublication(PublicationPostDTO publicationDto)
    {
        // Obtén el ID del usuario autenticado desde los claims
        int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var newPublication = _publicationService.Create(userId, publicationDto);
        return CreatedAtAction(nameof(GetById), new { id = newPublication.Id }, newPublication);
    }

    // Actualizar una publicación por ID
    [Authorize]
    [HttpPut("{id}")]
    public ActionResult<PublicationDTO> UpdatePublication(int id, PublicationPutDTO publicationToUpdate)
    {
        
        var updatedPublication = _publicationService.Update(id, publicationToUpdate);
        if (updatedPublication == null)
        {
            return NotFound("Publication not found");
        }

        return Ok(updatedPublication);
    }

    // Eliminar una publicación por ID
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var publication = _publicationService.GetById(id);
        if (publication == null)
        {
            return NotFound("Publication not found");
        }

        _publicationService.Delete(id);
        return NoContent();
    }
}
