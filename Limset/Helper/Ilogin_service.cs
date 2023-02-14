using Limset.Models;

namespace Limset.Helper
{
    public interface Ilogin_service
    {        
        public bool is_username_ok(string username);
        public bool is_password_ok(string password);
        public void create_password_hash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public bool verify_password_hash(string password, byte[] passwordHash, byte[] passwordSalt);
        public Task<users> is_user_available(string username);
        public Task<bool> is_user_active(string username);
        public Task<string> user_role(string username);        
    }
}
