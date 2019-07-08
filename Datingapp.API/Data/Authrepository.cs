using System;
using System.Threading.Tasks;
using Datingapp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Datingapp.API.Data
{
    public class Authrepository : IAuthRepository
    {
        private readonly DataContext _context;
        public Authrepository(DataContext context)
        {
            _context = context;
        }
        public async Task<user> Login(string username, string password)
        {
            var user = await _context.Users.Include(p => p.Photos).FirstOrDefaultAsync(x => x.Username == username);

            if(user == null)
            return null;

            if(!VerifyPassword(password, user.PasswordHash, user.PasswordSalt))
            return null;

            return user;
        }

        private bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
             using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        public async Task<user> Register(user aUser, string password)
        {
            byte[] passwordHash, passwordSalt;
            
        CreatePasswordHash(password,out passwordHash,out passwordSalt);
        aUser.PasswordHash = passwordHash;
        aUser.PasswordSalt = passwordSalt;
        await _context.Users.AddAsync(aUser);
        await _context.SaveChangesAsync();

        return aUser;

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash,out byte[] passwordSalt)
        {
           using (var hmac = new System.Security.Cryptography.HMACSHA512()){
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           }
        }

        public async Task<bool> UserExists(string username)
        {
            
            if (await _context.Users.AnyAsync(x=> x.Username == username))
            return true;

            return false;
        }
    }
}