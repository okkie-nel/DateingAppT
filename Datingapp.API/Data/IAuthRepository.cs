using System.Threading.Tasks;
using Datingapp.API.Models;

namespace Datingapp.API.Data
{
    public interface IAuthRepository
    {
         Task<user> Register(user User, string password);
    
        Task<user> Login(string username, string password);
    
        Task<bool> UserExists(string username);
    
    }
}