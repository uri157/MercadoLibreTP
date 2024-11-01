using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    // Obtener todas las transacciones
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<List<TransactionDTO>>> GetAll()
    {
        var transactions = await _transactionService.GetAll();
        return Ok(transactions);
    }

    // Obtener una transacción por ID
    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<TransactionDTO>> GetById(int id)
    {
        var transaction = await _transactionService.GetById(id);
        if (transaction == null)
        {
            return NotFound(); // Retorna 404 si no se encuentra
        }
        return Ok(transaction);
    }

    // Crear una nueva transacción
    [Authorize]
    [HttpPost]
    public async Task<ActionResult<TransactionDTO>> Create([FromBody] TransactionPostDTO transactionPostDto)
    {
        // Se asume que el userId es obtenido del contexto del usuario autenticado
        int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var transaction = await _transactionService.Create(userId, transactionPostDto);
        return CreatedAtAction(nameof(GetById), new { id = transaction.Id }, transaction);
    }

    // Actualizar una transacción existente
    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, [FromBody] TransactionPutDTO transactionPutDto)
    {
        // Se asume que el userId es obtenido del contexto del usuario autenticado
        int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var result = await _transactionService.Update(id, userId, transactionPutDto);
        if (!result)
        {
            return NotFound(); // Retorna 404 si no se encuentra
        }
        return NoContent(); // Retorna 204 si la actualización es exitosa
    }

    // Eliminar una transacción por ID
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _transactionService.Delete(id);
        if (!result)
        {
            return NotFound(); // Retorna 404 si no se encuentra
        }
        return NoContent(); // Retorna 204 si la eliminación es exitosa
    }

}
