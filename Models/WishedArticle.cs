using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class WishedArticle
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Esto indica que el ID será autogenerado
    public int Id { get; set; }

    public int IdUser { get; set; } // Clave foránea a User
    
    [JsonIgnore]
    public User User { get; set; }  // Propiedad de navegación hacia User

    public int IdPublication { get; set; } // Clave foránea a Publication

    public Publication Publication { get; set; }  // Propiedad de navegación hacia Publication

    // Constructor por defecto
    public WishedArticle()
    {
    }

    // Constructor con parámetros
    public WishedArticle(int id, int idUser, int idPublication)
    {
        Id = id;
        IdUser = idUser;
        IdPublication = idPublication;
    }

    // Método ToString sobreescrito
    public override string ToString()
    {
        return $"Id: {Id}, IdUser: {IdUser}, IdPublication: {IdPublication}";
    }
}
