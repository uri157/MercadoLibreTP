using System.Collections.Generic;
using System.Text.Json.Serialization;

public class Publication
{
    public int Id { get; set; }

    public int UserId { get; set; } // Clave foránea a User

    public int IdCategoria { get; set; } // Clave foránea a Category

    public string? Description { get; set; } // Descripción de la publicación

    public decimal Price { get; set; } // Precio

    public string? Title { get; set; } // Título de la publicación

    public int IdPublicationState { get; set; } // Clave foránea al estado de la publicación

    public int IdProductState { get; set; } // Clave foránea al estado de la publicación

    public int IdColor { get; set; } // Clave foránea al color del producto

    public int Stock { get; set; } // Cantidad disponible en stock

    // Propiedades de navegación
    public User User { get; set; }  // Propiedad de navegación hacia User
    public ProductState ProductState { get; set; }  // Estado del producto
    public PublicationState PublicationState { get; set; }  // Estado de la publicación
    public Category Category { get; set; }  // Categoría de la publicación
    public ICollection<UserQuestion> UserQuestions { get; set; }  // Preguntas del usuario
    public ICollection<PhotoPublication> PhotosPublication { get; set; }  // Fotos de la publicación
    public ICollection<ShoppingCartItem> ShoppingCartItems { get; set; } 
    // public ICollection<Review> Reviews { get; set; }  // Reseñas de la publicación

    [JsonIgnore]
    public ICollection<WishedArticle> WishedArticles { get; set; }  // Lista de artículos deseados
    public ICollection<Transaction> Sales { get; set; }

    // Constructor por defecto
    public Publication()
    {
        UserQuestions = new List<UserQuestion>();
        PhotosPublication = new List<PhotoPublication>();
        // Reviews = new List<Review>();
        WishedArticles = new List<WishedArticle>();
        Sales = new List<Transaction>(); // Inicializa la lista
        ShoppingCartItems = new List<ShoppingCartItem>();
    }

    // Constructor con parámetros
    public Publication(int id, int userId, int idCategoria, string? description, decimal price, string? title, int idState, int idColor, int stock)
    {
        Id = id;
        UserId = userId;
        IdCategoria = idCategoria;
        Description = description;
        Price = price;
        Title = title;
        IdPublicationState = idState;
        IdColor = idColor;
        Stock = stock;
        UserQuestions = new List<UserQuestion>();
        PhotosPublication = new List<PhotoPublication>();
        // Reviews = new List<Review>();
        WishedArticles = new List<WishedArticle>();
        Sales = new List<Transaction>(); // Inicializa la lista
        ShoppingCartItems = new List<ShoppingCartItem>();

    }

    // Método ToString sobreescrito
    public override string ToString()
    {
        return $"Id: {Id}, Title: {Title}, Price: {Price}, Stock: {Stock}, Description: {Description}";
    }
}
