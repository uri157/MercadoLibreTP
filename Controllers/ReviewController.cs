// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;
// using System.Security.Claims;

// [ApiController]
// [Route("api/reviews")]
// public class ReviewController : ControllerBase
// {
//     private readonly IReviewService _reviewService;

//     public ReviewController(IReviewService reviewService)
//     {
//         _reviewService = reviewService;
//     }

//     // Obtener todas las reseñas
//     [AllowAnonymous]
//     [HttpGet]
//     public ActionResult<IEnumerable<ReviewDTO>> GetAllReviews()
//     {
//         return Ok(_reviewService.GetAll());
//     }

//     // Obtener una reseña por ID
//     [AllowAnonymous]
//     [HttpGet("{id}")]
//     public ActionResult<ReviewDTO> GetById(int id)
//     {
//         var review = _reviewService.GetById(id);
//         if (review == null)
//         {
//             return NotFound("Review not found");
//         }
//         return Ok(review);
//     }

//     // Crear una nueva reseña
//     [Authorize]
//     [HttpPost]
//     public ActionResult<ReviewDTO> NewReview(ReviewPostPutDTO reviewDto)
//     {
//         // Mapear manualmente de ReviewPostPutDTO a Review
//         int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
//         var newReview = _reviewService.Create(userId, reviewDto); // Pasamos directamente el DTO al servicio

//         return CreatedAtAction(nameof(GetById), new { id = newReview.Id }, newReview);

//     }

//     // Actualizar una reseña existente
//     [Authorize]
//     [HttpPut("{id}")]
//     public ActionResult<ReviewDTO> UpdateReview(int id, ReviewPostPutDTO reviewToUpdate)
//     {
//         int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

//         // Obtener la reseña antes de actualizarla
//         var existingReview = _reviewService.GetById(id);
//         if (existingReview == null)
//         {
//             return NotFound("Review not found");
//         }

//         // Verificar si el usuario actual es el creador de la reseña
//         if (existingReview.IdUser != userId)
//         {
//             return Forbid("You do not have permission to edit this review, because you're not the buyer");
//         }

//         // Si la validación pasa, realizar la actualización

//         var updatedReview = _reviewService.Update(id,userId , reviewToUpdate);
//         if (updatedReview == null)
//         {
//             return NotFound("Review not found");
//         }

//         return Ok(updatedReview);
//     }

//     // Eliminar una reseña por ID
//     [Authorize(Roles = "admin")]
//     [HttpDelete("{id}")]
//     public ActionResult DeleteReview(int id)
//     {
//         var review = _reviewService.GetById(id);
//         if (review == null)
//         {
//             return NotFound("Review not found");
//         }

//         _reviewService.Delete(id);
//         return NoContent();
//     }
// }
