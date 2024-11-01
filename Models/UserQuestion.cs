using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class UserQuestion
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Esto indica que el ID será autogenerado
    public int Id { get; set; }

    public int IdUser { get; set; } // Clave foránea a User

    public int IdPublication { get; set; } // Clave foránea a Publication

    public string? Question { get; set; } // Pregunta del usuario

    public string? Answer { get; set; } // Respuesta a la pregunta

    // Propiedad de navegación: Referencia a la publicación relacionada
    public Publication Publication { get; set; }

    // Constructor por defecto
    public UserQuestion()
    {
    }

    // Constructor con parámetros
    public UserQuestion(int id, int idUser, int idPublication, string? question, string? answer)
    {
        Id = id;
        IdUser = idUser;
        IdPublication = idPublication;
        Question = question;
        Answer = answer;
    }

    // Método ToString sobreescrito
    override public string ToString()
    {
        return $"Id: {Id}, IdUser: {IdUser}, IdPublication: {IdPublication}, Question: {Question}, Answer: {Answer}";
    }
}
