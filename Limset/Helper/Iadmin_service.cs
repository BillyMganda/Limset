using Limset.Models;

namespace Limset.Helper
{
    public interface Iadmin_service
    {
        public bool are_all_details_available(string fname, string lname, string username, string password, string role);
        public bool credential_lengths(string username, string password);
        public bool is_user_available(string username);
        public void create_password_hash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        public Task<users> add_user(string fname, string lname, string username, string password, string role);  
        public Task<users> disable_user(string fname, string lname);

        //upload data from excel to datagridview
        //POST data in datagridview to online
    }
}
