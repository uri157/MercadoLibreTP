using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class Notification
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Esto indica que el ID será autogenerado
    public int Id { get; set; }  // Primary Key

    public string Text { get; set; }  // Texto de la notificación (requerido, máximo 500 caracteres)

    // Constructor por defecto
    public Notification()
    {
    }

    // Constructor con parámetros
    public Notification(int id, string text)
    {
        Id = id;
        Text = text;
    }

    // Método ToString sobreescrito para una representación en string
    public override string ToString()
    {
        return $"Notification ID: {Id}, Text: {Text}";
    }
}
