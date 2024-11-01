using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class UserPublication
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Esto indica que el ID será autogenerado
    public int Id { get; set; }

    public int IdUser { get; set; } // Clave foránea a User

    public int IdPublication { get; set; } // Clave foránea a Publication

    // Constructor por defecto
    public UserPublication()
    {
    }

    // Constructor con parámetros
    public UserPublication(int id, int idUser, int idPublication)
    {
        Id = id;
        IdUser = idUser;
        IdPublication = idPublication;
    }

    // Método ToString sobreescrito
    override public string ToString()
    {
        return $"Id: {Id}, IdUser: {IdUser}, IdPublication: {IdPublication}";
    }
}
