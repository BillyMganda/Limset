using Limset.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace Limset.Helper
{
    public class admin_service : Iadmin_service
    {        
        public admin_service()
        {
            
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
            using(var context = new LimSet_DbContext())
            {
                var user = context.users.FirstOrDefault(x => x.username == username);
                if (user == null)
                    return false;
                return true;
            }            
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

            using(var context = new LimSet_DbContext())
            {
                var user = context.users.Add(new_user);
                await context.SaveChangesAsync();
                return user.Entity;
            }            
        }

        public async Task<users> disable_user(string fname, string lname)
        {
            using (var context = new LimSet_DbContext())
            {
                var user = await context.users.Where(x => x.first_name == fname && x.last_name == lname).FirstOrDefaultAsync();
                if (user != null)
                {
                    user.is_active = false;

                    await context.SaveChangesAsync();
                    return user;
                }
                return null!;
            }                
        }
               
    }
}
