using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Category
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Esto indica que el ID será autogenerado
    public int Id { get; set; }

    public string? Name { get; set; } // Nombre de la categoría

    // Propiedad de navegación: Una lista de publicaciones relacionadas con esta categoría
    public ICollection<Publication> Publications { get; set; }

    // Constructor por defecto
    public Category()
    {
        Publications = new List<Publication>(); // Inicializa la lista
    }

    // Constructor con parámetros
    public Category(int id, string? name)
    {
        Id = id;
        Name = name;
        Publications = new List<Publication>(); // Inicializa la lista
    }

    // Método ToString sobreescrito
    override public string ToString()
    {
        return $"Id: {Id}, Name: {Name}";
    }
}
