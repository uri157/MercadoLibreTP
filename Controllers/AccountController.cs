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
{   private readonly IAccountService _accountService;
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AccountController(
        UserManager<User> userManager,
        IAccountService AccountService, 
        RoleManager<IdentityRole<int>> roleManager, 
        SignInManager<User> signInManager, 
        IConfiguration configuration)
    {
        _userManager = userManager;
        _accountService = AccountService;
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
                Errors = result.Errors.Select(e => e.Description) // Incluye los errores de validación
            });
        }

        return Ok(new { Message = "Usuario creado satisfactoriamente" });
    }

    // Solo el propietario del recurso puede modificar su información
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

            // Llama al servicio para actualizar el usuario, pasando el ID como entero
            var updatedUser = await _accountService.Update(currentUserId, model);

            if (updatedUser == null)
            {
                return NotFound(new { Message = "Usuario no encontrado" });
            }

            // Retorna el usuario actualizado en formato DTO
            return Ok(new
            {
                Message = "Usuario actualizado satisfactoriamente",
                User = updatedUser
            });
        }
        catch (InvalidOperationException ex)
        {
            // Error si no se encuentra o falla la actualización
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }


    // Login de usuarios y generación de JWT
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
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()), // ID del usuario
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Identificador único para el token
            };

            // Añadir los roles del usuario como claims
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

        // Manejo de error unificado y mensaje generalizado para mejorar la seguridad
        return Unauthorized(new { message = "Invalid username or password." });
    }

    
    // Asignar un rol a un usuario
    [Authorize(Roles = "admin")]
    //[AllowAnonymous]
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

    // Obtener la lista de roles, solo permitido para administradores
    [Authorize(Roles = "admin")]
    [HttpGet("roles")]
    public async Task<IActionResult> GetRoles()
    {
        // Cambia la lista a List<IdentityRole<int>>
        List<IdentityRole<int>> roles = await _roleManager.Roles.ToListAsync();
        return Ok(roles);
    }

    
    // Obtener la lista de usuarios, solo permitido para administradores
    [Authorize(Roles = "admin")]
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers()
    {
        try
        {
            // Verificar que el rol de administrador esté presente en los claims del usuario
            // if (!User.IsInRole("Admin"))
            // {
            //     return Forbid("Acceso denegado: solo los administradores pueden acceder a la lista de usuarios.");
            // }

            // Intentar obtener la lista de usuarios
            var users = await _accountService.GetAll();
            
            if (users == null || !users.Any())
            {
                return NotFound("No se encontraron usuarios en el sistema.");
            }

            return Ok(users);
        }
        catch (UnauthorizedAccessException)
        {
            // Manejar excepciones de acceso no autorizado
            return Forbid("No tienes autorización para realizar esta operación.");
        }
        catch (Exception ex)
        {
            // Manejar cualquier otro error no previsto
            return StatusCode(500, "Ocurrió un error interno en el servidor: " + ex.Message);
        }
    }

    // Obtener roles específicos de un usuario (podría limitarse al propio usuario o administradores)
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

    // Crear un rol, solo permitido para administradores
    [Authorize(Roles = "admin")]
    //[AllowAnonymous]
    [HttpPost("role")]
    public async Task<IActionResult> CreateRole([FromBody] string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            return BadRequest("El nombre del rol no puede estar vacío.");
        }

        // Verifica si el rol ya existe
        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (roleExists)
        {
            return Conflict($"El rol '{roleName}' ya existe.");
        }

        // Crea un nuevo rol
        var result = await _roleManager.CreateAsync(new IdentityRole<int>(roleName));

        if (result.Succeeded)
        {
            return Ok($"El rol '{roleName}' ha sido creado exitosamente.");
        }

        // Si algo falla, formatea y devuelve los errores
        var errorMessages = result.Errors.Select(e => e.Description).ToList();
        var errorResponse = new
        {
            Message = "Error al crear el rol.",
            Errors = errorMessages
        };

        return BadRequest(errorResponse);
    }

}