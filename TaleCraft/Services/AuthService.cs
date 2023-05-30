using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaleCraft.Helpers;
using TaleCraft.Interfaces;
using TaleCraft.Models;

namespace TaleCraft.Services;

public class AuthService : IAuthService
{
    private readonly DataContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(DataContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<User> Register(User user, string password)
    {
        if (await _context.Users.AnyAsync(x => x.Email == user.Email))
        {
            throw new Exception("Email already exists");
        }
        
        if (await _context.Users.AnyAsync(x => x.Username == user.Username))
        {
            throw new Exception("Username already exists");
        }
        
        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    
        return user;
    }

    public async Task<User> Login(string username, string password)
    {
        // Check if user exists
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        if (user == null)
        {
            // User not found
            return null;
        }
        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

        // Check if password is correct
        if (!VerifyPasswordHash(password, user.PasswordHash))
        {
            // Invalid password
            return null;
        }

        // Authentication successful
        return user;
    }

// Method to verify password
    private bool VerifyPasswordHash(string password, string storedHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, storedHash);
    }

    public string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Token").Value);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.Now.AddDays(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public string GenerateRefreshToken(int size = 32)
    {
        var randomNumber = new byte[size];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    
    public async Task<User> RefreshToken(string refreshToken)
    {
        var user = await _context.Users.SingleOrDefaultAsync(u => u.RefreshToken == refreshToken);

        if (user == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
        {
            return null;
        }

        // Generate new token and save to database
        user.RefreshToken = GenerateRefreshToken();
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
    
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return user;
    }
    
}