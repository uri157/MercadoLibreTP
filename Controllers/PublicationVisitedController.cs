using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Route("api/[controller]")]
[ApiController]
public class PublicationVisitedController : ControllerBase
{
    private readonly IPublicationVisitedService _publicationVisitedService;

    public PublicationVisitedController(IPublicationVisitedService publicationVisitedService)
    {
        _publicationVisitedService = publicationVisitedService;
    }

    // Obtener todos los registros de PublicationVisited
    [HttpGet]
    public ActionResult<IEnumerable<PublicationVisitedDTO>> GetAll()
    {
        var publicationsVisited = _publicationVisitedService.GetAll();
        return Ok(publicationsVisited);
    }

    // Obtener un registro por ID
    [HttpGet("{id}")]
    public ActionResult<PublicationVisitedDTO> GetById(int id)
    {
        var publicationVisited = _publicationVisitedService.GetById(id);
        if (publicationVisited == null)
        {
            return NotFound("PublicationVisited not found");
        }
        return Ok(publicationVisited);
    }

    // Crear un nuevo registro
    [HttpPost]
    public ActionResult<PublicationVisitedDTO> Create(int IdPublication)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var publicationVisited = new PublicationVisited
        {
            IdUser = userId,
            IdPublication = IdPublication
        };

        var createdPublicationVisited = _publicationVisitedService.Create(publicationVisited);
        return CreatedAtAction(nameof(GetById), new { id = createdPublicationVisited.Id }, createdPublicationVisited);
    }

    // // Actualizar un registro existente
    // [HttpPut("{id}")]
    // public ActionResult<PublicationVisitedDTO> Update(int id, [FromBody] PublicationVisitedDTO publicationVisitedDTO)
    // {
    //     if (!ModelState.IsValid)
    //     {
    //         return BadRequest(ModelState);
    //     }

    //     var updatedPublicationVisited = _publicationVisitedService.Update(id, new PublicationVisited
    //     {
    //         IdUser = publicationVisitedDTO.IdUser,
    //         IdPublication = publicationVisitedDTO.IdPublication
    //     });

    //     if (updatedPublicationVisited == null)
    //     {
    //         return NotFound("PublicationVisited not found");
    //     }

    //     return Ok(updatedPublicationVisited);
    // }

    // // Eliminar un registro
    // [HttpDelete("{id}")]
    // public ActionResult Delete(int id)
    // {
    //     _publicationVisitedService.Delete(id);
    //     return NoContent(); // Devuelve un 204 No Content si la eliminación fue exitosa
    // }
}
