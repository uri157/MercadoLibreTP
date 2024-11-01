using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class TransactionDbService : ITransactionService
{
    private readonly DbContext _context;

    public TransactionDbService(DbContext context)
    {
        _context = context;
    }

    // Obtener todas las transacciones
    public async Task<List<TransactionDTO>> GetAll()
    {
        return await _context.Set<Transaction>()
            .Include(t => t.Buyer)
            .Include(t => t.Publication)
            .Select(t => new TransactionDTO
            {
                Id = t.Id,
                IdBuyer = t.IdBuyer,
                IdPublication = t.IdPublication,
                TransactionDate = t.TransactionDate,
                Amount = t.Amount,
                Calification = t.Calification,
                ReviewText = t.ReviewText
            })
            .ToListAsync();
    }

    // Obtener una transacción por ID
    public async Task<TransactionDTO> GetById(int id)
    {
        var transaction = await _context.Set<Transaction>()
            .Include(t => t.Buyer)
            .Include(t => t.Publication)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (transaction == null)
        {
            return null; // o lanzar una excepción si prefieres
        }

        return new TransactionDTO
        {
            Id = transaction.Id,
            IdBuyer = transaction.IdBuyer,
            IdPublication = transaction.IdPublication,
            TransactionDate = transaction.TransactionDate,
            Amount = transaction.Amount,
            Calification = transaction.Calification,
            ReviewText = transaction.ReviewText
        };
    }

    // Crear una nueva transacción
    public async Task<TransactionDTO> Create(int userId, TransactionPostDTO transactionPostDto)
    {

        // Consulta la publicación para obtener el Amount
        var publication = await _context.Set<Publication>()
            .FirstOrDefaultAsync(p => p.Id == transactionPostDto.IdPublication);

        if (publication == null)
        {
            // Maneja el caso de publicación no encontrada
            throw new Exception("Publication not found");
        }

        var transaction = new Transaction
        {
            IdBuyer = userId,
            IdPublication = transactionPostDto.IdPublication,
            TransactionDate = DateTime.UtcNow, // O la fecha que prefieras
            Amount = publication.Price
        };

        _context.Set<Transaction>().Add(transaction);
        await _context.SaveChangesAsync();

        return new TransactionDTO
        {
            Id = transaction.Id,
            IdBuyer = transaction.IdBuyer,
            IdPublication = transaction.IdPublication,
            TransactionDate = transaction.TransactionDate,
            Amount = transaction.Amount,
            Calification = transaction.Calification,
            ReviewText = transaction.ReviewText
        };
    }

    // Actualizar una transacción existente
    public async Task<bool> Update(int id, int userId, TransactionPutDTO transactionPutDto)
    {
        var transaction = await _context.Set<Transaction>().FindAsync(id);
        if (transaction == null || transaction.IdBuyer != userId)
        {
            return false; // No se encontró la transacción o el usuario no tiene permiso para actualizarla
        }

        transaction.Calification = transactionPutDto.Calification;
        transaction.ReviewText = transactionPutDto.ReviewText;

        await _context.SaveChangesAsync();
        return true;
    }

    // Eliminar una transacción por ID
    public async Task<bool> Delete(int id)
    {
        var transaction = await _context.Set<Transaction>().FindAsync(id);
        if (transaction == null)
        {
            return false; // No se encontró la transacción
        }

        _context.Set<Transaction>().Remove(transaction);
        await _context.SaveChangesAsync();
        return true;
    }
}
