using TaleCraft.Models;
using System.Threading.Tasks;

public interface IAuthService
{
    Task<User> Register(User user, string password);
    Task<User> Login(string username, string password);
    string GenerateToken(User user);
    string GenerateRefreshToken(int size = 32);
    Task<User> RefreshToken(string refreshToken);
}