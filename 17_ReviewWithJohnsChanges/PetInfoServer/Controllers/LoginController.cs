using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetInfoServer.DAL;
using PetInfoServer.Models;
using PetInfoServer.Security;

namespace PetInfoServer.Controllers
{
    [Route("/")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IPasswordHasher passwordHasher;
        private readonly IUserDAO userDAO;


        public LoginController(ITokenGenerator _tokenGenerator, IPasswordHasher _passwordHasher,
            IUserDAO userDAO)
        {
            tokenGenerator = _tokenGenerator;
            passwordHasher = _passwordHasher;
            this.userDAO = userDAO;
        }

        [HttpGet]
        public ActionResult Ready()
        {
            return Ok("Server ready.");
        }

        [HttpGet("whoami")]
        public ActionResult WhoAmI()
        {
            string result = User.Identity.Name; 

            if (!string.IsNullOrEmpty(result))
            {
                return Ok(result);
            }
            else
            {
                return NoContent();
            }
          
        }

        [HttpPost("register")]
        public IActionResult Register(LoginUser userParam)
        {
            IActionResult result;
            User existingUser;

            try
            {
                existingUser = userDAO.GetUser(userParam.Username);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error in GetUser - " + ex.Message });
            }

            if (existingUser != null)
            {
                return Conflict(new { message = "Username already taken. Please choose a different username." });
            }

            User user;
            try
            {
                user = userDAO.AddUser(userParam.Username, userParam.Password);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Server error in AddUser - " + ex.Message });
            }

            if (user != null)
            {
                result = Created(user.Username, null); //values aren't read on client
            }
            else
            {
                result = BadRequest(new { message = "An error occurred and user was not created." });
            }

            return result;
        }

        [HttpPost("login")]
        public IActionResult Authenticate(LoginUser userParam)
        {
            // Default to bad username/password message
            IActionResult result = BadRequest(new { message = "Username or password is incorrect" });

            // Get the user by username
            User user = userDAO.GetUser(userParam.Username);

            // If we found a user and the password hash matches
            if (user != null && passwordHasher.VerifyHashMatch(user.PasswordHash, userParam.Password, user.Salt))
            {
                // Create an authentication token
                user.Role = user.Role ?? "";
                string token = tokenGenerator.GenerateToken(user.Id, user.Username, user.Role);

                // Create a ReturnUser object to return to the client
                ReturnUser retUser = new ReturnUser() { Id = user.Id, Username = user.Username, Role = user.Role, Token = token };

                // Switch to 200 OK
                result = Ok(retUser);
            }

            return result;
        }
    }
}