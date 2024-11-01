using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



public class AccountDbService : IAccountService
{
    private readonly UserManager<User> _userManager;

    public AccountDbService(UserManager<User> userManager) // Constructor with correct return type
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<UserDTO>> GetAll()
    {
        var users = await _userManager.Users.ToListAsync();
        return users.Select(user => new UserDTO
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,   
            Password = user.PasswordHash,
            ProfilePhotoId = user.ProfilePhotoId,
            DNI = user.DNI,
            Address = user.Address
        }).ToList();
    }

    public async Task<UserDTO> GetById(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        return user == null ? null : new UserDTO
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,   
            Password = user.PasswordHash,
            ProfilePhotoId = user.ProfilePhotoId,
            DNI = user.DNI,
            Address = user.Address
        };
    }

    public async Task<UserDTO> Update(int id, UserPostPutDTO userDto)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            throw new InvalidOperationException("Usuario no encontrado");
        }

        // Actualizar los campos opcionales y no opcionales
        user.UserName = userDto.Username ?? user.UserName;
        user.Email = userDto.Email ?? user.Email;
        user.FirstName = userDto.FirstName ?? user.FirstName;
        user.LastName = userDto.LastName ?? user.LastName;
        user.DNI = userDto.DNI ?? user.DNI;
        user.Address = userDto.Address ?? user.Address;
        user.ProfilePhotoId = userDto.ProfilePhotoId ?? user.ProfilePhotoId;
        user.PasswordHash = !string.IsNullOrEmpty(userDto.Password) 
                            ? _userManager.PasswordHasher.HashPassword(user, userDto.Password) 
                            : user.PasswordHash;

        // Intentar actualizar en la base de datos
        var result = await _userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Error al actualizar el usuario: " + string.Join(", ", result.Errors.Select(e => e.Description)));
        }

        // Devolver el UserDTO con la información actualizada
        return new UserDTO
        {
            Id = user.Id,
            Username = user.UserName,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            DNI = user.DNI,
            Address = user.Address,
            ProfilePhotoId = user.ProfilePhotoId
        };
    }



}
