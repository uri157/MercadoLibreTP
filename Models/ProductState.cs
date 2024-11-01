using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProductState
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  // Esto indica que el ID será autogenerado
    public int Id { get; set; }  // Primary Key

    public string Name { get; set; }  // Nombre del estado del producto

    public string Description { get; set; }  // Descripción del estado

    // Propiedad de navegación: Una lista de publicaciones relacionadas con este estado
    public ICollection<Publication> Publications { get; set; }

    // Constructor por defecto
    public ProductState()
    {
        Publications = new List<Publication>(); // Inicializa la lista
    }

    // Constructor con parámetros
    public ProductState(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
        Publications = new List<Publication>();
    }

    // Método ToString sobreescrito para una representación en string
    public override string ToString()
    {
        return $"ProductState ID: {Id}, Name: {Name}";
    }
}
