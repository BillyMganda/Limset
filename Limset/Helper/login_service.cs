using Limset.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;


namespace Limset.Helper
{
    internal class login_service : Ilogin_service
    {
        private readonly LimSet_DbContext _context;
        public login_service(LimSet_DbContext context)
        {
            _context = context;
        }
        public bool is_username_ok(string username)
        {
            if(username.Length < 6)
                return false;
            return true;
        }

        public bool is_password_ok(string password)
        {
            if (password.Length < 6)
                return false;
            return true;
        }

        public void create_password_hash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public bool verify_password_hash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computed_Hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computed_Hash.SequenceEqual(passwordHash);
            }
        }

        public async Task<users> is_user_available(string username)
        {
            var is_user_available = await _context.users.FirstOrDefaultAsync(x => x.username == username);
            if (is_user_available == null)
                return null!; ;
            return is_user_available;
        }
        public async Task<string> user_role(string username)
        {
            var user = await _context.users.FirstOrDefaultAsync(x => x.username == username);
            return user!.role;
        }
    }
}
