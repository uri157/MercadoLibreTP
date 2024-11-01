using System.Text.Json.Serialization;

public class State
{
    public int Id { get; set; }
    
    public string? Name { get; set; } // Nombre del estado
    
    public string? Description { get; set; } // Descripción del estado

    // Constructor por defecto
    public State()
    {
    }

    // Constructor con parámetros
    public State(int id, string? name, string? description)
    {
        Id = id;
        Name = name;
        Description = description;
    }

    // Método ToString sobreescrito
    override public string ToString()
    {
        return $"Id: {Id}, Name: {Name}, Description: {Description}";
    }
}
