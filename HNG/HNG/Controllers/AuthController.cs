using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HNG.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public AuthController(IAuthService authService)
        {
            
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(Login loginDTO)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ApiResponse<string>.Failed("Invalid model state.", 400, ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList()));
            }
            return Ok(await _authService.LoginAsync(loginDTO));
        }
    }
}
