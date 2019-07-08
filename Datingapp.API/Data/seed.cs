using System.Collections.Generic;
using System.Linq;
using Datingapp.API.Models;
using Newtonsoft.Json;

namespace Datingapp.API.Data
{
    public class seed
    {
        private readonly DataContext _context;
        public seed(DataContext context)
        {
            _context = context;

        }
        private void CreatePasswordHash(string password, out byte[] passwordHash,out byte[] passwordSalt)
        {
           using (var hmac = new System.Security.Cryptography.HMACSHA512()){
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
           }
        }

        public void SeedUsers(){
            if (!_context.Users.Any()){
 var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<user>>(userData);
            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;

                CreatePasswordHash("password", out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.Username = user.Username.ToLower();

                _context.Users.Add(user);

            }

            _context.SaveChanges();
            }
           
        }
    }
}