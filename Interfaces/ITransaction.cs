using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ITransactionService
{
    Task<List<TransactionDTO>> GetAll(); // Obtener todas las transacciones

    Task<TransactionDTO> GetById(int id); // Obtener una transacción por ID

    Task<TransactionDTO> Create(int userId, TransactionPostDTO transactionPostDto); // Crear una nueva transacción

    Task<bool> Update(int id, int userId, TransactionPutDTO transactionPutDto); // Actualizar una transacción existente

    Task<bool> Delete(int id); // Eliminar una transacción por ID
}
