using System.Collections.Generic;
using System.Text.Json.Serialization;

public class PublicationState
{
    public int Id { get; set; }  // Primary Key

    public string Name { get; set; }  // Nombre del estado de la publicación

    public string Description { get; set; }  // Descripción del estado

    // Propiedad de navegación: Una lista de publicaciones relacionadas con este estado
    public ICollection<Publication> Publications { get; set; }

    // Constructor por defecto
    public PublicationState()
    {
        Publications = new List<Publication>(); // Inicializa la lista
    }

    // Constructor con parámetros
    public PublicationState(int id, string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
        Publications = new List<Publication>(); // Inicializa la lista
    }

    // Método ToString sobreescrito
    public override string ToString()
    {
        return $"PublicationState ID: {Id}, Name: {Name}";
    }
}
