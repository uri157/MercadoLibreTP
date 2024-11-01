using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
public class PhotoPublication
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Esto indica que el ID será autogenerado
    public int Id { get; set; }
    public int IdPublication { get; set; }  // Primary Key 1

    public int IdPhoto { get; set; }  // Primary Key 2

    public Photo Photo { get; set; }  // Relación con la entidad Photo

    public Publication Publication { get; set; }  // Relación con la entidad Publication

    // Constructor por defecto
    public PhotoPublication()
    {
    }

    // Constructor con parámetros
    public PhotoPublication(int idPublication, int idPhoto)
    {
        IdPublication = idPublication;
        IdPhoto = idPhoto;
    }

    // Método ToString sobreescrito para una representación en string
    public override string ToString()
    {
        return $"Publication ID: {IdPublication}, Photo ID: {IdPhoto}";
    }
}
