// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Mvc;


// [ApiController]
// [Route("api/user")]
// public class UserController : ControllerBase
// {
//     private readonly IUserService _userService;

//     public UserController(IUserService userService)
//     {
//         _userService = userService;
//     }

//     [HttpGet]
//     public IActionResult GetAllUsers() => Ok(_userService.GetAll());

//     [HttpGet("{id}")]
//     public IActionResult GetUserById(int id)
//     {
//         var user = _userService.GetById(id);
//         return user != null ? Ok(user) : NotFound();
//     }

//     [HttpPut("{id}")]
//     public IActionResult UpdateUser(int id, [FromBody] UserDTO user)
//     {
//         var updatedUser = _userService.Update(id, user);
//         return updatedUser != null ? Ok(updatedUser) : NotFound();
//     }

//     [HttpDelete("{id}")]
//     public IActionResult DeleteUser(int id)
//     {
//         _userService.Delete(id);
//         return NoContent();
//     }

//     [HttpGet("{id}/profile-picture")]
//     public IActionResult GetProfilePicture(int id)
//     {
//         var photo = _userService.GetProfilePicture(id);
//         return photo != null ? Ok(photo) : NotFound();
//     }
// }
