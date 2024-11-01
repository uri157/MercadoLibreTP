// using System.ComponentModel.DataAnnotations;

// public class ReviewDTO
// {
//     [Required(ErrorMessage = "El campo Id es requerido.")]
//     public int Id { get; set; }

//     [Required(ErrorMessage = "El campo IdUser es requerido.")]
//     public int IdUser { get; set; }

//     [Required(ErrorMessage = "El campo IdPublication es requerido.")]
//     public int IdPublication { get; set; }

//     [MaxLength(1000, ErrorMessage = "La reseña no debe exceder los 1000 caracteres.")]
//     public string? ReviewText { get; set; }

//     [Required(ErrorMessage = "El campo Calification es requerido.")]
//     [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5.")]
//     public int Calification { get; set; }
// }


// public class ReviewPostPutDTO
// {

//     [Required(ErrorMessage = "El campo IdPublication es requerido.")]
//     public int IdPublication { get; set; }

//     [MaxLength(1000, ErrorMessage = "La reseña no debe exceder los 1000 caracteres.")]
//     public string? ReviewText { get; set; }

//     [Required(ErrorMessage = "El campo Calification es requerido.")]
//     [Range(1, 5, ErrorMessage = "La calificación debe estar entre 1 y 5.")]
//     public int Calification { get; set; }
// }
