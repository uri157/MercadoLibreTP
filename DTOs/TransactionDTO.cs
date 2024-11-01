using System;
using System.ComponentModel.DataAnnotations;

public class TransactionDTO
{
    public int Id { get; set; }                // Identificador único de la transacción

    public int IdBuyer { get; set; }            // Clave foránea hacia el usuario comprador

    public int? IdSeller { get; set; }          // Clave foránea hacia el vendedor (opcional)

    public int IdPublication { get; set; }      // Clave foránea hacia la publicación

    public DateTime TransactionDate { get; set; } // Fecha de la transacción

    public decimal Amount { get; set; }         // Monto de la transacción

    public int? Calification { get; set; }      // Calificación (1-5), puede ser nula

    public string? ReviewText { get; set; }     // Texto de la reseña
}

// DTO para crear una nueva transacción
public class TransactionPostDTO
{
    [Required]
    public int IdPublication { get; set; }      // Clave foránea hacia la publicación

}

// DTO para actualizar una transacción existente
public class TransactionPutDTO
{
    public int? Calification { get; set; }      // Calificación (1-5), puede ser nula

    public string? ReviewText { get; set; }     // Texto de la reseña
}
