using Limset.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Limset.Helper
{
    public class admin_service : Iadmin_service
    {
        private readonly LimSet_DbContext _context;
        public admin_service(LimSet_DbContext context)
        {
            _context = context;
        }
        public bool are_all_details_available(string fname, string lname, string username, string password, string role)
        {
            if (string.IsNullOrEmpty(fname) || string.IsNullOrEmpty(lname) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
                return false;
            return true;
        }

        public bool credential_lengths(string username, string password)
        {
            if (username.Length < 5 || password.Length < 6)
                return false;
            return true;
        }        
        public bool is_user_available(string username)
        {
            var user = _context.users.FirstOrDefaultAsync(x => x.username == username);
            if(user == null)
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
        public async Task<users> add_user(string fname, string lname, string username, string password, string role)
        {
            create_password_hash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var new_user = new users
            {
                first_name = fname,
                last_name = lname,
                full_name = fname + " " + lname,
                username = username,
                password_hash = passwordHash,
                password_salt = passwordSalt,
                date_created = DateTime.Now,
                role = role,
                is_active = true
            };

            var user = _context.users.Add(new_user);
            await _context.SaveChangesAsync();
            return user.Entity;
        }

        public async Task<users> disable_user(string fname, string lname)
        {
            var user = await _context.users.Where(x => x.first_name == fname && x.last_name == lname).FirstOrDefaultAsync();
            if(user != null)
            {
                user.is_active = false;

                await _context.SaveChangesAsync();
                return user;
            }
            return null!;
        }
    }
}
