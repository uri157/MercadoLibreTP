using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class UserNotification
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Esto indica que el ID será autogenerado
    public int Id { get; set; }
    public int IdNotification { get; set; }  // Primary Key 1

    public int IdUser { get; set; }  // Primary Key 2

    public bool Seen { get; set; }  // Indicador de si la notificación ha sido vista

    public Notification Notification { get; set; }  // Relación con la entidad Notification

    public User User { get; set; }  // Relación con la entidad User

    // Constructor por defecto
    public UserNotification()
    {
    }

    // Constructor con parámetros
    public UserNotification(int idNotification, int idUser, bool seen)
    {
        IdNotification = idNotification;
        IdUser = idUser;
        Seen = seen;
    }

    // Método ToString sobreescrito para una representación en string
    public override string ToString()
    {
        return $"Notification ID: {IdNotification}, User ID: {IdUser}, Seen: {Seen}";
    }
}
