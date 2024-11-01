// using Microsoft.EntityFrameworkCore;
// using System.Collections.Generic;
// using System.Linq;

// public class StateDbService : IStateService
// {
//     private readonly DbContext _context; // Contexto de la base de datos

//     public StateDbService(DbContext context)
//     {
//         _context = context;
//     }

//     // Crear un nuevo estado
//     public State Create(State state)
//     {
//         _context.States.Add(state);
//         _context.SaveChanges(); // Guardar cambios en la base de datos
//         return state;
//     }

//     // Eliminar un estado por su ID
//     public void Delete(int id)
//     {
//         var state = _context.States.Find(id);
//         if (state != null)
//         {
//             _context.States.Remove(state);
//             _context.SaveChanges(); // Guardar los cambios
//         }
//     }

//     // Obtener todos los estados
//     public IEnumerable<State> GetAll()
//     {
//         return _context.States.ToList();
//     }

//     // Obtener un estado por su ID
//     public State? GetById(int id)
//     {
//         return _context.States.Find(id);
//     }

//     // Actualizar un estado existente
//     public State? Update(int id, State stateToUpdate)
//     {
//         var existingState = _context.States.Find(id);
//         if (existingState != null)
//         {
//             existingState.Name = stateToUpdate.Name;
//             existingState.Description = stateToUpdate.Description;

//             _context.Entry(existingState).State = EntityState.Modified;
//             _context.SaveChanges(); // Guardar cambios
//             return existingState;
//         }

//         return null;
//     }
// }
