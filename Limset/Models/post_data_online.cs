using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Limset.Models
{
    public class post_data_online
    {
        public int reference_range_id { get; set; }
        public int test_format_id { get; set; }
        public string code { get; set; } = string.Empty;
        public int clinical_field_id { get; set; }
        public string clinical_condition { get; set; } = string.Empty;
        public string sex { get; set; } = string.Empty;
        public string age { get; set; } = string.Empty;
        public string text_description { get; set; } = string.Empty;
        public string phone_lo { get; set; } = string.Empty;
        public string phone_hi { get; set; } = string.Empty;
        public string flag_lo { get; set; } = string.Empty;
        public string flag_hi { get; set; } = string.Empty;
        public string alert_lo { get; set; } = string.Empty;
        public string alert_hi { get; set; } = string.Empty;
        public string auth_lo { get; set; } = string.Empty;
        public string auth_hi { get; set; } = string.Empty;
        public DateTime range_date { get; set; }
        public int line_position { get; set; }
        public bool is_child { get; set; }
    }
}
