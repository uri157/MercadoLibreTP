public interface IUserQuestionService
{
    public IEnumerable<UserQuestion> GetAll(); // Obtener todas las preguntas de usuarios
    public UserQuestion? GetById(int id); // Obtener una pregunta de usuario por su ID
    public UserQuestion Create(UserQuestion userQuestion); // Crear una nueva pregunta de usuario
    public void Delete(int id); // Eliminar una pregunta de usuario
    public UserQuestion? Update(int id, UserQuestion userQuestion); // Actualizar una pregunta de usuario existente
}
