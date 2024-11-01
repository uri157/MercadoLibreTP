using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

[ApiController]
[Route("api/account")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AccountController(
        UserManager<User> userManager,
        IAccountService accountService,
        RoleManager<IdentityRole<int>> roleManager,
        SignInManager<User> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _accountService = accountService;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }

    // Registro de usuarios
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(UserPostPutDTO model)
    {
        var userExists = await _userManager.FindByNameAsync(model.Username);
        if (userExists != null)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "El usuario ya existe" });
        }

        var user = new User
        {
            UserName = model.Username,
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            FirstName = model.FirstName,
            LastName = model.LastName,
            DNI = model.DNI,
            Address = model.Address,
            ProfilePhotoId = model.ProfilePhotoId
        };

        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new
            {
                Message = "Error al crear usuario",
                Errors = result.Errors.Select(e => e.Description)
            });
        }

        return Ok(new { Message = "Usuario creado satisfactoriamente" });
    }

    // Actualizar información de usuario
    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser(UserPostPutDTO model)
    {
        try
        {
            var currentUserIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (currentUserIdClaim == null || !int.TryParse(currentUserIdClaim, out int currentUserId))
            {
                return Forbid();
            }

            var updatedUser = await _accountService.Update(currentUserId, model);

            if (updatedUser == null)
            {
                return NotFound(new { Message = "Usuario no encontrado" });
            }

            return Ok(new
            {
                Message = "Usuario actualizado satisfactoriamente",
                User = updatedUser
            });
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    // Login y generación de JWT
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDTO model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);

        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo
            });
        }

        return Unauthorized(new { message = "Invalid username or password." });
    }

    // Asignar un rol a un usuario
    [Authorize(Roles = "admin")]
    [HttpPost("asignar-rol")]
    public async Task<IActionResult> AsignarRol([FromBody] RoleAssignmentDTO model)
    {
        var user = await _userManager.FindByNameAsync(model.Username);
        if (user == null)
        {
            return NotFound(new { Message = "Usuario no encontrado" });
        }

        var roleExists = await _userManager.IsInRoleAsync(user, model.Role);
        if (roleExists)
        {
            return BadRequest(new { Message = "El usuario ya tiene este rol" });
        }

        var result = await _userManager.AddToRoleAsync(user, model.Role);
        if (result.Succeeded)
        {
            return Ok(new { Message = "Rol asignado correctamente" });
        }

        return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "Error al asignar el rol" });
    }

    // Obtener la lista de roles (solo administradores)
    [Authorize(Roles = "admin")]
    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        var roles = await _roleManager.Roles.ToListAsync();
        return Ok(roles);
    }

    // Obtener lista de usuarios (solo administradores)
    [Authorize(Roles = "admin")]
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            var users = await _accountService.GetAll();

            if (users == null || !users.Any())
            {
                return NotFound("No se encontraron usuarios en el sistema.");
            }

            return Ok(users);
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid("No tienes autorización para realizar esta operación.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocurrió un error interno en el servidor: " + ex.Message);
        }
    }

     // Obtener lista de usuarios (solo administradores)
    [Authorize]
    [HttpGet("getUserInfo")]
    public async Task<IActionResult> GetUser()
    {
        try
        {
            int userId = Convert.ToInt32(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _accountService.GetById(userId); // Usa await aquí
            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }
            return Ok(user); // Devuelve el usuario encontrado
        }
        catch (UnauthorizedAccessException)
        {
            return Forbid("No tienes autorización para realizar esta operación.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocurrió un error interno en el servidor: " + ex.Message);
        }
    }



    // Obtener roles específicos de un usuario
    [Authorize(Roles = "admin, User")]
    [HttpGet("/users/{id}/roles")]
    public async Task<IActionResult> GetRoles(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            IList<string> roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }
        return BadRequest();
    }

    // Crear un rol (solo administradores)
    [Authorize(Roles = "admin")]
    [HttpPost("role")]
    public async Task<IActionResult> CreateRole([FromBody] string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            return BadRequest("El nombre del rol no puede estar vacío.");
        }

        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (roleExists)
        {
            return Conflict($"El rol '{roleName}' ya existe.");
        }

        var result = await _roleManager.CreateAsync(new IdentityRole<int>(roleName));

        if (result.Succeeded)
        {
            return Ok($"El rol '{roleName}' ha sido creado exitosamente.");
        }

        var errorMessages = result.Errors.Select(e => e.Description).ToList();
        return BadRequest(new
        {
            Message = "Error al crear el rol.",
            Errors = errorMessages
        });
    }
}
