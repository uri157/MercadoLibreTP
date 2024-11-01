using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Photo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  
    public int Id { get; set; }

    [Required]
    public string ImageData { get; set; } // Cadena Base64 de la imagen

    // Propiedades de navegación
    public ICollection<PhotoPublication> PhotosPublication { get; set; } 

    // Constructor por defecto
    public Photo() { }

    // Constructor con parámetros
    public Photo(int id, string imageData)
    {
        Id = id;
        ImageData = imageData;
    }

    // Método ToString sobreescrito
    public override string ToString()
    {
        return $"Id: {Id}, ImageData Length: {ImageData?.Length ?? 0} characters";
    }
}
