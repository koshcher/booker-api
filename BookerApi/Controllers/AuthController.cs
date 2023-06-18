using BookerApi.Config;
using BookerApi.Lib;

using BookerApi.Lib;

using Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookerApi.Controllers
{
    /// <summary> Controller for authenitication users </summary>
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ResultController
    {
        private readonly UserManager<User> userManager;

        public AuthController(UserManager<User> userManager)
        {
            this.userManager = userManager;
        }

        public record class LoginBody(string Email, string Password);

        [HttpPost("login")]
        public async Task<IActionResult> PasswordLogin([FromBody] LoginBody body)
        {
            var user = await userManager.FindByEmailAsync(body.Email);
            if (user == null) return Error("An account with such email is not found.", 404);

            var signed = await userManager.CheckPasswordAsync(user, body.Password);
            if (!signed) return Error("Password is incorrect.", 400);

            var token = TokenService.GenerateAccessToken(user.Id);
            return Data(token);
        }

        public record RegisterBody(string Email, string Password, string ConfirmPassword);

        [HttpPost("register")]
        public async Task<IActionResult> PasswordRegister([FromBody] RegisterBody body)
        {
            if (body.Password != body.ConfirmPassword) return Error("Confirm password doesn't match password.", 400);

            var userExist = await userManager.FindByEmailAsync(body.Email);
            if (userExist != null) return Error("An account with such email already exists.", 409);

            User user = new()
            {
                Email = body.Email,
                UserName = body.Email,
            };

            var creationResult = await userManager.CreateAsync(user, body.Password);
            if (!creationResult.Succeeded) return Error("Can't create user with such email and password.", 500);

            var token = TokenService.GenerateAccessToken(user.Id);
            return Data(token);
        }
    }
}