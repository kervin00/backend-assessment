using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Microsoft.EntityFrameworkCore;
using System.Text;

using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using Assessment.Models;



[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration; // Add this field

    public AuthController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration; // Initialize _configuration
    }

    //Register

    [HttpPost("register")]
    public async Task<IActionResult> Register(User model)
    {
        try
        {
            // Validate the input data
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest(new { Message = "Name, email and password are required." });
            }

        
            if (_context.Users is not null)
            {
                // Check if the email is already taken
                if (await _context.Users.AnyAsync(u => u.Email == model.Email))
                {
                    return BadRequest(new { Message = "Email is already taken." });
                }
            }
            else
            {
                // Handle the case where _context.Users is null 
            }

            // Hash the password
            string hashedPassword = HashPassword(model.Password);

            // Save the user to the database
            User newUser = new User
            {
                Name = model.Name,
                Email = model.Email,
                Password = hashedPassword
            };

            _context.Users?.Add(newUser); 
            await _context.SaveChangesAsync();

            return Ok(new { Message = "User registered successfully" });
        }
        catch
        {
            // Handle exceptions
            return StatusCode(500, new { Message = "Internal server error" });
        }
    }

    // HashPassword using bcrypt
    private string HashPassword(string password)
    {
      
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
 

  // Login
    [HttpPost("login")]
    public async Task<IActionResult> Login(User model)
    {
        try
        {
            
                // Find the user by email
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                // Check if the user exists
                if (user == null)
                {
                    return BadRequest(new { Message = "Invalid email or password" });
                }

                // Verify the password using BCrypt
                if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    return BadRequest(new { Message = "Invalid email or password" });
                }

                // Simulate an asynchronous operation (replace with actual async logic)
                await Task.Run(() =>
                {
                    // Simulate a CPU-bound operation or other asynchronous logic
                });

                // Generate a JWT token
                var token = GenerateJwtToken(user);

                // Return the token or any other authentication response
                return Ok(new { Token = token });
           
        }
        catch
        {
            // Handle exceptions
            return StatusCode(500, new { Message = "Internal server error" });
        }
    }
    // Generate JWT token
    private string GenerateJwtToken(User user)
    {
        try
        {
            // Claims represent the user identity
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                // You can add more claims here if needed
            };

            // Get the secret key from configuration
            var secretKey = _configuration.GetSection("JWT:SecretKey").Value; // Corrected key name

            // Create the security key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // Create signing credentials using the key and algorithm
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the JWT token
            var token = new JwtSecurityToken(
                issuer: _configuration.GetSection("JWT:Issuer").Value,
                audience: _configuration.GetSection("JWT:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddHours(Convert.ToDouble(_configuration.GetSection("JWT:ExpirationHours").Value)),
                signingCredentials: credentials
            );

            // Write the token as a string
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
        catch 
        {
        return null;
        }
    }


    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout()
    {
        try
        {
            // Get the user's id from the claims
            string userId = HttpContext.User.FindFirstValue(ClaimTypes.Name);

            // Optional: Check if the token is already blacklisted
            var authorizationHeader = HttpContext.Request.Headers["Authorization"];
            var token = authorizationHeader.ToString().Replace("Bearer ", "");
            if (TokenBlacklistService.IsTokenBlacklisted(token))
            {
                return Unauthorized(new { Message = "Token is already blacklisted" });
            }

            // Add the user's token to the blacklist
            TokenBlacklistService.AddToBlacklist(token);

            return Ok(new { Message = "User logged out successfully" });
        }
        catch
        {
            // Log or handle exceptions if needed
            return StatusCode(500, new { Message = "Internal server error" });
        }
    }





}