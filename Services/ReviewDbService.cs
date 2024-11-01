// using Microsoft.EntityFrameworkCore;
// using System.Collections.Generic;
// using System.Linq;

// public class ReviewDbService : IReviewService
// {
//     private readonly DbContext _context; // Contexto de la base de datos

//     public ReviewDbService(DbContext context)
//     {
//         _context = context;
//     }

//     // Crear una nueva reseña
//      public ReviewDTO Create(int idUser,ReviewPostPutDTO reviewDto)
//     {
//         Review newReview = new()
//         {
//             IdUser = idUser,
//             IdPublication = reviewDto.IdPublication,
//             ReviewText = reviewDto.ReviewText,
//             Calification = reviewDto.Calification
//         };

//         _context.Reviews.Add(newReview);
//         _context.SaveChanges(); // Guardar cambios en la base de datos
//         return new ReviewDTO 
//         {
//             Id = newReview.Id,
//             IdUser = idUser,
//             IdPublication = reviewDto.IdPublication,
//             ReviewText = reviewDto.ReviewText,
//             Calification = reviewDto.Calification
//         };
//     }

//     // Eliminar una reseña por su ID
//     public void Delete(int id)
//     {
//         var review = _context.Reviews.Find(id);
//         if (review != null)
//         {
//             _context.Reviews.Remove(review);
//             _context.SaveChanges(); // Guardar los cambios
//         }
//     }

//     // Obtener todas las reseñas
//     public IEnumerable<ReviewDTO> GetAll()
//     {
//         // Llamar al servicio para obtener todas las reseñas
//         return _context.Reviews.Select(r => new ReviewDTO
//             {
//                 Id = r.Id,
//                 IdUser = r.IdUser,
//                 IdPublication = r.IdPublication,
//                 Calification = r.Calification,
//                 ReviewText = r.ReviewText
//             }).ToList();
//     }

//     // Obtener una reseña por su ID
//     public ReviewDTO? GetById(int id)
//     {
//         var review = _context.Reviews.Find(id);
//         if (review != null)
//         {
//             return new ReviewDTO
//             {
//                 Id = review.Id,
//                 IdUser = review.IdUser,
//                 IdPublication = review.IdPublication,
//                 Calification = review.Calification,
//                 ReviewText = review.ReviewText
//             };
//         }
//         return null;
//     }



//     // public Review? GetById(int id)
//     // {
//     //     return _context.Reviews.Find(id);
//     // }

//     // Actualizar una reseña existente
//     public ReviewDTO? Update(int id, int idUser, ReviewPostPutDTO reviewToUpdate)
//     {
//         var existingReview = _context.Reviews.Find(id);
//         if (existingReview != null)
//         {
//             existingReview.IdUser = idUser;
//             existingReview.IdPublication = reviewToUpdate.IdPublication;
//             existingReview.ReviewText = reviewToUpdate.ReviewText;
//             existingReview.Calification = reviewToUpdate.Calification;

//             _context.Entry(existingReview).State = EntityState.Modified;
//             _context.SaveChanges(); // Guardar cambios
//             return new ReviewDTO 
//         {
//             Id = existingReview.Id,
//             IdUser = idUser,
//             IdPublication = reviewToUpdate.IdPublication,
//             ReviewText = reviewToUpdate.ReviewText,
//             Calification = reviewToUpdate.Calification
//         };
//         }

//         return null;
//     }
// }
