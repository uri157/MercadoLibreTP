public interface IAccountService
{
    Task<IEnumerable<UserDTO>> GetAll();
    Task<UserDTO> GetById(int id);
    Task<UserDTO> Update(int id, UserPostPutDTO user);
}
