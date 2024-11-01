public interface IStateService
{
    public IEnumerable<State> GetAll(); // Obtener todos los estados
    public State? GetById(int id); // Obtener un estado por su ID
    public State Create(State state); // Crear un nuevo estado
    public void Delete(int id); // Eliminar un estado
    public State? Update(int id, State state); // Actualizar un estado existente
}
