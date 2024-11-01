using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Text.Json.Serialization;

public class User : IdentityUser<int> // Usa int como el tipo de Id si lo prefieres
{
    public int? ProfilePhotoId { get; set; } // IdFotoPerfil opcional
    public string? FirstName { get; set; } // Nombre
    public string? LastName { get; set; } // Apellido
    public string? DNI { get; set; } // Documento de Identidad opcional
    public string? Address { get; set; } // Dirección opcional

    // Propiedades de navegación
    public List<Publication>? Publications { get; set; } // Publicaciones del usuario
    public List<Card>? Cards { get; set; } // Tarjetas asociadas al usuario
    public List<WishedArticle>? WishedArticles { get; set; } // Artículos deseados
    // public ICollection<Review>? Reviews { get; set; } // Reseñas dejadas por el usuario
    public List<UserNotification>? UserNotifications { get; set; } // Notificaciones del usuario
    public Photo? ProfilePhoto { get; set; } // Foto de perfil
    public ICollection<Transaction> Purchases { get; set; }


    public User()
    {
        Publications = new List<Publication>();
        Cards = new List<Card>();
        WishedArticles = new List<WishedArticle>();
        // Reviews = new List<Review>();
        UserNotifications = new List<UserNotification>();
        Purchases = new List<Transaction>();
    }
}
