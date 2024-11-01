// using System.Text.Json.Serialization;
// using System.ComponentModel.DataAnnotations;
// using System.ComponentModel.DataAnnotations.Schema;

// public class Review
// {
//     [Key]
//     [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Esto indica que el ID será autogenerado
//     public int Id { get; set; }

//     public int IdUser { get; set; }  // Clave foránea a User
//     public int IdPublication { get; set; }  // Clave foránea a Publication

//     public int Calification { get; set; }  // Calificación (ej. de 1 a 5 estrellas)
//     public string? ReviewText { get; set; }  // Texto de la reseña

//     // Propiedades de navegación
//     public User User { get; set; }  // Relación con User
//     public Publication Publication { get; set; }  // Relación con Publication

//     // Constructor por defecto
//     public Review()
//     {
//     }

//     // Constructor con parámetros
//     public Review(int id, int idUser, int idPublication, int calification, string? reviewText)
//     {
//         Id = id;
//         IdUser = idUser;
//         IdPublication = idPublication;
//         Calification = calification;
//         ReviewText = reviewText;
//     }

//     // Método ToString sobreescrito
//     public override string ToString()
//     {
//         return $"Id: {Id}, IdUser: {IdUser}, IdPublication: {IdPublication}, Calification: {Calification}, Review: {ReviewText}";
//     }
// }
