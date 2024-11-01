public interface IUserService
{
    public IEnumerable<User> GetAll();               // Obtener todos los usuarios
    public User? GetById(int id);                    // Obtener un usuario por su ID
    public User Create(UserDTO u);                   // Crear un nuevo usuario a partir del UserDTO
    public void Delete(int id);                      // Eliminar un usuario por su ID
    public User? Update(int id, UserDTO u);             // Actualizar un usuario por su ID
    public Photo? GetProfilePicture(int id);       // Obtener la imagen de perfil del usuario
}
