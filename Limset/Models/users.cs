using System.ComponentModel.DataAnnotations;

namespace Limset.Models
{
    public class users
    {
        [Key]
        public int id { get; set; }
        public string first_name { get; set; } = string.Empty;
        public string last_name { get; set; } = string.Empty;
        public string full_name { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public byte[] password_hash { get; set; } = new byte[32];
        public byte[] password_salt { get; set; } = new byte[32];
        public DateTime date_created { get; set; } = DateTime.Now;
        public string role { get; set; } = string.Empty;
        public bool is_active { get; set; } = true;
    }
}
